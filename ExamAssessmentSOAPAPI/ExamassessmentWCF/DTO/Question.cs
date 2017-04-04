using LMS1701.EA.SOAPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{
    [DataContract]
    public class Question
    {

     
        public Question()
        {
        
            this.Answers = new List<Answers>();
        }
        [DataMember]
        public int PKID { get; set; }
        [DataMember]
        public string Description { get; set; }

       
        [DataMember]
        public virtual ICollection<Answers> Answers { get; set; }
    }
}