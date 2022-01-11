using PRA.DBQuizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRA.ViewModels
{
    public class UserIKvizovi
    {
        public IEnumerable<Quiz> Quizovi { get; set; }
        public UserAcc User { get; set; }

        //QuizInfo
        public int IDQuiza { get; set; }
        public string NazivQuiza { get; set; }
        public IEnumerable<Question> Pitanja { get; set; }
        public IEnumerable<Answer> Odgovori { get; set; }


    }
}