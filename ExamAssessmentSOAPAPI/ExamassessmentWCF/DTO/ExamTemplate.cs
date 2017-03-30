using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{
    [DataContract]
    public class ExamTemplate
    {
        public ExamTemplate()
        {
            ExamQuestions = new List<ExamQuestion>();
        }
        [DataMember]
        public int PKID { get; set; }
        [DataMember]
        public string ExamTemplateName { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public string ExamTemplateID { get; set; }

        [DataMember]
        public virtual Examtype ExamType { get; set; }
        [DataMember]
        public List<ExamQuestion> ExamQuestions { get; set; }
    }
}