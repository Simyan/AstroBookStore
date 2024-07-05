using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BookInventory
{
    public class Author : IEntity
    {
        public Guid Id { get; private set; } 
        public string Name { get; private set; }

        private Author() { }

        public Author(string name, Guid guid) 
        { 
            this.Id = guid; this.Name = name;  
        }
    }
}
