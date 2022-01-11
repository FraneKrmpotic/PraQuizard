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
    public class QuestionController : ApiController
    {
        private DBQuizardEntities _context;

        public QuestionController()
        {
            _context = new DBQuizardEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpPost]
        public IHttpActionResult NoviQuestion(QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
            {
                // zbog IHttpactionResult
                return BadRequest();
            }

            var question = Mapper.Map<QuestionDto, Question>(questionDto);
            _context.Question.Add(question);
            _context.SaveChanges();

            //novi kupac dobiva ID pa ga dajemo Dto-u da ga dobije klijent nazad
            questionDto.IDQuestion = question.IDQuestion;

            //vracamo unified resource indentifier /api/Kupci/10  kupacDto je kreirani objekt a ovo prije je url api/kupac/ + njegov id
            return Created(new Uri(Request.RequestUri + "/" + question.IDQuestion), questionDto);
        }

        [HttpPut]
        public void Edit(int id, QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var QuestionUBazi = _context.Question.SingleOrDefault(k => k.IDQuestion == id);

            if (QuestionUBazi == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map<QuestionDto, Question>(questionDto, QuestionUBazi);

            _context.SaveChanges();
        }

        //DELETE:/api/Kupci/1
        [HttpDelete]
        public void Delete(int id)
        {

            var QuestionUBazi = _context.Question.SingleOrDefault(k => k.IDQuestion == id);

            if (QuestionUBazi == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var Answer = _context.Answer.Where(k => k.QuestionID == id).ToList();

            foreach (var answer in Answer)  
            {
                _context.Answer.RemoveRange(_context.Answer.Where(a => a.QuestionID == answer.QuestionID));
            }
            _context.SaveChanges();
            _context.Question.Remove(QuestionUBazi);
            _context.SaveChanges();
        }
    }
}
