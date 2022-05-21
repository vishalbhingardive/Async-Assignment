using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;
using Day39CaseStudy.Services.Factory;

namespace Day39CaseStudy.Services.UserInterface;

public class UserInterfaceCrudBrandService
{
    //CrudBrandService _brandService;           // TIGHTLY BOUND. VERY BAD

    readonly ICrudService<Brand> _brandService; // LOOSELY BOUND. VERY GOOD

    public UserInterfaceCrudBrandService()
    {
        //_brandService = new CrudBrandService();       // TIGHTLY BOUND. VERY BAD

        _brandService = CrudFactory.Create<Brand>();    // LOOSELY BOUND. VERY GOOD
    }

    public async Task AddAsync()
    {
        Console.WriteLine("Adding New Brand");
        Console.WriteLine("----------------");

        Console.Write("Enter Brand Name: ");
        var brandNameText = Console.ReadLine();

        var brand = new Brand { BrandName = brandNameText };

       await _brandService.AddAsync(brand);
    }

    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        return await _brandService.GetAllAsync();
    }

    public async Task UpdateAsync()
    {
        Console.WriteLine("Updating existing Brand");
        Console.WriteLine("-----------------------");

        Console.Write("Enter Brand Name to Update: ");
        var brandNameText = Console.ReadLine();

        var brand = await _brandService.GetByNameAsync(brandNameText);

        if (brand == null)
        {
            Console.WriteLine($"Brand Name {brandNameText} not found!!");
            return;
        }

        Console.WriteLine($"Found Brand: {brand}");

        Console.Write("Enter Brand Name to change: ");
        var changedBrandNameText = Console.ReadLine();

        brand.BrandName = changedBrandNameText;

       await _brandService.UpdateAsync(brand);
    }

    public async Task DeleteAsync()
    {
        Console.WriteLine("Deleting existing Brand");
        Console.WriteLine("-----------------------");

        Console.Write("Enter the Brand Id to delete: ");
        var brandIdText = Console.ReadLine();

        var brandId = int.Parse(brandIdText);

        try
        {
            await _brandService.DeleteAsync(brandId);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Delete Brand Failed!! {ex.Message}");
            Console.ResetColor();
        }
    }

    public async Task ShowAsync()
    {
        var brands = await _brandService.GetAllAsync();

        Console.WriteLine("Brand List");
        Console.WriteLine("=========================");

        Console.WriteLine(Brand.Header);
        Console.WriteLine("=========================");
        foreach (var brand in brands)
        {
            Console.WriteLine(brand);
            Console.WriteLine("-------------------------");
        }
        Console.WriteLine("-------------------------");
    }
}
