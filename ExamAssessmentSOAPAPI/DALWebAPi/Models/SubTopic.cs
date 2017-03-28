using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{
    [DataContract]
    public class SubTopic
    {
        [DataMember]
        public string Subtopic_Name { get; set; }
        [DataMember]
        public int Subtopic_ID { get; set; }
    }
}