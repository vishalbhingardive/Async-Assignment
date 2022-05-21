using Day39CaseStudy.DataAccess;
using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Day39CaseStudy.Services.DbService;

public class CrudCategoryService : ICrudService<Category>
{
    public async Task AddAsync(Category cat)
    {
        using var context = new SampleStoreDbContext();

        await context.Categories.AddAsync(cat);
       await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int categoryId)
    {
        using var context = new SampleStoreDbContext();

        var cat = from s in context.Categories
                  where s.CategoryId == categoryId
                  select s;

        if (cat == null)
        {
            Console.WriteLine($"CategoryId {categoryId} not found");
            return;
        }
        context.Categories.Remove(await cat.SingleOrDefaultAsync());
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        //using var context = new SampleStoreDbContext();
        //var categories = from c in context.Categories
        //                 select c;

        //foreach (var category in categories)
        //{
        //    Console.WriteLine($"{category}");

        //}
        //return categories.ToList();

        using var context = new SampleStoreDbContext();

        var list = from s in context.Categories

                   select s;


        return  await list.ToListAsync();

    }

    public async Task<Category> GetByNameAsync(string categoryName)
    {
        using var context = new SampleStoreDbContext();

        var list = from s in context.Categories
                   where s.CategoryName == categoryName
                   select s;

        foreach (var cat in list)
        {
            Console.WriteLine(cat);
            return cat;
        }

        return await list.SingleOrDefaultAsync();

    }

    public async Task UpdateAsync(Category cat)
    {
        using var context = new SampleStoreDbContext();

        context.Categories.Update(cat);
        await context.SaveChangesAsync();
    }

}