namespace Core.Specifications.App
{
    public class ProductSpecParams
    {
        public int PageIndex { get; set; } = 1;

        private const int MaxPageSize = 50;

        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize ? MaxPageSize : value);
        }

        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string Sort { get; set; }
    }
}