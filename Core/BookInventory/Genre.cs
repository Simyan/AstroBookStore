        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BookInventory
{
    public class Genre : IValueObject
    {  
        public string Name { get; private set; }

        private Genre() { }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
