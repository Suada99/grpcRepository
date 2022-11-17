using Calculator;
using Grpc.Core;
using System.Threading.Tasks;
using static Calculator.CalculatorService;

namespace server
{
    public class CalculatorServiceImpl : CalculatorServiceBase
    {
        public override Task<CalculateResponse> Calculate(CalculateRequest request, ServerCallContext context)
        {
            int result = request.FirstNum + request.SecondNum;

            return Task.FromResult(new CalculateResponse { Sum = result});
        }
    }
}
