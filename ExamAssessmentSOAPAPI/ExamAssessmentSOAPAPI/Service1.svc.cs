﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AutoMapper;
using EAD = ExamAssessmentDaal;
using LMS1701.EA.SOAPAPI;
using ExamAssessmentSOAPAPI.DTO;

namespace LMS1701.EA.SOAPAPI
{

    
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private EAD.ExamAssessmentEntities db = new EAD.ExamAssessmentEntities();

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public List<Answers>GetAnswersQuestion(int Questid)
        {
            var first = from c in db.QuestionAnswers
                         where c.QuestionID == Questid
                                                 select c.AnswerID;
            List<Answers> i = new List<Answers>();
            for(int k = 0; k < first.ToList().Count; k++)
            {
                var second = from x in db.Answers
                             where x.PKID == first.ToArray()[k]
                             select x;
                var resulter = Mapper.Map<Answers>(second);
                i.Add(resulter);

            }
            return i;

        }
        public List<SubTopic>GetSubtopicList()
        {
            var x = db.Subtopics.Select(j => Mapper.Map<SubTopic>(j));
            return x.ToList();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            EAD.Subtopic test = new EAD.Subtopic();
            AutoMapperConfiguration.Configure();
            SubTopic test2 = new SubTopic();
            test2 = Mapper.Map<SubTopic>(test);
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
