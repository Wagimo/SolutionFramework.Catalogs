using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SolutionFramework.Abstractions.IRepository;
using SolutionFramework.Application.ViewModels.Activities;
using SolutionFramework.Entities.Entities;
using SolutionFramework.Exceptions;

namespace SolutionFramework.Application.Features.Activities.Queries.GetById
{
    public class GetByIdActivityCommandHandler : IRequestHandler<GetByIdActivityCommand, ActivityVm>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdActivityCommandHandler> _logger;

        public GetByIdActivityCommandHandler(IActivityRepository activityRepository, IMapper mapper, ILogger<GetByIdActivityCommandHandler> logger)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ActivityVm> Handle(GetByIdActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _activityRepository.GetEntity<Activity>().FindAsync(request.Id);
                if (entity == null)
                    throw new NotFoundException("Activity", request.Id);
                var response = _mapper.Map<ActivityVm>(entity);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la Actividad {request.Id}", ex);
                throw;
            }
        }
    }
}
