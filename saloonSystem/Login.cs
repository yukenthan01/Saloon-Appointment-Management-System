using System;
using System.Text;
using System.Threading;

class Login
{
    private static string password = "";
    
    public Login()
    {
        Config.dictionary = Config.loadConfigFile(Config.ConfigFile);
        if (Config.dictionary.ContainsKey("Password"))
        {
            int attempt = 0;
            string heading = "**** Login ****";
            Config.alignCenter(heading);
            do
            {
                if (attempt > 0)
                {
                    Console.WriteLine("Please Try Again Password Not Matched! \n");
                }
                string enterText = "Please Enter Password: ";
                Console.Write(enterText);
                hidePassword(enterText);
                attempt++;
            }
            while (password != Config.dictionary["Password"] && (attempt != 4));
            Console.WriteLine(attempt);
            if (attempt == 4)
            {
                Console.WriteLine("\nPassword Attempt Exceeded. System Will Close In Few Seconds");
                Timer t = new Timer(timerC, null, 5000, 5000);
                Console.ReadLine();
            }
            else
            {
                Console.Write("\nThe password entered successfully!");
                Menus.menuLists();
            }
        }
        else
        {
            Console.Write("Please Check the Configuration File For Password Setting!!");
            Console.ReadLine();
        }
    }
    //Password hide to star
    //https://www.c-sharpcorner.com/article/show-asterisks-instead-of-characters-for-password-input-in-console-application/
    static void hidePassword(string EnterText)
    {
        try
        {
            //Console.Write(EnterText);
            password = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work  
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (string.IsNullOrWhiteSpace(password))
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Empty value not allowed.");
                            hidePassword(EnterText);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("");
                            break;
                        }
                    }
                }
            } while (true);
            //Console.WriteLine(EnteredVal);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void timerC(object state)
    {
        Environment.Exit(0);
    }
}
