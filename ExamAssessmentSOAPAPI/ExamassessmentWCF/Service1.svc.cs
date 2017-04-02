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
        public List<ExamQuestion> GetAllExamQuestion()
        {
            AutoMapperConfiguration.Configure();
            List<ExamQuestion> result = new List<ExamQuestion>();
            var dbExamQuestion = db.ExamQuestion;
            var dbCategories = db.Categories;
            var dbSubtopic = db.Subtopic;
            var dbCatSub = db.Categories_Subtopic;
          //  var dbQuestionID = db.ExamQuestionList.Select(s => s.QuestionID);
            for (int i = 0; i < dbExamQuestion.Count(); i++)
            {
                ExamQuestion question = new ExamQuestion();
                question.ExamQuestionID = dbExamQuestion.ElementAt(i).ExamQuestionID;
                question.ExamQuestionName = dbExamQuestion.ElementAt(i).ExamQuestionName;
                question.PKID = db.ExamQuestion.ElementAt(i).PKID;
                for(int j = 0; j < dbExamQuestion.ElementAt(i).ExamQuestion_Categories.Count();j++)
                {         
                    Category cat =  Mapper.Map<Category> (dbCategories.Where(s => s.Categories_ID == dbExamQuestion.ElementAt(i).ExamQuestion_Categories.ElementAt(j).Categories_ID));
                    List<int> listofSub = dbCatSub.Where(s => s.Categories_ID == cat.Categories_ID).Select(s => s.Subtopic_ID).ToList();

                    question.ExamQuestion_Categories.Add(cat);
                }
                         
            }
     
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
            if (Question.Count() > 0)
            {
                for (int k = 0; k < Question.ToList().Count; k++)
                {
                    var second = from x in db.Answer
                                 where x.PKID == Question.ToArray()[k]
                                 select x;
                    Answers ans = new Answers();
                    ans.PKID = second.ElementAt(0).PKID;
                    ans.Answer1 = second.ElementAt(0).Answer1;
                    i.Add(ans);
                    
                }
            }
            return i;

        }
       public ExamTemplate getExamTemplate(String id)
        {

            #region 
            /* var ExamTemplate = from TempExamTemplate in db.ExamTemplate
                                where TempExamTemplate.ExamTemplateID == id
                                select TempExamTemplate;*/
            #endregion
            List<EAD.ExamTemplate> ExamTemplate = db.ExamTemplate.Where(s => s.ExamTemplateID == id).ToList();
            ExamTemplate exam = new ExamTemplate();
            exam.PKID = ExamTemplate.First().PKID;
            exam.CreatedDate = ExamTemplate.First().CreatedDate;
            exam.ExamTemplateName = ExamTemplate.First().ExamTemplateName;
            exam.ExamTemplateID = ExamTemplate.FirstOrDefault().ExamTemplateID;
            exam.ExamType.PKID = ExamTemplate.FirstOrDefault().ExamType.PKID;
            exam.ExamType.ExamTypeName = ExamTemplate.FirstOrDefault().ExamType.ExamTypeName;
            #region
            /*var ExamTypes = from TempExamType in db.ExamType
                           where TempExamType.PKID == ExamTemplate.First().ExamTypeID
                           select TempExamType;*/
            #endregion
       
            #region
            /* var ExamQuestions = from TempExamQuestions in db.ExamTemplateQuestions
                                where TempExamQuestions.ExamTemplateID == exam.ExamTemplateID
                                select TempExamQuestions.ExamQuestionID;*/
            #endregion
            List<EAD.ExamTemplateQuestions> ExamQuestions = db.ExamTemplateQuestions.Where(s => s.ExamTemplateID == exam.ExamTemplateID).ToList();
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
                    #region
                    /* var ExamQuestion = (from TempQuest in dbExamQuestion
                                         where TempQuest.ExamQuestionID == ExamQuestions.ToList().ElementAt(i)
                                         select TempQuest);*/
                    #endregion
                    List<EAD.ExamQuestion>ExamQuestion = dbExamQuestion.Where(s => s.ExamQuestionID == ExamQuestions.ElementAt(i).ExamQuestionID).ToList();


                    ExamQuestion ExamQ = new ExamQuestion();
                    #region
                    //   String tempe = ExamQuestion.ToArray()[0].ExamQuestionID;
                    //   var jjd = ExamQuestion;
                    //  EAD.ExamQuestion t = ExamQuestion;
                    #endregion
                    ExamQ.ExamQuestionID = ExamQuestion.ElementAt(0).ExamQuestionID;
                    ExamQ.ExamQuestionName = ExamQuestion.ElementAt(0).ExamQuestionName;
                    ExamQ.PKID = ExamQuestion.ElementAt(0).PKID;
                    ExamQ.QuestionType.PKID = ExamQuestion.ElementAt(0).QuestionType.PKID;
                    ExamQ.QuestionType.QuestionTypeName = ExamQuestion.ElementAt(0).QuestionType.QuestionTypeName;
                    #region
                    /*var ExamQuestionList = from TempQuestion in dbExamQuestionList
                                   where TempQuestion.ExamQuestionID == ExamQ.ExamQuestionID
                                   select TempQuestion;*/
                    #endregion
                    var ExamQuestionList = dbExamQuestionList.Where(s => s.ExamQuestionID == ExamQ.ExamQuestionID).ToList();
                    for(int j= 0; j < ExamQuestionList.Count(); j++)
                    {
                        int tempID = ExamQuestionList.ElementAt(j).QuestionID;
                        #region
                        /*var Question = from TempQuestion in dbquestion
                                       where TempQuestion.PKID == tempID
                                       select TempQuestion;*/
                        #endregion
                        List<EAD.Question> Question = dbquestion.Where(s => s.PKID == tempID).ToList();
                        Question quest = new Question();
                        quest.PKID = Question.ElementAt(0).PKID;
                        quest.Description = Question.ElementAt(0).Description;
                        #region
                        /* var AnswersID = from QuestionAnswers in dbQuestionAnswers
                                         where QuestionAnswers.QuestionID == quest.PKID
                                         select QuestionAnswers;*/
                        #endregion
                        List<EAD.QuestionAnswers> AnswersID = db.QuestionAnswers.Where(s => s.QuestionID == quest.PKID).ToList();
                        for(int k = 0;  k < AnswersID.Count(); k++)
                        {
                            int answer = AnswersID.ElementAt(k).AnswerID;
                            var TheAnswer = from tempAnswer in dbanswer
                                            where tempAnswer.PKID == answer
                                            select tempAnswer;
                            Answers ans = new Answers();
                            ans.PKID = TheAnswer.ToArray()[0].PKID;
                            ans.Answer1 = TheAnswer.FirstOrDefault().Answer1;
                         //   ans.correct = new Correct();
                            bool isCorrect = AnswersID.FirstOrDefault().IsCorrect;
                           // ans.correct.isCorrect = isCorrect;
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
            #region
            /* var subjects = from tempSubjects in db.Subject
                     select tempSubjects; */
            #endregion
            List<EAD.Subject> subjects = db.Subject.ToList();
            
            for (int i = 0; i < subjects.ToList().Count; i++)
            {
                Subject newSubject = new Subject();
                newSubject = Mapper.Map<Subject>(subjects.ElementAt(i));
                #region
                /* newSubject.Subject_ID = subjects.ToList().ToArray()[i].Subject_ID;
                 newSubject.Subject_Name = subjects.ToList().ToArray()[i].Subject_Name;*/
                /*var categories = (from c in db.Subject_Categories
                         where c.Subject_ID == newSubject.Subject_ID
                         select c).ToList();*/
                #endregion

                List<EAD.Subject_Categories> categories = db.Subject_Categories.Where(c => c.Subject_ID == newSubject.Subject_ID).ToList();  
                if(categories.Count < 1)
                {
                    result.Add(newSubject);
                }
                else
                {
                    for (int b = 0; b < categories.Count; b++)
                    {
                        List<EAD.Categories> categoriesL = db.Categories.Where(c => c.Categories_ID == categories.ElementAt(b).Categories_ID).ToList();
                        Category Tempcategory = new Category();
                        #region
                        /* var Category = (from tempCat in db.Categories
                                        where tempCat.Categories_ID == categories.ElementAt(b).Categories_ID
                                        select tempCat).ToList();*/
                        #endregion
                        Tempcategory = new Category();


                        #region
                        //  Tempcategory = Mapper.Map<Category>(categories.ToArray()[b]);
                        /* var category = from c in db.Categories_Subtopic
                                          where c.Categories_ID == categories.ElementAt(b).Categories_ID
                                          select c;*/
                        #endregion

                        Tempcategory.Categories_ID = categoriesL.ElementAt(0).Categories_ID;                     
                        Tempcategory.Categories_Name = categoriesL.ElementAt(0).Categories_Name;

                        var SubtopicIDs = from TempID in db.Categories_Subtopic
                                          where TempID.Categories_ID == Tempcategory.Categories_ID
                                          select TempID.Subtopic_ID;
                        if(SubtopicIDs.Count() < 1)
                        {
                            newSubject.listCat.Add(Tempcategory);
                            result.Add(newSubject);
                        }
                        else
                        {
                            for (int c = 0; c < SubtopicIDs.ToList().Count; c++)
                            {
                                SubTopic newSub = new SubTopic();
                                /*var Subtopics = from TempSubtopic in db.Subtopic.ToList()
                                                   where TempSubtopic.Subtopic_ID == SubtopicIDs.ToList().ElementAt(c)
                                                   select TempSubtopic;*/

                                List<EAD.Subtopic> Subtopics = db.Subtopic.Where(s => s.Subtopic_ID == SubtopicIDs.ToList().ElementAt(c)).ToList();
                                newSub = new SubTopic();
                                newSub = Mapper.Map<SubTopic>(Subtopics.ElementAt(0));
                                #region
                                // newSub.Subtopic_Name = Subtopics.ElementAt(0).Subtopic_Name;
                                //newSub.Subtopic_ID = Subtopics.ToList().ElementAt(0).Subtopic_ID;
                                #endregion
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

        #region Devonte's Edits
        public List<Subject> GetExamSubject(string id)
        {
            //Database needs a view that has the exam template and subject connected

         /*   var s = db.FullExamTemplateInfo;
            var slist = from TempSubject in s.ToList()
                        where TempSubject.ExamTemplateID.Equals(id)
                        select TempSubject;
            slist = slist.ToList();
            AutoMapperConfiguration.Configure();
            List <Subject> result = new List<Subject>();
            foreach (var item in slist)
            {
                Mapper.Map<Subject>();
                result.Add(item);
            }
            */
            return null;
        }
        public List<Answers> GetQuestionAnswers(int sqid)
        {
            return null;
        }
        #endregion
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
