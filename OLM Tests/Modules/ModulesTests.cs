using Microsoft.AspNetCore.Mvc;
using Moq;
using Online_Learning_Management.Application.Modules.Responses;
using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Domain.Exceptions.Module;
using Online_Learning_Management.Domain.Interfaces.Modules;
using Online_Learning_Management.Infrastructure.DTOs.Module;
using Online_Learning_Management.Presentation.Controllers;

namespace OLM_Tests.Modules
{
    public class ModulesTests
    {
        private readonly Mock<IModuleService> _moduleService;
        private readonly ModulesController _modulesController;

        public ModulesTests()
        {
            _moduleService = new Mock<IModuleService>();
            _modulesController = new ModulesController(_moduleService.Object);
        }

        [Fact]
        public async Task GetAllModules_ReturnsOkResult_WithListOfModules()
        {
            var modules = new List<Module> { new Module { Id = Guid.NewGuid(), Name = "Test Module" } };
            _moduleService.Setup(service => service.GetAllModulesAsync()).ReturnsAsync(modules);

            var result = await _modulesController.GetAllModules();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Module>>(okResult.Value);
            Assert.Equal(modules, returnValue);
        }

        [Fact]
        public async Task GetModuleById_ReturnsOkResult_WithModule()
        {
            var moduleId = Guid.NewGuid();
            var module = new Module { Id = moduleId, Name = "Test Module" };
            _moduleService.Setup(service => service.GetModuleByIdAsync(moduleId)).ReturnsAsync(module);

            var result = await _modulesController.GetModuleById(moduleId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Module>(okResult.Value);
            Assert.Equal(module, returnValue);
        }

        [Fact]
        public async Task GetModuleById_ReturnsNotFound_WhenModuleNotFound()
        {
            var moduleId = Guid.NewGuid();
            _moduleService.Setup(service => service.GetModuleByIdAsync(moduleId)).ThrowsAsync(new ModuleNotFoundException());

            var result = await _modulesController.GetModuleById(moduleId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Module not found", notFoundResult.Value);
        }

        [Fact]
        public async Task AddModule_ReturnsOkResult_WithApiResponse()
        {
            var createModuleDto = new CreateModuleDTO { Name = "New Module" };
            var module = new Module { Id = Guid.NewGuid(), Name = "New Module" };
            _moduleService.Setup(service => service.AddModuleAsync(createModuleDto)).ReturnsAsync(module);

            var result = await _modulesController.AddModule(createModuleDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.Equal("User created successfully", returnValue.Message);
            Assert.Equal(module, returnValue.Data);
        }

        [Fact]
        public async Task UpdateModule_ReturnsOkResult_WithApiResponse()
        {
            var moduleId = Guid.NewGuid();
            var updateModuleDto = new UpdateModuleDTO { Name = "Updated Module" };
            var module = new Module { Id = moduleId, Name = "Updated Module" };
            _moduleService.Setup(service => service.UpdateModuleAsync(moduleId, updateModuleDto)).ReturnsAsync(module);

            var result = await _modulesController.UpdateModule(moduleId, updateModuleDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ApiResponse>(okResult.Value);
            Assert.Equal("User updated successfully", returnValue.Message);
            Assert.Equal(module, returnValue.Data);
        }

        [Fact]
        public async Task UpdateModule_ReturnsNotFound_WhenModuleNotFound()
        {
            var moduleId = Guid.NewGuid();
            var updateModuleDto = new UpdateModuleDTO { Name = "Updated Module" };
            _moduleService.Setup(service => service.UpdateModuleAsync(moduleId, updateModuleDto)).ThrowsAsync(new ModuleNotFoundException());

            var result = await _modulesController.UpdateModule(moduleId, updateModuleDto);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Module not found", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteModule_ReturnsNoContent()
        {
            var moduleId = Guid.NewGuid();
            _moduleService.Setup(service => service.DeleteModuleAsync(moduleId)).Returns(Task.CompletedTask);

            var result = await _modulesController.DeleteModule(moduleId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteModule_ReturnsNotFound_WhenModuleNotFound()
        {
            var moduleId = Guid.NewGuid();
            _moduleService.Setup(service => service.DeleteModuleAsync(moduleId)).ThrowsAsync(new ModuleNotFoundException());

            var result = await _modulesController.DeleteModule(moduleId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Module not found", notFoundResult.Value);
        }
    }
}
