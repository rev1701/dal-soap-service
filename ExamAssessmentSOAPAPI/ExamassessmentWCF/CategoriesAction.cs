using AutoMapper;
using LMS1701.EA.SOAPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAD = ExamAssessmentDaal;

namespace ExamassessmentWCF
{
    public class CategoriesAction
    {
        private static EAD.ExamAssessmentEntities db = new EAD.ExamAssessmentEntities();
        private static List<EAD.Categories> dbCategories = db.Categories.ToList();
        public static Category getCategory(int item)
        {
           
            var dbSubtopics = db.Subtopic.ToList();
            Category newCategory = new Category();
            newCategory = Mapper.Map<Category>(dbCategories.Where(x => x.Categories_ID == item).First());
            var subtopicIDS = dbCategories.Where(x => x.Categories_ID == item).First().Categories_Subtopic.Select(x => x.Subtopic_ID).ToList();
            foreach (var subID in subtopicIDS)
            {

                SubTopic newSubtopic = SubTopicAction.getSubtopic(subID);
                newCategory.subtopics.Add(newSubtopic);
            }
            return newCategory;
        }
    }
}