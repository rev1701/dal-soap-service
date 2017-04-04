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
using NLog;
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
            
        }
        public void spAddExistingSubtopicToCategory(String subtopic, String category)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spAddExistingSubtopicToCategory(subtopic, category, myOutputParamInt);
            
        }
        public void spAddNewCategoryType(String subject, String category)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spAddExistingSubtopicToCategory(subject, category, myOutputParamInt);
          
        }
        public void spAddQuestionAsExamQuestion(String ExamQuestionID, int QuestionID, String name, int QuestionType)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));
        
            db.spAddQuestionAsExamQuestion(ExamQuestionID, QuestionID,name,QuestionType, myOutputParamInt);
    
        }
        public void spAddQuestionCategories(String Categories, int PKID)
        {
            int result = 0;
            db.spAddQuestionCategories(Categories, PKID, result);

        }
   
        public void spAddQuestionToAnswer(int QuestionID, int AnswerID, bool isCorrect)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spAddQuestionToAnswer(QuestionID, AnswerID, isCorrect, myOutputParamInt);

        }
        public void spAddQuestionToExam(String ExamID, String ExamQuestionID, int weight)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spAddQuestionToExam(ExamID, ExamQuestionID, weight, myOutputParamInt);

        }
        public void spAddSubtopicType(string Subtopics, string Category)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spAddSubtopicType(Subtopics, Category, myOutputParamInt);

        }
        public void spDeleteQuestionCategory(String Categories, String ExamID)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spDeleteQuestionCategory(Categories,ExamID,myOutputParamInt);

        }
        public  void spRemoveAnswerFromQuestion(int QuestionID, int AnswerID)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spRemoveAnswerFromQuestion(QuestionID, AnswerID, myOutputParamInt);
 
        }
   
        public void spRemoveQuestionAsExamQuestion(String ExamQuestionID)
        {
            int result = 0;  
          
            db.spRemoveQuestionAsExamQuestion(ExamQuestionID, result);

        }
        public void spRemoveQuestionFromExam(String ExamID, String ExamQuestionID)
        {     
            ObjectParameter myOutputParamInt = new ObjectParameter("myOutputParamInt", typeof(int));
            db.spRemoveQuestionFromExam(ExamID,ExamQuestionID, myOutputParamInt);

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
            
            
            


            for (int i = 0; i < dbExamQuestion.Count; i++)
            {
                ExamQuestion question     = new ExamQuestion();
                question.ExamQuestionID   = dbExamQuestion.ElementAt(i).ExamQuestionID;
                question.ExamQuestionName = dbExamQuestion.ElementAt(i).ExamQuestionName;
                question.PKID             = dbExamQuestion.ElementAt(i).PKID;

                for(int j = 0; j < dbExamQuestion.ElementAt(i).ExamQuestion_Categories.Count;j++)
                {
                    Category cat = new Category();
                   

                    cat.Categories_ID = dbCategories.First(s => s.Categories_ID == dbExamQuestion.ElementAt(i).ExamQuestion_Categories.ElementAt(j).Categories_ID).Categories_ID;
                    cat.Categories_Name = dbCategories.Where(s => s.Categories_ID == dbExamQuestion.ElementAt(i).ExamQuestion_Categories.ElementAt(j).Categories_ID).First().Categories_Name;
                    
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
            var first = (from c in db.Question                    
                        select c).ToList();
            var dbQuestionAnswers = db.QuestionAnswers.ToList();
            for(int i = 0; i < first.ToList().Count; i++)
            {
                Question quest = new Question();
                quest.PKID = first.ElementAt(i).PKID;
                quest.Description = first.ElementAt(i).Description;
              
                quest.Answers = GetAnswersQuestion(quest.PKID);
                result.Add(quest);
               
            }
            return result;
        }
        public List<Answers> GetAnswersQuestion(int Questid)
        {
            if (db == null)
            {
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "The Database is null"));
            }
            else if (db.Database == null)
            {
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "The Database is null"));
            }
            else
            {
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "Nothing is Null"));
            }

            NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "GetAnswersQuestion Started"));
            try
            {

                AutoMapperConfiguration.Configure();
                List<int> AnswerID = db.QuestionAnswers.Where(c => c.QuestionID == Questid).Select(x => x.AnswerID).ToList();
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "Got AnswerID's"));
                List<EAD.Answer> AnswerDB = db.Answer.ToList();
                List<EAD.QuestionAnswers> dbQuestionAns = db.QuestionAnswers.ToList();
                List<Answers> ListOfAnswers = new List<Answers>();
                if (AnswerID.Count > 0)
                {
                    for (int k = 0; k < AnswerID.Count; k++)
                    {
                        EAD.Answer ans = (from tempanswer in AnswerDB
                                          where tempanswer.PKID == AnswerID.ElementAt(k)
                                          select tempanswer).First();
                        NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", $"Got Answers for Specified Question {Questid}"));
                        Answers answer = Mapper.Map<Answers>(ans);
                        if (dbQuestionAns.Where(s => s.QuestionID == Questid && s.AnswerID == answer.PKID).Select(s => s.IsCorrect).First() == true)
                        {
                            NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", $"Set correct answer for question with id {Questid}"));
                            answer.correct.isCorrect = true;
                        }
                        else
                        {
                            NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", $"Assigned answer {answer.PKID} to false for question {Questid}"));
                            answer.correct.isCorrect = false;
                        }


                        ListOfAnswers.Add(answer);

                    }
                }
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", $"returned a list of answes for question {Questid}"));
                return ListOfAnswers;
            }
            catch (Exception e)
            {
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", e.StackTrace));
                return null;
            }

        }


        public ExamTemplate getExamTemplate(String id)
        {

            
            List<EAD.ExamTemplate> ExamTemplate = db.ExamTemplate.Where(s => s.ExamTemplateID == id).ToList();
            ExamTemplate exam = new ExamTemplate();
            exam.PKID = ExamTemplate.First().PKID;
            exam.CreatedDate = ExamTemplate.First().CreatedDate;
            exam.ExamTemplateName = ExamTemplate.First().ExamTemplateName;
            exam.ExamTemplateID = ExamTemplate.FirstOrDefault().ExamTemplateID;
            exam.ExamType.PKID = ExamTemplate.FirstOrDefault().ExamType.PKID;
            exam.ExamType.ExamTypeName = ExamTemplate.FirstOrDefault().ExamType.ExamTypeName;
           
       
            
            List<EAD.ExamTemplateQuestions> ExamQuestions = db.ExamTemplateQuestions.Where(s => s.ExamTemplateID == exam.ExamTemplateID).ToList();
            if(ExamQuestions.Count < 1)
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
                    
                    List<EAD.ExamQuestion> ExamQuestion = dbExamQuestion.Where(s => s.ExamQuestionID == ExamQuestions.ElementAt(i).ExamQuestionID).ToList();


                    ExamQuestion ExamQ = new ExamQuestion();
                    
                    ExamQ.ExamQuestionID = ExamQuestion.ElementAt(0).ExamQuestionID;
                    ExamQ.ExamQuestionName = ExamQuestion.ElementAt(0).ExamQuestionName;
                    ExamQ.PKID = ExamQuestion.ElementAt(0).PKID;
                    ExamQ.QuestionType.PKID = ExamQuestion.ElementAt(0).QuestionType.PKID;
                    ExamQ.QuestionType.QuestionTypeName = ExamQuestion.ElementAt(0).QuestionType.QuestionTypeName;
                    
                    var ExamQuestionList = dbExamQuestionList.Where(s => s.ExamQuestionID == ExamQ.ExamQuestionID).ToList();
                    for(int j= 0; j < ExamQuestionList.Count; j++)
                    {
                        int tempID = ExamQuestionList.ElementAt(j).QuestionID;
                        
                        List<EAD.Question> Question = dbquestion.Where(s => s.PKID == tempID).ToList();
                        Question quest = new Question();
                        quest.PKID = Question.ElementAt(0).PKID;
                        quest.Description = Question.ElementAt(0).Description;
                      
                        List<EAD.QuestionAnswers> AnswersID = db.QuestionAnswers.Where(s => s.QuestionID == quest.PKID).ToList();
                        for(int k = 0;  k < AnswersID.Count(; k++)
                        {
                            int answer = AnswersID.ElementAt(k).AnswerID;
                            var TheAnswer = from tempAnswer in dbanswer
                                            where tempAnswer.PKID == answer
                                            select tempAnswer;
                            Answers ans = new Answers();
                            ans.PKID = TheAnswer.ToArray()[0].PKID;
                            ans.Answer1 = TheAnswer.FirstOrDefault().Answer1;
                            ans.correct.isCorrect = AnswersID.ElementAt(k).IsCorrect;

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
            
            List<EAD.Subject> subjects = db.Subject.ToList();
            
            for (int i = 0; i < subjects.ToList().Count; i++)
            {
                Subject newSubject = new Subject();
                newSubject = Mapper.Map<Subject>(subjects.ElementAt(i));
               

                List<EAD.Subject_Categories> categories = db.Subject_Categories.Where(c => c.Subject_ID == newSubject.Subject_ID).ToList();  
                if(categories.Count < 1)
                {
                    result.Add(newSubject);
                }
                else
                {
                    for (int b = 0; b < categories.Count; b++)
                    {
                        int tempID = categories.ElementAt(b).Categories_ID;
                        List<EAD.Categories> categoriesL = db.Categories.Where(c => c.Categories_ID == tempID).ToList();
                        Category Tempcategory = new Category();
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
                                int id = SubtopicIDs.ToList().ElementAt(c);
                                List<EAD.Subtopic> Subtopics = db.Subtopic.Where(s => s.Subtopic_ID == id).ToList();
                                newSub = Mapper.Map<SubTopic>(Subtopics.ElementAt(0));
                              
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
        
        

        public void AddAnswer(int QuestionID, string Answer, bool IC)
        {
            EAD.Answer ans = new EAD.Answer();
            ans.Answer1 = Answer;
            
            NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "The answer value has been added"));
            try
            {
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "before db.Answer.Add"));
                db.Answer.Add(ans);
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "After db.Answer.Add"));
                db.SaveChanges();
                var tempPKID = db.Answer.OrderByDescending(item => item.PKID).First();
                int NewAnswerID = tempPKID.PKID;
                if(tempPKID.PKID == null)
                {
                    NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "The PKID of new answer has never been recieved"));
                }
                else
                {
                    NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "temp PKID"));
                    NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "The answer PKID has been received"));
                }
                
                spAddQuestionToAnswer(QuestionID, NewAnswerID, IC);
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "Stored Procedure fired"));
            }
            catch (Exception ex)
            {
                // to do
            }
        }

        public void DeleteAnswer(string Answerdesc)
        {
            int answerID = 0;
            EAD.Answer removedAnswer = new EAD.Answer();
            try
            {
                foreach (var item in db.Answer) //Gets the Answer which will be needed so it can be removed
                {
                    if (item.Answer1 == Answerdesc)
                    {
                        answerID = item.PKID;
                        removedAnswer = item; //keeps a reference to the subtopic that will be removed
                    }
                }
            }
            catch (Exception ex)
            {
                // to do
            }

            try
            {
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
            catch (Exception ex)
            {
                // to do
            }
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

            foreach (var item in db.ExamType)
            {
                if (item.ExamTypeName.Equals(ExamType))
                {
                    newExt.ExamType.PKID = item.PKID;
                    newExt.ExamType.ExamTypeName = ExamType;
                }
            }
                       
            try
            {
                db.ExamTemplate.Add(newExt);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // to do
            }
        }

        //Check this out to see if logic works but i believe it works -Devonte
        public void EditExam(string exName, string ExamTemplateID)
        {
            try {
                foreach (var item in db.ExamTemplate)
                {
                    if (item.ExamTemplateID.Equals(ExamTemplateID))
                    {
                        item.ExamTemplateName = exName;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                // to do
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
            try
            {
                foreach (var item in db.Subtopic) //Gets the subtopicID which will be needed so it can be removed
                {
                    if (item.Subtopic_Name == SubtopicName)
                    {
                        subtopicID = item.Subtopic_ID;
                        removedTopic = item; //keeps a reference to the subtopic that will be removed
                    }
                }
            }
            catch (Exception ex)
            {
                // to do
            }

            try
            {


                foreach (var item in db.Categories_Subtopic) //Removes all references to the subtopic in the database
                {
                    if (item.Subtopic_ID == subtopicID)
                    {
                        db.Categories_Subtopic.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                // to do
            }
            db.Subtopic.Remove(removedTopic); // removes the subtopic from the subtopic table.
            db.SaveChanges();
        }

        public void RemoveSubtopicFromCategory(string SubtopicName, string CategoryName)
        {
            int subtopicID = 0;
            int categoryID = 0;

            EAD.Subtopic removedTopic = new EAD.Subtopic();
            try
            {
                foreach (var item in db.Subtopic) //Gets the subtopicID which will be needed so it can be removed
                {
                    if (item.Subtopic_Name == SubtopicName)
                    {
                        subtopicID = item.Subtopic_ID;
                    }
                }
            }
            catch (Exception ex)
            {
                // to do
            }

            try
            {
                foreach (var item in db.Categories) //Gets the categoryID which will be needed so it can be removed
                {
                    if (item.Categories_Name == CategoryName)
                    {
                        categoryID = item.Categories_ID;
                    }
                }
            }
            catch (Exception ex)
            {
                // to do
            }
            try
            {
                foreach (var item in db.Categories_Subtopic) //Finds the row on the junction table that contains the pair of values and removes it
                {
                    if (item.Subtopic_ID == subtopicID && item.Categories_ID == categoryID)
                    {
                        db.Categories_Subtopic.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                //to do 
            }
                db.SaveChanges();
        }

        public void DeleteCategory(string CategoryName)
        {
            int categoryID = 0;
            EAD.Categories removedCategory = new EAD.Categories();
            try
            {
                foreach (var item in db.Categories) //Gets the categoryID which will be needed so it can be removed
                {
                    if (item.Categories_Name == CategoryName)
                    {
                        categoryID = item.Categories_ID;
                        removedCategory = item; //keeps a reference to the category that will be removed
                    }
                }
            }
            catch (Exception ex)
            {
                //to do 
            }
            try
            {
                foreach (var item in db.Subject_Categories) //Removes all references to the category from subjects
                {
                    if (item.Categories_ID == categoryID)
                    {
                        db.Subject_Categories.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                //to do
            }

            try
            {
                foreach (var item in db.ExamQuestion_Categories) //Removes all references to the category from subjects
                {
                    if (item.Categories_ID == categoryID)
                    {
                        db.ExamQuestion_Categories.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                // to do
            }

            try
            {
                db.Categories.Remove(removedCategory); // removes the category from the subtopic table.
            }
            catch (Exception ex)
            {
                //to do
            }
            db.SaveChanges();
        }

        public void AddSubject(string SubjectName)
        {
            EAD.Subject addedSubject = new EAD.Subject(); //Object to be passed into Subject Table
            addedSubject.Subject_Name = SubjectName; //Only Needs Name property
            try
            {
                db.Subject.Add(addedSubject); //adds object to database
            }
            catch (Exception ex)
            {
                //to do 
            }
            db.SaveChanges();
        }


        public void RemoveCategoryFromSubject(string CategoryName, string SubjectName)
        {

            int subjectID = 0;
            int categoryID = 0;

            try
            {
                foreach (var item in db.Subject) //Gets the subjectID which will be needed so it can be removed
                {
                    if (item.Subject_Name == SubjectName)
                    {
                        subjectID = item.Subject_ID;
                    }
                }
            }
            catch (Exception ex)
            {
                //to do
            }

            try
            {
                foreach (var item in db.Categories) //Gets the categoryID which will be needed so it can be removed
                {
                    if (item.Categories_Name == CategoryName)
                    {
                        categoryID = item.Categories_ID;
                    }
                }
            }
            catch (Exception ex)
            {
                //to do 
            }

            try
            {
                foreach (var item in db.Subject_Categories) //Finds the row on the junction table that contains the pair of values and removes it
                {
                    if (item.Subject_ID == subjectID && item.Categories_ID == categoryID)
                    {
                        db.Subject_Categories.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                //to do
            }
            db.SaveChanges();
        }

        public void DeleteSubject(string SubjectName)
        {
            int subjectID = 0;
            EAD.Subject removedSubject = new EAD.Subject();

            try
            {
                foreach (var item in db.Subject) //Gets the subjectID which will be needed so it can be removed
                {
                    if (item.Subject_Name == SubjectName)
                    {
                        subjectID = item.Subject_ID;
                        removedSubject = item; //keeps a reference to the subject that will be removed
                    }
                }
            }
            catch (Exception ex)
            {
                // to do 
            }

            try
            {
                foreach (var item in db.Subject_Categories) //Removes all references to the subject in the database
                {
                    if (item.Subject_ID == subjectID)
                    {
                        db.Subject_Categories.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                //to do
            }

            try
            {
                db.Subject.Remove(removedSubject); // removes the subject from the subtopic table.
            }
            catch (Exception ex)
            {
                //to do
            }
            db.SaveChanges();
        }


        public void UpdateAnswer(int answerid, string oldanswer)
        {
            try
            {
                var answer = db.Answer.Where(x => x.PKID == answerid);
                answer.First().Answer1 = oldanswer;
            }
            catch (Exception ex)
            { 
                // to do
            }
            db.SaveChanges();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            EAD.Subtopic test = new EAD.Subtopic();
            AutoMapperConfiguration.Configure();
            SubTopic test2 = new SubTopic();
            try
            {
                test2 = Mapper.Map<SubTopic>(test);
            }
            catch (Exception ex)
            {
                // to do 
            }

            try
            {
                if (composite == null)
                {
                    throw new ArgumentNullException("composite");
                }
                if (composite.BoolValue)
                {
                    composite.StringValue += "Suffix";
                }
            }
            catch (Exception ex)
            {
                //to do
            }
            return composite;
        }

        public void AddExamQuestion(ExamQuestion examQuestion)
        {
            
            if (examQuestion == null)
            {
                throw new ArgumentNullException("Exam Question");
            }

            try
            {
                AutoMapperConfiguration.Configure();
                EAD.ExamQuestion DALExamQuestion = Mapper.Map<EAD.ExamQuestion>(examQuestion);
                db.ExamQuestion.Add(DALExamQuestion);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //to-do add a fault contract
            }

        }
    }
}
