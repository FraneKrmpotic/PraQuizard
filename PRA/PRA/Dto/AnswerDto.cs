using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRA.Dto
{
    public class AnswerDto
    {
        public int IDAnswer { get; set; }
        public string Answer1 { get; set; }
        public Nullable<int> RightAnswer { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> QuestionID { get; set; }
    }
}