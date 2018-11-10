using System;
using System.Threading.Tasks;
using Schedule.Data;
using Schedule.Data.Context;

namespace Schedule.BLL
{
    public class PersonService
    {
        private ScheduleContext _context;

        public Task Save(Person person)
        {
            using (_context = new ScheduleContext())
            {
                _context.People.Add(person);
                
                return _context.SaveChangesAsync();
            }
        }
    }
}
