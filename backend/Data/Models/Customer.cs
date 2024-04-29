using System;
using Data.Dtos;

namespace Data.Models
    // clase para definir la entidad customer.
{

    public class CustomerEntity
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public CustomerDto ToDto()
        {
            return new CustomerDto()
            {
                Address = Address,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                Id = Id ?? throw new Exception("el id no puede ser null")
            };
        }
    }
}

