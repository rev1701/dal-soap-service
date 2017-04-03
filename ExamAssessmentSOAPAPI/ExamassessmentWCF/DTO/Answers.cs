using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


namespace LMS1701.EA.SOAPAPI
{
    [DataContract]
    public class Answers
    {
        public Answers()
        {
            //   LanguageType = new LanguageType();
            correct = new Correct();
        }
        [DataMember]
        public int PKID { get; set; }
        [DataMember]
        public string Answer1 { get; set; }
       // [DataMember]
        //public LanguageType LanguageType { get; set; }
        [DataMember]
        public Correct correct;
    }
}