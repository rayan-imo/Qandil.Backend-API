using Qandil.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qandil.Service.AuthServices.Helper.Dtos.Responses
{
    public class CreateUserResponseDto
    {
        public string Message { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }
       public bool IsAuthenticated {  get; set; }=false;
    }
}
