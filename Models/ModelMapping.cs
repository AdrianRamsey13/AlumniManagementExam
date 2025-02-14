using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniManagement.AlumniService;
using AlumniManagement.FacultyService;
using AlumniManagement.MajorService;
using AlumniManagement.Models.DTO;
using AutoMapper;

namespace AlumniManagement.Models
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ModelMapping>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<DTO.AlumniDTO, AlumniService.AlumniDTO>().ReverseMap();
            CreateMap<DTO.FacultyDTO, FacultyService.FacultyDTO>().ReverseMap();
            CreateMap<DTO.MajorDTO, MajorService.MajorDTO>().ReverseMap();
            CreateMap<DTO.JobHistoryDTO, JobHistoryService.JobHistoryDTO>().ReverseMap();

        }

    }
}