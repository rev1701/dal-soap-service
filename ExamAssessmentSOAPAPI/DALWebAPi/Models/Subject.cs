using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{
    public class Subject
    {
        public int Subject_ID { get; set; }
        public string Subject_Name { get; set; }
        public List<Category> listCat { get; set; }
        public Subject()
        {
            listCat = new List<Category>();
        }

    }
}