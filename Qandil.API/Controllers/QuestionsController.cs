using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Responses.Questions;
using Qandil.Core.Common;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController(IQuestionService _questionService) : ControllerBase
    {
        [HttpGet("Card")]
        public async Task<ActionResult<ApiResponse<List<CardQuestionResponse>>>> GetQustionByCardName(string cardName)
        {
            var result = await _questionService.GetQuestionsByCardName(cardName);


            if (!result.IsSuccess)

                return BadRequest(new ApiResponse<string>

                {
                    Success = false,
                    MessageAr = "لا يوجد اسئلة لهذه البطاقة",
                    MessageEn = "there are no question for this card",
                });

            var questionResponses = result.Value.Select(CardQuestionResponse.Transform).ToList();

            return Ok(new ApiResponse<List<CardQuestionResponse>>
            {
                Success = true,
                MessageAr = "تم جلب أسئلة البطاقة بنجاح",
                MessageEn = "Card questions were successfully fetched",
                Data = questionResponses

            });

        }

        [HttpGet("Diagnosis")]
        public async Task<ActionResult<ApiResponse<List<DiagnosisQuestionResponse>>>> GetDiagnosisQuestions()
        {
            var result = await _questionService.GetDiagnosisQuestions();
         

            if (!result.IsSuccess)

                return BadRequest(new ApiResponse<List<string>>
                {
                    MessageAr = "لا يوجد اسئلة تشخيص ",
                    MessageEn = "there are no diagnosis questions ",
                });
            var questionResponses = result.Value.Select(DiagnosisQuestionResponse.Transform).ToList();
            return Ok(new ApiResponse<List<DiagnosisQuestionResponse>>
            {
                Success = true,
                MessageAr = "تم جلب أسئلة التشخيص بنجاح",
                MessageEn = "diagnosis questions were successfully fetched",
                Data = questionResponses
            });

        }


    }
}

