using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIFT.QuestionBank.Shared.Model
{
    public class Question
    {
        public string QuestionName { get; set; }
        public string QuestionText { get; set; }
        public ObservableCollection<QuestionChoice> Choices { get; } = new ObservableCollection<QuestionChoice>();

        public string ToGIFTString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"::{this.QuestionName}::");
            builder.AppendLine($"{this.QuestionText} {{");
            foreach (var choice in this.Choices)
            {
                builder.AppendLine($"  ~%{choice.Percentage}%{choice.Text}");
            }

            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
