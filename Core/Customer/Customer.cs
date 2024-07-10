using Core.BookInventory;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Customer
{
    public class Customer : IEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public string MobileNumber { get; private set; }

        private Customer() { }  

        public Customer(string name, string email, AddressDto address, string mobileNumber)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(name);
            ArgumentNullException.ThrowIfNullOrEmpty(email);
            ArgumentNullException.ThrowIfNullOrEmpty(mobileNumber);

            Id = new Guid();
            Name = name;
            Email = new Email(email);
            Address = new Address(address);
            MobileNumber = mobileNumber;
        }

    }
}
