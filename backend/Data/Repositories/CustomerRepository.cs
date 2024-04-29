using Data.Dtos;	
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private CustomerDbContext context;
		public CustomerRepository(CustomerDbContext dbcontext)
		{
			this.context = dbcontext;
		}

        

        public async Task<List<CustomerEntity>> GetAll()
        {
            return await context.Customers.ToListAsync();
        }

        // metodo para obtener un customer por Id
        public async Task<CustomerEntity> Get(long id )
		{
			return await context.Customers.FirstAsync(x => x.Id == id);
		}


		//metodo para eliminar un customer.
        public async Task<bool> Delete(long id)
        {
            CustomerEntity entity = await Get(id);
            context.Customers.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> Update(CustomerEntity customerEntity)
        {
            context.Customers.Update(customerEntity);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
        {
			CustomerEntity entity = new CustomerEntity()
			{
				Id = null,
				Address = customerDto.Address,
				Email = customerDto.Email,
				Phone = customerDto.Phone,
				FirstName = customerDto.FirstName,
				LastName = customerDto.LastName
			};

			// metodo para agregar un customer
			EntityEntry<CustomerEntity> response = await context.Customers.AddAsync(entity);
			await context.SaveChangesAsync();
			return await Get(response.Entity.Id ?? throw new Exception("No se ha podido guardar"));

        }
	}
}

