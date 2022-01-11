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
    public class QuizesController : ApiController
    {
        private DBQuizardEntities _context;

        public QuizesController()
        {
            _context = new DBQuizardEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpPost]
        public IHttpActionResult NoviQuiz(QuizDto quizDto)
        {
            if (!ModelState.IsValid)
            {
                // zbog IHttpactionResult
                return BadRequest();
            }

            var quiz = Mapper.Map<QuizDto, Quiz>(quizDto);
            _context.Quiz.Add(quiz);
            _context.SaveChanges();

            //novi kupac dobiva ID pa ga dajemo Dto-u da ga dobije klijent nazad
            quizDto.IDQuiz = quiz.IDQuiz;

            //vracamo unified resource indentifier /api/Kupci/10  kupacDto je kreirani objekt a ovo prije je url api/kupac/ + njegov id
            return Created(new Uri(Request.RequestUri + "/" + quiz.IDQuiz), quizDto);
        }

        [HttpPut]
        public void Edit(int id, QuizDto quizDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var QuizUBazi = _context.Quiz.SingleOrDefault(k => k.IDQuiz == id);

            if (QuizUBazi == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map<QuizDto, Quiz>(quizDto, QuizUBazi);

            _context.SaveChanges();
        }

        //DELETE:/api/Kupci/1
        [HttpDelete]
        public void Delete(int id)
        {

            var QuizUBazi = _context.Quiz.SingleOrDefault(k => k.IDQuiz == id);

            if (QuizUBazi == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var Question = _context.Question.Where(k => k.QuizID == id).ToList();

            foreach (var question in Question)
            {
                _context.Answer.RemoveRange(_context.Answer.Where(a => a.QuestionID == question.IDQuestion));
            }
            _context.SaveChanges();
            _context.Question.RemoveRange(_context.Question.Where(q => q.QuizID == id));
            _context.SaveChanges();
            _context.Quiz.Remove(QuizUBazi);
            _context.SaveChanges();
        }
    }
}
