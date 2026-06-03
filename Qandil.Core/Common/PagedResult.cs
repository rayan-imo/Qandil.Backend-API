namespace Qandil.Core.Common
{
    public record  PagedResult<T>(List<T> Items, int PageNumber, int PageSize, int TotalCount)
    {
        public PagedResult<TResult> MapTo<TResult>(Func<T, TResult> mapFunc)
        {
            return new PagedResult<TResult>(Items.Select(mapFunc).ToList(), PageNumber, PageSize, TotalCount);
        }
    
    } 
}

  