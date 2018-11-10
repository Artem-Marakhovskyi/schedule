using Schedule.BLL.DTO;
using Schedule.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Schedule.BLL
{
    public class ArrangementSearchContext
    {
        private Expression<Func<ViewArrangementDto, object>> _orderByCriterion =  dto => dto.DateTime;

        public IEnumerable<string> Tags { get; set; }

        public int? PersonId { get; set; }

        public int? SubjectId { get; set; }

        public DateTimeOffset? DateTime { get; set; }

        public int? Complexity { get; set; }

        public bool? IsHandled { get; set; }

        public Expression<Func<ViewArrangementDto, object>> OrderByCriterion
        {
            get => _orderByCriterion;
            set
            {
                if (value != null)
                {
                    _orderByCriterion = value;
                }
            }
        }

        public static ArrangementSearchContext Empty() => new ArrangementSearchContext();
    }
}
