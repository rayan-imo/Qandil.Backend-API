using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class QuestionOption: BaseEntity
    {
        public string Text { get; set; }
        public int? Value { get; set; }
        public int Order { get; set; }
        public Guid DiagnosisQuestionId { get; set; }
        public DiagnosisQuestion Question { get; set; }
    }
}
