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
        EAD.ExamAssessmentEntities db;
        List<ExamAssessmentDaal.Answer> dbanswer;
        public AnswerAction()
        {
            EAD.ExamAssessmentEntities db = new EAD.ExamAssessmentEntities();
            List<ExamAssessmentDaal.Answer> dbanswer = db.Answer.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Answers getAnswer(int id)
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