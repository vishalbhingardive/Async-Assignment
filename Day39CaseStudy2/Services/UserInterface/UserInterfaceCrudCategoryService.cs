using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;
using Day39CaseStudy.Services.Factory;

namespace Day39CaseStudy.Services.UserInterface;

public class UserInterfaceCrudCategoryService
{
    readonly ICrudService<Category> _categoryService;

    public UserInterfaceCrudCategoryService()
    {
        _categoryService = CrudFactory.Create<Category>();
    }

    public async Task AddAsync()
    {
        Console.WriteLine("Adding New Category");
        Console.WriteLine("-------------------");

        Console.Write("Enter Category Name:");
        var categoryText = Console.ReadLine();

        var cat = new Category { CategoryName = categoryText };
        await _categoryService.AddAsync(cat);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categoryService.GetAllAsync();
    }

    public async Task UpdateAsync()
    {
        Console.WriteLine("Updating Existing Category");
        Console.WriteLine("--------------------------");

        Console.WriteLine("Enter Category Name To Update");
        var categoryText = Console.ReadLine();

        var cat = await _categoryService.GetByNameAsync(categoryText);

        if (cat == null)
        {
            Console.WriteLine($"Category Name {categoryText} Not Found !!");
            return;
        }
        Console.WriteLine($"Found Category: {cat}");

        Console.WriteLine("Enter Category Name to change: ");
        var changeCategoryNameText = Console.ReadLine();

        cat.CategoryName = changeCategoryNameText;

         await _categoryService.UpdateAsync(cat);
    }


    public async Task DeleteAsync()
    {
        Console.WriteLine("Deleting existing Category");
        Console.WriteLine("--------------------------");

        Console.WriteLine("Enter the Category Id to Delete: ");
        var categoryIdText = Console.ReadLine();

        var categoryId = int.Parse(categoryIdText);

        try
        {
            await _categoryService.DeleteAsync(categoryId);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Delete Category Failed!! {ex.Message}");
            Console.ResetColor();
        }
    }

    public async Task ShowAsync()
    {
        var cat = await _categoryService.GetAllAsync();

        Console.WriteLine("Category list");
        Console.WriteLine("==================================");

        Console.WriteLine(Category.Header);

        Console.WriteLine("==================================");

        foreach (var item in cat)
        {
            Console.WriteLine(item);
            Console.WriteLine("----------------------------------");

        }
        Console.WriteLine("------------------");

    }

}

