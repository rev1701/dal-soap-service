using LMS1701.EA.SOAPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamassessmentWCF.DTO
{
    public class ExamQuestion
    {
        public ExamQuestion()
        {
            this.ExamQuestion_Categories = new List<Category>();
            quest = new List<Question>();
        }

        public int PKID { get; set; }
        public string ExamQuestionName { get; set; } 
        public QuestionType QuestionType { get; set; }
        public string ExamQuestionID { get; set; }
        public List<Question> quest { get; set; }
        
        public virtual ICollection<Category> ExamQuestion_Categories { get; set; }
    //    public virtual QuestionType QuestionType { get; set; }
    }
}