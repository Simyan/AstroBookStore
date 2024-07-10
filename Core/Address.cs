using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Customer
{
    public class Address : IValueObject
    {
        public string Street { get; private set; }
        public string BuildingName { get; private set; }

        public string Country { get; private set; }
        public string State { get; private set; }

        public string City { get; private set; }
        public string ZipCode { get; private set; }

        private Address() { }

        public Address(AddressDto dto)
        {
            

            Street = dto.Street;
            BuildingName = dto.BuildingName;
            Country = dto.Country;
            State = dto.State;
            City = dto.City;
            ZipCode = dto.ZipCode;
        }
    }
}
