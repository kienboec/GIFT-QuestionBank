using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GIFT.QuestionBank.Shared.Tokenizer
{
    public class GIFTTokenizer
    {
        public IEnumerable<GIFTToken> Tokenize(string content)
        {
            List<GIFTToken> tokens = new List<GIFTToken>();

            StringReader reader = new StringReader(content);
            StringBuilder builder = new StringBuilder();

            int cRaw;
            while ((cRaw = reader.Read()) != -1)
            {
                char c = (char) cRaw;
                if (c == ':' || c == '{' || c == '}' || c == '~' || c == '%')
                {
                    if (builder.Length > 0)
                    {
                        tokens.Add(new GIFTToken() {Value = builder.ToString().Trim()});
                        builder.Remove(0, builder.Length);
                    }

                    tokens.Add(new GIFTToken(){ Value = c.ToString() });
                }
                else
                {
                    builder.Append(c);
                }
            }

            if (builder.Length > 0)
            {
                tokens.Add(new GIFTToken() { Value = builder.ToString().Trim() });
            }

            return tokens;
        }
    }
}
