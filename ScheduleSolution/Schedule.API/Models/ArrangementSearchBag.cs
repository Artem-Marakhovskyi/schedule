using Schedule.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Schedule.BLL;

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

        [DisplayName("Tags")]
        public IEnumerable<string> Tags { get; set; }

        [DisplayName("Person")]        
        public int? PersonId { get; set; }

        [DisplayName("People")]
        public IEnumerable<PersonDto> People { get; set; }

        [DisplayName("Subject")]
        public int? SubjectId { get; set; }

        [DisplayName("Subject")]
        public IEnumerable<SubjectDto> Subjects { get; set; }

        [DisplayName("Start date")]
        public DateTimeOffset? DateTime { get; set; }

        [DisplayName("Complexity")]
        [Range(0, 10)]
        public int? Complexity { get; set; }

        [DisplayName("Is handled?")]
        public bool IsHandled { get; set; }

        [DisplayName("Sort by")]
        public string OrderProperty { get; set; } = nameof(DateTime);

        public IEnumerable<string> OrderProps => _orderCriterion.Keys.ToList();

        public Expression<Func<ViewArrangementDto, object>> OrderExpression => _orderCriterion[OrderProperty];
    }
}
