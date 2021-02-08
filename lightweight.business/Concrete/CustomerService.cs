using lightweight.business.Abstract;
using lightweight.business.Middleware;
using lightweight.core.Abstract;
using lightweight.data.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace lightweight.business.Concrete
{
    public class CustomerService:ICustomerService
    {
        private IRepository<Customers> _customerRepository;

        public CustomerService(IRepository<Customers> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ServiceResponse<Customers> Create(Customers customer)
        {
            var response = new ServiceResponse<Customers>(null);
            try
            {

                _customerRepository.Insert(customer); //db.savechange()
                return response;

            }
            catch (Exception e)
            {
                response.ExceptionMessage = e.Message;
                response.HasExceptionError = true;
                return response;
            }
        }

        public ServiceResponse<bool> DeleteById(int id)
        {
            var response = new ServiceResponse<Boolean>(null);
            try
            {
                Customers customer = _customerRepository.GetById(id);
                _customerRepository.Delete(customer); //db.savechange()
                return response;

            }
            catch (Exception e)
            {
                response.ExceptionMessage = e.Message;
                response.HasExceptionError = true;
                return response;
            }
        }

        public ServiceResponse<Customers> GetList()
        {
            var response = new ServiceResponse<Customers>(null);
            try
            {
                response.List = _customerRepository.Table.ToList();
                response.Count = response.List.Count;
                response.IsSuccessful = true;
                return response;
            }
            catch (Exception e)
            {
                response.ExceptionMessage = e.Message;
                response.HasExceptionError = true;
                return response;
            }
        }

        public ServiceResponse<Customers> Update(Customers customer)
        {
            var response = new ServiceResponse<Customers>(null);
            try
            {
                Customers oldCustomer = _customerRepository.GetById(customer.customerId);
                oldCustomer.firstName = customer.firstName;

                _customerRepository.Update(oldCustomer); //db.savechange()
                return response;

            }
            catch (Exception e)
            {
                response.ExceptionMessage = e.Message;
                response.HasExceptionError = true;
                return response;
            }
        }
    }
}
