﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ExamassessmentWCF.DTO;
namespace LMS1701.EA.SOAPAPI
{
    [DataContract]
    public class Answers
    {
        [DataMember]
        public int PKID { get; set; }
        [DataMember]
        public string Answer1 { get; set; }
        [DataMember]
        public String AddLanguageTypeID { get; set; }
        [DataMember]
        public Correct correct;
    }
}