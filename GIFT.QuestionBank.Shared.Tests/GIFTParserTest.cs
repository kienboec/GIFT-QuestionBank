using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GIFT.QuestionBank.Shared.Model;
using GIFT.QuestionBank.Shared.Parser;
using GIFT.QuestionBank.Shared.Tokenizer;
using NUnit.Framework;

namespace GIFT.QuestionBank.Shared.Tests
{
    public class GIFTParserTest
    {
        [Test]
        public void Parse_whenGettingTokens_thenShouldParseToModel()
        {
            // arrange
            var parser  = new GIFTParser();
            var tokens = GIFTToken.From(
                "",
                ":", ":", "PeopleInGrantsTomb", ":", ":",
                "What two people are entombed in Grant's tomb?", "{", "",
                "~", "%", "-100", "%", "No one",
                "~", "%", "50", "%", "Grant",
                "~", "%", "50", "%", "Grant's wife",
                "~", "%", "-100", "%", "Grant's father",
                "}", "");

            // act 
            var actual = parser.Parse(tokens);

            // assert
            CollectionAssert.AllItemsAreNotNull(actual);
            CollectionAssert.AllItemsAreInstancesOfType(actual, typeof(Question));
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(4, actual.First().Choices.Count);
        }
    }
}
