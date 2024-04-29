using System;
using Data.Dtos;

namespace CustomerApi.UseCases
{
        public interface IUpdateCustomerUseCase
        {
            Task<CustomerDto?> Execute(CustomerDto customer);
        }
}

