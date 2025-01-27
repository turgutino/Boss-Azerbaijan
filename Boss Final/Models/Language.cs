public class Language
{
    public string Name { get; set; }
    public string Level { get; set; }

    public Language(string name, string level)
    {
        Name = name;
        Level = level;
    }

    public override string ToString()
    {
        return $"{Name} - {Level}";
    }
}
