namespace Boss_Final.Models;
public class CV
{
    public string Specialization { get; set; } 
    public string School { get; set; } 
    public int UniversityScore { get; set; } 
    public List<string> Skills { get; set; } 
    public List<string> Companies { get; set; } 
    public List<WorkHistory> WorkHistory { get; set; } 
    public List<Language> Languages { get; set; } 
    public bool HasDistinctionDiploma { get; set; } 
    public string GitLink { get; set; } 
    public string LinkedIn { get; set; }

    public CV(){}

    public CV(string specialization, string school, int universityScore, List<string> skills, List<string> companies, List<WorkHistory> workHistory, List<Language> languages, bool hasDistinctionDiploma, string gitLink, string linkedIn)
    {
        Specialization = specialization;
        School = school;
        UniversityScore = universityScore;
        Skills = skills;
        Companies = companies;
        WorkHistory = workHistory;
        Languages = languages;
        HasDistinctionDiploma = hasDistinctionDiploma;
        GitLink = gitLink;
        LinkedIn = linkedIn;
    }

    public override string ToString() =>
    $"Specialization: {Specialization}\n" +
    $"School: {School}\n" +
    $"University Score: {UniversityScore}\n" +
    $"Skills: {string.Join(", ", Skills)}\n" +
    $"Companies: {string.Join(", ", Companies)}\n" +
    $"Work History: {string.Join(", ", WorkHistory.Select(w => $"{w.Company_Name} ({w.StartDate.ToShortDateString()} - {w.EndDate.ToShortDateString()})"))}\n" +
    $"Languages: {string.Join(", ", Languages.Select(l => $"{l.Name} ({l.Level})"))}\n" +
    $"Distinction Diploma: {HasDistinctionDiploma}\n" +
    $"GitHub: {GitLink}\n" +
    $"LinkedIn: {LinkedIn}";

}
