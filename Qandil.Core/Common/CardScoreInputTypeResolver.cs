using Qandil.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qandil.Core.Common
{
    public static class CardScoreInputTypeResolver
    {
        // بطاقة ولي الأمر بس Frequency، الباقي كلهم RawNumber
        public static ScoreInputType Resolve(CardType cardType)
        {
            return cardType switch
            {
                CardType.ParentEvaluation => ScoreInputType.Frequency,
                _ => ScoreInputType.RawNumber
            };
        }
    }
}
