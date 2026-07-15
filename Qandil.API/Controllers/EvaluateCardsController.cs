using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.EvaluateCards;
using Qandil.API.Dtos.Responses.EvaluateCards;
using Qandil.Core.Common;
using Qandil.Service.Dtos.AnswerDto.Requests;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluateCardsController(IAnswerService _answerService) : ControllerBase
    {


        [HttpPost("parent")]
        public async Task<ActionResult<ApiResponse<EvaluateCardResponse>>> EvaluateParentCard([FromBody] EvaluateCardRequest dto)
        {
            dto.CardName = "ولي الأمر";

            if (dto.Answers == null || !dto.Answers.Any())
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "لا توجد إجابات للحفظ",
                    MessageEn = "There are no answers to save",
                });
            }
            var EvaluateCardDto = new EvaluateCardRequestDto
            {
                DiagnosisId = dto.DiagnosisId,
                CardName = dto.CardName,
                Answers = dto.Answers,
            };

            var result = await _answerService.SaveAndEvaluateCardAsync(EvaluateCardDto);

            return Ok(new ApiResponse<EvaluateCardResponse>
            {
                Success = true,
                MessageAr = "تم حفظ الإجابات ",
                MessageEn = "answers saved successfully",
                Data = EvaluateCardResponse.Transform(result.Value)
            });
        }

        [HttpPost("under7")]
        public async Task<ActionResult<EvaluateCardResponse>> EvaluateUnder7Card([FromBody] EvaluateCardRequestDto dto)
        {
            dto.CardName = "أقل من 7 سنوات";

            if (dto.Answers == null || !dto.Answers.Any())
            {
                return BadRequest(new ApiResponse<string>
                {
                    MessageAr = "لا توجد إجابات للحفظ",
                    MessageEn = "There are no answers to save",
                });
            }

            var result = await _answerService.SaveAndEvaluateCardAsync(dto);

            return Ok(new ApiResponse<EvaluateCardResponse>
            {
                Success = true,
                MessageAr = "تم حفظ الإجابات ",
                MessageEn = "answers saved successfully",
                Data = EvaluateCardResponse.Transform(result.Value)
            });
        }


        [HttpPost("above7")]
        public async Task<ActionResult<ApiResponse<EvaluateCardResponse>>> EvaluateAbove7Card([FromBody] EvaluateCardRequestDto dto)
        {
            dto.CardName = "فوق 7 سنوات";

            if (dto.Answers == null || !dto.Answers.Any())
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "لا توجد إجابات للحفظ",
                    MessageEn = "There are no answers to save",
                });
            }

            var result = await _answerService.SaveAndEvaluateCardAsync(dto);

            return Ok(new ApiResponse<EvaluateCardResponse>
            {
                Success = true,
                MessageAr = "تم حفظ الإجابات ",
                MessageEn = "answers saved successfully",
                Data = EvaluateCardResponse.Transform(result.Value)
            });
        }
    }
}



