using AutoMapper;
using LMS1701.EA.SOAPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAD = ExamAssessmentDaal;
namespace LMS1701.EA.SOAPAPI
{
    public class ExamAction
    {
        EAD.ExamAssessmentEntities db;
        public ExamAction()
        {
            EAD.ExamAssessmentEntities db = new EAD.ExamAssessmentEntities();
        }
      
        /// <summary>
        /// Given an id of an exam template it returns
        /// an exam template EAD variable.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public   ExamTemplate getExam(string id)
        {
             db = new EAD.ExamAssessmentEntities();
            List<EAD.ExamTemplate> ExamTemplate = db.ExamTemplate.Where(s => s.ExamTemplateID == id).ToList();
            ExamTemplate exam = new ExamTemplate();
            exam.PKID = ExamTemplate.First().PKID;
            exam.CreatedDate = ExamTemplate.First().CreatedDate;
            exam.ExamTemplateName = ExamTemplate.First().ExamTemplateName;
            exam.ExamTemplateID = ExamTemplate.FirstOrDefault().ExamTemplateID;
            exam.ExamType.PKID = ExamTemplate.FirstOrDefault().ExamType.PKID;
            exam.ExamType.ExamTypeName = ExamTemplate.FirstOrDefault().ExamType.ExamTypeName;
            return exam;
        }
        /// <summary>
        /// Given an EAD ExamQuestion it will convert the Exam Question 
        /// into a DTO exam question.
        /// </summary>
        /// <param name="ExamQuestion"></param>
        /// <returns></returns>
        public ExamQuestion getExamQuestion(EAD.ExamQuestion ExamQuestion)
        {
            AutoMapperConfiguration.Configure();
            ExamQuestion ExamQ = new ExamQuestion();
            ExamQ.ExamQuestionID = ExamQuestion.ExamQuestionID;
            ExamQ.ExamQuestionName = ExamQuestion.ExamQuestionName;
            ExamQ.PKID = ExamQuestion.PKID;
            ExamQ.QuestionType.PKID = ExamQuestion.QuestionType.PKID;
            ExamQ.QuestionType.QuestionTypeName = ExamQuestion.QuestionType.QuestionTypeName;
            return ExamQ;
        }
      
    }
}