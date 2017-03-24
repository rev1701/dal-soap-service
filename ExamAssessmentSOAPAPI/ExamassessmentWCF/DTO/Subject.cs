using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{
    [DataContract]
    public class Subject
    {
        [DataMember]
        public int Subject_ID { get; set; }
        [DataMember]
        public string Subject_Name { get; set; }
        [DataMember]
        public List<Category> listCat { get; set; }
        public Subject()
        {
            listCat = new List<Category>();
        }

    }
}