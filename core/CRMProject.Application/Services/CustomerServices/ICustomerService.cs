using CRMProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMProject.Application.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<List<GetAllCustomersDto>> GetAllCustomersAsync();
        Task CreateCustomersAsync(CreateCustomerDto model);
    }
}
