using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Models
{
    public class IncomeProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Count { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
