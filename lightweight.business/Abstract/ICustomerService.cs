using lightweight.data.Model;
using System;
using System.Collections.Generic;

namespace lightweight.business.Abstract
{
    public interface ICustomerService
    {
        ServiceResponse<Customers> GetList();

        ServiceResponse<Customers> Update(Customers customer);
        ServiceResponse<Customers> Create(Customers customer);
        ServiceResponse<Boolean> DeleteById(int id); 

        //Customers Update(Customers customer);

        //Customers Create(Customers customer);

        //void Delete(int id);

    }
}
