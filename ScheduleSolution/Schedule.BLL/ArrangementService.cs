using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.BLL.DTO;
using Schedule.Data;
using Schedule.Data.Context;

namespace Schedule.BLL
{
    public class ArrangementService
    {
        private readonly TagArrangementService _tagArrangementService;
        private readonly PersonService _personService;
        private readonly SubjectService _subjectService;
        private readonly ScheduleContext _context;
        private readonly TagService _tagService;

        public ArrangementService(
            ScheduleContext context,
            TagService tagService,
            TagArrangementService tagArrangementService,
            PersonService personService,
            SubjectService subjectService)
        {
            _personService = personService;
            _subjectService = subjectService;
            _context = context;
            _tagService = tagService;
            _tagArrangementService = tagArrangementService;
        }

        public ArrangementDto Empty()
        {
            return new ArrangementDto()
            {
                AvailablePeople = _context.People.Select(e => new PersonDto()
                    {Id = e.Id, ShortDescription = $"{e.Name} {e.Surname}, {e.AlternativeEgo}"}).ToList(),
                AvailableSubjects = _context.Subjects.Select(e => new SubjectDto()
                    {Id = e.Id, Label = $"{e.Name} {e.Description}"}),
                AvailableTags = _context.Tags.Select(e => new TagDto() {Content = e.Content, TagId = e.Id})
            };
        }

        public async Task<List<ViewArrangementDto>> GetAsync(ArrangementSearchContext searchContext)
        {
            IEnumerable<Tag> filtrationTags;
            searchContext = searchContext ?? ArrangementSearchContext.Empty();
            if (searchContext.Tags != null)
            {
                filtrationTags = _context.Tags.Where(e => searchContext.Tags.Contains(e.Content)).Include(e => e.TagArrangements).ToList();
            }
            else
            {
                filtrationTags = _context.Tags.Include(e => e.TagArrangements).ToList();
            }
            
            var arrangements = await ApplyFilter(_context.Arrangements.AsQueryable(), searchContext).ToListAsync();

            var peopleIdDistinct = arrangements.Select(e => e.PersonId).Distinct();
            var subjectsIdDistinct = arrangements.Select(e => e.SubjectId).Distinct();
            var tagIds = arrangements.SelectMany(e => e.TagArrangements.Select(q => q.TagId)).Distinct();
            var people = await _personService.GetAsync(e => peopleIdDistinct.Contains(e.Id));
            var subjects = await _subjectService.GetAsync(e => subjectsIdDistinct.Contains(e.Id));
            var tags = await _tagService.GetAsync(e => tagIds.Contains(e.Id));

            return arrangements.Select(arrangement => new ViewArrangementDto()
                {
                    DateTime = arrangement.DateTime,
                    Complexity = arrangement.Complexity,
                    Id = arrangement.Id,
                    IsHandled = arrangement.IsHandled,
                    Subject = subjects.First(e => e.Id == arrangement.SubjectId).Name,
                    SelectedPerson = people.First(e => e.Id == arrangement.PersonId).AlternativeEgo,
                    Tags = tags.Where(t => tags.Select(e => e.Id).Intersect(arrangement.TagArrangements.Select(q => q.TagId)).Contains(t.Id)).Select(e => e.Content)
                }).AsQueryable().OrderBy(searchContext.OrderByCriterion).ToList();
        }

        public async Task<ArrangementDto> GetAsync(int id)
        {
            var arrangement = _context.Arrangements.Include(e => e.TagArrangements).First(e => id == e.Id);
            var tagIds = arrangement.TagArrangements.Select(e => e.TagId);
            var person = (await _personService.GetAsync(e => e.Id == arrangement.PersonId)).First();
            var subject = (await _subjectService.GetAsync(e => e.Id == arrangement.SubjectId)).First();
            var tags = (await _tagService.GetAsync(e => tagIds.Contains(e.Id))).Select(e => e.Id);

            var current = Empty();
            current.Id = arrangement.Id;
            current.Complexity = arrangement.Complexity;
            current.DateTime = arrangement.DateTime;
            current.IsHandled = arrangement.IsHandled;
            current.SelectedPersonId = person.Id;
            current.SelectedSubjectId = subject.Id;
            current.AvailableTags = current.AvailableTags.ToList();
            foreach (var t in current.AvailableTags)
            {
                t.Selected = tags.Contains(t.TagId);
            }

            return current;
        }

        public void Handle(int id)
        {
            _context.Arrangements.First(e => e.Id == id).IsHandled = true;
            _context.SaveChanges();
        }

        public async Task CreateOrUpdateAsync(ArrangementDto arrangementDto)
        {
            var arrangement = MapToArrangement(arrangementDto);

            if (arrangement.Id == 0)
            {
                _context.Arrangements.Add(arrangement);
            }
            else
            {
                _context.Arrangements.Update(arrangement);
            }    
            
            await _context.SaveChangesAsync();
           
            //var nonExistingTags 
            //    = arrangementDto
            //        .SelectedTags
            //        .Where(e => e.TagId == 0)
            //        .Select(e => new Tag() { Content = e.Content });
            //await _tagService.AddRangeAsync(nonExistingTags);
            await _tagArrangementService.RemoveAsync(arrangement);
            
            var selectedTagsContent = arrangementDto.AvailableTags.Where(e => e.Selected).Select(e => e.Content).ToArray();
            var dbTags = _context.Tags.Where(e => selectedTagsContent.Contains(e.Content)).ToList();
            var tagArrangements = dbTags.Select(e => new TagArrangement() { ArrangementId = arrangement.Id, TagId = e.Id });
            await _tagArrangementService.AddRangeAsync(tagArrangements);

            await _context.SaveChangesAsync();
        }

        private IQueryable<Arrangement> ApplyFilter(
            IQueryable<Arrangement> arrangements,
            ArrangementSearchContext searchContext)
        {
            if (searchContext.Complexity.HasValue)
            {
                arrangements = arrangements.Where(e => e.Complexity >= searchContext.Complexity.Value);
            }

            if (searchContext.DateTime.HasValue)
            {
                arrangements = arrangements.Where(e => e.DateTime >= searchContext.DateTime.Value);
            }

            if (searchContext.IsHandled.HasValue)
            {
                arrangements = arrangements.Where(e => e.IsHandled == searchContext.IsHandled.Value);
            }

            if (searchContext.PersonId.HasValue)
            {
                arrangements = arrangements.Where(e => e.PersonId == searchContext.PersonId.Value);
            }

            if (searchContext.SubjectId.HasValue)
            {
                arrangements = arrangements.Where(e => e.SubjectId == searchContext.SubjectId.Value);
            }

            return arrangements.Include(e => e.TagArrangements);
        }

        private Arrangement MapToArrangement(ArrangementDto arrangementDto)
        {
            return new Arrangement()
            {
                Complexity = arrangementDto.Complexity,
                DateTime = arrangementDto.DateTime,
                Id = arrangementDto.Id,
                IsHandled = arrangementDto.IsHandled,
                PersonId = arrangementDto.SelectedPersonId,
                SubjectId = arrangementDto.SelectedSubjectId
            };
        }
    }
}
