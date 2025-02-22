using Library.Models.Common;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Library.DataAccess.Extensions;

public static class FuncResult
{
    public static async ValueTask<ResultModel<T>> GetResultAsync<T>(this ValueTask<T> func)
    {
        try
        {
            return new ResultModel<T>(await func);
        }
        catch(Exception ex)
        {
            return new ResultModel<T>(ex.Message);
        }
    }
}
