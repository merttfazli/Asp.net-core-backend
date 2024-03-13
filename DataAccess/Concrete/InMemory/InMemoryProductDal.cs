using DataAccess.Abstract;
using Entitites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
              _products = new List<Product>
              {
                  new Product{ProductId=1,CategoryId=1,ProductName="Telefon",UnitPrice=300,UnitsInStock=15},
                  new Product{ProductId=2,CategoryId=1,ProductName="Bilgisayar",UnitPrice=300,UnitsInStock=20},
                  new Product{ProductId=3,CategoryId=1,ProductName="Klavye",UnitPrice=100,UnitsInStock=10},
                  new Product{ProductId=4,CategoryId=1,ProductName="Mouse",UnitPrice=50,UnitsInStock=13},
                  new Product{ProductId=5,CategoryId=1,ProductName="Kulaklık",UnitPrice=150,UnitsInStock=14},
              };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete=_products.SingleOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(x=>x.CategoryId==categoryId).ToList();
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName= product.ProductName;
            productToUpdate.CategoryId= product.CategoryId;
            productToUpdate.UnitPrice= product.UnitPrice;
            productToUpdate.UnitsInStock= product.UnitsInStock;
        }
    }
}
