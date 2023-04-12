using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyStore.BLL
{
    class roomPaymentBLL
    {
        public int room_no { get; set; }
        public string item { get; set; }
        public decimal rate { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public DateTime date { get; set; }
    }
}
