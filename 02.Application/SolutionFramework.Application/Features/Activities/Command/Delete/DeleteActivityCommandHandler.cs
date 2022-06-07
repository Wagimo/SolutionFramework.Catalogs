using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SolutionFramework.Abstractions.IRepository;

namespace SolutionFramework.Application.Features.Activities.Command.Delete
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, bool>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteActivityCommand> _logger;

        public DeleteActivityCommandHandler(IActivityRepository activityRepository, IMapper mapper, ILogger<DeleteActivityCommand> logger)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Iniciando con el proceso de eliminación de la actividad: {request.Id}");
                var success = await _activityRepository.DeleteAsync(request.Id);
                _logger.LogInformation($"Proceso de eliminación Satisfactorio. Actividad : {request.Id}");
                return success;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar la actividad {request.Id}", ex);
                throw;
            }

        }
    }
}
