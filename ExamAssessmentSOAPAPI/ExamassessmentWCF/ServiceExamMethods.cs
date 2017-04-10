using AutoMapper;
using ExamassessmentWCF;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAD = ExamAssessmentDaal;
namespace LMS1701.EA.SOAPAPI
{
    public partial class Service1
    {
       
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
                    Category cat = CategoriesAction.getCategory(dbExamQuestion.ElementAt(i)
                                                    .ExamQuestion_Categories.ElementAt(j).Categories_ID);

                    #region
                    /*    cat.Categories_ID = dbExamQuestion.ElementAt(i).ExamQuestion_Categories.ElementAt(j).Categories_ID;
                        cat.Categories_Name = dbCategories.Where(s => s.Categories_ID == dbExamQuestion.ElementAt(i).ExamQuestion_Categories.ElementAt(j).Categories_ID).First().Categories_Name;

                        List<int> listofSub = dbCatSub.Where(s => s.Categories_ID == cat.Categories_ID).Select(s => s.Subtopic_ID).ToList();
                        for (int k = 0; k < listofSub.Count(); k++)
                        {
                            var subtopic = dbSubtopic.Where(s => s.Subtopic_ID == listofSub.ElementAt(k));
                            SubTopic sub = 
                            sub.Subtopic_ID = subtopic.ElementAt(0).Subtopic_ID;
                            sub.Subtopic_Name = subtopic.ElementAt(0).Subtopic_Name;
                            cat.subtopics.Add(sub);
                        }*/
                    #endregion
                    question.ExamQuestion_Categories.Add(cat);
                }

                List<int> QuestionIDs = dbQuestExam.Where(s => s.ExamQuestionID == question.ExamQuestionID)
                                                   .Select(c => c.QuestionID).ToList();
                EAD.Question tempQuestion;

                for (int j = 0; j < QuestionIDs.Count; j++)
                {
                    tempQuestion = dbQuestion.Where(s => s.PKID == QuestionIDs.ElementAt(j))
                                             .First();
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
                List<int> AnswerID = db.QuestionAnswers.Where(c => c.QuestionID == Questid)
                                                       .Select(x => x.AnswerID).ToList();
                NLogConfig.logger.Log(new LogEventInfo(LogLevel.Info, "WFCLogger", "Got AnswerID's"));
                List<EAD.Answer> AnswerDB = db.Answer.ToList();
                List<EAD.QuestionAnswers> dbQuestionAns = db.QuestionAnswers
                                                            .ToList();
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
                        if (dbQuestionAns.Where(s => s.QuestionID == Questid && s.AnswerID == answer.PKID)
                                          .Select(s => s.IsCorrect).First() == true)
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
            AutoMapperConfiguration.Configure();
            List<EAD.ExamTemplate> ExamTemplate = db.ExamTemplate.Where(s => s.ExamTemplateID == id).ToList();
            ExamTemplate exam = ExamAction.getExam(id);
            List<EAD.ExamTemplateQuestions> ExamQuestions = db.ExamTemplateQuestions
                                                              .Where(s => s.ExamTemplateID == exam.ExamTemplateID)
                                                              .ToList();
            if (ExamQuestions.Count < 1)
            {
                return exam;
            }
            else
            {
                var dbExamQuestion = db.ExamQuestion.ToList();
                var dbExamQuestionList = db.ExamQuestionList.ToList();
                var dbQuestionAnswers = db.QuestionAnswers.ToList();
                for (int i = 0; i < ExamQuestions.Count(); i++)
                {

                    List<EAD.ExamQuestion> ExamQuestion = dbExamQuestion
                                                          .Where(s => s.ExamQuestionID == ExamQuestions.ElementAt(i).ExamQuestionID)
                                                          .ToList();
                    ExamQuestion ExamQ = ExamAction.getExamQuestion(ExamQuestion.ElementAt(0));
                    var categoryIDs = ExamQuestion.ElementAt(0).ExamQuestion_Categories.Select(x => x.Categories_ID).ToList();
                    List<Category> ExamQCategories = new List<Category>();
                    foreach (var item in categoryIDs)
                    {
                        Category newCategory = CategoriesAction.getCategory(item);
                        ExamQCategories.Add(newCategory);
                        #region
                        /* Category newCategory = new Category();
                         newCategory = Mapper.Map<Category>(dbCategories.Where(x => x.Categories_ID == item).First());
                         var subtopicIDS = dbCategories.Where(x => x.Categories_ID == item).First().Categories_Subtopic.Select(x => x.Subtopic_ID).ToList();
                         foreach (var subID in subtopicIDS)
                         {
                             SubTopic newSubtopic = new SubTopic();
                             newSubtopic = Mapper.Map<SubTopic>(dbSubtopics.Where(x => x.Subtopic_ID == subID).First());
                             newCategory.subtopics.Add(newSubtopic);
                         }
                         ExamQCategories.Add(newCategory); */
                        #endregion
                    }
                    ExamQ.ExamQuestion_Categories = ExamQCategories;
                    var ExamQuestionList = dbExamQuestionList.Where(s => s.ExamQuestionID == ExamQ.ExamQuestionID).ToList();
                    for (int j = 0; j < ExamQuestionList.Count; j++)
                    {
                        int tempID = ExamQuestionList.ElementAt(j).QuestionID;
                        #region    // List<EAD.Question> Question = dbquestion.Where(s => s.PKID == tempID).ToList();

                        // quest.PKID = Question.ElementAt(0).PKID;
                        //quest.Description = Question.ElementAt(0).Description;
                        #endregion
                        Question quest = QuestionAction.getQuestion(tempID);
                        List<EAD.QuestionAnswers> AnswersID = dbQuestionAnswers.Where(s => s.QuestionID == quest.PKID).ToList();
                        for (int k = 0; k < AnswersID.Count; k++)
                        {
                            int answer = AnswersID.ElementAt(k).AnswerID;
                            Answers ans = AnswerAction.getAnswer(answer);
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
                Subject newSubject = Mapper.Map<Subject>(subjects.ElementAt(i));


                List<EAD.Subject_Categories> categories = db.Subject_Categories.Where(c => c.Subject_ID == newSubject.Subject_ID)
                                                            .ToList();
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
    }
}