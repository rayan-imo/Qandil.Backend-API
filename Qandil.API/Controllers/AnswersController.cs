using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController(IAnswerService _answerService) : ControllerBase
    {
    }

             
}
