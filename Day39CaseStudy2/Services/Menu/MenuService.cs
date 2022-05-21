namespace Day39CaseStudy.Services.Menu;

public enum MenuOptions
{
    Unknown = -1,
    Exit = 0,
    BrandAdd = 1,
    BrandUpdate = 2,
    BrandDelete = 3,
    BrandShow = 4,
    ProductAdd = 5,
    ProductUpdate = 6,
    ProductDelete = 7,
    ProductShow = 8,
    CategoryAdd = 9,
    CategoryUpdate = 10,
    CategoryDelete = 11,
    CategoryShow = 12,

}

public class MenuService : IMenuService
{
    private readonly string[] _mainMenuItems =
    {
        "1) Brand",
        "2) Product",
        "3) Category",
        "0) Exit"
    };

    private readonly string[] _subMenuItems =
    {
        "1) Add",
        "2) Update",
        "3) Delete",
        "4) Show",
        "0) Go Back"
    };

    public MenuOptions Show()
    {
        MenuOptions menuOption;

        do
        {
            var menuId = ShowMenuListing("=== Main Menu ===", _mainMenuItems);

            switch (menuId)
            {
                case 0:
                    return MenuOptions.Exit;
                case -1:
                    continue;
            }

            var selectedMainMenuText = GetMenuText(_mainMenuItems[menuId - 1]);
            var subMenuId = ShowMenuListing(selectedMainMenuText, _subMenuItems);

            if (subMenuId == 0 || subMenuId == -1)
                continue;

            menuOption = CalculateMenuOption(menuId, subMenuId);

            if (menuOption == MenuOptions.Unknown)
                continue;

            break;
        } while (true);


        ShowInformationMessage(menuOption.ToString());
        return menuOption;
    }

    private static void ShowInformationMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private MenuOptions CalculateMenuOption(int menuId, int subMenuId)
    {
        var masterMenuId = (menuId - 1) * _mainMenuItems.Length + subMenuId;

        var selectedMenuOption = (MenuOptions)masterMenuId;

        var minEnumValue = Enum.GetValues(typeof(MenuOptions)).Cast<int>().Min();
        var maxEnumValue = Enum.GetValues(typeof(MenuOptions)).Cast<int>().Max();

        if ((int)selectedMenuOption >= minEnumValue && (int)selectedMenuOption <= maxEnumValue)
            return selectedMenuOption;

        ShowErrorMessage("Invalid Menu Item Selected");
        return MenuOptions.Unknown;
    }

    private static void ShowErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.Beep();
        Console.ResetColor();
    }

    private static int ShowMenuListing(string title, string[] menuItems)
    {
        ShowTitle(title);

        foreach (var mainMenuItem in menuItems)
            Console.WriteLine(GetMenuText(mainMenuItem));

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Select an option: ");
        var selectedMenu = Console.ReadKey();
        Console.ResetColor();

        Console.WriteLine();
        var menuId = selectedMenu.KeyChar - '0';

        if (menuId >= 0 && menuId < menuItems.Length) return menuId;

        ShowErrorMessage("Invalid Menu Item Selected");
        return -1;
    }

    private static void ShowTitle(string title)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        var line = new string('-', title.Length);
        Console.WriteLine(line);
        Console.WriteLine(title);
        Console.WriteLine(line);
        Console.ResetColor();
    }

    private static string GetMenuText(string menuItem)
    {
        return $"| {menuItem,-13} |";
    }
}
