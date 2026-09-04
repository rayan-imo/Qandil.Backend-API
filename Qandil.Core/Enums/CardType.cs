using System.ComponentModel;

namespace Qandil.Core.Enums
{
    public enum CardType
    {
        [Description("غير محدد")]
        None = 0,

        [Description("بطاقة تقييم الأسئلة الموجهة لولي الأمر")]
        ParentEvaluation = 1,

        [Description("بطاقة تقييم الأطفال من 4 إلى 7 سنوات")]
        Child4To7Years = 2,

        [Description("بطاقة تقييم الأطفال فوق 7 سنوات")]
        ChildAbove7Years = 3,

        [Description("بطاقة تقييم الأطفال تم تشخيصهم سابقاً")]
        PreviouslyDiagnosed = 4
    }
}
