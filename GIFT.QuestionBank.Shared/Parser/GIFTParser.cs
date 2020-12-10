using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GIFT.QuestionBank.Shared.Model;
using GIFT.QuestionBank.Shared.Tokenizer;

namespace GIFT.QuestionBank.Shared.Parser
{
    public class GIFTParser
    {
        public IEnumerable<Question> Parse(string giftContent)
        {
            var tokenizer = new GIFTTokenizer();
            return this.Parse(tokenizer.Tokenize(giftContent));
        }

        public IEnumerable<Question> Parse(IEnumerable<GIFTToken> tokens)
        {
            List<Question> questions = new List<Question>();
            Question currentQuestion = null;
            int state = 0;

            int currentQuestionChoicePercentage = 0;

            foreach (var token in tokens)
            {
                if (token.IsEmpty)
                {
                    continue;
                }

                if ((state == 0 || state == 1 || state == 3 || state == 4) &&
                    token.Value == ":")
                {
                    state++;
                }
                else if (state == 2)
                {
                    currentQuestion = new Question() { QuestionName = token.Value };
                    state++;
                }
                else if (state == 5)
                {
                    currentQuestion.QuestionText = token.Value;
                    state++;
                }
                else if ((state == 6 && token.Value == "{") ||
                         (state == 7 && token.Value == "~") ||
                         (state == 8 && token.Value == "%") ||
                         (state == 10 && token.Value == "%"))
                {
                    state++;
                }
                else if (state == 9)
                {
                    try
                    {
                        currentQuestionChoicePercentage = int.Parse(token.Value);
                    }
                    catch (Exception exc)
                    {
                        throw new InvalidDataException($"Invalid data '{token.Value}' in state = {state}", exc);
                    }

                    state++;
                }
                else if (state == 11)
                {
                    currentQuestion.Choices.Add(new QuestionChoice()
                    {
                        Text = token.Value,
                        Feedback = string.Empty,
                        Percentage = currentQuestionChoicePercentage
                    });
                    state = 7;
                }
                else if (state == 7 && token.Value == "}")
                {
                    questions.Add(currentQuestion);
                    currentQuestion = null;
                    state = 0;
                }
                else
                {
                    throw new InvalidDataException($"Invalid data '{token.Value}' in state = {state}");
                }
            }

            return questions;
        }
    }
}
