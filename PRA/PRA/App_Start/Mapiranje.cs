using AutoMapper;
using PRA.DBQuizard;
using PRA.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRA.App_Start
{
    public class Mapiranje : Profile
    {
        public Mapiranje()
        {
            Mapper.CreateMap<UserAcc, UserDto>();
            Mapper.CreateMap<UserDto, UserAcc>();

            Mapper.CreateMap<Quiz, QuizDto>();
            Mapper.CreateMap<QuizDto, Quiz>();

            Mapper.CreateMap<Question, QuestionDto>();
            Mapper.CreateMap<QuestionDto, Question>();

            Mapper.CreateMap<Answer, AnswerDto>();
            Mapper.CreateMap<AnswerDto, Answer>();
        }
    }
}