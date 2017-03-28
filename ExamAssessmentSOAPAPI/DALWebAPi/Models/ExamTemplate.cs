using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ExamassessmentWCF.DTO
{
    public class ExamTemplate
    {
        public ExamTemplate()
        {
            ExamQuestions = new List<ExamQuestion>();
        }
        public int PKID { get; set; }
        public string ExamTemplateName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ExamTemplateID { get; set; }
        //   public int ExamTypeID { get; set; }
        public virtual Examtype ExamType { get; set; }
        public List<ExamQuestion> ExamQuestions { get; set; }
    }
}