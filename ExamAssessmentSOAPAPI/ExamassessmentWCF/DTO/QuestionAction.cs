using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAD = ExamAssessmentDaal;
using DTO = ExamassessmentWCF;
using LMS1701.EA.SOAPAPI;

namespace LMS1701.EA.SOAPAPI
{
    public class QuestionAction
    {
        private EAD.ExamAssessmentEntities db;
        private List<EAD.Question> dbquestion;
        public QuestionAction()
        {
            db = new EAD.ExamAssessmentEntities();
            dbquestion = db.Question.ToList();
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tempID"></param>
        /// <returns></returns>
        public  Question getQuestion(int tempID)
        {
            List<EAD.Question> Question = dbquestion.Where(s => s.PKID == tempID).ToList();
            Question quest = new Question();
            quest.PKID = Question.ElementAt(0).PKID;
            quest.Description = Question.ElementAt(0).Description;
            return quest;
        }
    }
}