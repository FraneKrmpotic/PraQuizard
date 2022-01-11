using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRA.Dto
{
    public class QuestionDto
    {
        public int IDQuestion { get; set; }
        public string Question1 { get; set; }
        public int Duration { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> QuizID { get; set; }

    }
}