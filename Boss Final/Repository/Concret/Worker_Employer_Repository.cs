using Boss_Final.Models;
using Boss_Final.Repository.Abstract;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


namespace Boss_Final.Repository.Concret;
public class Worker_Employer_Repository : IWorker_Employer_Repository
{
    private readonly DbContext _dbContext;
    public Worker_Employer_Repository() { }
    public Worker_Employer_Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public void Add_Worker()
    {
        string name, surname, sheher, phone, gmail, password;
        int age;

        Console.Write("Enter Name: ");
        name = Console.ReadLine();

        Console.Write("Enter Surname: ");
        surname = Console.ReadLine();

        Console.Write("Enter Gmail: ");
        gmail = Console.ReadLine();

        Console.Write("Enter Password: ");
        password = Console.ReadLine();

        Console.Write("Enter Sheher: ");
        sheher = Console.ReadLine();

        Console.Write("Enter Phone: ");
        phone = Console.ReadLine();

        while (true)
        {
            Console.Write("Enter Age: ");
            try
            {
                age = Convert.ToInt32(Console.ReadLine());
                if (age > 0) break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Age must be a positive number.");
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Please enter a valid age.");
                Console.ResetColor();
            }
        }

       
        int verificationCode = new Random().Next(100000, 999999);
        Console.WriteLine("\nSending verification code to your email...");
        try
        {
            SendVerificationCode(gmail, verificationCode);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Failed to send email: {ex.Message}");
            Console.ResetColor();
            return;
        }

        Console.WriteLine("Verification code sent. Please check your email.");
        while (true)
        {
            Console.Write("Enter the verification code: ");
            int userInputCode;
            if (int.TryParse(Console.ReadLine(), out userInputCode) && userInputCode == verificationCode)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Verification successful!");
                Console.ResetColor();
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid code. Please try again.");
                Console.ResetColor();
            }
        }

      
        string specialization, school, gitLink, linkedIn;
        bool hasDistinctionDiploma;
        int universityScore;

        Console.Write("Enter Specialization: ");
        specialization = Console.ReadLine();

        Console.Write("Enter School Name: ");
        school = Console.ReadLine();

        while (true)
        {
            Console.Write("Enter University Score: ");
            try
            {
                universityScore = Convert.ToInt32(Console.ReadLine());
                if (universityScore >= 0 && universityScore <= 700) break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("University score must be between 0 and 700.");
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Please enter a valid score.");
                Console.ResetColor();
            }
        }

        while (true)
        {
            Console.Write("Do you have a distinction diploma? (yes/no): ");
            string input = Console.ReadLine().ToLower();
            if (input == "yes")
            {
                hasDistinctionDiploma = true;
                break;
            }
            else if (input == "no")
            {
                hasDistinctionDiploma = false;
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Please enter 'yes' or 'no'.");
                Console.ResetColor();
            }
        }

        Console.Write("Enter GitHub Link: ");
        gitLink = Console.ReadLine();

        Console.Write("Enter LinkedIn Link: ");
        linkedIn = Console.ReadLine();

        List<string> skills = new List<string>();
        Console.Write("Enter Skills (type 'done' to stop): ");
        while (true)
        {
            string skill = Console.ReadLine();
            if (skill.ToLower() == "done") break;
            skills.Add(skill);
        }

        List<string> companies = new List<string>();
        Console.Write("Enter Companies (type 'done' to stop): ");
        while (true)
        {
            string company = Console.ReadLine();
            if (company.ToLower() == "done") break;
            companies.Add(company);
        }

        List<Language> languages = new List<Language>();
        Console.Write("Enter Languages (type 'done' to stop): ");
        while (true)
        {
            Console.Write("Enter Language Name (or type 'done' to stop): ");
            string languageName = Console.ReadLine();
            if (languageName.ToLower() == "done") break;

            string languageLevel = string.Empty;

            while (true)
            {
                Console.Write("Enter Language Level (Beginner, Intermediate, Advanced): ");
                try
                {
                    languageLevel = Console.ReadLine();
                    if (languageLevel.Equals("Beginner", StringComparison.OrdinalIgnoreCase) ||
                        languageLevel.Equals("Intermediate", StringComparison.OrdinalIgnoreCase) ||
                        languageLevel.Equals("Advanced", StringComparison.OrdinalIgnoreCase))
                    {

                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid level! Please enter 'Beginner', 'Intermediate', or 'Advanced'.");
                        Console.ResetColor();
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("An unexpected error occurred. Please try again.");
                    Console.ResetColor();
                }
            }
            languages.Add(new Language(languageName, languageLevel));
        }

        List<WorkHistory> workHistories = new List<WorkHistory>();
        Console.Write("Enter Work History (type 'done' to stop): ");
        while (true)
        {
            Console.Write("Enter Company Name (or type 'done' to stop): ");
            string companyName = Console.ReadLine();
            if (companyName.ToLower() == "done") break;

            Console.Write("Enter Start Date (e.g., MM/DD/YYYY): ");
            DateTime startDate;
            while (!DateTime.TryParse(Console.ReadLine(), out startDate))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid date format. Please re-enter Start Date (e.g., MM/DD/YYYY): ");
                Console.ResetColor();
            }

            Console.Write("Enter End Date (e.g., MM/DD/YYYY or 'present'): ");
            string endDateInput = Console.ReadLine();
            DateTime endDate;

            if (endDateInput.ToLower() == "present")
            {
                endDate = DateTime.Now;
            }
            else
            {
                while (!DateTime.TryParse(endDateInput, out endDate))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invalid date format. Please re-enter End Date (e.g., MM/DD/YYYY): ");
                    Console.ResetColor();
                }
            }
            workHistories.Add(new WorkHistory(companyName, startDate, endDate));
        }
        Worker newWorker = new Worker(
            name,
            surname,
            gmail,
            password,
            sheher,
            phone,
            age,
            new CV(specialization, school, universityScore, skills, companies, workHistories, languages, hasDistinctionDiploma, gitLink, linkedIn)
        );

        string filePath = "WorkerDatabasee.json";
        DbContext existingData;

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {
            existingData = new DbContext();
        }

        existingData.Workers.Add(newWorker);

        string updatedJson = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, updatedJson);
        Console.Write("\u001b[32m");
        Console.WriteLine("Worker successfully added to the database.");
        Console.WriteLine("\nEnglish CV successfully created, and you have signed up.");
        Console.WriteLine("\nPlease go back to the Sign Up section and log in.");
        Console.Write("\u001b[0m");
    }

    private void SendVerificationCode(string email, int code)
    {
        MailMessage message = new MailMessage();
        message.From = new MailAddress("strongtiger606@gmail.com");
        message.To.Add(email);
        message.Subject = "Verification Code";
        message.Body = $"Your verification code is: {code}";

        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("strongtiger606@gmail.com", "fotn olzs vjjw doum"),
            EnableSsl = true
        };

        smtpClient.Send(message);
    }
    public void Show_All()
    {
        string filePath = "WorkerDatabasee.json";
        DbContext existingData;
        if (File.Exists(filePath))
        {
        
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {
          
            existingData = new DbContext();
        }
        foreach (var worker in existingData.Workers)
        {
            Console.WriteLine(worker.ToString());
            Console.WriteLine(new string('-', 50)); 
        }
    }

    public void Worker_Sign_Up()
    {
        Console.Clear();
        Console.WriteLine("                                                   \x1b[34m    Login\x1b[0m");
        string gmail, password;
        while (true)
        {
            Console.Write("Enter Gmail : ");
            gmail = Console.ReadLine();

           
            if (gmail.Contains("@") && gmail.Contains("."))
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Gmail format! Please enter a valid Gmail address.");
                Console.ResetColor();
            }
        }
        while (true)
        {
            Console.Write("Enter Password : ");
            password = ReadPassword();
            break;
        }
        string filePath = "WorkerDatabasee.json";
        DbContext existingData;

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {
            existingData = new DbContext();
        }

        bool check = false;

        while (!check)
        {
            foreach (var worker in existingData.Workers)
            {
                if (worker.Worker_Gmail == gmail && worker.Worker_Password == password)
                {
                    Console.WriteLine("\x1b[32m                                                Successfully sign up\n\x1b[0m");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("\x1b[34m                                                           My Account\x1b[0m");
                    Console.WriteLine("");
                    check = true;
                    ShowMenu(worker); 
                    break;
                }
            }
            if (!check)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Gmail or Password are incorrect. Please try again.");
                Console.ResetColor();
                Thread.Sleep(1000);
                Console.Clear();
                Console.Write("Enter Gmail : ");
                gmail = Console.ReadLine();
                Console.Write("Enter Password : ");
                password = ReadPassword();
            }
        }
    }
    public void ShowMenu(Worker worker)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\x1b[34mSelect correct one: \x1b[0m");
            Console.WriteLine("\n1. View CV");
            Console.WriteLine("2. View All Vacancies");
            Console.WriteLine("3. View My Profile");
            Console.WriteLine("4. Edit CV");
           
            Console.WriteLine("5. Logout");
            Console.Write("\nEnter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewCV(worker);
                    break;
                case "2":
                    ViewAllVacancies(worker);
                    break;
                case "3":
                    ViewProfile(worker);
                    break;
                case "4":
                    EditCV(worker.Worker_Id);
                    break;
                
                case "5":
                    Console.WriteLine("\nLogging out...");
                    Thread.Sleep(1000);
                    return; 
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice! Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }
    public void ViewCV(Worker worker)
    {
        Console.Clear();
        Console.WriteLine("\x1b[34m                                                      Your CV\x1b[0m");
        string filePath = "WorkerDatabasee.json";
        DbContext existingData;

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
            var currentWorker = existingData.Workers.FirstOrDefault(w => w.Worker_Id == worker.Worker_Id);
            if (currentWorker != null && currentWorker.Worker_CV != null)
            {
                Console.WriteLine(currentWorker.Worker_CV.ToString());
            }
            else
            {
                Console.WriteLine("You don't have a CV yet.");
            }
        }
        else
        {
            Console.WriteLine("No data file found.");
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey(); 
    }
    public void ViewAllVacancies(Worker worker)
    {
        Console.Clear();
        Console.WriteLine("\x1b[34mAll Vacancies:\n\x1b[0m");
        Show_All_Vacancies();

        Console.Write("\nSelect the vacancy ID you want to send your CV to : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID entered. Please try again.");
            return;
        }

        string filePath = "EmployerDataBase4.json";
        DbContext existingData;

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {
            existingData = new DbContext();
        }

        foreach (var employer in existingData.Employers)
        {
            if (id == employer.Employer_Id)
            {
                employer.Notfications.Add(worker.Worker_Id);

                string emailBody = $@"
                <h2>Hörmətli {employer.Employer_Name},</h2>
                <p>{worker.Worker_Name} iş elanınıza müraciət etdi. Bildirişlər bölmənizi yoxlamağınızı tövsiyə edirik.</p>
                <p>İşçinin tam məlumatını və CV-sini bildirişlər bölməsində tapa bilərsiniz.</p>";
                SendEmail(employer.Employer_Gmail, "Yeni İş Müraciəti", emailBody);
                Console.WriteLine("\x1b[32mYour CV and application have been successfully sent.\x1b[0m");

            }
        }
        lock (new object())
        {
            string updatedJson = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedJson);
        }
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
    static void SendEmail(string toEmail, string subject, string body)
    {
        string fromEmail = "strongtiger606@gmail.com";
        string password = "fotn olzs vjjw doum";

        if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("SMTP configuration information is missing.");
            return;
        }

        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = true
        };

        MailMessage message = new MailMessage
        {
            From = new MailAddress(fromEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        message.To.Add(toEmail);
        smtp.Send(message);
    }

    public void ViewProfile(Worker worker)
    {
        Console.Clear();
        Console.WriteLine("\x1b[34mMy Profile:\x1b[0m");
        string filePath = "WorkerDatabasee.json";
        DbContext existingData;

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {
            existingData = new DbContext();
        }

        var currentWorker = existingData.Workers.FirstOrDefault(w => w.Worker_Id == worker.Worker_Id);

        if (currentWorker != null)
        {
           
            Console.WriteLine(currentWorker.ToString());
        }
        else
        {
            Console.WriteLine("Profile not found.");
        }
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

      


    public void EditCV(int workerId)
    {
      
        string filePath = "WorkerDatabasee.json";
        DbContext existingData;

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No data found!");
            Console.ResetColor();
            return;
        }

        Worker worker = existingData.Workers.FirstOrDefault(w => w.Worker_Id == workerId);
        if (worker == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Worker not found!");
            Console.ResetColor();
            return;
        }

        Console.Clear();
        Console.WriteLine("\x1b[34mEdit Your CV:\x1b[0m");
        ViewCV(worker);Console.WriteLine();
        worker.Worker_CV.Specialization = GetInput("Specialization", worker.Worker_CV.Specialization);
        worker.Worker_CV.School = GetInput("School", worker.Worker_CV.School);
        worker.Worker_CV.UniversityScore = GetIntInput("University Score", worker.Worker_CV.UniversityScore);

        Console.Write("Please enter new skills (comma separated) or press Enter to keep current:");
        var newSkills = Console.ReadLine();
        if (!string.IsNullOrEmpty(newSkills))
            worker.Worker_CV.Skills = new List<string>(newSkills.Split(','));

        Console.Write("Please enter new companies (comma separated) or press Enter to keep current:");
        var newCompanies = Console.ReadLine();
        if (!string.IsNullOrEmpty(newCompanies))
            worker.Worker_CV.Companies = new List<string>(newCompanies.Split(','));

        Console.Write("Enter GitHub Link (or press Enter to keep current): ");
        string gitLink = Console.ReadLine();
        if (!string.IsNullOrEmpty(gitLink)) worker.Worker_CV.GitLink = gitLink;

        Console.Write("Enter LinkedIn Link (or press Enter to keep current): ");
        string linkedIn = Console.ReadLine();
        if (!string.IsNullOrEmpty(linkedIn)) worker.Worker_CV.LinkedIn = linkedIn;

      
        List<Language> updatedLanguages = new List<Language>();
        Console.Write("Update Languages (type 'done' to stop, press Enter to keep current) , ");
        foreach (var language in worker.Worker_CV.Languages)
        {
            Console.Write($"Current Language : {language.Name}, Level : {language.Level}\n");
            Console.Write("Do you want to update this language? (y/n) : ");
            if (Console.ReadLine().ToLower() == "y")
            {
                string languageLevel = string.Empty;
                while (true)
                {
                    Console.Write("Enter new Language Level (Beginner, Intermediate, Advanced) : ");
                    languageLevel = Console.ReadLine();
                    if (new[] { "Beginner", "Intermediate", "Advanced" }.Contains(languageLevel, StringComparer.OrdinalIgnoreCase))
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid level! Please enter 'Beginner', 'Intermediate', or 'Advanced'.");
                        Console.ResetColor();
                    }
                }
                updatedLanguages.Add(new Language(language.Name, languageLevel));
            }
            else
            {
                updatedLanguages.Add(language); 
            }
        }
        worker.Worker_CV.Languages = updatedLanguages;


        List<WorkHistory> updatedWorkHistories = new List<WorkHistory>();
        Console.WriteLine("Update Work History (type 'done' to stop, press Enter to keep current):");
        foreach (var workHistory in worker.Worker_CV.WorkHistory)
        {
            Console.WriteLine($"Current Job: {workHistory.Company_Name}, Start: {workHistory.StartDate.ToShortDateString()}, End: {(workHistory.EndDate == DateTime.MaxValue ? "Present" : workHistory.EndDate.ToShortDateString())}");
            Console.Write("Do you want to update this job? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                DateTime startDate;
                Console.Write("Enter new Start Date (MM/DD/YYYY): ");
                while (!DateTime.TryParse(Console.ReadLine(), out startDate))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invalid date format. Please re-enter Start Date (e.g., MM/DD/YYYY): ");
                    Console.ResetColor();
                }

                DateTime endDate;
                Console.Write("Enter new End Date (MM/DD/YYYY or 'present'): ");
                string endDateInput = Console.ReadLine();
                endDate = endDateInput.ToLower() == "present" ? DateTime.MaxValue : DateTime.Parse(endDateInput);

                updatedWorkHistories.Add(new WorkHistory(workHistory.Company_Name, startDate, endDate));
            }
            else
            {
                updatedWorkHistories.Add(workHistory); 
            }
        }
        worker.Worker_CV.WorkHistory = updatedWorkHistories;

        
        string updatedJson = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, updatedJson);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("                                        Worker CV successfully updated.");
        Thread.Sleep(1000);
        Console.ResetColor();
    }

 
    private string GetInput(string fieldName, string defaultValue)
    {
        Console.Write($"{fieldName} (current: {defaultValue}):");
        string input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? defaultValue : input;
    }

    private int GetIntInput(string fieldName, int defaultValue)
    {
        Console.Write($"{fieldName} (current: {defaultValue}):");
        string input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? defaultValue : int.Parse(input);
    }

   

    public void Add_Employer()
    {
        string name, surname, sheher, phone, gmail, password;
        int age;

        Console.Write("Enter Name: ");
        name = Console.ReadLine();

        Console.Write("Enter Surname: ");
        surname = Console.ReadLine();

        Console.Write("Enter Gmail: ");
        gmail = Console.ReadLine();

        Console.Write("Enter Password: ");
        password = Console.ReadLine();

        Console.Write("Enter Sheher: ");
        sheher = Console.ReadLine();

        Console.Write("Enter Phone: ");
        phone = Console.ReadLine();

        while (true)
        {
            Console.Write("Enter Age: ");
            try
            {
                age = Convert.ToInt32(Console.ReadLine());
                if (age > 0) break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Age must be a positive number.");
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input! Please enter a valid age.");
                Console.ResetColor();
            }
        }

       
        int verificationCode = new Random().Next(100000, 999999);
        Console.WriteLine("\nSending verification code to your email...");
        try
        {
            SendVerificationCode(gmail, verificationCode);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Failed to send email: {ex.Message}");
            Console.ResetColor();
            return;
        }

        Console.WriteLine("Verification code sent. Please check your email.");
        while (true)
        {
            Console.Write("Enter the verification code: ");
            int userInputCode;
            if (int.TryParse(Console.ReadLine(), out userInputCode) && userInputCode == verificationCode)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Verification successful!");
                Console.ResetColor();
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid code. Please try again.");
                Console.ResetColor();
            }
        }

        Console.Write("\u001b[32m");
        Console.WriteLine("\n\n                                       Everything is prepared for you in a second");
        Thread.Sleep(1000);

        Console.WriteLine("\n                                                     Perfect...");
        Thread.Sleep(1000);

        Console.WriteLine("\n                                               Now let's create a Vacancy.");
        Thread.Sleep(1000);

        Console.Write("\u001b[0m");

        Console.WriteLine("Enter Vacancy Details:");

        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Company Name: ");
        string companyName = Console.ReadLine();

        Console.Write("Location: ");
        string location = Console.ReadLine();

        Console.Write("Salary: ");
        decimal salary;
        while (!decimal.TryParse(Console.ReadLine(), out salary))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input! Please enter a valid salary.");
            Console.ResetColor();
        }

        Console.Write("Employment Type (e.g., Full-time, Part-time): ");
        string employmentType = Console.ReadLine();

        Console.Write("Requirements: ");
        string requirements = Console.ReadLine();

        Console.Write("Responsibilities: ");
        string responsibilities = Console.ReadLine();

        Console.Write("Posted Date (e.g., MM/DD/YYYY): ");
        DateTime postedDate;
        while (!DateTime.TryParse(Console.ReadLine(), out postedDate))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid date! Please enter a valid Posted Date (e.g., MM/DD/YYYY).");
            Console.ResetColor();
        }

        Console.Write("Deadline (e.g., MM/DD/YYYY): ");
        DateTime deadline;
        while (!DateTime.TryParse(Console.ReadLine(), out deadline))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid date! Please enter a valid Deadline (e.g., MM/DD/YYYY).");
            Console.ResetColor();
        }

        Console.Write("Contact Email: ");
        string contactEmail = Console.ReadLine();

        Console.Write("Contact Phone: ");
        string contactPhone = Console.ReadLine();

        Console.Write("Is the vacancy active? (yes/no): ");
        bool isActive = Console.ReadLine().ToLower() == "yes";

        Console.Write("Experience Level (e.g., Beginner, Intermediate, Advanced): ");
        string experienceLevel = Console.ReadLine();


        Employer employer = new Employer(
            name,
            surname,
            gmail,
            password,
            sheher,
            phone,
            age,
            _dbContext.Notfications,
            new Vacancy(title, description, companyName, location, salary, employmentType, requirements, responsibilities, postedDate,
            deadline, contactEmail, contactPhone, isActive, experienceLevel)
        );

        string filePath = "EmployerDataBase4.json";
        DbContext existingData;

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {
            existingData = new DbContext();
        }

        existingData.Employers.Add(employer);

        string updatedJson = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, updatedJson);

        Console.Write("\u001b[32m");
        Console.WriteLine("Employer successfully added to the database.");
        Console.WriteLine("\nEnglish CV successfully created, and you have signed up.");
        Console.WriteLine("\nPlease go back to the Sign Up section and log in.");
        Console.Write("\u001b[0m");
    }

    
    public void Show_All_Employers()
    {
        string filePath = "EmployerDataBase4.json";

        DbContext existingData;


        if (File.Exists(filePath))
        {

            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {

            existingData = new DbContext();
        }

        foreach (var employer in existingData.Employers)
        {
            Console.WriteLine(employer.ToString());
            Console.WriteLine(new string('-', 50));
        }
    }
    public void Show_All_Vacancies()
    {
        string filePath = "EmployerDataBase4.json";

        DbContext existingData;
        if (File.Exists(filePath))
        {

            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {

            existingData = new DbContext();
        }


        foreach (var employer in existingData.Employers)
        {
            Console.WriteLine(employer.Employer_Name);
            Console.WriteLine(employer.Employer_Surname);
            Console.WriteLine(employer.Employer_Sheher);
            Console.WriteLine(employer.Employer_Phone);
            Console.WriteLine(employer.Vacancy.ToString());
            Console.WriteLine(new string('-', 50));
        }
    }
    public void Employer_Sign_Up()
    {
        Console.Clear();
        Console.WriteLine("                                                   \x1b[34m    Login\x1b[0m");

        string gmail, password;
        while (true)
        {
            Console.Write("Enter Gmail : ");
            gmail = Console.ReadLine();

     
            if (gmail.Contains("@") && gmail.Contains("."))
            {
                break; 
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Gmail format! Please enter a valid Gmail address.");
                Console.ResetColor();
            }
        }

        while (true)
        {
            Console.Write("Enter Password : ");
            password = ReadPassword();  
            break;
        }

        string filePath = "EmployerDataBase4.json";

        DbContext existingData;

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {
            existingData = new DbContext();
        }

        bool check = false;

        
        while (!check)
        {
            foreach (var employer in existingData.Employers)
            {
                if (employer.Employer_Gmail == gmail && employer.Employer_Password == password)
                {
                    Console.WriteLine("\x1b[32m                                                Successfully signed in\n\x1b[0m");
                    Thread.Sleep(1000);
                    Console.Clear();
                    ShowEmployerMenu(employer);
                    check = true;
                    break;
                }
            }

            if (!check)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Gmail or Password are incorrect. Please try again.");
                Console.ResetColor();
                Thread.Sleep(1000);
                Console.Clear();
               
                Console.Write("Enter Gmail : ");
                gmail = Console.ReadLine();

                Console.Write("Enter Password : ");
                password = ReadPassword(); 
            }
        }
    }

    public void ShowEmployerMenu(Employer employer)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\x1b[34mEmployer Menu:\x1b[0m");
            Console.WriteLine("\n1. View My Vacancies");
            Console.WriteLine("2. Notifications");
            Console.WriteLine("3. View My Profile");
            Console.WriteLine("4. Edit Vacancy");
            Console.WriteLine("5. Logout");

            Console.Write("\nEnter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewMyVacancies(employer.Employer_Id);
                    break;
                case "2":
                    ViewNotifications(employer);
                    break;
                case "3":
                    ViewMyProfile(employer);
                    break;
                case "4":
                    EditVacancy(employer.Employer_Id); 
                    break;
                case "5":
                    Console.WriteLine("\nLogging out...");
                    Thread.Sleep(1000);
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice! Please try again.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    public void ViewMyVacancies(int employerId)
    {
        string filePath = "EmployerDataBase4.json";
        DbContext existingData;

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No data found!");
            Console.ResetColor();
            return;
        }

        Employer employer = existingData.Employers.FirstOrDefault(e => e.Employer_Id == employerId);
        if (employer == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Employer not found!");
            Console.ResetColor();
            return;
        }

        Console.Clear();
        Console.WriteLine("\x1b[34mYour Vacancies:\x1b[0m");

        
        if (employer.Vacancy != null)
        {
            
            Console.WriteLine(employer.Vacancy.ToString());
        }
        else
        {
            Console.WriteLine("You have not created any vacancies yet.");
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

    public void ViewNotifications(Employer employer)
    {
        string filePath1 = "WorkerDatabasee.json";
        string filePath2 = "EmployerDataBase4.json";

        
        DbContext existingData = null;
        DbContext existingData2 = null;

        try
        {
            
            string json1 = File.ReadAllText(filePath1);
            existingData = JsonSerializer.Deserialize<DbContext>(json1) ?? new DbContext();

           
            string json2 = File.ReadAllText(filePath2);
            existingData2 = JsonSerializer.Deserialize<DbContext>(json2) ?? new DbContext();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error has occurred {ex.Message}");
            return;
        }

        Console.Clear();
        Console.WriteLine("\x1b[34mYour Notifications :\n\x1b[0m");

        HashSet<int> processedIds = new HashSet<int>();
        bool anyAccepted = false;

        foreach (var notificationId in employer.Notfications)
        {
            if (!processedIds.Contains(notificationId))
            {
                foreach (var worker in existingData.Workers)
                {
                    if (worker.Worker_Id == notificationId)
                    {
                        
                        Console.WriteLine(worker.ToString());

                        Console.WriteLine("Worker's CV : ");
                        Console.WriteLine(worker.Worker_CV.ToString());
                        Console.WriteLine("---------------------------------------------------------------");
                        processedIds.Add(notificationId);
                        break;
                    }
                }
            }
        }

        if (processedIds.Count == 0)
        {
            Console.WriteLine("\nYou currently have no notifications.");
        }
        else
        {
            try
            {
                bool validIdEntered = false;

                while (!validIdEntered)
                {
                    Console.Write("\nEnter worker ID (or press Enter to go back): ");

                    string input = Console.ReadLine();

                 
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Returning to the previous menu...");
                        return;
                    }
                    if (int.TryParse(input, out int selectedWorkerId) && processedIds.Contains(selectedWorkerId))
                    {
                        
                        var selectedWorker = existingData.Workers.FirstOrDefault(w => w.Worker_Id == selectedWorkerId);

                        if (selectedWorker != null)
                        {
                            
                            SendEmail3(selectedWorker.Worker_Gmail, "Təbriklər!", "Təbrik edirik, sizin müraciətiniz qəbul edilmişdir.");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"A congratulatory message has been sent to the worker named {selectedWorker.Worker_Name}.");
                            Console.ResetColor();  

                            anyAccepted = true;
                            validIdEntered = true;  
                        }
                        else
                        {
                            Console.WriteLine("The selected worker was not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The correct ID was not entered. Please try again.");
                    }
                }

                  

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending the notification : {ex.Message}");
            }
            finally
            {
                if (anyAccepted)
                {
                    
                    employer.Notfications.RemoveAll(id => processedIds.Contains(id));

                    var employerInDb = existingData2.Employers.FirstOrDefault(e => e.Employer_Id == employer.Employer_Id);
                    if (employerInDb != null)
                    {
                        employerInDb.Notfications = employer.Notfications;
                    }
                    try
                    {
                        string updatedJson = JsonSerializer.Serialize(existingData2, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText(filePath2, updatedJson);
                        Console.WriteLine("\nAll notifications have been reset and data has been updated.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while updating the information : {ex.Message}");
                    }

          
                }
            }
        }

        Console.ReadKey();
    }

    private void SendEmail3(string toEmail, string subject, string body)
    {
        try
        {
            string fromEmail = "strongtiger606@gmail.com";
            string password = "fotn olzs vjjw doum";       

            if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("SMTP configuration information is missing.");
                return;
            }

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);
            smtp.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while sending the email : {ex.Message}");
        }
    }


    public void EditVacancy(int employerId)
    {
       
        string filePath = "EmployerDataBase4.json";
        DbContext existingData;

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<DbContext>(existingJson) ?? new DbContext();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No data found!");
            Console.ResetColor();
            return;
        }

        Employer employer = existingData.Employers.FirstOrDefault(e => e.Employer_Id == employerId);
        if (employer == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Employer not found!");
            Console.ResetColor();
            return;
        }

        Console.Clear();
        Console.WriteLine("\x1b[34mCurrent Vacancy :\x1b[0m");
        Console.WriteLine(employer.Vacancy.ToString());
        Console.WriteLine();
        employer.Vacancy.Title = GetInput2("Title", employer.Vacancy.Title);
        employer.Vacancy.Description = GetInput2("Description", employer.Vacancy.Description);
        employer.Vacancy.CompanyName = GetInput2("Company Name", employer.Vacancy.CompanyName);
        employer.Vacancy.Location = GetInput2("Location", employer.Vacancy.Location);

        employer.Vacancy.Salary = GetDecimalInput("Salary", employer.Vacancy.Salary);

        employer.Vacancy.EmploymentType = GetInput2("Employment Type", employer.Vacancy.EmploymentType);
        employer.Vacancy.Requirements = GetInput2("Requirements", employer.Vacancy.Requirements);
        employer.Vacancy.Responsibilities = GetInput2("Responsibilities", employer.Vacancy.Responsibilities);

        employer.Vacancy.PostedDate = GetDateInput("Posted Date", employer.Vacancy.PostedDate);
        employer.Vacancy.Deadline = GetDateInput("Deadline", employer.Vacancy.Deadline);

        employer.Vacancy.ContactEmail = GetInput2("Contact Email", employer.Vacancy.ContactEmail);
        employer.Vacancy.ContactPhone = GetInput2("Contact Phone", employer.Vacancy.ContactPhone);

        string isActiveInput = GetInput2("Is Active? (yes/no)", employer.Vacancy.IsActive ? "yes" : "no");
        employer.Vacancy.IsActive = isActiveInput.ToLower() == "yes";

        employer.Vacancy.ExperienceLevel = GetInput2("Experience Level", employer.Vacancy.ExperienceLevel);

        
        string updatedJson = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, updatedJson);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Vacancy successfully updated.");
        Console.ResetColor();
    }

    
    private string GetInput2(string fieldName, string defaultValue)
    {
        Console.Write($"{fieldName} (current: {defaultValue}):");
        string input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? defaultValue : input;
    }

    private decimal GetDecimalInput(string fieldName, decimal defaultValue)
    {
        Console.Write($"{fieldName} (current: {defaultValue}):");
        string input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? defaultValue : decimal.Parse(input);
    }

    private DateTime GetDateInput(string fieldName, DateTime defaultValue)
    {
        Console.Write($"{fieldName} (current: {defaultValue.ToShortDateString()}):");
        string input = Console.ReadLine();
        return string.IsNullOrEmpty(input) ? defaultValue : DateTime.Parse(input);
    }




    public void ViewMyProfile(Employer employer)
    {
        Console.Clear();
        Console.WriteLine("\x1b[34mYour Profile:\x1b[0m");
        Console.WriteLine(employer.ToString());

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }



    public string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;

        while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
            else if (key.Key != ConsoleKey.Backspace)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        }

        Console.WriteLine();
        return password;
    }

}
