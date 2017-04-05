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
                cfg.CreateMap<ExamAssessmentDaal.Subtopic, SubTopic>().ForMember(dest => dest.Subtopic_ID, opts => opts.MapFrom(src => src.Subtopic_ID))
                                                                      .ForMember(dest => dest.Subtopic_Name, opts => opts.MapFrom(src => src.Subtopic_Name));
                cfg.CreateMap<ExamAssessmentDaal.Subject, Subject>();
                cfg.CreateMap<ExamAssessmentDaal.Categories, Category>().ForMember(dest => dest.Categories_ID, opts => opts.MapFrom(src => src.Categories_ID))
                                                                        .ForMember(dest => dest.Categories_Name, opts => opts.MapFrom(src => src.Categories_Name));
                cfg.CreateMap<ExamAssessmentDaal.Answer, Answers>().ForMember(dest => dest.PKID, opt => opt.MapFrom(src => src.PKID))
                                                                  .ForMember(dest => dest.Answer1, opt => opt.MapFrom(src => src.Answer1));
                                                                  
                cfg.CreateMap<ExamAssessmentDaal.LanguageType, LanguageType>().ForMember(dest => dest.PKID, opt => opt.MapFrom(src => src.PKID))                                                               
                                                                 .ForMember(dest => dest.PKID, opt => opt.MapFrom(src => src.PKID))
                                                                 .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.LanguageName));
                cfg.CreateMap<ExamAssessmentDaal.ExamTemplate, ExamTemplate>();

            });
            

        }
    }
}