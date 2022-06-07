using AutoMapper;
using SolutionFramework.Application.Features.Activities.Command.Create;
using SolutionFramework.Application.Features.Activities.Command.Update;
using SolutionFramework.Application.ViewModels.Activities;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Application.Mappings.Activities
{
    public class MappingActivityProfile : Profile
    {
        public MappingActivityProfile()
        {
            CreateMap<Activity, ActivityVm>().ReverseMap();
            CreateMap<CreateActivityCommand, Activity>();
            CreateMap<UpdateActivityCommand, Activity>();
        }
    }
}
