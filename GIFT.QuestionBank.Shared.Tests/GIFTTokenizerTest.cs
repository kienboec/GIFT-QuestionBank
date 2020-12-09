using System.Collections.Generic;
using GIFT.QuestionBank.Shared.Tokenizer;
using NUnit.Framework;

namespace GIFT.QuestionBank.Shared.Tests
{
    public class GIFTTokenizerTest
    {
        [Test]
        public void Tokenize_whenGettingASingleNormalGIFTQuestion_thenReturnTokens()
        {
            // arrange
            var tokenizer = new GIFTTokenizer();
            var testdata = @"
::PeopleInGrantsTomb::
What two people are entombed in Grant's tomb? {
   ~%-100%No one
   ~%50%Grant
   ~%50%Grant's wife
   ~%-100%Grant's father
}
";
            // act
            var actual = tokenizer.Tokenize(testdata);

            // assert
            CollectionAssert.AreEqual(GIFTToken.From(
                "", 
                ":", ":", "PeopleInGrantsTomb", ":", ":",
                "What two people are entombed in Grant's tomb?", "{", "", 
                "~", "%", "-100", "%", "No one",
                "~", "%", "50", "%", "Grant",
                "~", "%", "50", "%", "Grant's wife",
                "~", "%", "-100", "%", "Grant's father",
                "}", ""), actual);
        }
    }
}