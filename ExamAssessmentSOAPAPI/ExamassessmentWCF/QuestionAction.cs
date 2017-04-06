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
        private static EAD.ExamAssessmentEntities db = new EAD.ExamAssessmentEntities();
     
        private static List <EAD.Question> dbquestion = db.Question.ToList();

        public static Question getQuestion(int tempID)
        {
             List<EAD.Question> Question = dbquestion.Where(s => s.PKID == tempID).ToList();
             Question quest = new Question();
             quest.PKID = Question.ElementAt(0).PKID;
             quest.Description = Question.ElementAt(0).Description;
             return quest;
        }
    }
}