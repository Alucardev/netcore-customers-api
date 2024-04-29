using CustomerApi.UseCases;
using Data.Dtos;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace CustomerApi.Controllers
{
	[ApiController]
	[Route("api/[control" +
        "ler]")]
	public class CustomersController : Controller
	{
        ICustomerRepository CustomerRepository;
        private readonly IUpdateCustomerUseCase updateCustomerUseCase;

        public CustomersController(ICustomerRepository repository, IUpdateCustomerUseCase updateCustomerUseCase)
        {
            CustomerRepository = repository;
            this.updateCustomerUseCase = updateCustomerUseCase;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await CustomerRepository.GetAll();
            List<CustomerDto> customerDtos = new List<CustomerDto>();
            foreach (var customer in customers)
            {
                var dto = customer.ToDto();
                customerDtos.Add(dto);
            }
            return new OkObjectResult(customerDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(long id)
		{
            CustomerEntity result = await CustomerRepository.Get(id);
            if (result == null)
                return new NotFoundResult();
            return new OkObjectResult(result.ToDto());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomers(long id)
        {
            var result = await CustomerRepository.Delete(id);
            if (!result)
                return new NotFoundResult(); 
            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
        {
            CustomerEntity result = await CustomerRepository.Add(customer);

            return new CreatedResult($"https://localhost:7030/api/customer/{result.Id}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
        {
            CustomerDto? result = await updateCustomerUseCase.Execute(customer);

            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }
    }
}

