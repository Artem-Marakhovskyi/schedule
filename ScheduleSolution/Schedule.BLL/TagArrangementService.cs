using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.Data;
using Schedule.Data.Context;

namespace Schedule.BLL
{
    public class TagArrangementService
    {
        private readonly ScheduleContext _context;

        public TagArrangementService(
            ScheduleContext context)
        {
            _context = context;
        }

        public Task AddRangeAsync(IEnumerable<TagArrangement> tagArrangements)
        {
            _context.TagArrangement.AddRange(tagArrangements);
            return _context.SaveChangesAsync();
        }

        public Task RemoveAsync(Arrangement arrangement)
        {
            var intendedToDelete = _context.TagArrangement.Where(e => e.ArrangementId == arrangement.Id).ToList();
            _context.TagArrangement.RemoveRange(intendedToDelete);

            return _context.SaveChangesAsync();
        }
    }
}
