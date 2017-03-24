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
            var subjects = from tempSubjects in db.Subjects
                    select tempSubjects;
            for (int i = 0; i < subjects.ToList().Count; i++)
            {
                Subject newSubject = new Subject();
                newSubject.Subject_ID = subjects.ToList().ToArray()[i].Subject_ID;
                newSubject.Subject_Name = subjects.ToList().ToArray()[i].Subject_Name;
                var categories = from c in db.Subject_Categories
                         where c.Subject_ID == newSubject.Subject_ID
                         select c;
                if(categories.ToList().Count < 1)
                {
                    result.Add(newSubject);
                }
                else
                {
                    for (int b = 0; b < categories.ToList().Count; b++)
                    {

                        Category Tempcategory = new Category();
                        var categoryID = from c in db.Categories_Subtopic
                                         where c.Categories_ID == categories.ToList().ToArray()[b].Categories_ID
                                         select c.Categories_ID;
                        Tempcategory.Categories_ID = categories.ToList().ToArray()[b].Categories_ID;
                        var CategoryName = from tempName in db.Categories
                                           where tempName.Categories_ID == Tempcategory.Categories_ID
                                           select tempName;
                        Tempcategory.Categories_Name = CategoryName.ToArray().ElementAt(0).Categories_Name;
                        var SubtopicIDs = from TempID in db.Categories_Subtopic
                                          where TempID.Categories_ID == Tempcategory.Categories_ID
                                          select TempID.Subtopic_ID;
                        if(SubtopicIDs.ToList().Count() < 0)
                        {
                            newSubject.listCat.Add(Tempcategory);
                            result.Add(newSubject);
                        }
                        else
                        {
                            for (int c = 0; c < SubtopicIDs.ToList().Count; c++)
                            {
                                SubTopic newSub = new SubTopic();
                                var NewSubtopics = from SubtopicCount in db.Subtopics
                                                   where SubtopicCount.Subtopic_ID == SubtopicIDs.ToArray()[c]
                                                   select SubtopicCount;
                                
                                for (int dd = 0; dd < 2; dd++)
                                {
                                    newSub = new SubTopic();
                                    newSub.Subtopic_Name = "dd";
                                    newSub.Subtopic_ID = 3;
                                    Tempcategory.subtopics.Add(newSub);
                                    
                                }


                            }
                            newSubject.listCat.Add(Tempcategory);
                            result.Add(newSubject);
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
