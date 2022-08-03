using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class JsonDeserialize
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.exists?view=net-6.0 
    public static List<Customer> JsonDeserializer (string filePath)
    {
        var customers = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(filePath));
        return customers;
    }
    
    public static List<Staff> staffJsonDeserializer(string filePath)
    {
        var allStaff = JsonConvert.DeserializeObject<List<Staff>>(File.ReadAllText(filePath));
        return allStaff;
    }
    public static List<Booking> bookingJsonDeserializer(string filePath)
    {
        
        var bookings = JsonConvert.DeserializeObject<List<Booking>>(File.ReadAllText(filePath));
        if(bookings != null)
        {
            return bookings;

        }
        else
        {
            return null;
        }
    }
}
