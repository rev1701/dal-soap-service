using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace LMS1701.EA.SOAPAPI
{
    public partial class Service1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public void spAddExistingCategory(String subject, String category)

        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));

            db.spAddExistingCategory(subject, category, myOutputParamInt);

        }
        public void spAddExistingSubtopicToCategory(String subtopic, String category)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));
            db.spAddExistingSubtopicToCategory(subtopic, category, myOutputParamInt);

        }
        public void spAddNewCategoryType(String subject, String category)
        {

            ObjectParameter myOutputParamInt = new ObjectParameter("result", (int)0);
            db.spAddExistingSubtopicToCategory(subject, category, myOutputParamInt);

        }
        public void spAddQuestionAsExamQuestion(String ExamQuestionID, int QuestionID, String name, int QuestionType)
        {
            ObjectParameter myOutputParamInt = new ObjectParameter("result", typeof(int));

            db.spAddQuestionAsExamQuestion(ExamQuestionID, QuestionID, name, QuestionType, myOutputParamInt);

        }
    }
}