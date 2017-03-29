using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ExamassessmentWCF.DTO
{
    public class Examtype
    {
        public Examtype()
        {
            ExamTemplates = new List<ExamTemplate>();
        }
        public int PKID { get; set; }
        public string ExamTypeName { get; set; }
        public virtual ICollection<ExamTemplate> ExamTemplates { get; set; }
    }
}