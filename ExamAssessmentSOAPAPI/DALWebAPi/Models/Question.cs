using LMS1701.EA.SOAPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ExamassessmentWCF.DTO
{
    public class Question
    {

       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
         //   this.ExamQuestionLists = new HashSet<ExamQuestionList>();
            this.Answers = new List<Answers>();
        }
        public int PKID { get; set; }
        public string Description { get; set; }
        public Correct correct { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //  public virtual ICollection<ExamQuestionList> ExamQuestionLists { get; set; }
        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answers> Answers { get; set; }
    }
}