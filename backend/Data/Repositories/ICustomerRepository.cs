using Data.Dtos;
using Data.Models;

namespace Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<CustomerEntity>> GetAll();
        Task<CustomerEntity> Get(long id);
        Task<CustomerEntity> Add(CreateCustomerDto customerDto);
        Task<bool> Update(CustomerEntity customerEntity);
        Task<bool> Delete(long id);

    }
}

