using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.Dtos
{
    public class TaskModelDTO
    {
        public string Id { get; set; }
        public string TaskText { get; set; }
        public bool Status { get; set; }
    }
}
