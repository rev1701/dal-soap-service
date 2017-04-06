using AutoMapper;
using LMS1701.EA.SOAPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAD = ExamAssessmentDaal;
namespace ExamassessmentWCF
{
    public class SubTopicAction
    {
        private static EAD.ExamAssessmentEntities db = new EAD.ExamAssessmentEntities();
        private static List<EAD.Subtopic> dbSubtopics = db.Subtopic.ToList();    
     
        public static SubTopic getSubtopic(int SubtopicID)
        {     
            SubTopic newSubtopic = new SubTopic();
            newSubtopic = Mapper.Map<SubTopic>(dbSubtopics.Where(x => x.Subtopic_ID == SubtopicID).First());
            return newSubtopic;
        }
    }
}