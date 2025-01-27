using System.Text.Json;

namespace Boss_Final.Models;
public class Vacancy
{   
    public int Vacancy_Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; }
    public string CompanyName { get; set; }
    public string Location { get; set; } 
    public decimal Salary { get; set; } 
    public string EmploymentType { get; set; } 
    public string Requirements { get; set; }
    public string Responsibilities { get; set; }
    public DateTime PostedDate { get; set; } 
    public DateTime Deadline { get; set; }
    public string ContactEmail { get; set; } 
    public string ContactPhone { get; set; } 
    public bool IsActive { get; set; } 
    public string ExperienceLevel { get; set; }

    private static int next_Id2;

    static Vacancy()
    {
        next_Id2 = LoadNextId2(); 
    }
    public Vacancy(){}

    public Vacancy(string title, string description, string companyName, string location, decimal salary, string employmentType, string requirements, string responsibilities, DateTime postedDate, DateTime deadline, string contactEmail, string contactPhone, bool isActive, string experienceLevel)
    {
        Vacancy_Id = ++next_Id2;

        Title = title;
        Description = description;
        CompanyName = companyName;
        Location = location;
        Salary = salary;
        EmploymentType = employmentType;
        Requirements = requirements;
        Responsibilities = responsibilities;
        PostedDate = postedDate;
        Deadline = deadline;
        ContactEmail = contactEmail;
        ContactPhone = contactPhone;
        IsActive = isActive;
        ExperienceLevel = experienceLevel;
        SaveNextId2();
    }

    private static int LoadNextId2()
    {
        string filePath = "EmployerDataBase4.json";

      
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var existingData = JsonSerializer.Deserialize<DbContext>(json);
            if (existingData?.Employers?.Any() == true)
            {
                return existingData.Employers.Max(e => e.Employer_Id);
            }
        }
        return 0;
    }
    private static void SaveNextId2()
    {
        string filePath = "EmployerDataBase4.json";

        var existingData = File.Exists(filePath)
            ? JsonSerializer.Deserialize<DbContext>(File.ReadAllText(filePath))
            : new DbContext();

        string updatedJson = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
        

        File.WriteAllText(filePath, updatedJson);
    }
    public override string ToString()
    {
        return $@"
    Vacancy ID: {Vacancy_Id}
    Title: {Title}
    Description: {Description}
    Company: {CompanyName}
    Location: {Location}
    Salary: {Salary:C} 
    Job Type: {EmploymentType}
    Requirements: {Requirements}
    Responsibilities: {Responsibilities}
    Posted Date: {PostedDate:yyyy-MM-dd}
    Deadline: {Deadline:yyyy-MM-dd}
    Contact Email: {ContactEmail}
    Contact Phone: {ContactPhone}
    Status: {(IsActive ? "Active" : "Closed")}
    Experience Level: {ExperienceLevel}";
    }
}
