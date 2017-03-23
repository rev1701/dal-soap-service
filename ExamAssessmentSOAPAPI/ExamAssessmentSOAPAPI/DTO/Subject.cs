using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{
    public class Subject
    {
        public int Subject_ID { get; set; }
        public string Subject_Name { get; set; }
        public List<Category> listC { get; set; }

    }
}