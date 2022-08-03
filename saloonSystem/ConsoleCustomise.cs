using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

class ConsoleCustomise
{
    public static string backgroundColor = "";
    public static string textColor = "";

    public static TextInfo textFormatter = new CultureInfo("en-US", false).TextInfo;

    //Full screen code 
    /*https://social.msdn.microsoft.com/Forums/vstudio/en-US/62f5f6a2-127f-4fa5-a6a9-69efb167baa2/consolesetwindowsize-and-position-help?forum=csharpgeneral*/

    [DllImport("kernel32.dll", ExactSpelling = true)]

    public static extern IntPtr GetConsoleWindow();

    public static IntPtr ThisConsole = GetConsoleWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public const int Maximize = 3;

    public static void executeConsoleCustomise()
    {
        Console.Clear();
        consoleCustomiseColor();
        consoleCustomiseSize();
        companyTitle();
        companyName();

    }
    public static void singleTextCenter(string textforcenter)
    {
        Console.WriteLine();
        Console.SetCursorPosition((Console.WindowWidth - textforcenter.Length) / 2, Console.CursorTop);
        Console.WriteLine(textforcenter);
    }
    
    public static void consoleCustomiseColor()  
    {
        //Check  the configuration file
        try
        {
            Config.dictionary = Config.loadConfigFile(Config.ConfigFile);

            backgroundColor = textFormatter.ToTitleCase(Config.dictionary.ContainsKey("BackgroundColor") ? Config.dictionary["BackgroundColor"] : "Black");
            textColor = textFormatter.ToTitleCase(Config.dictionary.ContainsKey("TextColor") ? Config.dictionary["TextColor"] : "Black");

            // Check the spelling of the colors in the configuration file
            try
            {
                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), backgroundColor);
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), textColor);
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " Please check the spelling at the configuration file");
            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message + " Configuration file is missing please include that file and rerun the system!");
        }
    }

    public static void consoleCustomiseSize()
    {
        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
        ConsoleCustomise.ShowWindow(ConsoleCustomise.ThisConsole, ConsoleCustomise.Maximize);
    }
    public static void companyName()
    {
        string CompanyName = companyDetails();
        CompanyName = "**** "+ CompanyName + " ****";
        Console.WriteLine();
        Console.SetCursorPosition((Console.WindowWidth - CompanyName.Length) / 2, Console.CursorTop);
        Console.WriteLine(CompanyName);


    }
    public static void companyTitle()
    {
        string CompanyName = companyDetails();
        Console.Title = CompanyName;
    }
    public static string companyDetails()
    {
        string CompanyName = "XYZ Hair Dressing Saloon";
        //string title = "Hair Dressing Saloon System";
        
        try
        {
            Config.dictionary = Config.loadConfigFile(Config.ConfigFile);
            CompanyName = Config.dictionary.ContainsKey("CompanyName") ? Config.dictionary["CompanyName"].ToUpper() : CompanyName;
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message + " Configuration file is missing please include that file and rerun the system!");
        }

        return CompanyName;
    }
}