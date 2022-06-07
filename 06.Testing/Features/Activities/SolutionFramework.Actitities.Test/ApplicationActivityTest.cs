using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SolutionFramework.Application.Features.Activities.Command.Create;
using SolutionFramework.Application.Features.Activities.Command.Update;
using SolutionFramework.Application.Mappings.Activities;
using SolutionFramework.Application.ViewModels.Activities;
using SolutionFramework.EFcore.Model;
using SolutionFramework.Entities.Entities;
using SolutionFramework.SqlServer;
using SolutionFramework.SqlServer.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SolutionFramework.Actitities.Test
{
    public class ApplicationActivityTest
    {
        private readonly IMapper _mapper;
        private readonly SqlServerContext _context;
        private readonly DbContextOptionsBuilder _builder;
        private readonly DbContextOptions _options;
        public ApplicationActivityTest()
        {
            var mapperconfig = new MapperConfiguration(c => c.AddProfile<MappingActivityProfile>());
            _mapper = mapperconfig.CreateMapper();

            //DbContextOptionsBuilder -> Clase que provee EF con el fin de crear un contexto
            _builder = new DbContextOptionsBuilder<SqlServerContext>();
            _options = _builder.UseInMemoryDatabase(nameof(ApplicationActivityTest)).Options;
            _context = new SqlServerContext(_options);
        }
        [Fact]
        public void CreateActivityCommandHandler_RequestNull_ArgumentNullException()
        {

            //Arrange            
            var _logger = new Mock<ILogger<CreateActivityCommandHandler>>();

            var authenticatedUser = new AuthenticatedUser<string>()
            {
                Name = "Pedro",
                Id = Guid.NewGuid().ToString(),
                IsApplication = false,
                Authenticated = true
            };

            var activityRepository = new ActivityRepository(authenticatedUser, _context);

            var handler = new CreateActivityCommandHandler(activityRepository, _mapper, _logger.Object);

            var command = new CreateActivityCommand()
            {
                Description = "sss",
                Name = "sss"
            };

            //Act  && Assert         

            Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task CreateActivityCommandHandler_RequestIsNotNull_ReturnDto()
        {

            //Arrange
            var _logger = new Mock<ILogger<CreateActivityCommandHandler>>();

            var authenticatedUser = new AuthenticatedUser<string>()
            {
                Name = "Pedro",
                Id = Guid.NewGuid().ToString(),
                IsApplication = false,
                Authenticated = true
            };

            var activityRepository = new ActivityRepository(authenticatedUser, _context);

            var handler = new CreateActivityCommandHandler(activityRepository, _mapper, _logger.Object);

            var command = new CreateActivityCommand()
            {
                Description = "Descripcion Actividad de Prueba",
                Name = "Actividad de Prueba"
            };

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ActivityVm>(result);
            Assert.Equal(result.Name, command.Name);
            Assert.Equal(result.Description, command.Description);
            Assert.True(result.State);

        }

        [Fact]
        public async Task UpdateActivityCommandHandler_RequestIsNull_InvalidCastException()
        {

            //Arrange           
            var _logger = new Mock<ILogger<UpdateActivityCommandHandler>>();

            var authenticatedUser = new AuthenticatedUser<string>()
            {
                Name = "Pedro",
                Id = Guid.NewGuid().ToString(),
                IsApplication = false,
                Authenticated = true
            };

            var activityRepository = new ActivityRepository(authenticatedUser, _context);

            var handler = new UpdateActivityCommandHandler(activityRepository, _mapper, _logger.Object);

            var command = new UpdateActivityCommand()
            {
                Description = "sss",
                Name = "sss",
                State = true
            };

            //Act && Assert
            await Assert.ThrowsAsync<InvalidCastException>(() => handler.Handle(null, CancellationToken.None));

        }

        [Fact]
        public async Task UpdateActivityCommandHandler_RequestIsNotNull_SuccessValue()
        {

            //Arrange           
            var _logger = new Mock<ILogger<UpdateActivityCommandHandler>>();

            var authenticatedUser = new AuthenticatedUser<string>()
            {
                Name = "Pedro",
                Id = Guid.NewGuid().ToString(),
                IsApplication = false,
                Authenticated = true
            };

            var activityRepository = new ActivityRepository(authenticatedUser, _context);

            var newEntity = await activityRepository.CreateAsync(new Activity()
            {
                Id = Guid.NewGuid(),
                Name = nameof(Activity.Name),
                Description = nameof(Activity.Description),
                State = true,
                IdUserCreator = Guid.NewGuid().ToString(),
                DateCreated = DateTime.UtcNow,
            });


            var command = new UpdateActivityCommand()
            {
                Description = "Description Updated",
                Name = "Name Updated",
                State = false,
                Id = newEntity
            };

            var handler = new UpdateActivityCommandHandler(activityRepository, _mapper, _logger.Object);

            //Act
            var success = await handler.Handle(command, CancellationToken.None);

            //Assert    

            Assert.True(success);

        }


    }
}
