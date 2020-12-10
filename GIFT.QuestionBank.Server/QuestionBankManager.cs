using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIFT.QuestionBank.Shared.Model;
using GIFT.QuestionBank.Shared.Parser;

namespace GIFT.QuestionBank.Server
{
    public class QuestionBankManager
    {
        public List<Question> GetQuestions()
        {
            var myData = @"
::PeopleInGrantsTomb::
What two people are entombed in Grant's tomb? {
   ~%-100%No one
   ~%50%Grant
   ~%50%Grant's wife
   ~%-100%Grant's father
}
";
            GIFTParser parser = new GIFTParser();
            return parser.Parse(myData).ToList();
        }
    }
}
