using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamassessmentWCF.DTO
{
    public class QuestionType
    {
       
        public QuestionType()
        {
            this.ExamQuestions = new HashSet<ExamQuestion>();
        }

        public int PKID { get; set; }
        public string QuestionTypeName { get; set; }

    
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
    }
}