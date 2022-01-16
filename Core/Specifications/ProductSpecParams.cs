namespace Core.Specifications
{
    public class ProductSpecParams
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < MaxPageSize ? value : MaxPageSize;
        }

        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string Sort { get; set; }

        private const int MaxPageSize = 20;

        private int _pageSize = 6;
    }
}
