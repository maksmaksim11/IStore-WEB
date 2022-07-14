using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IStore_WEB_.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string PreviewImage { get; set; }
        public double RetailPrice { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
    }
}
