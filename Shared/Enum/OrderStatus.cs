using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Order
{
    public enum OrderStatus
    {
        Requested = 1,
        Completed = 2,
        Cancelled = 3,
        Returned = 4,
    }
}
