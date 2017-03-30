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
using System.Data.Entity.Core.Objects;

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
        
        public void spAddExistingCategory(String subject, String category)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spAddExistingCategory(subject, category, myOutputParamInt);
          //  return int.Parse(myOutputParamInt.Value.ToString());
        }
        public void spAddExistingSubtopicToCategory(String subtopic, String category)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spAddExistingSubtopicToCategory(subtopic, category, myOutputParamInt);
          //  return int.Parse(myOutputParamInt.Value.ToString());
        }
        public void spAddNewCategoryType(String subject, String category)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spAddExistingSubtopicToCategory(subject, category, myOutputParamInt);
          //   return int.Parse(myOutputParamInt.Value.ToString());
        }
        public void spAddQuestionAsExamQuestion(String ExamQuestionID, int QuestionID, String name, int QuestionType)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));
          //  myOutputParamInt.Value = 15;
            db.spAddQuestionAsExamQuestion(ExamQuestionID, QuestionID,name,QuestionType, myOutputParamInt);
          //  return 1;//int.Parse(myOutputParamInt.Value.ToString());
        }
        public void spAddQuestionCategories(String Categories, int PKID)
        {
            int result = 0;
            db.spAddQuestionCategories(Categories, PKID, result);
           // return result;
        }
   
        public void spAddQuestionToAnswer(int QuestionID, int AnswerID, bool isCorrect)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spAddQuestionToAnswer(QuestionID, AnswerID, isCorrect, myOutputParamInt);
        //    return int.Parse(myOutputParamInt.Value.ToString());
        }
        public void spAddQuestionToExam(String ExamID, String ExamQuestionID, int weight)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spAddQuestionToExam(ExamID, ExamQuestionID, weight, myOutputParamInt);
          //  return int.Parse(myOutputParamInt.Value.ToString());
        }
        public void spAddSubtopicType(string Subtopics, string Category)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spAddSubtopicType(Subtopics, Category, myOutputParamInt);
            //return int.Parse(myOutputParamInt.Value.ToString());
        }
        public void spDeleteQuestionCategory(String Categories, String ExamID)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spDeleteQuestionCategory(Categories,ExamID,myOutputParamInt);
           // return int.Parse(myOutputParamInt.Value.ToString());
        }
        public  void spRemoveAnswerFromQuestion(int QuestionID, int AnswerID)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spRemoveAnswerFromQuestion(QuestionID, AnswerID, myOutputParamInt);
          //  return int.Parse(myOutputParamInt.Value.ToString());
        }
        public void spRemoveCategory(String categoryName)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spRemoveCategory(categoryName, myOutputParamInt);
            //return int.Parse(myOutputParamInt.Value.ToString());
        }
        public void spRemoveQuestionAsExamQuestion(String ExamQuestionID)
        {
            int result = 0;
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spRemoveQuestionAsExamQuestion(ExamQuestionID, result);
          //  return result;
        }
        public void spRemoveQuestionFromExam(String ExamID, String ExamQuestionID)
        {
            
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spRemoveQuestionFromExam(ExamID,ExamQuestionID, myOutputParamInt);
            //return int.Parse(myOutputParamInt.Value.ToString());
        }
        public List<Question> GetAllQuestions()
        {
            AutoMapperConfiguration.Configure();
            List<Question> result = new List<Question>();
            var first = from c in db.Question                    
                        select c;
            var dbQuestionAnswers = db.QuestionAnswers;
            for(int i =0; i < first.ToList().Count; i++)
            {
                Question quest = new Question();
                quest.PKID = first.ToList().ElementAt(i).PKID;
                quest.Description = first.ToList().ElementAt(i).Description;
                var second = from x in dbQuestionAnswers
                             where x.QuestionID == quest.PKID
                             select x;
                quest.Answers.ToList().AddRange((GetAnswersQuestion(quest.PKID).ToList()));
                result.Add(quest);
            }
            return result;
        }
        public List<Answers> GetAnswersQuestion(int Questid)
        {
            AutoMapperConfiguration.Configure();
            var Question = from c in db.QuestionAnswers
                        where c.QuestionID == Questid
                        select c.AnswerID;
            List<Answers> i = new List<Answers>();
            for (int k = 0; k < Question.ToList().Count; k++)
            {
                var second = from x in db.Answer
                             where x.PKID == Question.ToArray()[k]
                             select x;
                Answers ans = new Answers();
                ans = Mapper.Map<Answers>(second);
        
                i.Add(ans);

            }
            return i;

        }
       public ExamTemplate getExamTemplate(String id)
        {
           var result = from TempExamTemplate in db.FullExamTemplateInfo
                         where TempExamTemplate.ExamTemplateID == id
                         select TempExamTemplate;

            
            var ExamTemplate = from TempExamTemplate in db.ExamTemplate
                               where TempExamTemplate.ExamTemplateID == id
                               select TempExamTemplate;
            ExamTemplate exam = new ExamTemplate();
            exam.PKID = ExamTemplate.First().PKID;
            exam.CreatedDate = ExamTemplate.First().CreatedDate;
            exam.ExamTemplateName = ExamTemplate.First().ExamTemplateName;
            exam.ExamTemplateID = ExamTemplate.FirstOrDefault().ExamTemplateID;
            var ExamTypes = from TempExamType in db.ExamType
                           where TempExamType.PKID == ExamTemplate.First().ExamTypeID
                           select TempExamType;
            Examtype type = new Examtype();
            //type.PKID = ExamTypes.FirstOrDefault().PKID;
            //type.ExamTypeName = ExamTypes.FirstOrDefault().ExamTypeName;
            //exam.ExamType = type;
            var ExamQuestions = from TempExamQuestions in db.ExamTemplateQuestions
                               where TempExamQuestions.ExamTemplateID == exam.ExamTemplateID
                               select TempExamQuestions.ExamQuestionID;
            if(ExamQuestions.Count() < 1)
            {
                return exam;
            }
            else
            {
                var dbExamQuestion = db.ExamQuestion.ToList();
                var dbExamQuestionList = db.ExamQuestionList.ToList();
                var dbQuestionAnswers = db.QuestionAnswers.ToList();
                var dbquestion = db.Question.ToList();
                var dbanswer = db.Answer.ToList();
                for (int i = 0; i < ExamQuestions.Count(); i++)
                {
                    var ExamQuestion = (from TempQuest in dbExamQuestion
                                        where TempQuest.ExamQuestionID == ExamQuestions.ToList().ElementAt(i)
                                        select TempQuest);


                    ExamQuestion ExamQ = new ExamQuestion();
                    //   String tempe = ExamQuestion.ToArray()[0].ExamQuestionID;
                    var jjd = ExamQuestion;
                  //  EAD.ExamQuestion t = ExamQuestion;
                    ExamQ.ExamQuestionID = ExamQuestion.FirstOrDefault().ExamQuestionID;
                    ExamQ.ExamQuestionName = ExamQuestion.FirstOrDefault().ExamQuestionName;
                    ExamQ.PKID = ExamQuestion.FirstOrDefault().PKID;
                    ExamQ.QuestionType.PKID = ExamQuestion.FirstOrDefault().QuestionType.PKID;
                    ExamQ.QuestionType.QuestionTypeName = ExamQuestion.FirstOrDefault().QuestionType.QuestionTypeName;
                    
                    var ExamQuestionList = from TempQuestion in dbExamQuestionList
                                   where TempQuestion.ExamQuestionID == ExamQ.ExamQuestionID
                                   select TempQuestion;
                    for(int j= 0; j < ExamQuestionList.ToList().Count(); j++)
                    {
                        int tempID = ExamQuestionList.ToList().ElementAt(j).QuestionID;
                        var Question = from TempQuestion in dbquestion
                                       where TempQuestion.PKID == tempID
                                       select TempQuestion;
                        Question quest = new Question();
                        quest.PKID = Question.FirstOrDefault().PKID;
                        quest.Description = Question.FirstOrDefault().Description;
                        var AnswersID = from QuestionAnswers in dbQuestionAnswers
                                        where QuestionAnswers.QuestionID == quest.PKID
                                        select QuestionAnswers;
                        for(int k = 0;  k < AnswersID.ToList().Count(); k++)
                        {
                            int answer = AnswersID.ToList().ToArray()[k].AnswerID;
                            var TheAnswer = from tempAnswer in dbanswer
                                            where tempAnswer.PKID == answer
                                            select tempAnswer;
                            Answers ans = new Answers();
                            ans.PKID = TheAnswer.ToArray()[0].PKID;
                            ans.Answer1 = TheAnswer.FirstOrDefault().Answer1;
                            ans.correct = new Correct();
                            bool isCorrect = AnswersID.FirstOrDefault().IsCorrect;
                            ans.correct.isCorrect = isCorrect;
                            quest.Answers.Add(ans);
                        }
                        ExamQ.quest.Add(quest);
                        exam.ExamQuestions.Add(ExamQ);
                       
                    }

                  

                }
                return exam;
            }
            
                     
        }
        public List<Subject> GetAllSubject()
        {
            AutoMapperConfiguration.Configure();
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
                      //  Tempcategory = Mapper.Map<Category>(categories.ToArray()[b]);
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
                                var Subtopics = from TempSubtopic in db.Subtopic.ToList()
                                                   where TempSubtopic.Subtopic_ID == SubtopicIDs.ToList().ElementAt(c)
                                                   select TempSubtopic;
                                
                                
                                   newSub = new SubTopic();
                                // newSub = Mapper.Map<SubTopic>(Subtopics.ElementAt(0));
                                newSub.Subtopic_Name = "jaja";//Subtopics.FirstOrDefault().Subtopic_Name;
                                newSub.Subtopic_ID = 111;//Subtopics.ToList().FirstOrDefault().Subtopic_ID;
                                    Tempcategory.subtopics.Add(newSub);
                                    
                                


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
