using Day39CaseStudy.DataAccess;
using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Day39CaseStudy.Services.DbService;

public class CrudBrandService : ICrudService<Brand>
{



    public async Task AddAsync(Brand brand)
    {
        using var context = new SampleStoreDbContext();

       await context.Brands.AddAsync(brand);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        using var context = new SampleStoreDbContext();

        var list = from s in context.Brands

                   select s;

        return await list.ToListAsync();
    }

    public async Task UpdateAsync(Brand brand)
    {
        using var context = new SampleStoreDbContext();

        context.Brands.Update(brand);
        await context.SaveChangesAsync();
    }

    public async Task<Brand> GetByNameAsync(string brandName)
    {

        using var context = new SampleStoreDbContext();

        var list = from s in context.Brands
                   where s.BrandName == brandName
                   select s;

        return await list.SingleOrDefaultAsync();
    }

    public async Task DeleteAsync(int brandId)
    {
        using var context = new SampleStoreDbContext();

        var brand = from s in context.Brands
                    where s.BrandId == brandId
                    select s;
        //var brand = context.Brands.Find(brandId);

        if (brand == null)
        {
            Console.WriteLine($"BrandId {brandId} not found");
            return;
        }

        context.Brands.Remove(await brand.SingleOrDefaultAsync());
        await context.SaveChangesAsync();
    }
}
