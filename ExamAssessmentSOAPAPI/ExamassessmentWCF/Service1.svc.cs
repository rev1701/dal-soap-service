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
using ExamassessmentWCF.DTO;

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
                var second = from x in db.Answer
                             where x.PKID == first.ToArray()[k]
                             select x;
                var resulter = Mapper.Map<Answers>(second);
                i.Add(resulter);

            }
            return i;

        }
       public ExamTemplate getExamTemplate(int id)
        {
         //   List<ExamTemplate> getResults = new List<ExamTemplate>();
        /*    var result = from TempExamTemplate in db.FullExamTemplateInfo
                         where TempExamTemplate.ExamTemplateID == id
                         select TempExamTemplate;
           result.First()
            return result.ToDictionary*/
           // return result.;
            
            var ExamTemplate = from TempExamTemplate in db.ExamTemplate
                               where TempExamTemplate.PKID == id
                               select TempExamTemplate;
            ExamTemplate exam = new ExamTemplate();
            exam.PKID = ExamTemplate.First().PKID;
            exam.CreatedDate = ExamTemplate.First().CreatedDate;
            exam.ExamTemplateName = ExamTemplate.First().ExamTemplateName;
            exam.ExamTemplateID = ExamTemplate.First().ExamTemplateID;
            var ExamTypes = from TempExamType in db.ExamType
                           where TempExamType.PKID == ExamTemplate.First().ExamTypeID
                           select TempExamType;
            Examtype type = new Examtype();
            type.PKID = ExamTypes.First().PKID;
            type.ExamTypeName = ExamTypes.First().ExamTypeName;
            exam.ExamType = type;
            var ExamQuestions = from TempExamQuestions in db.ExamTemplateQuestions
                               where TempExamQuestions.ExamTemplateID == exam.ExamTemplateID
                               select TempExamQuestions.ExamQuestionID;
            if(ExamQuestions.Count() < 1)
            {
                return exam;
            }
            else
            {
               for(int i = 0; i < ExamQuestions.Count(); i++)
                {
                    var ExamQuestion = from TempQuest in db.ExamQuestion
                                       where TempQuest.ExamQuestionID == ExamQuestions.ElementAt(i)
                                       select TempQuest;


                    ExamQuestion ExamQ = new ExamQuestion();
                    ExamQ.ExamQuestionID = ExamQuestion.First().ExamQuestionID;
                    ExamQ.ExamQuestionName = ExamQuestion.First().ExamQuestionName;
                    ExamQ.PKID = ExamQuestion.First().PKID;
                    ExamQ.QuestionType.PKID = ExamQuestion.First().QuestionType.PKID;
                    ExamQ.QuestionType.QuestionTypeName = ExamQuestion.First().QuestionType.QuestionTypeName;
                    var ExamQuestionList = from TempQuestion in db.ExamQuestionList
                                   where TempQuestion.ExamQuestionID == ExamQuestion.First().ExamQuestionID
                                   select TempQuestion;
                    for(int j= 0; j < ExamQuestionList.Count(); j++)
                    {
                        var Question = from TempQuestion in db.Question
                                       where TempQuestion.PKID == ExamQuestionList.ElementAt(j).QuestionID
                                       select TempQuestion;
                        Question quest = new Question();
                        quest.PKID = Question.ElementAt(j).PKID;
                        quest.Description = Question.ElementAt(j).Description;
                        var AnswersID = from QuestionAnswers in db.QuestionAnswers
                                        where QuestionAnswers.QuestionID == quest.PKID
                                        select QuestionAnswers;
                        for(int k = 0;  k < AnswersID.Count(); k++)
                        {
                            var TheAnswer = from tempAnswer in db.Answer
                                            where tempAnswer.PKID == AnswersID.ElementAt(k).AnswerID
                                            select tempAnswer;
                            Answers ans = new Answers();
                            ans.PKID = TheAnswer.First().PKID;
                            ans.Answer1 = TheAnswer.First().Answer1;
                            ans.correct = new Correct();
                            ans.correct.isCorrect = AnswersID.ElementAt(k).IsCorrect;
                            quest.Answers.Add(ans);
                        }
                        ExamQ.quest = quest;
                        exam.ExamQuestions.Add(ExamQ);
                       
                    }

                  

                }
                return exam;
            }
            
                     
        }
        public List<Subject> GetAllSubject()
        {
            List<Subject> result = new List<Subject>();
            var subjects = from tempSubjects in db.Subject
                    select tempSubjects;
            for (int i = 0; i < subjects.ToList().Count; i++)
            {
                Subject newSubject = new Subject();
                newSubject = Mapper.Map<Subject>(subjects.ToArray()[i]);
               /* newSubject.Subject_ID = subjects.ToList().ToArray()[i].Subject_ID;
                newSubject.Subject_Name = subjects.ToList().ToArray()[i].Subject_Name;*/
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
                        Tempcategory = Mapper.Map<Category>(categories.ToArray()[b]);
                      /*  var categoryID = from c in db.Categories_Subtopic
                                         where c.Categories_ID == categories.ToList().ToArray()[b].Categories_ID
                                         select c.Categories_ID;
                        
                        Tempcategory.Categories_ID = categories.ToList().ToArray()[b].Categories_ID;
                        var CategoryName = from tempName in db.Categories
                                           where tempName.Categories_ID == Tempcategory.Categories_ID
                                           select tempName;
                        Tempcategory.Categories_Name = CategoryName.ToArray().ElementAt(0).Categories_Name;*/
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
                                var Subtopics = from SubtopicCount in db.Subtopic
                                                   where SubtopicCount.Subtopic_ID == SubtopicIDs.ToArray()[c]
                                                   select SubtopicCount;
                                
                                for (int d = 0; d < Subtopics.ToList().Count; d++)
                                {
                                   newSub = new SubTopic();
                                   newSub = Mapper.Map<SubTopic>(Subtopics.ToArray()[d]);
                                  /*  newSub.Subtopic_Name = Subtopics.ToList().ToArray()[d].Subtopic_Name;
                                    newSub.Subtopic_ID = Subtopics.ToList().ToArray()[d].Subtopic_ID;*/
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
            var x = db.Subtopic.Select(j => Mapper.Map<SubTopic>(j));
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
