using Qandil.Core.Common;
using Qandil.Core.Enums;

namespace Qandil.Core.Entity
{
    public class DiagnosisQuestion : BaseEntity
    {
        public CardType? CardType { get; set; }
        public string? MainTitle { get; set; }    
        public string SubTitle { get; set; }

        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }  // Boolean, Score, Text, Options
        public int Order { get; set; }

        public ScoreInputType? ScoreInputType { get; set; }
        public int? MinValue { get; set; }   // بس لما ScoreInputType == RawNumber (مثلاً 0)
        public int? MaxValue { get; set; }   // بس لما ScoreInputType == RawNumber (مثلاً 2)

        public ICollection<DiagnosisAnswer> Answers { get; set; }
        public ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();
    } 
}
