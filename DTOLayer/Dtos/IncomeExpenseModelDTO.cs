using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.Dtos
{
    public class IncomeExpenseModelDTO
    {
        public string IncomeExpense { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public DateTime Date { get; set; }
    }
}
