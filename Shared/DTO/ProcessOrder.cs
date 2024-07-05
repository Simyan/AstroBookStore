using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class Purchase        
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchasedOn { get; set; }
    }


    public class Restock
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public int Quantity { get; set; }
        public string Remarks { get; set; }

    }


    public class ProcessOrder
    {
        public long Id { get; set; }
        
        public IEnumerable<Purchase>? Purchases { get; set; }
        public IEnumerable<Restock>? Restocks { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ProcessedOn { get; set; }

    }
}
