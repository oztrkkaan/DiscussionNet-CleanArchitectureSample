
namespace DiscussionNet.Domain.Common
{

    public interface IPagedList
    {
        int PageCount { get; }
        int TotalCount { get; }
        int PageSize { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        bool IsFirstPage { get; }
        bool IsLastPage { get; }
        int ItemStart { get; }
        int ItemEnd { get; }
        int PageNumber { get; }
    }

    public interface IPagedList<T> : IPagedList, IList<T> { }

    public class PagedList<T> : List<T>, IPagedList<T>
    {
        //Properties
        public int PageCount { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }

        public bool HasPreviousPage { get; private set; }
        public bool HasNextPage { get; private set; }
        public bool IsFirstPage { get; private set; }
        public bool IsLastPage { get; private set; }

        public int ItemStart { get; private set; }
        public int ItemEnd { get; private set; }
        public int PageNumber { get { return PageIndex + 1; } }
        public int PageIndex { get; private set; }

        //Constructors
        public PagedList(IEnumerable<T> source, int pageNumber, int pageSize)
            : this(source.AsQueryable(), pageNumber, pageSize)
        {
        }
        public PagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            //Error Checking
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", "Value can not be less than 1");
            if (source == null)
                source = new List<T>().AsQueryable();
            if (pageNumber < 1) pageNumber = 1;

            //Paging Info
            PageSize = pageSize;
            PageIndex = pageNumber - 1;
            TotalCount = source.Count();
            PageCount = TotalCount > 0 ? (int)Math.Ceiling(TotalCount / (double)pageSize) : 0;

            //Navigation Info
            HasPreviousPage = PageIndex > 0;
            HasNextPage = PageIndex < PageCount - 1;
            IsFirstPage = PageIndex <= 0;
            IsLastPage = PageIndex >= PageCount - 1;

            ItemStart = PageIndex * PageSize + 1;
            ItemEnd = Math.Min(PageIndex * PageSize + PageSize, TotalCount);

            //If Source is Empty
            if (TotalCount <= 0)
                return;

            int totalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            if (PageIndex >= totalPages)
                AddRange(source.Skip((totalPages - 1) * PageSize).Take(PageSize));
            else
                AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));
        }
    }
}
