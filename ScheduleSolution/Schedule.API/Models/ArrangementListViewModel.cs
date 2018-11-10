using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.API.Models
{
    public class ArrangementListViewModel
    {
        public ArrangementSearchBag SearchBag { get; set; }

        public IEnumerable<BLL.DTO.ViewArrangementDto> ViewArrangementDtos { get; set; }

    }
}
