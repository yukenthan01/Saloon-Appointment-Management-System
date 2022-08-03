using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class StaffAvailability
{
    public const string FilePath = "booking/booking.json";
    public const string StaffFilePath = "staff/staff.json";

    public void viewStaffAvailability()
    {
        try {
            int service;
            string date = "";
            string time;
            int staffId;
            int customerId = 0;
            string minusTime = "";
            ConsoleCustomise.executeConsoleCustomise();
            string heading = "**** View Staff Availability Details ****"; ;
            Config.alignCenter(heading);
            Console.WriteLine("\n");

            DateTime dateTimeObj;
            int attempt = 0;
            do
            {
                if (attempt > 0)
                {
                    Console.WriteLine("Please enter valid date\n");
                }
                date = Config.fieldValidator("Date for availability(dd/mm/yyyy) ", "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$", "Not a valid date, try again.\n");
                dateTimeObj = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                attempt++;
            }
            while (dateTimeObj < DateTime.Today);
            time = Config.fieldValidator("Enter booking time(HH:MM) ", "^([01][0-9]|2[0-3]):([0-5][0-9])$", "Not a valid time, try again.\n");

            var allStaff = JsonDeserialize.JsonDeserializer(StaffFilePath);
            var bookings = JsonDeserialize.bookingJsonDeserializer(FilePath);

            minusTime = DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture).AddMinutes(-30).ToString("HH:mm");
            bookings = bookings.Where(a => (a.Date == date) && (a.Time == minusTime)).ToList();
            var availableStaff = bookings.Where(a => (a.Date != date) && (a.Time != minusTime)).ToList();

            allStaff.OrderBy(x => x.Id).ToList();
            if (bookings != null)
            {
                if (allStaff != null)
                {
                    Console.WriteLine("\nSelect the staff id: ");
                    for (int i = 0; i < allStaff.Count; i++)
                    {
                        Console.WriteLine(i + 1 + "-" + allStaff[i].Name);
                    }
                    staffId = Config.questionNumberValidation(allStaff.Count, "Select the staff ID: ", "Please check the staff ID \n");

                    //booking.date = date;
                    //booking.Time = time;
                    //booking.service = services[service - 1];
                    //booking.staffid = allStaff[1].Id;
                    //booking.status = "booked";
                    //booking.customerId = customerId;
                    //storeBooking(booking);
                }
                else
                {
                    Config.backEnterkey("No Staff found!. Check the staff details");
                }
            }
            else
            {
                Config.backEnterkey("No booking!");
            }

            Console.ReadLine();



        }
        catch (Exception e)
        {
            Config.backEnterkey("File is Missing Check the files");
        }
       }
}

