using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class JsonSerialize
{
    // I refer from
    //https://www.newtonsoft.com/json/help/html/SerializingJSON.htm
    public static void bookingJsonSerializer(Booking booking, string filePath)
    {
        if (!File.Exists(filePath))
        {
            var createFile = File.Create(filePath);
            createFile.Close();
        }
        // Read existing json data
        var jsonData = File.ReadAllText(filePath);
        // De-serialize to object or create new list
        var bookinglist = JsonConvert.DeserializeObject<List<Booking>>(jsonData)
                              ?? new List<Booking>();
        bookinglist = bookinglist.OrderByDescending(x => x.Id).ToList();
        int id;
        if (bookinglist.Count == 0)
        {
            id = 1;
        }
        else
        {
            id = bookinglist[0].Id + 1;
        }

        // Add any new employees
        bookinglist.Add(new Booking()
        {
            Id = id,
            Service =booking.Service,
            Date = booking.Date,
            Time = booking.Time,
            StaffId = booking.StaffId,
            Status = booking.Status,
            CustomerId = booking.CustomerId
        });
        // Update json data string
        jsonData = JsonConvert.SerializeObject(bookinglist);
        System.IO.File.WriteAllText(filePath, jsonData);
    }
    public static void JsonSerializer(Customer cu,string filePath)
    {
        if (!File.Exists(filePath))
        {
            var createFile = File.Create(filePath);
            createFile.Close();
        }
        // Read existing json data
        var jsonData = File.ReadAllText(filePath);
        // De-serialize to object or create new list
        var customerList = JsonConvert.DeserializeObject<List<Customer>>(jsonData)
                              ?? new List<Customer>();
        customerList = customerList.OrderByDescending(x => x.Id).ToList();
        int id;
        if (customerList.Count == 0)
        {
            id = 1;
        }
        else
        {
            id = customerList[0].Id + 1;
        }

        // Add any new employees
        customerList.Add(new Customer()
        {
            Id =id,Name = cu.Name,Dob = cu.Dob,PhoneNo=cu.PhoneNo,PostCode = cu.PostCode
        });
        // Update json data string
        jsonData = JsonConvert.SerializeObject(customerList);
        System.IO.File.WriteAllText(filePath, jsonData);
    }
    public static void updateJsonSerializer(List<Customer> updateCustomer, string filePath)
    {
        if (File.Exists(filePath))
        {
            var jsonData = File.ReadAllText(filePath);
            jsonData = JsonConvert.SerializeObject(updateCustomer);
            File.WriteAllText(filePath, jsonData);
        }
        
    }
    public static void updateBookingJsonSerializer(List<Booking> updateCustomer, string filePath)
    {
        if (File.Exists(filePath))
        {
            var jsonData = File.ReadAllText(filePath);
            jsonData = JsonConvert.SerializeObject(updateCustomer);
            File.WriteAllText(filePath, jsonData);
        }

    }
    public static void updateStaffJsonSerializer(List<Staff> updateCustomer, string filePath)
    {
        if (File.Exists(filePath))
        {
            var jsonData = File.ReadAllText(filePath);
            jsonData = JsonConvert.SerializeObject(updateCustomer);
            File.WriteAllText(filePath, jsonData);
        }

    }
    public static void staffJsonSerializer(Staff cu, string filePath)
    {
        if (!File.Exists(filePath))
        {
            var createFile = File.Create(filePath);
            createFile.Close();
        }
        // Read existing json data
        var jsonData = System.IO.File.ReadAllText(filePath);
        // De-serialize to object or create new list
        var customerList = JsonConvert.DeserializeObject<List<Staff>>(jsonData)
                              ?? new List<Staff>();
        customerList = customerList.OrderByDescending(x => x.Id).ToList();
        int id;
        if (customerList.Count == 0)
        {
            id = 1;
        }
        else
        {
            id = customerList[0].Id + 1;
        }

        // Add any new employees
        customerList.Add(new Staff()
        {
            Id = id,
            Name = cu.Name,
            Dob = cu.Dob,
            PhoneNo = cu.PhoneNo,
            PostCode = cu.PostCode
        });
        // Update json data string
        jsonData = JsonConvert.SerializeObject(customerList);
        System.IO.File.WriteAllText(filePath, jsonData);
    }
}
