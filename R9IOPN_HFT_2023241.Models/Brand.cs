using System;
using System.Collections.Generic;

namespace R9IOPN_HFT_2023241.Models
{
    public class Brand
    {
        public int BrandId{ get; set; }
        public string Name { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
