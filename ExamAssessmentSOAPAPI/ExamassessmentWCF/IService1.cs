﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LMS1701.EA.SOAPAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    //Interface of the Service
    [ServiceContract]
    public interface IService1
    {
        //Exposing the Methods For the Service
        [OperationContract]
        string GetData(int value);
        //syntax
        //Make sure you add one of these for every method defined you want to add to the service for it to show up as useable
        //[OperationContract]
        //[WebInvoke(Method ="Name of HTTP Method" = WebMessageFormat.Json, BodyStyole = WebMessageBodyStyle.Wrapped, UriTemplate = "Name Of Method"]) 
        //METHOD SIGNATURE GOES HERE


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "TestThis")]
        List<SubTopic> GetSubtopicList();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetAnswersQuestionQuestID={QuestID}")]
        List<Answers> GetAnswersQuestion(int Questid);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetAllInfo")]
        List<Subject> GetAllSubject();
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetAllInfo?id={id}")]
        ExamTemplate getExamTemplate(String id);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetAllQuestion")]
        List<Question> GetAllQuestions();
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetAllExamQuestion")]
        List<ExamQuestion> GetAllExamQuestion();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddExistingCategory")]
        void spAddExistingCategory(String subject, String category);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddExistingSubtopicToCategory")]
        void spAddExistingSubtopicToCategory(String subtopic, String category);

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveQuestionFromExam")]
        void RemoveQuestionFromExam(string ExamID, string ExamQuestionID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddNewCategoryType")]
        void spAddNewCategoryType(String subject, String category);
        //     [OperationContract]
        //     CompositeType GetDataUsingDataContract(CompositeType composite);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddQuestionAsExam")]
        void spAddQuestionAsExamQuestion(String ExamQuestionID, int QuestionID, String name, int QuestionType);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddQuestionCategories")]
        void AddQuestionCategories(String Categories, string ExamQuestionID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddQuestionToAnswer")]
        void spAddQuestionToAnswer(int QuestionID, int AnswerID, bool isCorrect);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddQuestionToExam")]
        void spAddQuestionToExam(String ExamID, String ExamQuestionID, int weight);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddSubtopicType")]
        void spAddSubtopicType(String Subtopics,String Category);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteQuestionCategory")]
        void spDeleteQuestionCategory(String Categories, String ExamID);

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteQuestionCategory")]
        void DeleteQuestionCategory(String Category, String ExamQID);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveAnswerQuestion")]
        void spRemoveAnswerFromQuestion(int QuestionID, int AnswerID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveQuestionAsExamQuestion")]
        void spRemoveQuestionAsExamQuestion(String ExamQuestionID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveQuestionFromExam")]
        void spRemoveQuestionFromExam(String ExamQuestionID,String QuestionID);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddSubject")]
        void AddSubject(string SubjectName);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteSubtopic")]
        void DeleteSubtopic(string SubtopicName);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveSubtopicFromCategory")]
        void RemoveSubtopicFromCategory(string SubtopicName, string CategoryName);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddExamQuestion")]
        void AddExamQuestion(ExamQuestion examQuestion);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveSubtopicFromCategory")]
        void DeleteCategory(string CategoryName);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddAnswer")]
        void AddAnswer(int QuestionID, string Answer, bool IC);

        [OperationContract]
        [WebInvoke(Method = "Delete", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteAnswer")]
        void DeleteAnswer(string Answerdesc);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveCategoryFromSubject")]
        void RemoveCategoryFromSubject(string CategoryName, string SubjectName);

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteSubject")]
        void DeleteSubject(string SubjectName);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateAnswer")]
        void UpdateAnswer(int answerid, string newdesc );

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteExam")]
        void DeleteExam(string SubjectName);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddNewExamTemplate")]
        void AddNewExam(string ExamTemplateName, string ExamTemplateID, string ExamTypeName);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetExamIDList")]
        List<string> GetExamIDList();
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
