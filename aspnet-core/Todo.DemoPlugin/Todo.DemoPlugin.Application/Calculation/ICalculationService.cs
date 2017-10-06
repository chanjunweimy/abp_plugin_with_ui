using Abp.Application.Services;

namespace Todo.DemoPlugin.Calculation
{
    public interface ICalculationService : IApplicationService
    {
        int Calculation_Add(int x, int y);
    }
}
