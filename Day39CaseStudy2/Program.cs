// See https://aka.ms/new-console-template for more information

using Day39CaseStudy.Services.Menu;
using Day39CaseStudy.Services.UserInterface;


/*
Requirement: 
1. Create a CRUD Screen for Brand & Product
2. Display a report of brand wise products
 */


IMenuService menuService = new MenuService();
var uiBrandService = new UserInterfaceCrudBrandService();
var uiProductService = new UserInterfaceCrudProductService();
var uiCategoryService = new UserInterfaceCrudCategoryService();

do
{
    var menuOptions = menuService.Show();

    switch (menuOptions)
    {
        case MenuOptions.Exit:
            return;
        case MenuOptions.BrandAdd:
            uiBrandService.AddAsync();
            break;
        case MenuOptions.BrandUpdate:
            uiBrandService.UpdateAsync();
            break;
        case MenuOptions.BrandDelete:
            uiBrandService.DeleteAsync();
            break;
        case MenuOptions.BrandShow:
            uiBrandService.ShowAsync();
            break;
        case MenuOptions.ProductAdd:
            uiProductService.AddAsync();
            break;
        case MenuOptions.ProductUpdate:
            uiProductService.UpdateAsync();
            break;
        case MenuOptions.ProductDelete:
            uiProductService.DeleteAsync();
            break;
        case MenuOptions.ProductShow:
            uiProductService.ShowAsync();
            break;
        case MenuOptions.CategoryAdd:
            uiCategoryService.AddAsync();
            break;
        case MenuOptions.CategoryUpdate:
            uiCategoryService.UpdateAsync();
            break;
        case MenuOptions.CategoryDelete:
            uiCategoryService.DeleteAsync();
            break;
        case MenuOptions.CategoryShow:
            uiCategoryService.ShowAsync();
            break;

    }

} while (true);