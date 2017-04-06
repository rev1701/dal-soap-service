using LMS1701.EA.SOAPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAD = ExamAssessmentDaal;

namespace ExamassessmentWCF
{
    public class AnswerAction
    {
        private static EAD.ExamAssessmentEntities db = new EAD.ExamAssessmentEntities();
        public static List<ExamAssessmentDaal.Answer> dbanswer = db.Answer.ToList();
        public static Answers getAnswer(int id)
        {
            var TheAnswer = from tempAnswer in dbanswer
                            where tempAnswer.PKID == id
                            select tempAnswer;
            Answers ans = new Answers();
            ans.PKID = TheAnswer.ToArray()[0].PKID;
            ans.Answer1 = TheAnswer.FirstOrDefault().Answer1;
           
            return ans;

        }
    }
}