using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Staff
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
        set { phoneNo = value; }
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
    public const string FilePath = "staff/staff.json";
    public void sortStaff()
    {
        int filed = 0;
        int type = 0;
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Sort Staff Details****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        Console.WriteLine("Sort by");
        Console.WriteLine("1- ID");
        Console.WriteLine("2- Name\n");
        //Console.Write("Choose field? ");
        filed = Config.questionNumberValidation(2, "Choose field? ", "Please check the field option");
        Console.WriteLine("\nChoose the order for sorting");
        Console.WriteLine("1- Ascending");
        Console.WriteLine("2- Descending\n");
        type = Config.questionNumberValidation(2, "Enter the sorting type? ", "Please check the sorting option");
        string jsonData = File.ReadAllText(FilePath);
        //var staffs = JsonConvert.DeserializeObject<List<Staff>>(jsonData);
        var staffs = JsonDeserialize.staffJsonDeserializer(FilePath);
        if (type == 1)
        {
            if (filed == 1)
            {
                staffs = staffs.OrderBy(x => x.Id).ToList();
            }
            else if (filed == 2)
            {
                staffs = staffs.OrderBy(x => x.Name).ToList();
            }

        }
        else if (type == 2)
        {
            if (filed == 1)
            {
                staffs = staffs.OrderByDescending(x => x.Id).ToList();
            }
            else if (filed == 2)
            {
                staffs = staffs.OrderByDescending(x => x.Name).ToList();
            }
        }

        var table = new ConsoleTable("ID", "Name", "Date of Birth", "Phone No", "Post Code");
        for (int i = 0; i < staffs.Count; i++)
        {
            table.AddRow(staffs[i].Id, staffs[i].Name, staffs[i].Dob, staffs[i].PhoneNo, staffs[i].PostCode);
        }
        table.Write(Format.Alternative);
        string confirmQuestion = "\nDo you want sort again? ";
        string confirm = Config.confirmationDetails(confirmQuestion);
        if (confirm == "y")
        {
            sortStaff();
        }
        else if (confirm == "n")
        {
            SubMenu.staffSubMenu("Staff");
        }
    }
    public void deletestaff()
    {
        string answer = "";
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Delete Staff Details ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        string dob = Config.fieldValidator("Date of Birth (dd/mm/yyyy)", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date of birth, try again.\n");
        string name = Config.fieldValidator("Fullname", @"^[a-zA-Z\s]+$", "Invalid character, try again.\n");

        var allstaff = JsonDeserialize.JsonDeserializer(FilePath);
        int index = allstaff.FindIndex(a => (a.Dob == dob) && (a.Name == name));
        string confirmQuestion = "Are you sure do you want delete?";
        answer = Config.confirmationDetails(confirmQuestion);

        if (answer == "y")
        {
            if (allstaff.Exists(a => (a.Dob == dob) && (a.Name == name)))
            {

                allstaff.Remove(allstaff[index]);
                JsonSerialize.updateJsonSerializer(allstaff, FilePath);
                Console.WriteLine("Deleted Succeful!");
                string confirmQuestionAnother = "Do you want delete another staff? ";
                string confirm = Config.confirmationDetails(confirmQuestionAnother);
                if (confirm == "y")
                {
                    deletestaff();
                }
                else if (confirm == "n")
                {
                    SubMenu.staffSubMenu("Staff");
                }

            }
            else
            {
                Console.WriteLine("Staff NOT found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                deletestaff();
            }
        }
        else if (answer == "n")
        {
            SubMenu.staffSubMenu("Staff");
        }

        Console.ReadLine();
    }
    public void searchStaffs()
    {
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Search Staff Details ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        string dob = Config.fieldValidator("Date of Birth (dd/mm/yyyy)", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date of birth, try again.\n");
        string name = Config.fieldValidator("Fullname", @"^[a-zA-Z\s]+$", "Invalid character, try again.");
        string jsonData = File.ReadAllText(FilePath);
        var staffs = JsonConvert.DeserializeObject<List<Staff>>(jsonData);
        for (int i = 0; i < staffs.Count; i++)
        {
            if (staffs[i].Dob == dob && staffs[i].Name == name)
            {
                Console.WriteLine("1- ID : " + staffs[i].Id);
                Console.WriteLine("2- Fullname : " + staffs[i].Name);
                Console.WriteLine("3- Phone Number : " + staffs[i].PhoneNo);
                Console.WriteLine("4- Date of Birth : " + staffs[i].Dob);
                Console.WriteLine("5- Post Code : " + staffs[i].PostCode + "\n");
            }
            else
            {
                Console.WriteLine("Staff NOT found");
                Config.saveMessage("Press any key to continue...");
                deletestaff();
            }
        }

        string confirmQuestion = "Do you want search again? ";
        string confirm = Config.confirmationDetails(confirmQuestion);
        if (confirm == "y")
        {
            searchStaffs();
        }
        else if (confirm == "n")
        {
            SubMenu.staffSubMenu("Staff");
        }
        //Console.ReadLine();
    }
    public void editStaff()
    {
        Staff staff = new Staff();
        int selectedQuestion = 0;
        string updateDetails = "";
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Edit Staff Details ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        string dob = Config.fieldValidator("Date of Birth (dd/mm/yyyy)", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date of birth, try again.\n");
        //Console.Write("Enter the Fullname :");
        string name = Config.fieldValidator("Fullname", @"^[a-zA-Z\s]+$", "Invalid character, try again.");

        var allStaff = JsonDeserialize.staffJsonDeserializer(FilePath);
        int index = allStaff.FindIndex(a => (a.Dob == dob) && (a.Name == name));
        if (allStaff.Exists(a => (a.Dob == dob) && (a.Name == name)))
        {
            Console.WriteLine("1- Fullname : " + allStaff[index].Name + "\n");
            Console.WriteLine("2- Phone Number : " + allStaff[index].PhoneNo + "\n");
            Console.WriteLine("3- Date of Birth : " + allStaff[index].Dob + "\n");
            Console.WriteLine("4- Post Code : " + allStaff[index].PostCode + "\n");
            selectedQuestion = Config.questionNumberValidation(4, "Please select the field : ", "Please check the field option");

            if (selectedQuestion > 0)
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

                allStaff.Add(new Staff()
                {
                    Id = allStaff[index].Id,
                    Name = (selectedQuestion == 1) ? updateDetails : allStaff[index].Name,
                    PhoneNo = (selectedQuestion == 2) ? updateDetails : allStaff[index].PhoneNo,
                    Dob = (selectedQuestion == 3) ? updateDetails : allStaff[index].Dob,
                    PostCode = (selectedQuestion == 2) ? updateDetails : allStaff[index].PostCode
                }); ;
                allStaff.Remove(allStaff[index]);
                JsonSerialize.updateStaffJsonSerializer(allStaff, FilePath);
                Console.WriteLine("Updated Succeful!");

                string confirmQuestion = "Do you want update another Staff details?";
                string confirm = Config.confirmationDetails(confirmQuestion);
                if (confirm == "y")
                {
                    editStaff();
                }
                else if (confirm == "n")
                {
                    SubMenu.staffSubMenu("Staff");
                }
            }
        }
        else
        {
            Console.WriteLine("Staff NOT found!!");
            Config.backEnterkey("Press enter to search again..");
            editStaff();
        }
    }
    public void viewStaffs()
    {
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** View Staff Details ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        var allstaff = JsonDeserialize.staffJsonDeserializer(FilePath);
        allstaff.OrderByDescending(x => x.Id).ToList();
        if (allstaff != null)
        {
            var table = new ConsoleTable("ID", "Name", "Date of Birth", "Phone No", "Post Code");
            for (int i = 0; i < allstaff.Count; i++)
            {
                table.AddRow(allstaff[i].Id, allstaff[i].Name, allstaff[i].Dob, allstaff[i].PhoneNo, allstaff[i].PostCode);
                //.Write(Format.Alternative);
            }
            table.Write(Format.Alternative);
            Console.Write("Press any key for back..");
            Console.ReadKey();
            SubMenu.staffSubMenu("Staff");
        }
        else
        {
            Console.Write("No Staff found!!");
            Console.Write("Press any key for back..");
            Console.ReadKey();
            SubMenu.staffSubMenu("Staff");
        }
        
    }
    public void getStaffDetails()
    {
        string confirm;
        string confirmQuestion = "";
        ConsoleCustomise.executeConsoleCustomise();
        string heading = "**** Staff Details ****"; ;
        Config.alignCenter(heading);
        Console.WriteLine("\n");
        Staff staff = new Staff();
        do
        {
            string name = Config.fieldValidator("Fullname", @"^[a-zA-Z\s]+$", "Invalid character, try again.");
            staff.Name = name;
            string phonoNo = Config.fieldValidator("Phone Number", @"^[0-9]{10,11}$", "Not a valid phone number, try again.");
            staff.PhoneNo = phonoNo;
            string dob = Config.fieldValidator("Date of Birth (dd/mm/yyyy)", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date of birth, try again.\n");
            staff.Dob = dob;
            string postcode = Config.fieldValidator("Post Code", @"^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$", "Not a valid post code, try again.");
            staff.PostCode = postcode.ToUpper();
            confirmQuestion = "Do you confirm all the details are correct?";
            confirm = Config.confirmationDetails(confirmQuestion);
        }
        while (confirm == "n");
        storeStaff(staff);
        Console.ReadLine();
    }
    public void storeStaff(Staff cu)
    {
        JsonSerialize.staffJsonSerializer(cu, FilePath);
        Config.saveMessage("Staff Added Successful");
        string confirmQuestion = "Do you want add another staff? ";
        string confirm = Config.confirmationDetails(confirmQuestion);
        if (confirm == "y")
        {
            getStaffDetails();
        }
        else if (confirm == "n")
        {
            SubMenu.staffSubMenu("Staff");
        }
    }
}

