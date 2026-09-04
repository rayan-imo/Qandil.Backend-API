using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.QuestionOptionsDto.Responses
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }

        public ScoreInputType? ScoreInputType { get; set; }   // Frequency أو RawNumber
        public int? MinValue { get; set; }                    // بس لو RawNumber
        public int? MaxValue { get; set; }
    }

}
