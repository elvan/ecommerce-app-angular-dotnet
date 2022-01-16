using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification(ProductSpecParams productParams)
            : base(x =>
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);

            AddOrderBy(product => product.Name);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(product => product.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(product => product.Price);
                        break;
                    default:
                        AddOrderBy(product => product.Name);
                        break;
                }
            }
        }

        public ProductWithTypesAndBrandsSpecification(int id)
            : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
        }
    }
}
