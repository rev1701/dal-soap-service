using LMS1701.EA.SOAPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{
    [DataContract]
    public class ExamQuestion
    {
        
        public ExamQuestion()
        {
            this.ExamQuestion_Categories = new List<Category>();
            this.QuestionType = new QuestionType();
            this.quest = new List<Question>();
        }
        [DataMember]
        public int PKID { get; set; }
        [DataMember]
        public string ExamQuestionName { get; set; }
        [DataMember]
        public QuestionType QuestionType { get; set; }
        [DataMember]
        public string ExamQuestionID { get; set; }
        [DataMember]
        public List<Question> quest { get; set; }
        [DataMember]
        public virtual ICollection<Category> ExamQuestion_Categories { get; set; }

    }
}