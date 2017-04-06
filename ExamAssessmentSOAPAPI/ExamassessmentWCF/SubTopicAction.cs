using AutoMapper;
using LMS1701.EA.SOAPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAD = ExamAssessmentDaal;
namespace LMS1701.EA.SOAPAPI
{
    public class SubTopicAction
    {
        private static EAD.ExamAssessmentEntities db = new EAD.ExamAssessmentEntities();
        private static List<EAD.Subtopic> dbSubtopics = db.Subtopic.ToList();    

        /// <summary>
        /// Returns A subtopic DTO objects based upon
        /// the id given.
        /// </summary>
        /// <param name="SubtopicID"></param>
        /// <returns></returns>
        public static SubTopic getSubtopic(int SubtopicID)
        {     
            SubTopic newSubtopic = new SubTopic();
            newSubtopic = Mapper.Map<SubTopic>(dbSubtopics.Where(x => x.Subtopic_ID == SubtopicID).First());
            return newSubtopic;
        }
    }
}