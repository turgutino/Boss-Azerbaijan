using Boss_Final.Models;
using System.Text.Json;

public class DbContext
{
        public List<string> Skills { get; set; } = new List<string>();
        public List<string> Companies { get; set; } = new List<string>();
        public List<Language> Languages { get; set; } = new List<Language>();
        public List<Worker> Workers { get; set; } = new List<Worker>();
        public List<WorkHistory> Workhistory { get; set; } = new List<WorkHistory>();

        public List<Employer> Employers { get; set; }= new List<Employer>(); 

        public List<int> Notfications { get; set; }=new List<int>();
}
