using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIFT.QuestionBank.Shared.Tokenizer
{
    public class GIFTToken
    {
        public string Value { get; set; }

        public static IEnumerable<GIFTToken> From(params string[] values)
        {
            return values.Select(item => new GIFTToken(){Value = item}).ToList();
        }

        public bool IsEmpty => string.IsNullOrWhiteSpace(Value);

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if ((this.Value == null && obj != null) ||
                (this.Value != null && obj == null))
            {
                return false;
            }

            if (this.Value == null && obj == null)
            {
                return true;
            }

            if (obj is GIFTToken giftTokenObject)
            {
                return this.Value.Equals(giftTokenObject.Value);
            }

            return false;
        }

        public override string ToString()
        {
            return $"Token: {this.Value}";
        }
    }
}
