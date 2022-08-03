using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Booking
{
    [JsonProperty]
    private int id;
    public int Id   
    {
        get { return id; }
        set { id = value; }
    }

    [JsonProperty]
    private int customerId;
    public int CustomerId   
    {
        get { return customerId; }
        set { customerId = value; }
    }
    [JsonProperty]
    private string date = "";
    public string Date   
    {
        get { return date; }
        set { date = value; }
    }
    [JsonProperty]
    private string time = "";
    public string Time  
    {
        get { return time; }
        set { time = value; }
    }
    [JsonProperty]
    private int staffid ;
    public int StaffId 
    {
        get { return staffid; }
        set { staffid = value; }
    }
    [JsonProperty]
    private string status;
    public string Status
    {
        get { return status; }
        set { status = value; }
    }
    [JsonProperty]
    private string service;
    public string Service
    {
        get { return service; }
        set { service = value; }
    }
    public const string FilePath = "booking/booking.json";
    public const string StaffFilePath = "staff/staff.json";
    public const string CustomerFilePath = "customer/customer.json";
    public static List<string> services = new List<string>() { "Barbering", "Hair Colour", "Hair Dressing", "Nail & Beauty" };


    public Booking()
    {
        if (!File.Exists(FilePath))
        {
            var createFile = File.Create(FilePath);
            createFile.Close();
        }
    }
    public void editBooking()
    {
        Booking booking = new Booking();
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Update Booking Details ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        string id = Config.fieldValidator("Booking ID", "[1-9]|10", "Not a valid Booking ID, try again.\n");
        int bookingId = int.Parse(id);
        var bookings = JsonDeserialize.bookingJsonDeserializer(FilePath);
        
        if (bookings.Exists(a => a.Id == bookingId))
        {
            
            var table = new ConsoleTable("Booking ID", "Date", "Time", "Staff ID", "Staff Name", "Customer ID", "CUstomer Name");
            Console.WriteLine("1- Booking ID : " + bookings.FirstOrDefault(a => a.Id == bookingId).Id + "\n");
            Console.WriteLine("2- Date : " + bookings.FirstOrDefault(a => a.Id == bookingId).Date + "\n");
            Console.WriteLine("3- Time : " + bookings.FirstOrDefault(a => a.Id == bookingId).Time + "\n");
            Console.WriteLine("4- Staff ID : " + bookings.FirstOrDefault(a => a.Id == bookingId).staffid + "\n");
            Console.WriteLine("5- Customer ID : " + bookings.FirstOrDefault(a => a.Id == bookingId).CustomerId + "\n");
            Console.WriteLine("6- Status : " + bookings.FirstOrDefault(a => a.Id == bookingId).Status + "\n");
            int index = bookings.FindIndex(a => a.Id == bookingId);
            if (bookings.FirstOrDefault(a => a.Id == bookingId).Status == "booked")
            {
                string confirmQuestion = "Do you want update cancelled?";
                string confirm = Config.confirmationDetails(confirmQuestion);
                if (confirm == "y")
                {
                    bookings.Add(new Booking()
                    {
                        Id = bookings.FirstOrDefault(a => a.Id == bookingId).Id,
                        Date = bookings.FirstOrDefault(a => a.Id == bookingId).Date,
                        Time = bookings.FirstOrDefault(a => a.Id == bookingId).Time,
                        CustomerId = bookings.FirstOrDefault(a => a.Id == bookingId).customerId,
                        StaffId = bookings.FirstOrDefault(a => a.Id == bookingId).staffid,
                        Service = bookings.FirstOrDefault(a => a.Id == bookingId).Service,
                        Status = "cancelled" 
                    
                    }); ;
                    bookings.Remove(bookings[index]);
                    JsonSerialize.updateBookingJsonSerializer(bookings, FilePath);
                    SubMenu.bookingSubMenu("Booking");
                }
                else if (confirm == "n")
                {
                    SubMenu.bookingSubMenu("Booking");
                }
            }
            else
            {
                Console.WriteLine("Already Booking Cancelled!!");
                Config.backEnterkey("Press enter to search again..");
                SubMenu.bookingSubMenu("Booking");
            }
            
        }
        else
        {
            Console.WriteLine("Booking ID not found!!");
            Config.backEnterkey("Press enter to search again..");
            editBooking();
        }
    }

    public void searchBookings()
    {
        int filed = 0;
        string searchValue ;
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Search Booking Details****";
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        string dob = Config.fieldValidator("Date of Birth (dd/mm/yyyy)", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date of birth, try again.\n");
        string name = Config.fieldValidator("Fullname", @"^[a-zA-Z\s]+$", "Invalid character, try again.");
        var customers = JsonDeserialize.JsonDeserializer(CustomerFilePath);
        var staff = JsonDeserialize.staffJsonDeserializer(StaffFilePath);
        var bookings = JsonDeserialize.bookingJsonDeserializer(FilePath);
        int index = customers.FindIndex(a => (a.Dob == dob) && (a.Name == name));
        int customerId = customers[index].Id;
        if (customers.Exists(a => (a.Dob == dob) && (a.Name == name)))
        {
            
          bookings = bookings.Where(x => x.CustomerId == customerId).ToList();
            var table = new ConsoleTable("Booking ID", "Date", "Time", "Staff ID", "Staff Name", "Customer ID", "CUstomer Name");
            for (int i = 0; i < bookings.Count; i++)
            {
                table.AddRow(
                    bookings[i].Id,
                    bookings[i].Date,
                    bookings[i].Time,
                    bookings[i].staffid,
                    staff.FirstOrDefault(x => x.Id == bookings[i].StaffId).Name,
                    bookings[i].customerId,
                    customers[index].Name
                );
            }
            table.Write(Format.Alternative);
            string confirmQuestion = "\nDo you want search again? ";
            string confirm = Config.confirmationDetails(confirmQuestion);
            if (confirm == "y")
            {
                searchBookings();
            }
            else if (confirm == "n")
            {
                SubMenu.bookingSubMenu("Booking");
            }

        }
        else
        {
            Console.WriteLine("Customer NOT found!!");
            string searchAgaing = "Do you want search again? ";
            string confirmBooking = Config.confirmationDetails(searchAgaing);
            if (confirmBooking == "y")
            {
                searchBookings();
            }
            else if (confirmBooking == "n")
            {
                SubMenu.bookingSubMenu("Booking");
            }
        }
        //customers = customers.OrderByDescending(x => x.Id).ToList();
        //var bookings1 = bookings.Where(x => (x.searchValue == "booked") ).ToList();
       // Console.WriteLine(bookings1.Count);
        
    }
    public void viewBookings()
    {
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** View Booking Details ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        var bookings = JsonDeserialize.bookingJsonDeserializer(FilePath);
        var customer = JsonDeserialize.JsonDeserializer(CustomerFilePath);
        var staff = JsonDeserialize.staffJsonDeserializer(StaffFilePath);
        
        bookings.OrderByDescending(x => x.Id).ToList();
        var table = new ConsoleTable("Booking ID", "Date", "Time", "Service", "Staff ID", "Staff Name", "Customer ID", "CUstomer Name");
        //string ss = bookings[0].CustomerId;
        for (int i = 0; i < bookings.Count; i++)
        {
            table.AddRow(
                bookings[i].Id,
                bookings[i].Date,
                bookings[i].Time,
                bookings[i].Service,
                bookings[i].staffid,
                staff.FirstOrDefault(x => x.Id == bookings[i].StaffId).Name,
                bookings[i].customerId,
                customer.FirstOrDefault(x => x.Id == bookings[i].CustomerId).Name
            );
        }
        table.Write(Format.Alternative);
        Console.Write("Press any key for back..");
        Console.ReadKey();
        SubMenu.bookingSubMenu("Booking");
    }

    public void sortBooking()
    {
        int filed = 0;
        int type = 0;
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Sort Booking Details****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        Console.WriteLine("Sort by");
        Console.WriteLine("1- ID");
        Console.WriteLine("2- Date");
        Console.WriteLine("3- Time");
        Console.WriteLine("4- Staff ID");
        Console.WriteLine("5- Customer ID");
        //Console.Write("Choose field? ");
        filed = Config.questionNumberValidation(5, "Choose field? ", "Please check the field option");
        Console.WriteLine("\nChoose the order for sorting");
        Console.WriteLine("1- Ascending");
        Console.WriteLine("2- Descending\n");
        type = Config.questionNumberValidation(2, "Enter the sorting type? ", "Please check the sorting option");

        var bookings = JsonDeserialize.bookingJsonDeserializer(FilePath);
        //customers = customers.OrderByDescending(x => x.Id).ToList();

        if (type == 1)
        {
            if (filed == 1)
            {
                bookings = bookings.OrderBy(x => x.Id).ToList();
            }
            else if (filed == 2)
            {
                bookings = bookings.OrderBy(x => x.Date).ToList();
            }
            else if (filed == 3)
            {
                bookings = bookings.OrderBy(x => x.Time).ToList();
            }
            else if (filed == 4)
            {
                bookings = bookings.OrderBy(x => x.staffid).ToList();
            }
            else if (filed == 5)
            {
                bookings = bookings.OrderBy(x => x.customerId).ToList();
            }

        }
        else if (type == 2)
        {

            if (filed == 1)
            {
                bookings = bookings.OrderByDescending(x => x.Id).ToList();
            }
            else if (filed == 2)
            {
                bookings = bookings.OrderByDescending(x => x.Date).ToList();
            }
            else if (filed == 3)
            {
                bookings = bookings.OrderByDescending(x => x.Time).ToList();
            }
            else if (filed == 4)
            {
                bookings = bookings.OrderByDescending(x => x.staffid).ToList();
            }
            else if (filed == 5)
            {
                bookings = bookings.OrderByDescending(x => x.customerId).ToList();
            }
        }
        var customer = JsonDeserialize.JsonDeserializer(CustomerFilePath);
        var staff = JsonDeserialize.staffJsonDeserializer(CustomerFilePath);
        var table = new ConsoleTable("Booking ID", "Date", "Time", "Staff ID","Staff Name", "Customer ID","CUstomer Name");
        for (int i = 0; i < bookings.Count; i++)
        {
            table.AddRow(
                bookings[i].Id, 
                bookings[i].Date, 
                bookings[i].Time, 
                bookings[i].staffid,
                staff.FirstOrDefault(x => x.Id == bookings[i].StaffId).Name, 
                bookings[i].customerId,
                customer.FirstOrDefault(x => x.Id == bookings[i].CustomerId).Name
            );
        }
        table.Write(Format.Alternative);
        string confirmQuestion = "\nDo you want sort again? ";
        string confirm = Config.confirmationDetails(confirmQuestion);
        if (confirm == "y")
        {
            sortBooking();
        }
        else if (confirm == "n")
        {
            SubMenu.bookingSubMenu("Booking");
        }
    }
    public void getBooking()
    {
        try
        {
            int service;
            string date = "";
            string time;
            int staffId;
            int customerId = 0;
            string minusTime = "";
            Booking booking = new Booking();
            ConsoleCustomise.executeConsoleCustomise();
            string heading = "**** Booking  ****"; ;
            Config.alignCenter(heading);
            Console.WriteLine("\n Enter the customer details.");
            string dob = Config.fieldValidator("Date of Birth (dd/mm/yyyy)", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date of birth, try again.\n");
            string name = Config.fieldValidator("Fullname", @"^[a-zA-Z\s]+$", "Invalid character, try again.");
            var customers = JsonDeserialize.JsonDeserializer(CustomerFilePath);

            int index = customers.FindIndex(x => (x.Dob == dob) && (x.Name == name));
            if (customers.Exists(x => (x.Dob == dob) && (x.Name == name)))
            {
                customerId = customers[index].Id;

                Console.WriteLine("Select service :");
                for (int i = 0; i < services.Count; i++)
                {
                    Console.WriteLine(i + 1 + "-" + services[i]);
                }
                service = Config.questionNumberValidation(services.Count, "Select the service: ", "Please check the service option");

                DateTime dateTimeObj;
                int attempt = 0;
                do
                {
                    if (attempt > 0)
                    {
                        Console.WriteLine("Please enter valid date\n");
                    }
                    date = Config.fieldValidator("Enter booking date(dd/mm/yyyy) ", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date, try again.\n");
                    dateTimeObj = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    attempt++;
                }
                while (dateTimeObj < DateTime.Today);
                time = Config.fieldValidator("Enter booking time(HH:MM) ", "^([01][0-9]|2[0-3]):([0-5][0-9])$", "Not a valid time, try again.\n");

                var allStaff = JsonDeserialize.JsonDeserializer(StaffFilePath);
                var bookings = JsonDeserialize.bookingJsonDeserializer(FilePath);
                minusTime = DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture).AddMinutes(-30).ToString("HH:mm");

                //var getBookings = bookings.FirstOrDefault(a => a.Date == "01/02/2022");
                //Console.WriteLine(bookings.Count);
                allStaff.OrderBy(x => x.Id).ToList();
                
                if (bookings == null)
                {
                    if (allStaff != null)
                    {
                        Console.WriteLine("\nSelect the staff id: ");
                        for (int i = 0; i < allStaff.Count; i++)
                        {
                            Console.WriteLine(i + 1 + "-" + allStaff[i].Name);
                        }
                        staffId = Config.questionNumberValidation(allStaff.Count, "Select the staff ID: ", "Please check the staff ID \n");

//                        int index2 = allStaff.FindIndex(a => a.Id == staffId -1 );

                        booking.date = date;
                        booking.Time = time;
                        booking.service = services[service - 1];
                        booking.staffid = allStaff[staffId - 1].Id;
                        booking.status = "booked";
                        booking.customerId = customerId;
                        storeBooking(booking);
                    }else
                    {
                        Config.backEnterkey("No Staff found!. Check the staff details");
                    }
                }
                else
                {
                    var availableStaff = bookings.Where(a => (a.Date != date) && (a.Time != minusTime)).ToList();
                    minusTime = DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture).AddMinutes(-30).ToString("HH:mm");
                    bookings = bookings.Where(a => (a.Date == date) && (a.Time == minusTime)).ToList();
                    
                    //Console.WriteLine(allStaff.Count);
                    if (allStaff.Count > bookings.Count)
                    {
                        var result = allStaff.Where(p => !bookings.Any(p2 => p2.StaffId == p.Id)).ToList();
                        Console.WriteLine("\nSelect the staff id: ");
                        for (int i = 0; i < result.Count; i++)
                        {
                            Console.WriteLine(i + 1 + "-" + result[i].Name);
                        }
                        staffId = Config.questionNumberValidation(result.Count, "Select the staff ID: ", "Please check the staff ID \n");
                        booking.date = date;
                        booking.Time = time;
                        booking.service = services[service - 1];
                        booking.staffid = allStaff[staffId - 1].Id;
                        booking.status = "booked";
                        booking.customerId = customerId;
                        storeBooking(booking);
                    }
                    else
                    {
                        // Console.WriteLine("Date and time slot unavailable. Change date and time");
                        Config.backEnterkey("Date and time slot unavailable. Change date and time");
                    }
                }
            }//customer validation end
            else
            {
                Console.WriteLine("Details not match please register before booking");
                string checkCustomer = "Do you want register customer? ";
                checkCustomer = Config.confirmationDetails(checkCustomer);
                if (checkCustomer == "y")
                {
                    Customer customer = new Customer();
                    customer.getCutomerDetails();
                }
                else if (checkCustomer == "n")
                {
                    Config.backEnterkey("You can't Book");
                    SubMenu.bookingSubMenu("Booking");
                }
            }

            Console.ReadLine();
            //if (dateTimeObj > DateTime.Today)
            //{
            //   Console.WriteLine(date);
            //}
            //else
            //{
            //    Console.WriteLine("sda");
            //}

            //getBooking();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Config.backEnterkey(e.Message);
        }
    }
    public void storeBooking(Booking booking)
    {
        JsonSerialize.bookingJsonSerializer(booking, FilePath);
        Config.saveMessage("Booking Successful");
        string confirmQuestion = "Do you want add another booking? ";
        string confirm = Config.confirmationDetails(confirmQuestion);
        if (confirm == "y")
        {
            getBooking();
        }
        else if (confirm == "n")
        {
            SubMenu.bookingSubMenu("Booking");
        }
    }
}
