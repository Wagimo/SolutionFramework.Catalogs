using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SolutionFramework.Abstractions.IRepository;
using SolutionFramework.Entities.Entities;

namespace SolutionFramework.Application.Features.Activities.Command.Update
{
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, bool>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateActivityCommandHandler> _logger;

        public UpdateActivityCommandHandler(IActivityRepository activityRepository, IMapper mapper, ILogger<UpdateActivityCommandHandler> logger)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Iniciando con el proceso de actualización de la actividad : {request?.Id}");
            var activity = _mapper.Map<Activity>(request);
            if (activity == null)
            {
                _logger.LogError($"Actividad no encontrada en la BD: {request?.Id}");
                throw new InvalidCastException("Error al mapear el request hacia la entidad");
            }

            activity.Id = Guid.Empty;
            var success = await _activityRepository.UpdateAsync(request.Id, activity);
            _logger.LogInformation($"Proceso de actualización finalizado satisfactoriamente!. Actividad {request.Id}");
            return success;

        }
    }
}
