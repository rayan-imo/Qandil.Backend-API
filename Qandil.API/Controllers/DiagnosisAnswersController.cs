using Microsoft.AspNetCore.Mvc;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisAnswersController(IAnswerService _answerService) : ControllerBase
    {

    }
}
