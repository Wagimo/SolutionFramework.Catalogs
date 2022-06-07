using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SolutionFramework.Abstractions.IRepository;
using SolutionFramework.Application.ViewModels.Activities;
using SolutionFramework.EFcore.Extensions;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Application.Features.Activities.Queries.GetAll
{
    public class GetAllActivitiesCommandHandler : IRequestHandler<GetAllActivitiesCommand, List<ActivityVm>>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllActivitiesCommandHandler> _logger;

        public GetAllActivitiesCommandHandler(IActivityRepository activityRepository, IMapper mapper, ILogger<GetAllActivitiesCommandHandler> logger)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ActivityVm>> Handle(GetAllActivitiesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var activities = await _activityRepository
                                .GetEntity<Activity>()
                                .ToPageAsync(request.CurrentPage, request.PageSize);

                var response = _mapper.Map<List<ActivityVm>>(activities.Data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener las actividades desde la BD", ex);
                throw;
            }
        }
    }
}
