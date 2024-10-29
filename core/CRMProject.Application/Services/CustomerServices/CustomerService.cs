using CRMProject.Application.Dtos;
using CRMProject.Application.Interfaces;
using CRMProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMProject.Application.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;
        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }
        public async Task CreateCustomersAsync(CreateCustomerDto model)
        {
            await _repository.CreateAsync(new Customer
            {
                CompanyName = model.CompanyName,
                ContactPersonName= model.ContactPersonName,
                Phone=model.Phone,
            });
        }

        public async Task<List<GetAllCustomersDto>> GetAllCustomersAsync()
        {
            var customers = await _repository.GetAllAsync();
            return customers.Select(x => new GetAllCustomersDto
            {
                Id = x.Id,
                CompanyName = x.CompanyName,
                ContactPersonName= x.ContactPersonName,
                Phone=x.Phone,

            }).ToList();
        }
    }
}
