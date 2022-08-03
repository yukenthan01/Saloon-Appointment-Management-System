using System;
using System.Collections.Generic;

class Menus
{
    public static List<string> menus = new List<string>() { "Booking","Customer", "Staff", "Staff Availability" };
    public static string selectedMenu;
    public static void menuLists()
    {
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Menu ****"; ;
        Config.alignCenter(heading);
        int verticalStart = (Console.WindowHeight - menus.Count) / 2;
        int position = verticalStart;
        for (int i = 0; i < menus.Count; i++)
        {
            string singleMenu = i + 1 +"-"+menus[i];
            Config.alignCenter(singleMenu);
        }
        mainMenuSelect(menus.Count);
    }
    public static void mainMenuSelect(int totalMenu)
    {
        int integer;
        int attempt = 0;
        int selectedMenuInteger = 0;
        do
        {
            if (attempt > 0)
            {
                Console.WriteLine("Please Check your Menu Option Number! \n");
            }
            Console.Write("Please Select The Menu Number : ");
            selectedMenu = Console.ReadLine();
            
            if (int.TryParse(selectedMenu, out integer))
            {
                selectedMenuInteger = int.Parse(selectedMenu);
            }
            attempt++;
        }
        while (selectedMenuInteger > totalMenu || !int.TryParse(selectedMenu, out integer));

        switch (selectedMenuInteger)
        {
            case 1:
                SubMenu.bookingSubMenu(menus[selectedMenuInteger - 1]);
                break;
            case 2:
                SubMenu.customerSubMenu(menus[selectedMenuInteger - 1]);
                break;
            case 3:
                SubMenu.staffSubMenu(menus[selectedMenuInteger - 1]);
                break;
            case 4:
                StaffAvailability staffAvailability = new StaffAvailability();
                staffAvailability.viewStaffAvailability();
                break;
            default:
                Console.WriteLine("Please choose the correct menu!");
                
                break;
        }

    }
}

