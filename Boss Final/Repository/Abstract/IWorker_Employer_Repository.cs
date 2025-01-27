using Boss_Final.Models;

namespace Boss_Final.Repository.Abstract;

public interface IWorker_Employer_Repository
{
    public void Add_Worker();

    public void Show_All();

    public void Worker_Sign_Up();

    public void ShowMenu(Worker worker);

    public string ReadPassword();

    public void Add_Employer();

    public void Show_All_Employers();

    public void Show_All_Vacancies();

    public void Employer_Sign_Up();

    public void ShowEmployerMenu(Employer employer);
}
