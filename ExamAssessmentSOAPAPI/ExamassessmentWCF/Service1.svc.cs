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
            var dbExamQuestion = db.ExamQuestion.ToList();
            var dbCategories = db.Categories.ToList();
            var dbSubtopic = db.Subtopic.ToList();
            var dbCatSub = db.Categories_Subtopic.ToList();
            var dbQuestExam = db.ExamQuestionList.ToList();
            var dbQuestion = db.Question.ToList();
          //  var dbQuestionID = db.ExamQuestionList.Select(s => s.QuestionID);
            for (int i = 0; i < dbExamQuestion.Count(); i++)
            {
                ExamQuestion question = new ExamQuestion();
                question.ExamQuestionID = dbExamQuestion.ElementAt(i).ExamQuestionID;
                question.ExamQuestionName = dbExamQuestion.ElementAt(i).ExamQuestionName;
                question.PKID = dbExamQuestion.ElementAt(i).PKID;
                for(int j = 0; j < dbExamQuestion.ElementAt(i).ExamQuestion_Categories.Count();j++)
                {
                    Category cat = new Category();
                   

                    cat.Categories_ID = dbCategories.Where(s => s.Categories_ID == dbExamQuestion.ElementAt(i).ExamQuestion_Categories.ElementAt(j).Categories_ID).First().Categories_ID;
                    cat.Categories_Name = dbCategories.Where(s => s.Categories_ID == dbExamQuestion.ElementAt(i).ExamQuestion_Categories.ElementAt(j).Categories_ID).First().Categories_Name;
                    // Category cat =  Mapper.Map<Category> (dbCategories.Where(s => s.Categories_ID == dbExamQuestion.ElementAt(i).ExamQuestion_Categories.ElementAt(j).Categories_ID));
                    List<int> listofSub = dbCatSub.Where(s => s.Categories_ID == cat.Categories_ID).Select(s => s.Subtopic_ID).ToList();
                    for(int k= 0; k < listofSub.Count(); k++)
                    {
                        var subtopic = dbSubtopic.Where(s => s.Subtopic_ID == listofSub.ElementAt(k));
                        SubTopic sub = new SubTopic();
                        sub.Subtopic_ID = subtopic.ElementAt(0).Subtopic_ID;
                        sub.Subtopic_Name = subtopic.ElementAt(0).Subtopic_Name;
                        cat.subtopics.Add(sub);
                    }
                    question.ExamQuestion_Categories.Add(cat);
                }

                List<int> QuestionIDs = dbQuestExam.Where(s => s.ExamQuestionID == question.ExamQuestionID).Select(c => c.QuestionID).ToList();
                EAD.Question tempQuestion;
                for (int j = 0; j < QuestionIDs.Count; j++)
                {
                    tempQuestion = dbQuestion.Where(s => s.PKID == QuestionIDs.ElementAt(j)).First();
                    Question newQuest = new Question();
                    newQuest.PKID = tempQuestion.PKID;
                    newQuest.Description = tempQuestion.Description;
                    newQuest.Answers = GetAnswersQuestion(newQuest.PKID);
                    question.quest.Add(newQuest);
                    
                }
                result.Add(question);
            }
            return result;
     
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
                quest.Answers = GetAnswersQuestion(quest.PKID);
                result.Add(quest);
               
            }
            return result;
        }
        public List<Answers> GetAnswersQuestion(int Questid)
        {
            
            AutoMapperConfiguration.Configure();
            List<int> AnswerID = db.QuestionAnswers.Where(c => c.QuestionID == Questid).Select(x => x.AnswerID).ToList();
            List < EAD.Answer >AnswerDB = db.Answer.ToList();
            List < EAD.QuestionAnswers > dbQuestionAns = db.QuestionAnswers.ToList();            
            List<Answers> ListOfAnswers = new List<Answers>();
            if (AnswerID.Count() > 0)
            {
                for (int k = 0; k < AnswerID.ToList().Count; k++)
                {
                    EAD.Answer ans =( from tempanswer in AnswerDB
                                 where tempanswer.PKID == AnswerID.ElementAt(k)
                                 select tempanswer).First();

                    Answers answer = Mapper.Map<Answers>(ans);
                    if(dbQuestionAns.Where(s => s.QuestionID == Questid && s.AnswerID == answer.PKID).Select(s => s.IsCorrect).First() == true)
                    {
                        answer.correct.isCorrect = true;
                    }
                    else
                    {
                        answer.correct.isCorrect = false;
                    }

                   // ans.Answer1 = second.First().Answer1;
                    ListOfAnswers.Add(answer);
                    
                }
            }
            return ListOfAnswers;

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
        /* public List<Subject> GetExamSubject(string id)
        {
            Database needs a view that has the exam template and subject connected

           var s = db.FullExamTemplateInfo;
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
           
            return null;
        }
        */
        public List<Answers> GetQuestionAnswers(int sqid)
        {
            return null;
        }

        public void AddAnswer(int QuestionID, string Answer, bool IC)
        {
            EAD.Answer ans = new EAD.Answer();
            ans.Answer1 = Answer;
            try
            {
                db.Answer.Add(ans);
                db.SaveChanges();
                var tempPKID = db.Answer.OrderByDescending(item => item.PKID).First();
                int NewAnswerID = tempPKID.PKID;
                spAddQuestionToAnswer(QuestionID, NewAnswerID, IC);
            }
            catch (Exception ex)
            {
            }
        }

        public void DeleteAnswer(string Answerdesc)
        {
            int answerID = 0;
            EAD.Answer removedAnswer = new EAD.Answer();
            foreach (var item in db.Answer) //Gets the Answer which will be needed so it can be removed
            {
                if (item.Answer1 == Answerdesc)
                {
                    answerID = item.PKID;
                    removedAnswer = item; //keeps a reference to the subtopic that will be removed
                }
            }
            foreach (var item in db.QuestionAnswers) //Removes all references to the Answer in the database
            {
                if (item.AnswerID == answerID)
                {
                    db.QuestionAnswers.Remove(item);
                }
            }
            db.Answer.Remove(removedAnswer); // removes the Answer from the Answer table.
            db.SaveChanges();
        }

        public void DeleteExam(string ExamTID)
        {
            
            EAD.ExamTemplate removedExam = new EAD.ExamTemplate();
            foreach (var item in db.ExamTemplate) //Gets the Exam which will be needed so it can be removed
            {
                if (item.ExamTemplateID.Equals(ExamTID))
                {
                    
                    removedExam = item; //keeps a reference to the Exam that will be removed
                }
            }
            foreach (var item in db.ExamTemplateQuestions) //Removes all references to the Exam in the database
            {
                if (item.ExamTemplateID == ExamTID)
                {
                    db.ExamTemplateQuestions.Remove(item);
                }
            }
            db.ExamTemplate.Remove(removedExam); // removes the ExamTemplate from the ExamTemplate table.
            db.SaveChanges();
        }
        
        
        public void AddNewExam(string exName, string exTID, string ExamType)
        {
            EAD.ExamTemplate newExt = new EAD.ExamTemplate();
            newExt.ExamTemplateName = exName;
            newExt.ExamTemplateID = exTID;
            newExt.ExamType.ExamTypeName = ExamType;
            try
            {
                d
            }
        }

        #endregion


        public List<SubTopic> GetSubtopicList()
        {
            var x = db.Subtopic.Select(j => Mapper.Map<SubTopic>(j));
            return x.ToList();
        }

        public void DeleteSubtopic(string SubtopicName)
        {
            int subtopicID = 0;
           EAD.Subtopic removedTopic = new EAD.Subtopic();
            foreach(var item in db.Subtopic) //Gets the subtopicID which will be needed so it can be removed
            {
                if (item.Subtopic_Name==SubtopicName)
                {
                    subtopicID = item.Subtopic_ID;
                    removedTopic = item; //keeps a reference to the subtopic that will be removed
                }
            }

            foreach(var item in db.Categories_Subtopic) //Removes all references to the subtopic in the database
            {
                if (item.Subtopic_ID==subtopicID)
                {
                    db.Categories_Subtopic.Remove(item);
                }
            }

            db.Subtopic.Remove(removedTopic); // removes the subtopic from the subtopic table.
            db.SaveChanges();
        }

        public void RemoveSubtopicFromCategory(string SubtopicName, string CategoryName)
        {
            int subtopicID = 0;
            int categoryID = 0;

            EAD.Subtopic removedTopic = new EAD.Subtopic();
            foreach (var item in db.Subtopic) //Gets the subtopicID which will be needed so it can be removed
            {
                if (item.Subtopic_Name == SubtopicName)
                {
                    subtopicID = item.Subtopic_ID;
                }
            }

            foreach (var item in db.Categories) //Gets the categoryID which will be needed so it can be removed
            {
                if (item.Categories_Name == CategoryName)
                {
                    categoryID = item.Categories_ID;
                }
            }
            foreach (var item in db.Categories_Subtopic) //Finds the row on the junction table that contains the pair of values and removes it
            {
                if (item.Subtopic_ID == subtopicID && item.Categories_ID == categoryID)
                {
                    db.Categories_Subtopic.Remove(item);
                }
            }
            db.SaveChanges();
        }

        public void DeleteCategory(string CategoryName)
        {
            int categoryID = 0;
            EAD.Categories removedCategory = new EAD.Categories();
            foreach (var item in db.Categories) //Gets the categoryID which will be needed so it can be removed
            {
                if (item.Categories_Name == CategoryName)
                {
                    categoryID = item.Categories_ID;
                    removedCategory = item; //keeps a reference to the category that will be removed
                }
            }

            foreach (var item in db.Subject_Categories) //Removes all references to the category from subjects
            {
                if (item.Categories_ID == categoryID)
                {
                    db.Subject_Categories.Remove(item);
                }
            }
            foreach (var item in db.ExamQuestion_Categories) //Removes all references to the category from subjects
            {
                if (item.Categories_ID == categoryID)
                {
                    db.ExamQuestion_Categories.Remove(item);
                }
            }
            db.Categories.Remove(removedCategory); // removes the category from the subtopic table.
            db.SaveChanges();
        }

        public void AddSubject(string SubjectName)
        {
            EAD.Subject addedSubject = new EAD.Subject(); //Object to be passed into Subject Table
            addedSubject.Subject_Name = SubjectName; //Only Needs Name property
            db.Subject.Add(addedSubject); //adds object to database
            db.SaveChanges();
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
