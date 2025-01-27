using Boss_Final.Login_System.Abstract;
using Boss_Final.Repository.Concret;
using System.Text.Json;
using System.Transactions;


namespace Boss_Final.Login_System.Concret;

public class Login : ILogin
{
    public DbContext _db;
    public Worker_Employer_Repository Employer_Repository=new Worker_Employer_Repository();

    public void Introduction()
    {
        int select = -1;
        bool validInput = false;

        Console.WriteLine("                                                   \x1b[34mBOSS \x1b[31mSYSTEM \x1b[32mAZERBAIJAN\x1b[0m");
        Console.WriteLine("1 - > Worker");
        Console.WriteLine("2 - > Employer");
        Console.WriteLine("3 - > See All Vacancies");
        Console.WriteLine("4 - > Exit\n"); 

        while (!validInput)
        {
            Console.Write("\u001b[34mPlease choose one : \u001b[0m");

            try
            {
                select = Convert.ToInt32(Console.ReadLine());

                if (select >= 1 && select <= 4)  
                {
                    validInput = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please select a correct option (1, 2, 3 or 4).");
                    Console.ResetColor();
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Please enter a number.");
                Console.ResetColor();
            }
        }

        if (select == 1)
        {
            Worker_Introduction();
        }
        else if (select == 2)
        {
            Employer_Introduction();
        }
        else if (select == 3) 
        {
            Employer_Repository.Show_All_Vacancies();
            Introduction();
        }
        else if (select == 4)  
        {
            Console.WriteLine("Exiting the program...");
            Environment.Exit(0); 
        }
    }
    public void Worker_Introduction()
    {
        Console.Clear();
        Console.WriteLine("                                                   \x1b[34m       Login\x1b[0m");
        Console.WriteLine("1 - > Sign in");
        Console.WriteLine("2 - > Sign up\n");

        int select = -1; 
        bool validInput = false; 

        while (!validInput) 
        {
            Console.Write("\u001b[34mPlease choose one : \u001b[0m");

            try
            {
                select = Convert.ToInt32(Console.ReadLine());

                if (select == 1 || select == 2) 
                {
                    validInput = true; 
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please select a correct option (1 or 2).");
                    Console.ResetColor();
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Please enter a number.");
                Console.ResetColor();
            }
        }

      
        if (select == 1)
        {
            Worker_Sign_In();
            Console.Clear();
            Employer_Repository.Worker_Sign_Up();

        }
        else if (select == 2)
        {
            Employer_Repository.Worker_Sign_Up();
        }
    }


    public void Worker_Sign_In()
    {
        Console.Clear();
        Console.WriteLine("\u001b[34m                                                     Registration\u001b[0m");
        var dbContext = new DbContext(); 
        Employer_Repository = new Worker_Employer_Repository(dbContext);

        Employer_Repository.Add_Worker();
        
    }

    public void Employer_Sign_In() {
        Console.Clear();
        Console.WriteLine("\u001b[34m                                                     Registration\u001b[0m");
        var dbContext = new DbContext();
        Employer_Repository = new Worker_Employer_Repository(dbContext);

        Employer_Repository.Add_Employer();
    }
    public void Employer_Introduction() {

        Console.Clear();
        Console.WriteLine("                                                   \x1b[34m       Login\x1b[0m");
        Console.WriteLine("1 - > Sign in");
        Console.WriteLine("2 - > Sign up\n");

        int select = -1;
        bool validInput = false;

        while (!validInput)
        {
            Console.Write("\u001b[34mPlease choose one : \u001b[0m");

            try
            {
                select = Convert.ToInt32(Console.ReadLine());

                if (select == 1 || select == 2)
                {
                    validInput = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please select a correct option (1 or 2).");
                    Console.ResetColor();
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Please enter a number.");
                Console.ResetColor();
            }
        }


        if (select == 1)
        {
            Employer_Sign_In();
            Console.Clear();
            Employer_Repository.Employer_Sign_Up();
            
        }
        else if (select == 2)
        {
            Employer_Repository.Employer_Sign_Up();
        }

    }
}
