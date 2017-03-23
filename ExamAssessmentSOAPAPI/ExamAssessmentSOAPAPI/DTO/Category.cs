using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{
    [DataContract]
    public class Category
    {
        [DataMember]
        public int Categories_ID { get; set; }
        [DataMember]
        public string Categories_Name { get; set; }
        [DataMember]
        public List<SubTopic> subtopics { get; set; }

    }
}