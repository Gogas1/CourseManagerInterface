using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Models
{
    public class Income
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Supplier { get; set; }
        public decimal Summ { get; set; }
    }
}
