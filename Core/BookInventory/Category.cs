        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BookInventory
{
    public class Category : IValueObject
    {  
        public string Name { get; private set; }

        private Category() { }

        public Category(string name)
        {
            Name = name;
        }
    }
}
