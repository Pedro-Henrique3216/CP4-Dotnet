using Microsoft.Extensions.DependencyInjection;
using MottuChallenge.Application.UseCases.Addresses;
using MottuChallenge.Application.UseCases.Employees;
using MottuChallenge.Application.UseCases.Sectors;
using MottuChallenge.Application.UseCases.SectorTypes;
using MottuChallenge.Application.UseCases.Spots;
using MottuChallenge.Application.UseCases.Yards;

namespace MottuChallenge.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<CreateSectorUseCase>();
        services.AddScoped<GetAllSectorsUseCase>();
        services.AddScoped<GetSectorByIdUseCase>();
        services.AddScoped<CreateSectorTypeUseCase>();
        services.AddScoped<DeleteSectorTypeUseCase>();
        services.AddScoped<GetAllSectorTypesUseCase>();
        services.AddScoped<UpdateSectorTypeUseCase>();
        services.AddScoped<CreateYardUseCase>();
        services.AddScoped<GetAllYardsUseCase>();
        services.AddScoped<GetYardByIdUseCase>();
        services.AddScoped<GetAddressByIdUseCase>();
        services.AddScoped<SpotUseCase>();
        services.AddScoped<CreateEmployeeUseCase>();
        services.AddScoped<GetAllEmployeesUseCase>();
        services.AddScoped<GetEmployeeByEmailUseCase>();
        services.AddScoped<UpdateEmployeeUseCase>();
        services.AddScoped<DeleteEmployeeUseCase>();
            
        return services;
    }
}