using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

class Customer
{
    [JsonProperty]
    private int id;
    public int Id   // property
    {
        get { return id; }
        set { id = value; }
    }
    [JsonProperty]
    private string name = "";
    public string Name   // property
    {
        get { return name; }
        set { name = value; }
    }
    [JsonProperty]
    private string phoneNo = "";
   
    public string PhoneNo  // property
    {
        get { return phoneNo; }
        set { phoneNo = value;}
    }
    [JsonProperty]
    private string dob = "";
    public string Dob  // property
    {
        get { return dob; }
        set { dob = value; }
    }
    [JsonProperty]
    private string postCode = "";
    public string PostCode  // property
    {
        get { return postCode; }
        set { postCode = value; }
    }
    public const string FilePath = "customer/customer.json";
    public void sortCustomers()
    {
        int filed = 0;
        int type = 0;
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Sort Customer Details****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        Console.WriteLine("Sort by");
        Console.WriteLine("1- ID");
        Console.WriteLine("2- Name\n");
        //Console.Write("Choose field? ");
        filed = Config.questionNumberValidation(2, "Choose field? ","Please check the field option");
        Console.WriteLine("\nChoose the order for sorting");
        Console.WriteLine("1- Ascending");
        Console.WriteLine("2- Descending\n");
        type = Config.questionNumberValidation(2, "Enter the sorting type? ", "Please check the sorting option");

        var customers = JsonDeserialize.JsonDeserializer(FilePath);
        //customers = customers.OrderByDescending(x => x.Id).ToList();
        if (type == 1)
        {
            if (filed == 1)
            {
                customers = customers.OrderBy(x => x.Id).ToList();
            }
            else if (filed == 2)
            {
                customers = customers.OrderBy(x => x.Name).ToList();
            }

        }
        else if (type == 2)
        {
            if (filed == 1)
            {
                customers = customers.OrderByDescending(x => x.Id).ToList();
            }
            else if (filed == 2)
            {
                customers = customers.OrderByDescending(x => x.Name).ToList();
            }
        }

        var table = new ConsoleTable("ID", "Name", "Date of Birth", "Phone No", "Post Code");
        for (int i = 0; i < customers.Count; i++)
        {
            table.AddRow(customers[i].Id, customers[i].Name, customers[i].Dob, customers[i].PhoneNo, customers[i].PostCode);
        }
        table.Write(Format.Alternative);
        string confirmQuestion = "\nDo you want sort again? ";
        string confirm = Config.confirmationDetails(confirmQuestion);
        if (confirm == "y")
        {
            sortCustomers();
        }
        else if (confirm == "n")
        {
            SubMenu.customerSubMenu("Customer");
        }
    }
    public void deleteCustomer()
    {
        string answer = "";
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Delete Customer ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        string dob = Config.fieldValidator("Date of Birth (dd/mm/yyyy) : ", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date of birth, try again.\n");
        string name = Config.fieldValidator("Fullname", @"^[a-zA-Z\s]+$", "Invalid character, try again.\n");

        var customers = JsonDeserialize.JsonDeserializer(FilePath);
        int index = customers.FindIndex(a => (a.Dob == dob) && (a.Name == name));
        string confirmQuestion = "Are you sure do you want delete?";
        answer = Config.confirmationDetails(confirmQuestion);
        
        if (answer == "y")
        {
            if (customers.Exists(a => (a.Dob == dob) && (a.Name == name)))
            {
            
                customers.Remove(customers[index]);
                JsonSerialize.updateJsonSerializer(customers, FilePath);
                Console.WriteLine("Deleted Succeful!");
                string confirmQuestionAnother = "Do you want delete another customer? ";
                string confirm = Config.confirmationDetails(confirmQuestionAnother);
                if (confirm == "y")
                {
                    deleteCustomer();
                }
                else if (confirm == "n")
                {
                    SubMenu.customerSubMenu("Customer");
                }
            
            }
            else
            {
                Console.WriteLine("Customer NOT found.");
                Config.backEnterkey("Press any key to continue...");
                deleteCustomer();
            }
        }
        else if (answer == "n")
        {
            SubMenu.customerSubMenu("Customer");
        }

        Console.ReadLine();
    }
      
    public void searchCustomer()
    {
        //https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.contains?view=net-6.0
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Search Customer Details ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        string dob = Config.fieldValidator("Date of Birth (dd/mm/yyyy)", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date of birth, try again.\n");
        string name = Config.fieldValidator("Fullname", @"^[a-zA-Z\s]+$", "Invalid character, try again.");
        var customers = JsonDeserialize.JsonDeserializer(FilePath);
        int index = customers.FindIndex(x => (x.dob == dob) && (x.name == name));
        if (customers.Exists(x => (x.dob == dob) && (x.name == name)))
        {
            Console.WriteLine("1- ID : " + customers[index].Id);
            Console.WriteLine("2- Fullname : " + customers[index].Name);
            Console.WriteLine("3- Phone Number : " + customers[index].PhoneNo);
            Console.WriteLine("4- Date of Birth : " + customers[index].Dob);
            Console.WriteLine("5- Post Code : " + customers[index].PostCode + "\n");
        }
        else
        {
            Console.WriteLine("Customer NOT found");
        }
        string confirmQuestion = "Do you want search again? ";
        string confirm = Config.confirmationDetails(confirmQuestion);
        if (confirm == "y")
        {
            searchCustomer();
        }
        else if (confirm == "n")
        {
            SubMenu.customerSubMenu("Customer");
        }
        Console.ReadLine();
    }
    public void editCustomers()
    {
        Customer customer = new Customer();
        int selectedQuestion = 0;
        string updateDetails = "";
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Edit Customer Details ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        //Console.Write("Enter the Date of Birth :");
        string dob = Config.fieldValidator("Date of Birth (dd/mm/yyyy)", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date of birth, try again.\n");
        //Console.Write("Enter the Fullname :");
        string name = Config.fieldValidator("Fullname", @"^[a-zA-Z\s]+$", "Invalid character, try again.");
        
        var customers = JsonDeserialize.JsonDeserializer(FilePath);
        int index = customers.FindIndex(a => (a.Dob == dob) && (a.Name == name));
        if (customers.Exists(a =>(a.Dob == dob) && (a.Name == name)))
        {
            Console.WriteLine("1- Fullname : " + customers[index].Name + "\n");
            Console.WriteLine("2- Phone Number : " + customers[index].PhoneNo + "\n");
            Console.WriteLine("3- Date of Birth : " + customers[index].Dob + "\n");
            Console.WriteLine("4- Post Code : " + customers[index].PostCode + "\n");
            selectedQuestion = Config.questionNumberValidation(4, "Please select the field : ", "Please check the field option");
            
            if (selectedQuestion >0)
            {
                if (selectedQuestion == 1)
                {
                    updateDetails = Config.fieldValidator("Fullname", @"^[a-zA-Z\s]+$", "Invalid character, try again.");
                }
                else if (selectedQuestion == 2)
                {
                    updateDetails = Config.fieldValidator("Phone Number", @"^[0-9]{10,11}$", "Not a valid phone number, try again.");
                }
                else if (selectedQuestion == 3)
                {
                    updateDetails = Config.fieldValidator("Date of Birth (dd/mm/yyyy)", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date of birth, try again.\n");
                }
                else if (selectedQuestion == 4)
                {
                    updateDetails = Config.fieldValidator("Post Code", @"^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$", "Not a valid post code, try again.");
                }
                
                customers.Add(new Customer()
                {
                    Id = customers[index].Id,
                    Name = (selectedQuestion == 1) ? updateDetails : customers[index].Name,
                    PhoneNo = (selectedQuestion == 2) ? updateDetails : customers[index].PhoneNo,
                    Dob = (selectedQuestion == 3) ? updateDetails : customers[index].Dob,
                    PostCode = (selectedQuestion == 2) ? updateDetails : customers[index].PostCode
                });;
                customers.Remove(customers[index]);
                JsonSerialize.updateJsonSerializer(customers, FilePath);
                Console.WriteLine("Updated Succeful!");

                string confirmQuestion = "Do you want update another customer details?";
                string confirm = Config.confirmationDetails(confirmQuestion);
                if (confirm == "y")
                {
                    editCustomers();
                }
                else if (confirm == "n")
                {
                    SubMenu.customerSubMenu("Customer");
                }
            }
        }
        else
        {
            Console.WriteLine("Customer NOT found!!");
            Config.backEnterkey("Press enter to search again..");
            editCustomers();
        }
    }
    public void viewCustomers()
    {
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** View Customer Details ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        var cu = JsonDeserialize.JsonDeserializer(FilePath);
        cu.OrderBy(a => a.id).ToList();
        // I refer from this url
        //https://github.com/khalidabuhakmeh/ConsoleTables 
        if (cu != null) 
        { 
            var table = new ConsoleTable("ID", "Name", "Date of Birth", "Phone No", "Post Code");
            for (int i = 0; i < cu.Count; i++)
            {

                table.AddRow(cu[i].Id, cu[i].Name, cu[i].Dob, cu[i].PhoneNo, cu[i].PostCode);
                //             .Write(Format.Alternative);
            }
            table.Write(Format.Alternative);
            Config.backEnterkey("Press any key to back..");
            SubMenu.customerSubMenu("Customer");
        }
        else
        {
            Console.Write("No Customer found!!");
            Console.Write("Press any key for back..");
            Console.ReadKey();
            SubMenu.staffSubMenu("Staff");
        }
    }

    public void getCutomerDetails()
    {
        string confirm;
        string confirmQuestion = "";
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Customer ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        Customer customer = new Customer();
        do
        {
            string name = Config.fieldValidator("Fullname", @"^[a-zA-Z\s]+$", "Invalid character, try again.");
            customer.Name = name;
            string phonoNo = Config.fieldValidator("Phone Number", @"^[0-9]{10,11}$", "Not a valid phone number, try again.");
            customer.PhoneNo = phonoNo;
            string dob = Config.fieldValidator("Date of Birth (dd/mm/yyyy)", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date of birth, try again.\n");
            customer.Dob = dob;
            string postcode = Config.fieldValidator("Post Code", @"^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$", "Not a valid post code, try again.");
            customer.PostCode = postcode.ToUpper();
            confirmQuestion = "Do you confirm all the details are correct?";
            confirm = Config.confirmationDetails(confirmQuestion);
        }
        while (confirm == "n");
        //List<Customer> customers = new List<Customer>();
        //customers.Add(customer);
        storeCustomer(customer);
        Console.ReadLine();
    }
    public void storeCustomer(Customer cu)
    {
        JsonSerialize.JsonSerializer(cu, FilePath);
        Config.saveMessage("Customer Added Successful");
        string confirmQuestion = "Do you want add another customer?";
        string confirm = Config.confirmationDetails(confirmQuestion);
        if(confirm == "y")
        {
            getCutomerDetails();
        }
        else if(confirm == "n")
        {
            SubMenu.customerSubMenu("Customer");
        }
    }
    
   
    

}
