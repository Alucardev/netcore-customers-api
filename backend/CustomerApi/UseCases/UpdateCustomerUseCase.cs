using Data.Repositories;
using Data.Dtos;
using CustomerApi.UseCases;

namespace CustomersApi.CasosDeUso
{
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {

        private readonly ICustomerRepository repository;

        public UpdateCustomerUseCase(ICustomerRepository repository)
        {
            this.repository = repository;
        }


        // recive un customerdto editado, lo busca en la db y lo actualiza.
        public async Task<CustomerDto?> Execute(CustomerDto customer)
        {
            var entity = await repository.Get(customer.Id); 
            if (entity == null)
                return null;

            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Email = customer.Email;
            entity.Phone = customer.Phone;
            entity.Address = customer.Address;

            await repository.Update(entity);
            return entity.ToDto();

        }

    }
}