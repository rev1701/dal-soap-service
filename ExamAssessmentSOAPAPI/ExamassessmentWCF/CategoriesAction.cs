using AutoMapper;
using LMS1701.EA.SOAPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EAD = ExamAssessmentDaal;

namespace LMS1701.EA.SOAPAPI
{
    public class CategoriesAction
    {
        EAD.ExamAssessmentEntities db;
        List<EAD.Categories> dbCategories;
        SubTopicAction SubTopicAction;
        public CategoriesAction()
        {
            EAD.ExamAssessmentEntities db = new EAD.ExamAssessmentEntities();
            List<EAD.Categories> dbCategories = db.Categories.ToList();
            SubTopicAction = new SubTopicAction();
        }
       
        public  Category getCategory(int item)
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