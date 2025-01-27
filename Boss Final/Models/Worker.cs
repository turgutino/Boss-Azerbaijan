using System.Text.Json;

namespace Boss_Final.Models
{
    public class Worker
    {
        public int Worker_Id { get; set; }
        public string Worker_Name { get; set; }
        public string Worker_Surname { get; set; }
        public string Worker_Gmail { get; set; }
        public string Worker_Password { get; set; }
        public string Worker_Sheher { get; set; }
        public string Worker_Phone { get; set; }
        public int Worker_Age { get; set; }
        public CV Worker_CV { get; set; }

        private static int next_Id;

        static Worker()
        {
            next_Id = LoadNextId();  
        }

        public Worker() { }

        public Worker(string worker_Name, string worker_Surname, string worker_Gmail, string worker_Password, string worker_Sheher, string worker_Phone, int worker_Age, CV worker_CV)
        {
            Worker_Id = ++next_Id;  
            Worker_Name = worker_Name;
            Worker_Surname = worker_Surname;
            Worker_Gmail = worker_Gmail;
            Worker_Password = worker_Password;
            Worker_Sheher = worker_Sheher;
            Worker_Phone = worker_Phone;
            Worker_Age = worker_Age;
            Worker_CV = worker_CV;

            SaveNextId();  
        }

        
        private static int LoadNextId()
        {
            string filePath = "WorkerDatabasee.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var existingData = JsonSerializer.Deserialize<DbContext>(json);
                return existingData?.Workers.Max(w => w.Worker_Id) ?? 0;  
            }
            return 0; 
        }

        
        private static void SaveNextId()
        {
            string filePath = "WorkerDatabasee.json";
            var existingData = File.Exists(filePath)
                ? JsonSerializer.Deserialize<DbContext>(File.ReadAllText(filePath))
                : new DbContext();

            string updatedJson = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedJson); 
        }

        public override string ToString() =>
            $"\nWorker ID: {Worker_Id}\n" +
            $"Name: {Worker_Name}\n" +
            $"Surname: {Worker_Surname}\n" +
            $"Gmail: {Worker_Gmail}\n" +
            //$"Password: {Worker_Password}\n" +
            $"City: {Worker_Sheher}\n" +
            $"Phone: {Worker_Phone}\n" +
            $"Age: {Worker_Age}\n";
            //$"CV:\n {(Worker_CV != null ? Worker_CV.ToString() : "No CV available")}";
    }
}
