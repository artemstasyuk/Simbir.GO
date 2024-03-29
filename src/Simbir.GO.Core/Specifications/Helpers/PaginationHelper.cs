using Simbir.GO.Server.ApplicationCore.Specifications.Base;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Helpers;

public static class PaginationHelper
{
    private const int DefaultPage = 1;
    
    private const int DefaultPageSize = 10;

    private static int CalculateTake(int pageSize)
    {
        return pageSize <= 0 ? DefaultPageSize : pageSize;
    }

    private static int CalculateSkip(int pageSize, int page)
    {
        page = page <= 0 ? DefaultPage : page;

        return CalculateTake(pageSize) * (page - 1);
    }

    public static int CalculateTake(BaseFilter baseFilter)
    {
        return CalculateTake(baseFilter.Count);
    }
    public static int CalculateSkip(BaseFilter baseFilter)
    {
        return CalculateSkip(baseFilter.Count, baseFilter.Start);
    }
}