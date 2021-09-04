using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStock.Util
{
    public static class QueryableExtension
    {

        public static async Task<PagedResponse<TDestination>> GetPagedAsync<T, TDestination>(this IQueryable<T> query, PagedRequest pageRequest, IMapper _mapper)
                                         where T : class
                                         where TDestination : class, new()
        {
            var queryData = query.ProjectTo<TDestination>(_mapper.ConfigurationProvider);
            if (pageRequest.PageSize != 0)
                queryData = queryData.TakePage(pageRequest.PageNumber, pageRequest.PageSize);
            var totalRecords = query.Count();

            return new PagedResults<TDestination>
            {
                PageNumber = pageRequest.PageNumber,
                PageSize = pageRequest.PageSize == 0 ? totalRecords : pageRequest.PageSize,
                TotalNumberOfRecords = totalRecords,
                Results = await queryData.ToListAsync()
            };
        }
    }
}
