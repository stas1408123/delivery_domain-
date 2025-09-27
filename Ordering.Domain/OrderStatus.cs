using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain
{
    public enum OrderStatus
    {
        New = 0,
        InProgress = 1,
        Delivering = 2,
        Compled = 3,
    }
}
