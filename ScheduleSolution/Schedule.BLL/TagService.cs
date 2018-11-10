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
    public class TagService
    {
        private readonly ScheduleContext _context;

        public TagService(ScheduleContext context)
        {
            _context = context;
        }

        public Task AddRangeAsync(IEnumerable<Tag> tag)
        {
            _context.Tags.AddRange(tag);
            return _context.SaveChangesAsync();
        }

        public Task<List<TagDto>> GetAsync()
        {
            return _context.Tags.OrderBy(e => e.Content).Select(e => new TagDto() {Content = e.Content}).ToListAsync();
        }

        public Task CreateAsync(TagDto tagDto)
        {
            _context.Tags.Add(new Tag() {Content = tagDto.Content});
            return _context.SaveChangesAsync();
        }

        public Task<List<Tag>> GetAsync(Expression<Func<Tag, bool>> predicate)
        {
            return _context.Tags.Where(predicate).ToListAsync();
        }
    }
}
