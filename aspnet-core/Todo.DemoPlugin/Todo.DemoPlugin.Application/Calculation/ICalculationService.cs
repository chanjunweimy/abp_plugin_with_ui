using Abp.Application.Services;

namespace Todo.DemoPlugin.Calculation
{
    public interface ICalculationService : IApplicationService
    {
        double Calculation_Add(double x, double y);
    }
}
