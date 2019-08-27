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
    public class UserService:IUserService
    {
        private IRepository<Users> _userRepository;

        public UserService(IRepository<Users> userRepository)
        {
            _userRepository = userRepository;
        }

        public ServiceResponse<Users> Authenticate(string user,string pass)
        {
            var response = new ServiceResponse<Users>(null);
          try
          {
              var userget = _userRepository.Table.FirstOrDefault(x => x.email == user && x.password == Cipher.Encrypt("password",pass));

            // return null if user not found
            if (userget == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("jwt secret key. change please ");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userget.id.ToString()),
                    new Claim(ClaimTypes.Role, userget.role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userget.token = tokenHandler.WriteToken(token);

            // remove password before returning
            userget.password = null;
            response.Entity= userget;
            response.IsSuccessful=true;
            return response;
          }
          catch (System.Exception ex)
          {
              response.ExceptionMessage=ex.Message;
              response.HasExceptionError=true;
              return response;
            
          }
            
        }

        public ServiceResponse<Users> GetList()
        {
            var response = new ServiceResponse<Users>(null);
            try
            {
                response.List = _userRepository.Table.ToList();
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
    }
}