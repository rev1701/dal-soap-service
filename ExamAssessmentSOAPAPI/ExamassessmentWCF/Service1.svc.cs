using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AutoMapper;
using EAD = ExamAssessmentDaal;
using LMS1701.EA.SOAPAPI;


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
        public List<Answers> GetAnswersQuestion(int Questid)
        {
            var first = from c in db.QuestionAnswers
                        where c.QuestionID == Questid
                        select c.AnswerID;
            List<Answers> i = new List<Answers>();
            for (int k = 0; k < first.ToList().Count; k++)
            {
                var second = from x in db.Answers
                             where x.PKID == first.ToArray()[k]
                             select x;
                var resulter = Mapper.Map<Answers>(second);
                i.Add(resulter);

            }
            return i;

        }
        public List<Subject> GetAllSubject()
        {
            List<Subject> result = new List<Subject>();
            var x = from c in db.Subjects
                    select c;
            for (int i = 0; i < x.ToList().Count; i++)
            {
                Subject sub = new Subject();
                sub.Subject_ID = x.ToList().ToArray()[i].Subject_ID;
                sub.Subject_Name = x.ToList().ToArray()[i].Subject_Name;
                var ab = from c in db.Subject_Categories
                         where c.Subject_ID == sub.Subject_ID
                         select c;

                for (int b = 0; b < ab.ToList().Count; b++)
                {
                    Category cat = new Category();
                    var abc = from c in db.Categories_Subtopic
                              where c.Categories_ID == ab.ToList().ToArray()[b].Categories_ID
                              select c;
                    cat.Categories_ID = abc.ToArray()[b].Categories_ID;
                    var cde = from c in db.Categories
                              where c.Categories_ID == cat.Categories_ID
                              select c;
                    cat.Categories_Name = cat.Categories_Name;
                    var xe = from gg in db.Categories_Subtopic
                             where gg.Categories_ID == abc.ToArray()[b].Categories_ID
                             select gg;
                    for (int c = 0; c < xe.ToList().Count; c++)
                    {
                        SubTopic subT = new SubTopic();
                        var xed = from xx in db.Subtopics
                                  where xx.Subtopic_ID == xe.ToArray()[c].Subtopic_ID
                                  select xx;
                        for (int dd = 0; dd < xed.ToList().Count; dd++)
                        {
                            subT.Subtopic_Name = xed.ToArray()[dd].Subtopic_Name;
                            subT.Subtopic_ID = xed.ToArray()[dd].Subtopic_ID;
                            cat.subtopics.Add(subT);
                            sub.listC.Add(cat);
                            result.Add(sub);
                        }


                    }

                }

            }
            return result.ToList();
        }
        public List<SubTopic> GetSubtopicList()
        {
            var x = db.Subtopics.Select(j => Mapper.Map<SubTopic>(j));
            return x.ToList().ToArray().ToList();
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
