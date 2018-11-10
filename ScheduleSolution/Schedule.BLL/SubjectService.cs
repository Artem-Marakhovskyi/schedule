using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.Data;
using Schedule.Data.Context;

namespace Schedule.BLL
{
    public class SubjectService
    {
        private readonly ScheduleContext _context;

        public SubjectService(
            ScheduleContext context)
        {
            _context = context;
        }

        public Task<List<Subject>> GetAsync()
        {
            return _context.Subjects.OrderBy(e => e.Name).ToListAsync();
        }
        
        public Task CreateAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            return _context.SaveChangesAsync();
        }

        public Task<List<Subject>> GetAsync(Expression<Func<Subject, bool>> searchCriterion)
        {
            return _context.Subjects.Where(searchCriterion).ToListAsync();
        }
    }
}
