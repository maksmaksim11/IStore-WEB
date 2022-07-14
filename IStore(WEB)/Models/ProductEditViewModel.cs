using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IStore_WEB_.Models
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string VendorCode { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public double? RetailPrice { get; set; }
        public int CategoryId { get; set; }
        public int? PackageId { get; set; }
        public int? CountInStorage { get; set; }
        public int? WarrantyMonth { get; set; }
        public string Series { get; set; }
        public string Model { get; set; }
    }
}
