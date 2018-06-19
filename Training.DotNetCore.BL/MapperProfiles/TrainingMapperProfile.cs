using AutoMapper;

namespace Training.DotNetCore.BL.MapperProfiles
{
    public class TrainingMapperProfile : Profile
    {
        public TrainingMapperProfile()
        {
            CreateMap<BL.Model.Training, DA.Model.Training>();
        }
    }
}