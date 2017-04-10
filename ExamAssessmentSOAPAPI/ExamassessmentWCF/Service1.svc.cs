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
    //db.SaveChanges means Saves changes to the database
    public class Service1 : IService1
    {
        private ExamAction ExamAction = new ExamAction();
        private EAD.ExamAssessmentEntities db = new EAD.ExamAssessmentEntities();
        ExamAction Action = new ExamAction();
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        /// <summary>
        /// Adds an category to a subject ion the junction table
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="category"></param>
        public void spAddExistingCategory(String subject, String category)

        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));

            db.spAddExistingCategory(subject, category, myOutputParamInt);

        }
        /// <summary>
        /// Adds an existing Subtopic to a category.
        /// </summary>
        /// <param name="subtopic"></param>
        /// <param name="category"></param>
        public void spAddExistingSubtopicToCategory(String subtopic, String category)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));
            db.spAddExistingSubtopicToCategory(subtopic, category, myOutputParamInt);

        }
        /// <summary>
        /// Adds a new Category.
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="category"></param>
        public void spAddNewCategoryType(String subject, String category)
        {
            
            ObjectParameter myOutputParamInt = new ObjectParameter("result", (int)0);
            db.spAddExistingSubtopicToCategory(subject, category, myOutputParamInt);

        }
        /// <summary>
        /// Adds a question as an Exam Question 
        /// </summary>
        /// <param name="ExamQuestionID"></param>
        /// <param name="QuestionID"></param>
        /// <param name="name"></param>
        /// <param name="QuestionType"></param>
        public void spAddQuestionAsExamQuestion(String ExamQuestionID, int QuestionID, String name, int QuestionType)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));

            db.spAddQuestionAsExamQuestion(ExamQuestionID, QuestionID, name, QuestionType, myOutputParamInt);

        }
      /*  public void spAddQuestionCategories(String Categories, int PKID)
        {
            int result = 0;
            db.spAddQuestionCategories(Categories, PKID, result);

        }*/
        /// <summary>
        /// Adds a Categorie to an exam Question.
        /// </summary>
        /// <param name="Categories"></param>
        /// <param name="ExamQuestionID"></param>
        public void AddQuestionCategories(String Categories, String ExamQuestionID)
        {
            var Categoreis = db.Categories.Where(c => c.Categories_Name == Categories);
            if(Categoreis.Count() > 0)
            {
                var QuestionID = db.ExamQuestion.Where(c => c.ExamQuestionID == ExamQuestionID);
                if(QuestionID.Count() > 0)
                {
                    EAD.ExamQuestion_Categories temp = new EAD.ExamQuestion_Categories();
                    temp.Categories_ID = Categoreis.First().Categories_ID;
                    temp.ExamQuestion_ID = db.ExamQuestion.First(s => s.ExamQuestionID == ExamQuestionID).PKID;
                    db.ExamQuestion_Categories.Add(temp);
                    NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", temp.Categories_ID.ToString())); 
                    //Logs the List of IDs for the Categoires attached to the Exam Question

                    NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", temp.ExamQuestion_ID.ToString()));
                    //Logs the Exam Question ID of which Exam Question working with
                   

                }
                
            }
            db.SaveChanges(); //Saves Changes to the Database
 

        }

        /// <summary>
        /// Adds a Question to Answer
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <param name="AnswerID"></param>
        /// <param name="isCorrect"></param>
        public void spAddQuestionToAnswer(int QuestionID, int AnswerID, bool isCorrect)
        {
            ObjectParameter result = new ObjectParameter("result", typeof(int));
            db.spAddQuestionToAnswer(QuestionID, AnswerID, isCorrect, result);

        }
        /// <summary>
        /// Removess a Question from an exam
        /// </summary>
        /// <param name="ExamID"></param>
        /// <param name="ExamQuestionID"></param>
        public void RemoveQuestionFromExam(string ExamID, string ExamQuestionID)
        {
            var itemtoRemove = db.ExamTemplateQuestions.First(x => x.ExamTemplateID == ExamID && x.ExamQuestionID == ExamQuestionID);
            db.ExamTemplateQuestions.Remove(itemtoRemove);
            db.SaveChanges();
        }
        /// <summary>
        /// Adds a Question to an Exam
        /// </summary>
        /// <param name="ExamID"></param>
        /// <param name="ExamQuestionID"></param>
        /// <param name="weight"></param>
        public void spAddQuestionToExam(string ExamID, string ExamQuestionID, int weight)
        {
            
            EAD.ExamTemplateQuestions toadd = new EAD.ExamTemplateQuestions();
            toadd.ExamTemplateID = ExamID;
            toadd.ExamQuestionID = ExamQuestionID;
            toadd.QuestionWeight = weight;
            db.ExamTemplateQuestions.Add(toadd);
            db.SaveChanges();

        }
        /// <summary>
        /// Adds a subtopic type
        /// </summary>
        /// <param name="Subtopics"></param>
        /// <param name="Category"></param>
        public void spAddSubtopicType(string Subtopics, string Category)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));
            db.spAddSubtopicType(Subtopics, Category, myOutputParamInt);

        }
        /// <summary>
        /// Removes Question From Category
        /// </summary>
        /// <param name="Categories"></param>
        /// <param name="ExamID"></param>
        public void spDeleteQuestionCategory(String Categories, String ExamID)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));
            db.spDeleteQuestionCategory(Categories, ExamID, myOutputParamInt);

        }
        /// <summary>
        /// Deletes From Junction Table. Removes a Category from a Question.
        /// </summary>
        /// <param name="Category"></param>
        /// <param name="ExamQID"></param>
        public void DeleteQuestionCategory(String Category, String ExamQID)
        {
            var Categoreis = db.Categories.Where(c => c.Categories_Name == Category);
            if (Categoreis.Count() > 0)
            {
                var QuestionID = db.ExamQuestion.Where(c => c.ExamQuestionID == ExamQID);
                if (QuestionID.Count() > 0)
                {
                    EAD.ExamQuestion_Categories temp = new EAD.ExamQuestion_Categories();
                    temp.Categories_ID = Categoreis.First().Categories_ID;
                    temp.ExamQuestion_ID = db.ExamQuestion.First(s => s.ExamQuestionID == ExamQID).PKID;
                    db.ExamQuestion_Categories.Remove(db.ExamQuestion_Categories.Where(s => s.Categories_ID == temp.Categories_ID && s.ExamQuestion_ID == temp.ExamQuestion_ID).First());//.Where(s => s.Categories_ID == temp.Categories_ID && s.ExamQuestion_ID == temp.ExamQuestion_ID).;
                    NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", temp.Categories_ID.ToString()));
                    NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", temp.ExamQuestion_ID.ToString()));


                }

            }
            db.SaveChanges();
        }
        /// <summary>
        /// Removes an Answers from Question
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <param name="AnswerID"></param>
        public void spRemoveAnswerFromQuestion(int QuestionID, int AnswerID)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));
            db.spRemoveAnswerFromQuestion(QuestionID, AnswerID, myOutputParamInt);

        }

        public void spRemoveQuestionAsExamQuestion(String ExamQuestionID)
        {
            int result = 0;

            db.spRemoveQuestionAsExamQuestion(ExamQuestionID, result);

        }
        public void spRemoveQuestionFromExam(String ExamID, String ExamQuestionID)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));
            db.spRemoveQuestionFromExam(ExamID, ExamQuestionID, myOutputParamInt);

        }
        /// <summary>
        /// Returns all the exam Questions
        /// This includes everything attached to an exam Questions
        /// such as all the categories
        /// and all the Questions attached to that exam Question
        /// And all the answes attached to the Questions.
        /// </summary>
        /// <returns></returns>
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
                ExamQuestion question = new ExamQuestion();
                question.ExamQuestionID = dbExamQuestion.ElementAt(i).ExamQuestionID;
                question.ExamQuestionName = dbExamQuestion.ElementAt(i).ExamQuestionName;
                question.PKID = dbExamQuestion.ElementAt(i).PKID;
                question.QuestionType.PKID = dbExamQuestion.ElementAt(i).QuestionTypeID;
                question.QuestionType.QuestionTypeName = dbExamQuestion.ElementAt(i).QuestionType.QuestionTypeName;
                for (int j = 0; j < dbExamQuestion.ElementAt(i).ExamQuestion_Categories.Count; j++)
                {
                    Category cat = new Category();


                    cat.Categories_ID = dbExamQuestion.ElementAt(i).ExamQuestion_Categories.ElementAt(j).Categories_ID;
                    cat.Categories_Name = dbCategories.Where(s => s.Categories_ID == dbExamQuestion.ElementAt(i).ExamQuestion_Categories.ElementAt(j).Categories_ID).First().Categories_Name;

                    List<int> listofSub = dbCatSub.Where(s => s.Categories_ID == cat.Categories_ID).Select(s => s.Subtopic_ID).ToList();
                    for (int k = 0; k < listofSub.Count(); k++)
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
        /// <summary>
        /// Returns a list of all the Questions
        /// </summary>
        /// <returns></returns>
        public List<Question> GetAllQuestions()
        {
            AutoMapperConfiguration.Configure();
            List<Question> result = new List<Question>();
            var first = (from c in db.Question
                         select c).ToList();
            var dbQuestionAnswers = db.QuestionAnswers.ToList();
            for (int i = 0; i < first.ToList().Count; i++)
            {
                Question quest = new Question();
                quest.PKID = first.ElementAt(i).PKID;
                quest.Description = first.ElementAt(i).Description;

                quest.Answers = GetAnswersQuestion(quest.PKID);
                result.Add(quest);

            }
            return result;
        }
        /// <summary>
        /// Returns a list of answers for the question id passed in
        /// </summary>
        /// <param name="Questid"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a full exam Template
        /// which inclueds the Exam Template itself 
        /// as well as all the exam Questions attached to that exam.
        /// The exam Questions will have multiple questions attached to them.
        /// Exam Questions Can have multiple categoreis attached to them.
        /// Each question has all the answers attached to it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExamTemplate getExamTemplate(String id)
        {
            AutoMapperConfiguration.Configure();

            List<EAD.ExamTemplate> ExamTemplate = db.ExamTemplate.Where(s => s.ExamTemplateID == id).ToList();
            ExamTemplate exam = Action.getExam(id);


            List<EAD.ExamTemplateQuestions> ExamQuestions = db.ExamTemplateQuestions.Where(s => s.ExamTemplateID == exam.ExamTemplateID).ToList();
            if (ExamQuestions.Count < 1)
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
                var dbCategories = db.Categories.ToList();
                var dbSubtopics = db.Subtopic.ToList();
                
                for (int i = 0; i < ExamQuestions.Count(); i++)
                {

                    List<EAD.ExamQuestion> ExamQuestion = dbExamQuestion.Where(s => s.ExamQuestionID == ExamQuestions.ElementAt(i).ExamQuestionID).ToList();


                    ExamQuestion ExamQ = new ExamQuestion();

                    ExamQ.ExamQuestionID = ExamQuestion.ElementAt(0).ExamQuestionID;
                    ExamQ.ExamQuestionName = ExamQuestion.ElementAt(0).ExamQuestionName;
                    ExamQ.PKID = ExamQuestion.ElementAt(0).PKID;
                    ExamQ.QuestionType.PKID = ExamQuestion.ElementAt(0).QuestionType.PKID;
                    ExamQ.QuestionType.QuestionTypeName = ExamQuestion.ElementAt(0).QuestionType.QuestionTypeName;
                    var categoryIDs=ExamQuestion.ElementAt(0).ExamQuestion_Categories.Select(x => x.Categories_ID).ToList();
                    List <Category>ExamQCategories = new List<Category>();

                    foreach (var item in categoryIDs)
                    {
                        Category newCategory = new Category();
                        newCategory = Mapper.Map<Category>(dbCategories.Where(x => x.Categories_ID == item).First());
                        var subtopicIDS = dbCategories.Where(x => x.Categories_ID == item).First().Categories_Subtopic.Select(x => x.Subtopic_ID).ToList();
                        foreach (var subID in subtopicIDS)
                        {
                            SubTopic newSubtopic = new SubTopic();
                            newSubtopic = Mapper.Map<SubTopic>(dbSubtopics.Where(x => x.Subtopic_ID == subID).First());
                            newCategory.subtopics.Add(newSubtopic);
                        }
                        ExamQCategories.Add(newCategory);
                    }
                    ExamQ.ExamQuestion_Categories = ExamQCategories;
                    var ExamQuestionList = dbExamQuestionList.Where(s => s.ExamQuestionID == ExamQ.ExamQuestionID).ToList();
                    for (int j = 0; j < ExamQuestionList.Count; j++)
                    {
                        int tempID = ExamQuestionList.ElementAt(j).QuestionID;

                        List<EAD.Question> Question = dbquestion.Where(s => s.PKID == tempID).ToList();
                        Question quest = new Question();
                        quest.PKID = Question.ElementAt(0).PKID;
                        quest.Description = Question.ElementAt(0).Description;
                        List<EAD.QuestionAnswers> AnswersID = dbQuestionAnswers.Where(s => s.QuestionID == quest.PKID).ToList();
                        for (int k = 0; k < AnswersID.Count; k++)
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
        /// <summary>
        /// Returnsa list of all the SUbjects with their categories
        /// and categories has a list of subtopics.
        /// </summary>
        /// <returns></returns>
        public List<Subject> GetAllSubject()
        {
            AutoMapperConfiguration.Configure();
            List<Subject> result = new List<Subject>();

            List<EAD.Subject> subjects = db.Subject.ToList();

            for (int i = 0; i < subjects.ToList().Count; i++)
            {
                Subject newSubject = Mapper.Map<Subject>(subjects.ElementAt(i));


                List<EAD.Subject_Categories> categories = db.Subject_Categories.Where(c => c.Subject_ID == newSubject.Subject_ID).ToList();
                if (categories.Count < 1)
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
                        if (SubtopicIDs.Count() < 1)
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


        /// <summary>
        /// This method attaches an answer to a question using the entity that represents the question answers table.
        /// Logging and Exception handling is present
        /// </summary>
        /// <param name="QuestionID">The Subquestion ID of the question you would like to add an answer to</param>
        /// <param name="Answer">The Actual Answer</param>
        /// <param name="IC">The boolean value that represents if the answer is the correct answer for the specified question</param>
        public void AddAnswer(int QuestionID, string Answer, bool IC)
        {
            
            EAD.Answer ans = new EAD.Answer();
            ans.Answer1 = Answer; 

            NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "The answer value has been added"));
            try
            {
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "before db.Answer.Add"));
                db.Answer.Add(ans);   //add the answer object to the database Answer Table
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "After db.Answer.Add"));
                db.SaveChanges();    
                var tempPKID = db.Answer.OrderByDescending(item => item.PKID).First(); //Get the PKID of the answer that was just inserted
                int NewAnswerID = tempPKID.PKID; 
                if (tempPKID.PKID == null)
                {
                    NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "The PKID of new answer has never been recieved"));
                }
                else
                {
                    NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "temp PKID"));
                    NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "The answer PKID has been received"));
                }

                spAddQuestionToAnswer(QuestionID, NewAnswerID, IC);  //Call a method that uses a stored procedure call to update the junction table in the database according to the question Answer combination
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "Stored Procedure fired"));
            }
            catch (Exception ex)
            {
                // TODO
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// Might want to change this Method's parameters and functionality to use the PKID because to delete you have to type in the whole answer instead of easily using PKID and also the whole string has to match which is more work
        /// <param name="Answerdesc">The actual answer string</param>
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
                        removedAnswer = item;
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO
            }

            try
            {
                foreach (var item in db.QuestionAnswers) //Removes all references to the Answer in the database
                {
                    if (item.AnswerID == answerID) 
                    {
                        db.QuestionAnswers.Remove(item); //Remove the row from the database
                    }
                }
                db.Answer.Remove(removedAnswer); // removes the Answer from the Answer table.
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO
            }
        }

        /// <summary>
        /// Delete An Exam Using the ExamTemplateID 
        /// </summary>
        /// <param name="ExamTID">The ExamTemplateID of the exam to be deleted</param>
        public void DeleteExam(string ExamTID)
        {

            EAD.ExamTemplate removedExam = new EAD.ExamTemplate(); 
            try
            {
                foreach (var item in db.ExamTemplate) //Gets the Exam which will be needed so it can be removed
                {
                    if (item.ExamTemplateID.Equals(ExamTID))
                    {

                        removedExam = item; 
                    }
                }
                foreach (var item in db.ExamTemplateQuestions) //Removes all references to the Exam in the database
                {
                    if (item.ExamTemplateID == ExamTID) 
                    {
                        db.ExamTemplateQuestions.Remove(item); //Delete from Junction Table
                    }
                }
                db.ExamTemplate.Remove(removedExam); // removes the ExamTemplate from the ExamTemplate table.
                db.SaveChanges(); 
            }
            catch (Exception ex) 
            {
                //TODO
            }
        }
        
        /// <summary>
        /// This Method adds a brand new exam to the database. The Parameters passed in defines the new values in the database
        /// </summary>
        /// <param name="exName">The name of the Exam</param>
        /// <param name="exTID">The Exam Template ID</param>
        /// <param name="ExamType">The Exam Type</param>
        public void AddNewExam(string exName, string exTID, string ExamType)
        {
            EAD.ExamTemplate newExt = new EAD.ExamTemplate(); 
            newExt.ExamTemplateName = exName;
            newExt.ExamTemplateID = exTID; 
            newExt.ExamTypeID = db.ExamType.Where(x => x.ExamTypeName == ExamType).Select(x => x.PKID).First(); //
            newExt.CreatedDate = DateTime.Now; //generates the current DateTime
            try
            {
                db.ExamTemplate.Add(newExt); // adds the new exam template to the database
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
            try
            {
                foreach (var item in db.ExamTemplate) 
                {
                    if (item.ExamTemplateID.Equals(ExamTemplateID)) 
                    {
                        item.ExamTemplateName = exName; // store the current Exam Template Name passed in into the database's current 
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

        /// <summary>
        /// Gets The List of Subtopics in the Database 
        /// </summary>
        /// <returns></returns>
        public List<SubTopic> GetSubtopicList() 
        {
            var x = db.Subtopic.Select(j => Mapper.Map<SubTopic>(j)); 
            // select all of the subtopics as entity objects, and maps to EAD.Subtopic using automapper
            return x.ToList(); 
        }

        /// <summary>
        /// Deletes a Subtopic in the Subtopic Database based on the name
        /// </summary>
        /// <param name="SubtopicName">Subtopic Name for removal</param>
        public void DeleteSubtopic(string SubtopicName)
        {
            int subtopicID = 0;
            EAD.Subtopic removedTopic = new EAD.Subtopic(); //Make a Data Access Library Subtopic Object to prepare to remove this object from the database
            try
            {
                foreach (var item in db.Subtopic) //Gets the subtopicID which will be needed so it can be removed
                {
                    if (item.Subtopic_Name == SubtopicName) 
                    {
                        subtopicID = item.Subtopic_ID;
                        removedTopic = item; 
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

                db.Subtopic.Remove(removedTopic); // removes the subtopic from the subtopic table.
                db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                // to do
            }
        }


        /// <summary>
        /// Removes A Subtopic from being attached to a Category in a Junction Table in the Database
        /// </summary>
        /// <param name="SubtopicName">Name of the Subtopic to be dettached</param>
        /// <param name="CategoryName">Name of the Category</param>
        public void RemoveSubtopicFromCategory(string SubtopicName, string CategoryName)
        {
            //DISCLAIMER: ; in hindsight it might have been easier to pass in the id of the subtopic name and the id of the category name. The belief was that this method would be easier to use with knowledge of the subtopic and category name without going back and forth to the database to find those things out.

            
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
                        db.Categories_Subtopic.Remove(item); //Remove that row where both ID's fit that condition
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //to do 
            }
        }

        /// <summary>
        /// This Method makes sure that the Category does not have any dependencies and then deletes it from the categories table
        /// </summary>
        /// <param name="CategoryName">Name of the Category for deleteion</param>
        public void DeleteCategory(string CategoryName)
        {
            int categoryID = 0; //prepare to store the CategoryID
            EAD.Categories removedCategory = new EAD.Categories(); 
            try
            {
                foreach (var item in db.Categories)
                {
                    if (item.Categories_Name == CategoryName) //If the database's current Category Name matches the one passed in
                    {
                        categoryID = item.Categories_ID; 
                        removedCategory = item; 
                    }
                }
            }
            catch (Exception ex) 
            {
                //to do 
            }
            try
            {
                foreach (var item in db.Subject_Categories) { //Iterates through the Subject_Categories table
              
                    if (item.Categories_ID == categoryID) 
                    {
                        db.Subject_Categories.Remove(item); //Removes all references to the category from subjects
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
                        db.ExamQuestion_Categories.Remove(item); // Remove that row in the junction table ExamQuestion_Categories
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
                db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                //to do
            }
                }

        /// <summary>
        /// Adds New subject to the database using SubjectName
        /// </summary>
        /// <param name="SubjectName">New Subject Name</param>
        public void AddSubject(string SubjectName)
        {
            EAD.Subject addedSubject = new EAD.Subject(); 
            addedSubject.Subject_Name = SubjectName;
            try
            {
                db.Subject.Add(addedSubject); //adds object to database
                db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                //to do
            }
        }


        /// <summary>
        /// Detattach a category from a subject using both the category name and the subject name
        /// </summary>
        /// <param name="CategoryName">Category Name to be removed from subject</param>
        /// <param name="SubjectName">Subject Name that Category is connected to</param>
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
                        categoryID = item.Categories_ID; // store the CategoryID from the database to a variable
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
                        db.Subject_Categories.Remove(item); //Remove the row from the junction table
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                //to do
            }
            
        }


        /// <summary>
        /// This Method Deletes a subject
        /// </summary>
        /// <param name="SubjectName">The name of the Subject up for deletion</param>
        public void DeleteSubject(string SubjectName) 
        {
            int subjectID = 0;
            EAD.Subject removedSubject = new EAD.Subject(); 

            try
            {
                //Gets the subjectID which will be needed so it can be removed
                foreach (var item in db.Subject)
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
                //Removes all references to the subject in the database
                foreach (var item in db.Subject_Categories) //For each row in the Subject Categories Junction Table 
                {
                    if (item.Subject_ID == subjectID) 
                    {
                        db.Subject_Categories.Remove(item); //Remove the row from the junction table
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
                db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                //to do
            }
        }
        
        /// <summary>
        /// //Updates the Answer in the database using the answer id and the new answer
        /// </summary>
        /// <param name="answerid">The ID of the answer you want to change</param>
        /// <param name="newanswer">The string of the new answer you want to store</param>
        public void UpdateAnswer(int answerid, string newanswer) 
        {
            try
            {
                var answer = db.Answer.Where(x => x.PKID == answerid);
                answer.First().Answer1 = newanswer; //replace the old answer in the databse with the new answer passed in
                db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                // to do
            }
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {

            AutoMapperConfiguration.Configure(); 
            SubTopic test2 = new SubTopic(); 
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
        public List<string> GetExamIDList()
        {
            List<string> IDList = new List<string>();
            var examIDS = from exam in db.ExamTemplate
                          select exam.ExamTemplateID;

            foreach (var item in examIDS)
            {
                IDList.Add(item);
            }
            return IDList;
        }
        public void AddExamQuestion(ExamQuestion examQuestion) // Adds an exam question using a whole exam question object/model/EAD
        {

            if (examQuestion == null) //Exam Question does it exist
            {
                throw new ArgumentNullException("Exam Question"); //You passed in a empty exam Question Object
            }
            AutoMapperConfiguration.Configure();
            EAD.ExamQuestion DALExamQuestion = new EAD.ExamQuestion(); 
            DALExamQuestion.ExamQuestionID = examQuestion.ExamQuestionID;
            DALExamQuestion.ExamQuestionName = examQuestion.ExamQuestionName;
            DALExamQuestion.QuestionTypeID = examQuestion.QuestionType.PKID;

            db.ExamQuestion.Add(DALExamQuestion);
            db.SaveChanges();
            foreach (var subquestion in examQuestion.quest)
            {

                EAD.Question questiontoAdd = new EAD.Question();
                EAD.ExamQuestionList questioncombination = new EAD.ExamQuestionList();
                questiontoAdd.Description = subquestion.Description;
                db.Question.Add(questiontoAdd);
                db.SaveChanges();


                questioncombination.ExamQuestionID = DALExamQuestion.ExamQuestionID;
                questioncombination.QuestionID = questiontoAdd.PKID;


                //adds to subquestion table

                db.ExamQuestionList.Add(questioncombination); //adds to subquestion/examquestion junction table

                foreach (var answer in subquestion.Answers)
                {
                    EAD.Answer answertoAdd = new EAD.Answer();
                    answertoAdd.Answer1 = answer.Answer1;
                    answertoAdd.AddLanguageTypeID = 1; //Change the Language type to 1
                    EAD.QuestionAnswers answercombination = new EAD.QuestionAnswers();
                    db.Answer.Add(answertoAdd); // adds answer to answer Table
                    db.SaveChanges(); 
                    answercombination.AnswerID = answertoAdd.PKID;
                    answercombination.QuestionID = questiontoAdd.PKID;
                    answercombination.IsCorrect = answer.correct.isCorrect;


                    db.QuestionAnswers.Add(answercombination); // Add the QuestionAnswers junction table row
                    db.SaveChanges(); 
                }


            }

        }

    }
}