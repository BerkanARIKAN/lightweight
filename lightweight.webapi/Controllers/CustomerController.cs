using System.Collections.Generic;
using lightweight.business.Abstract;
using lightweight.data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace lightweight.webapi.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
      
        [HttpGet]
        public ServiceResponse<Customers> GetAll()
        {
            var response = _customerService.GetList();
            return response;
        }

        [HttpPost("addCustomer")]
        public ServiceResponse<Customers> CreateCustomer([FromBody] Customers customer)
        {
            return _customerService.Create(customer);
        }
    }
}
