using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SubMenu
{
    public static string selectedMenu;
    public static void bookingSubMenu(string MainMenu)
    {
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "****" + MainMenu + "****"; ;
        Config.alignCenter(heading);
        ConsoleCustomise.singleTextCenter("1 - Add " + MainMenu);
        ConsoleCustomise.singleTextCenter("2 - View " + MainMenu);
        ConsoleCustomise.singleTextCenter("3 - Edit " + MainMenu);
        ConsoleCustomise.singleTextCenter("4 - Sort " + MainMenu);
        ConsoleCustomise.singleTextCenter("5 - Search " + MainMenu);
        ConsoleCustomise.singleTextCenter("6 - Main Menu ");
        subMenuSelect(6, MainMenu);
    }
    public static void customerSubMenu(string MainMenu)
    {
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "****" + MainMenu + "****"; ;
        Config.alignCenter(heading);
        ConsoleCustomise.singleTextCenter("1 - Add " + MainMenu);
        ConsoleCustomise.singleTextCenter("2 - View " + MainMenu);
        ConsoleCustomise.singleTextCenter("3 - Edit " + MainMenu);
        ConsoleCustomise.singleTextCenter("4 - Delete " + MainMenu);
        ConsoleCustomise.singleTextCenter("5 - Sort " + MainMenu);
        ConsoleCustomise.singleTextCenter("6 - Search " + MainMenu);
        ConsoleCustomise.singleTextCenter("7 - Main Menu ");
        subMenuSelect(7, MainMenu);
    }
    public static void staffSubMenu(string MainMenu)
    {
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "****" + MainMenu + "****"; ;
        Config.alignCenter(heading);
        ConsoleCustomise.singleTextCenter("1 - Add " + MainMenu);
        ConsoleCustomise.singleTextCenter("2 - View " + MainMenu);
        ConsoleCustomise.singleTextCenter("3 - Edit " + MainMenu);
        ConsoleCustomise.singleTextCenter("4 - Delete " + MainMenu);
        ConsoleCustomise.singleTextCenter("5 - Sort " + MainMenu);
        ConsoleCustomise.singleTextCenter("6 - Search " + MainMenu);
        ConsoleCustomise.singleTextCenter("7 - Main Menu ");
        subMenuSelect(7, MainMenu);
    }
    public static void subMenuSelect(int totalMenu,string selectedMenuName)
    {
        int integer;
        int attempt = 0;
        int selectedMenuInteger = 0;
        Customer customer = new Customer();
        Staff staff = new Staff();
        Booking booking = new Booking();
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
        selectedMenuName = (selectedMenuInteger == 7) ? "Main Menu" : selectedMenuName;
        switch (selectedMenuInteger)
        {
            case 1:
                if(selectedMenuName == "Customer")
                {
                    customer.getCutomerDetails();
                }else if (selectedMenuName == "Staff")
                {
                    staff.getStaffDetails();
                }
                else if (selectedMenuName == "Booking")
                {
                    booking.getBooking();
                }
                break;
            case 2:
                if (selectedMenuName == "Customer")
                {
                    customer.viewCustomers();
                }
                else if (selectedMenuName == "Staff")
                {
                    staff.viewStaffs();
                }
                else if (selectedMenuName == "Booking")
                {
                    booking.viewBookings();
                }
                break;
            case 3:
                if (selectedMenuName == "Customer")
                {
                    customer.editCustomers();
                }
                else if (selectedMenuName == "Staff")
                {
                    staff.editStaff();
                }
                else if (selectedMenuName == "Booking")
                {
                    booking.editBooking();
                }
                break;
            case 4:
                if (selectedMenuName == "Customer")
                {
                    customer.deleteCustomer();
                }
                else if (selectedMenuName == "Staff")
                {
                    staff.deletestaff();
                }
                else if (selectedMenuName == "Booking")
                {
                    booking.sortBooking();
                }
                break;
            case 5:
                if (selectedMenuName == "Customer")
                {
                    customer.sortCustomers();
                }
                else if (selectedMenuName == "Staff")
                {
                    staff.sortStaff();
                }
                if (selectedMenuName == "Booking")
                {
                    booking.searchBookings();
                }
                break;
            case 6:
                if (selectedMenuName == "Customer")
                {
                    customer.searchCustomer();
                }
                if (selectedMenuName == "Staff")
                {
                    staff.searchStaffs();
                }
                if (selectedMenuName == "Booking")
                {
                    Menus.menuLists();
                }
                break;
            case 7:
                if (selectedMenuName == "Main Menu")
                {
                    Menus.menuLists();
                }
                break;
            default:
                Console.WriteLine("Please choose the correct menu!");

                break;
        }

    }
}

