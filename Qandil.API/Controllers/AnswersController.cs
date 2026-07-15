using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qandil.Core.Common;
using Qandil.Service.Dtos.AnswerDto.Requests;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController(IAnswerService _answerService) : ControllerBase
    {



    }
}
