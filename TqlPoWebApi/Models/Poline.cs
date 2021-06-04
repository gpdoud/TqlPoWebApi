using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TqlPoWebApi.Models {
    public class Poline {

        public int Id { get; set; }
        public int PoId { get; set; }
        public virtual Po Po { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item {get; set; }
        public int Quantity { get; set; } = 1;

        public Poline() { }
    }
}
