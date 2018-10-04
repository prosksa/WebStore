using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;

namespace WebStore.Infrastructure.Interfaces
{
	public interface IProductData
	{
		IEnumerable<Section> GetSections();
		IEnumerable<Brand> GetBrands();
		IEnumerable<Product> GetProducts(ProductFilter filter);
		Product GetProductById(int id);
	}
}
