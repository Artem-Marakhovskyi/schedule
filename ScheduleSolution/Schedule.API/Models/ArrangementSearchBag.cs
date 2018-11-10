using Schedule.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Schedule.API.Models
{
    public class ArrangementSearchBag
    {
        private IDictionary<string, Expression<Func<ViewArrangementDto, object>>> _orderCriterion =
            new Dictionary<string, Expression<Func<ViewArrangementDto, object>>>()
            {
                {nameof(Complexity), e => e.Complexity},
                {nameof(DateTime), e => e.DateTime}
            };

        public IEnumerable<string> Tags { get; set; }

        public int? PersonId { get; set; }

        public IEnumerable<PersonDto> People { get; set; }

        public int? SubjectId { get; set; }

        public IEnumerable<SubjectDto> Subjects { get; set; }

        public DateTimeOffset? DateTime { get; set; }

        public int? Complexity { get; set; }

        public bool IsHandled { get; set; }

        public string OrderProperty { get; set; } = nameof(DateTime);

        public IEnumerable<string> OrderProps => _orderCriterion.Keys.ToList();

        public Expression<Func<ViewArrangementDto, object>> OrderExpression => _orderCriterion[OrderProperty];
    }
}
