using System.Text.Json;

namespace Boss_Final.Models;
public class Employer
{
    public int Employer_Id { get; set; }
    public string Employer_Name { get; set; }
    public string Employer_Surname { get; set; }
    public string Employer_Gmail { get; set; }
    public string Employer_Password { get; set; }
    public string Employer_Sheher { get; set; }
    public string Employer_Phone { get; set; }
    public int Employer_Age { get; set; }
    public List<int> Notfications { get; set; }
    public Vacancy Vacancy { get; set; }

    private static int next_Id3;

    static Employer()
    {
        next_Id3 = LoadNextId2(); 
    }

    public Employer() { }

    public Employer(string employer_Name, string employer_Surname, string employer_Gmail, string employer_Password, string employer_Sheher, string employer_Phone, int employer_Age, List<int> notfications, Vacancy vacancy)
    {
        Employer_Id = ++next_Id3;
        Employer_Name = employer_Name;
        Employer_Surname = employer_Surname;
        Employer_Gmail = employer_Gmail;
        Employer_Password = employer_Password;
        Employer_Sheher = employer_Sheher;
        Employer_Phone = employer_Phone;
        Employer_Age = employer_Age;
        Notfications = notfications;
        Vacancy = vacancy;
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
        string vacancyInfo = Vacancy != null
            ? Vacancy.ToString()
            : "No vacancy available.";
            
                 return $@"
             
             First Name: {Employer_Name}
             Last Name: {Employer_Surname}
             Gmail: {Employer_Gmail}
             City: {Employer_Sheher}
             Phone: {Employer_Phone}
             Age: {Employer_Age}";
             
    }
}
