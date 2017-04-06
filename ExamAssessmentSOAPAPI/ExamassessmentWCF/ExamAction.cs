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
        private static EAD.ExamAssessmentEntities db = new EAD.ExamAssessmentEntities();
        public static  ExamTemplate getExam(string id)
        {
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
        public static ExamQuestion getExamQuestion(EAD.ExamQuestion ExamQuestion)
        {
            AutoMapperConfiguration.Configure();
            ExamQuestion ExamQ = new ExamQuestion();
            ExamQ.ExamQuestionID = ExamQuestion.ExamQuestionID;
            ExamQ.ExamQuestionName = ExamQuestion.ExamQuestionName;
            ExamQ.PKID = ExamQuestion.PKID;// Mapper.Map<ExamQuestion>(ExamQuestion);
            ExamQ.QuestionType.PKID = ExamQuestion.QuestionType.PKID;
            ExamQ.QuestionType.QuestionTypeName = ExamQuestion.QuestionType.QuestionTypeName;
            return ExamQ;
        }
      
    }
}