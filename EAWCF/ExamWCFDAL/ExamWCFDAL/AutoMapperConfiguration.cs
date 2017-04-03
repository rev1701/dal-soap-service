using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Mappers;
using ExamAssessmentDAL;
namespace LMS1701.EA.SOAPAPI
{
    public static class AutoMapperConfiguration
    {

        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<ExamAssessmentDAL.Subtopic, SubTopic>().ForMember(dest => dest.Subtopic_ID, opts => opts.MapFrom(src => src.Subtopic_ID))
                                                                      .ForMember(dest => dest.Subtopic_Name, opts => opts.MapFrom(src => src.Subtopic_Name));
                cfg.CreateMap<ExamAssessmentDAL.Subject, Subject>();
                cfg.CreateMap<ExamAssessmentDAL.Categories_Subtopic, Category>().ForMember(dest => dest.Categories_ID, opts => opts.MapFrom(src => src.Categories_ID))
                                                                        .ForMember(dest => dest.Categories_Name, opts => opts.MapFrom(src => src.Categories_ID));
                cfg.CreateMap<ExamAssessmentDAL.Answer, Answers>().ForMember(dest => dest.PKID, opt => opt.MapFrom(src => src.PKID))
                                                                  .ForMember(dest => dest.Answer1, opt => opt.MapFrom(src => src.Answer1));
                                                                  
                cfg.CreateMap<ExamAssessmentDAL.LanguageType, LanguageType>().ForMember(dest => dest.PKID, opt => opt.MapFrom(src => src.PKID))                                                               
                                                                 .ForMember(dest => dest.PKID, opt => opt.MapFrom(src => src.PKID))
                                                                 .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.LanguageName));

                cfg.CreateMap<ExamAssessmentDAL.ExamTemplate, ExamTemplate>();

            });
            

        }
    }
}