using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Runtime.Session;

namespace Todo.DemoPlugin.Calculation
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public class CalculationService : ApplicationService, ICalculationService
    {
        public int Calculation_Add(int x, int y)
        {
            return x + y;
        }
    }
}