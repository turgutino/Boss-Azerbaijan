namespace Boss_Final.Models;

public class WorkHistory
{
    public string Company_Name { get; set; }

    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }

    
    public WorkHistory(){}

    public WorkHistory(string company_Name, DateTime startDate, DateTime endDate)
    {
        Company_Name = company_Name;
        StartDate = startDate;
        EndDate = endDate;
       
    }

    public override string ToString() => $"Company Name : {Company_Name}\nStart Date : {StartDate}\nEnd Date : {EndDate}";
}
    
     
