using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

class Config
{
    public const string ConfigFile = "../configuration/config.txt";
    public static string selectedMenu;
    public static Dictionary<string, string> dictionary = new Dictionary<string, string>();
    //Get the data from config file
    public static string[] splitLine(string configuration)
    {
        string[] splitedValue = configuration.Split(':');
        return splitedValue;
    }

    //Convert the collected data from the config file into C# usable format
    public static Dictionary<string, string> loadConfigFile(string fileName)
    {
        string[] getAllLine = File.ReadAllLines(fileName);

        for (int i = 0; i < getAllLine.Length; i++)
        {
            if (getAllLine[i].Length > 0)
            {
                dictionary[splitLine(getAllLine[i])[0]] = splitLine(getAllLine[i])[1];
            }
        }

        return dictionary;
    }
    public static void alignCenter(string aligntext)
    {
        Console.WriteLine("\n\n");
        Console.SetCursorPosition((Console.WindowWidth - aligntext.Length) / 2, Console.CursorTop);
        Console.WriteLine(aligntext);
        
    }
    public static bool validateMenuInteger(string selectMenu)
    {
        int n;
        while (!int.TryParse(selectMenu, out n))
        {
            Console.WriteLine("You entered an invalid number");
            return false;
            
        }
        return true;
    }
    public static string confirmationDetails(string saveMessage)
    {
        string confirmation = "";
        int count = 0;
        do
        {
            if (count > 0)
            {
                Console.WriteLine("Check you option!");
            }
            Console.Write(saveMessage + " (Y / N) :");
            confirmation = Console.ReadLine().ToLower();
            count++;
        }
        while (confirmation != "y" && confirmation != "n");
        return confirmation;
    }
    public static void saveMessage(string message)
    {
        Console.WriteLine(message);
        
    }
    public static int questionNumberValidation(int totalMenu,string question,string errorMessage)
    {
        int integer;
        int attempt = 0;
        int selectedMenuInteger = 0;
        do
        {
            if (attempt > 0)
            {
                Console.WriteLine(errorMessage +"! \n");
            }
            Console.Write(question);
            selectedMenu = Console.ReadLine();
//            validateMenuInteger(selectedMenu);
            if (int.TryParse(selectedMenu, out integer))
            {
                selectedMenuInteger = int.Parse(selectedMenu);
            }
            attempt++;
        }
        while (selectedMenuInteger == 0 || selectedMenuInteger > totalMenu || !int.TryParse(selectedMenu, out integer));
        return selectedMenuInteger;
    }
    
    public static string fieldValidator(string field,string condition,string errorMessage)
    {
        // My conditions
        // https://regex101.com/
        string enteredValue = "";
        bool conditions;
        int attempt = 0;
        do
        {
            if (attempt > 0)
            {
                Console.WriteLine(errorMessage);
            }
            Console.Write("Enter "+ field + ":");
            enteredValue = Console.ReadLine();
            conditions = Regex.IsMatch(enteredValue, condition);
            attempt++;
            
        }
        while (!conditions);
        return enteredValue;
    }
    
    public static void backEnterkey(string message)
    {
        Console.Write(message);
        Console.ReadKey();
    }
}

