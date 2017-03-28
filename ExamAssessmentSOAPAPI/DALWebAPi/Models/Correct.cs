using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ExamassessmentWCF.DTO
{
    [DataContract]
    public class Correct
    {
        [DataMember]
        public Boolean isCorrect { get; set; }
         
    }
}