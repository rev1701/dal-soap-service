using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{
    public class Category
    {
        public int Categories_ID { get; set; }
        public string Categories_Name { get; set; }
        public List<SubTopic> subtopics { get; set; }

    }
}