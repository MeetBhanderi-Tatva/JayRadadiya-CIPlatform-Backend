using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CI_Platform.Entity.RequestModel;
using CI_Platform.Entity.ResponseModel;

namespace CI_Platform.Entity
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Configure the Mappings
            //Mapping Employee to EmployeeDTO
            //Source: Employee and Destination: EmployeeDTO
            //CreateMap<Employee, EmployeeDTO>();
            //Mapping EmployeeDTO to Employee
            //Source: EmployeeDTO and Destination: Employee
            //CreateMap<EmployeeDTO, Employee>();


            CreateMap<RegisterRequest, User>().ReverseMap();
            CreateMap<CreateMissionModel, Mission>().ReverseMap();
            //CreateMap<Mission, Missions>()
            //    .ForMember(src => src.MissionImage, opt => opt.MapFrom(des => des.MissionMedias.FirstOrDefault().Image));
            //CreateMap<User, RegisterRequest>();
            //CreateMap<DropdownResponseModel, City>()
            //    .ForMember(src => src.CityId, opt => opt.MapFrom(des => des.Value))
            //    .ForMember(src => src.CityName, opt => opt.MapFrom(des => des.Text)).ReverseMap();
            //CreateMap<DropdownResponseModel, Country>()
            //    .ForMember(src => src.CountryId, opt => opt.MapFrom(des => des.Value))
            //    .ForMember(src => src.CountryName, opt => opt.MapFrom(des => des.Text)).ReverseMap();
            //CreateMap<DropdownResponseModel, Skill>()
            //    .ForMember(src => src.SkillId, opt => opt.MapFrom(des => des.Value))
            //    .ForMember(src => src.Skills, opt => opt.MapFrom(des => des.Text)).ReverseMap();
        }
    }
}
