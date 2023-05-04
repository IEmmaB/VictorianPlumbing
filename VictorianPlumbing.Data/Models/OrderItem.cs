using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictorianPlumbing.Data.Models
{
    public class OrderItem
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
    }
}
