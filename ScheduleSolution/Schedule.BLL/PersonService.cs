using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schedule.Data;
using Schedule.Data.Context;

namespace Schedule.BLL
{
    public class PersonService
    {
        private ScheduleContext _context;

        public PersonService(ScheduleContext context)
        {
            _context = context;
        }

        public Task SaveAsync(Person person)
        {
            _context.People.Add(person);

            return _context.SaveChangesAsync();
        }

        public Task<List<Person>> GetAsync()
        {
            return _context.People.OrderBy(e => e.AlternativeEgo).ToListAsync();
        }

        public Task<List<Person>> GetAsync(Expression<Func<Person, bool>> searchCriterion)
        {
            return _context.People.Where(searchCriterion).ToListAsync();
        }
    }
}
