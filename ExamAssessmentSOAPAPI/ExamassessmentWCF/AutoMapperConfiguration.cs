using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Mappers;
using ExamAssessmentDaal;
namespace LMS1701.EA.SOAPAPI
{
    public static class AutoMapperConfiguration
    {

        public static void Configure()
        {
            Mapper.Initialize(cfg => {
            cfg.CreateMap<ExamAssessmentDaal.Subtopic, SubTopic>();
                cfg.CreateMap<ExamAssessmentDaal.Subject, Subject>();
                cfg.CreateMap<ExamAssessmentDaal.Category, Category>();
                cfg.CreateMap<ExamAssessmentDaal.Answer, Answer>();
                cfg.CreateMap<ExamAssessmentDaal.ExamTemplate, ExamTemplate>();

            });
            

        }
    }
}