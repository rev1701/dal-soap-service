using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{

    [DataContract]
    public class QuestionType
    {
       
        public QuestionType()
        {
           // this.ExamQuestions = new HashSet<ExamQuestion>();
        }
        [DataMember]
        public int PKID { get; set; }
        [DataMember]
        public string QuestionTypeName { get; set; }

       // [DataMember]
       // public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
    }
}