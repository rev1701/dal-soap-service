//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamAssessmentDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Categories_Subtopic
    {
        public int Categories_Subtopic_ID { get; set; }
        public int Categories_ID { get; set; }
        public int Subtopic_ID { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Subtopic Subtopic { get; set; }
    }
}
