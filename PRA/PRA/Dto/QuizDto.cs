using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRA.Dto
{
    public class QuizDto
    {
        public int IDQuiz { get; set; }
        public string Title { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> UserAccID { get; set; }

    }
}