using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SolutionFramework.Abstractions.IRepository;
using SolutionFramework.Application.ViewModels.Activities;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Application.Features.Activities.Command.Create
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, ActivityVm>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateActivityCommandHandler> _logger;

        public CreateActivityCommandHandler(IActivityRepository activityRepository, IMapper mapper, ILogger<CreateActivityCommandHandler> logger)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ActivityVm> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando con la creación de la actividad");
            var activity = _mapper.Map<Activity>(request);
            if (activity == null)
            {
                _logger.LogInformation("Error al leer los datos de la actividad desde el Request!");
                throw new ArgumentNullException("Error al obtener los datos de la actividad desde la solicitud del usaurio.");
            }


            activity.Id = Guid.NewGuid();
            activity.State = true;
            await _activityRepository.CreateAsync(activity);
            var response = _mapper.Map<ActivityVm>(activity);
            _logger.LogInformation("Proceso de creación finalizado satisfactoriamente!");
            return response;

        }
    }
}
