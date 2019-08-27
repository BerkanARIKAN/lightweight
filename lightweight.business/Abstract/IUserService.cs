using lightweight.data.Model;
using System.Collections.Generic;

namespace lightweight.business.Abstract
{
    public interface IUserService
    {
        ServiceResponse<Users> Authenticate(string user,string pass);
        ServiceResponse<Users> GetList();
    }
}