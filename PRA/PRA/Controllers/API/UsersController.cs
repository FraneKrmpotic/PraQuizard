using AutoMapper;
using PRA.DBQuizard;
using PRA.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PRA.Controllers.API
{
    public class UsersController : ApiController
    {
        private DBQuizardEntities _context;

        public UsersController()
        {
            _context = new DBQuizardEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //POST:/api/Users
        [HttpPost] //ZA KREIRANJE JE POST
        public IHttpActionResult NewUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                // zbog IHttpactionResult
                return BadRequest();
            }

            var user = Mapper.Map<UserDto, UserAcc>(userDto);
            _context.UserAcc.Add(user);
            _context.SaveChanges();

            //novi kupac dobiva ID pa ga dajemo Dto-u da ga dobije klijent nazad
            userDto.IDUserAcc = user.IDUserAcc;

            //vracamo unified resource indentifier /api/Kupci/10  kupacDto je kreirani objekt a ovo prije je url api/kupac/ + njegov id
            return Created(new Uri(Request.RequestUri + "/" + user.IDUserAcc), userDto);
        }
        [HttpPut]
        public void UpdateUser(int id, UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var UserUBazi = _context.UserAcc.SingleOrDefault(u => u.IDUserAcc == id);

            if (UserUBazi == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map<UserDto, UserAcc>(userDto, UserUBazi);

            _context.SaveChanges();
        }

        [System.Web.Http.HttpDelete]
        public void DeleteUser(int id)
        {

            var UserUBazi = _context.UserAcc.SingleOrDefault(u => u.IDUserAcc == id);

            if (UserUBazi == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var Quiz = _context.Quiz.Where(q => q.UserAccID == id).ToList();

            foreach (var quiz in Quiz)
            {

                var QuestionOfOneSpecificQuiz = _context.Question.Where(o => o.QuizID == quiz.IDQuiz);

                foreach (var answer in QuestionOfOneSpecificQuiz)
                {
                    _context.Answer.RemoveRange(_context.Answer.Where(a => a.QuestionID == answer.IDQuestion));
                }

                _context.Question.RemoveRange(_context.Question.Where(q => q.QuizID == quiz.IDQuiz));
                
            }


            _context.SaveChanges();
            _context.Quiz.RemoveRange(_context.Quiz.Where(q => q.UserAccID == id));
            _context.SaveChanges();
            _context.UserAcc.Remove(UserUBazi);
            _context.SaveChanges();
        }

    }
}