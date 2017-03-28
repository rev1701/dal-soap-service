using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ExamassessmentWCF.DTO;
namespace LMS1701.EA.SOAPAPI
{
    public class Answers
    {
        public int PKID { get; set; }
        public string Answer1 { get; set; }
        public string AddLanguageTypeID { get; set; }
        public Correct correct;
    }
}