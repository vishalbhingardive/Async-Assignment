using Day39CaseStudy.DataAccess;
using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Day39CaseStudy.Services.DbService;

public class CrudProductService : ICrudService<Product>
{

    public async Task AddAsync(Product product)
    {
        using var context = new SampleStoreDbContext();

       await context.Products.AddAsync(product);
       await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {



        using var context = new SampleStoreDbContext();

        //return context.Products
        //    .Include("Brand")
        //    .Include("Category")
        //    .OrderBy(p => p.BrandId)
        //        .ThenBy(p => p.ProductId)
        //    .ToList();


        //var result = (from product in context.Products.ToList()
        //             join brand in context.Brands
        //             on product.BrandId equals brand.BrandId
        //             join category in context.Categories
        //             on product.CategoryId equals category.CategoryId
        //             orderby  product.BrandId,
        //             product.ProductId
        //              select product).ToList();

        // return result;

        var result = (from p in context.Products
                      join b in context.Brands
                      on p.BrandId equals b.BrandId
                      join c in context.Categories
                      on p.CategoryId equals c.CategoryId
                      orderby p.BrandId, p.ProductId,p.CategoryId

                      select new Product
                      {
                          ProductId = p.ProductId,
                          ProductName = p.ProductName,
                          BrandId = p.BrandId,
                          CategoryId = p.CategoryId,
                          Brand = b,
                          Category = c,
                          ModelYear = p.ModelYear,
                          ListPrice = p.ListPrice


                      });

        var products = await result.ToListAsync();

        return  products;



    }


    public async Task UpdateAsync(Product product)
    {
        using var context = new SampleStoreDbContext();

        context.Products.Update(product);
       await context.SaveChangesAsync();
    }

    public async Task<Product> GetByNameAsync(string productName)
    {
        using var context = new SampleStoreDbContext();
        var list = from s in context.Products
                   where s.ProductName == productName
                   select s;

        return await list.SingleOrDefaultAsync();

        //var product = context.Products.SingleOrDefault(b => b.ProductName == productName);
        //return product;
    }

    public async Task DeleteAsync(int productId)
    {
        using var context = new SampleStoreDbContext();

        var list = from s in context.Products
                   where s.ProductId == productId
                   select s;

        //var product = context.Products.Find(productId);

        if (list == null)
        {
            Console.WriteLine($"ProductId {productId} not found");

        }

        context.Products.Remove(await list.SingleOrDefaultAsync());
        await context.SaveChangesAsync();
    }


}
