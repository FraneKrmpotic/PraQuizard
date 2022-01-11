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
    public class AnswerController : ApiController
    {
        private DBQuizardEntities _context;

        public AnswerController()
        {
            _context = new DBQuizardEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpPost]
        public IHttpActionResult NoviAnswer(AnswerDto answerDto)
        {
            if (!ModelState.IsValid)
            {
                // zbog IHttpactionResult
                return BadRequest();
            }

            var answer = Mapper.Map<AnswerDto, Answer>(answerDto);
            _context.Answer.Add(answer);
            _context.SaveChanges();

            //novi kupac dobiva ID pa ga dajemo Dto-u da ga dobije klijent nazad
            answerDto.IDAnswer = answer.IDAnswer;

            //vracamo unified resource indentifier /api/Kupci/10  kupacDto je kreirani objekt a ovo prije je url api/kupac/ + njegov id
            return Created(new Uri(Request.RequestUri + "/" + answer.IDAnswer), answerDto);
        }

        [HttpPut]
        public void Edit(int id, AnswerDto answerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var AnswerUBazi = _context.Answer.SingleOrDefault(a => a.IDAnswer == id);

            if (AnswerUBazi == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map<AnswerDto, Answer>(answerDto, AnswerUBazi);

            _context.SaveChanges();
        }

        //DELETE:/api/Kupci/1
        [HttpDelete]
        public void Delete(int id)
        {

            var AnswerUBazi = _context.Answer.SingleOrDefault(k => k.IDAnswer == id);

            if (AnswerUBazi == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Answer.Remove(AnswerUBazi);
            _context.SaveChanges();
        }
    }
}
