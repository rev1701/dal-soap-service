using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{
    [DataContract]
    public class LanguageType
    {
        public LanguageType()
        {

        }
        [DataMember]
        public int PKID { get; set; }
        [DataMember]
        public string LanguageName { get; set; }

       
    }
}