using System.Text.Json;

List<Campus> campuses =
[
    new Campus(1, "CU", "123 Main St"),
    new Campus(2, "CURLA", "456 Elm St"),
    new Campus(3, "CURC", "789 Oak St")
];

string peopleFile = "people.json";
List<Person> people = new List<Person>();

    Console.Write(File.Exists(peopleFile));

if (File.Exists(peopleFile))
{
    using FileStream file = File.OpenRead(peopleFile);
    people = await JsonSerializer.DeserializeAsync<List<Person>>(file) ?? [];
}

Console.WriteLine("***** Campus Identity Menu *****");
Console.WriteLine("--------------------------------");
Console.WriteLine("1. List.");
Console.WriteLine("2. Add.");
Console.WriteLine("3. Find by name.");
Console.WriteLine("---------------------------------");
Console.WriteLine(JsonSerializer.Serialize(people.Where(p=>p.CardIssuedOn == null).OrderBy(p=>p.CampusId)));
Console.WriteLine(people.GroupBy(p=>p.Type));

if (people != null && people.Count>0)
{
    if (File.Exists(peopleFile))
    {
        using FileStream file = File.OpenWrite(peopleFile);

        await JsonSerializer.SerializeAsync(file, people);
    }
}

public enum PersonType
{
    Student,
    Faculty,
    Staff
}

public record Campus(int Id, string Name, string Location);
public record Person(int Id, string FullName, PersonType Type, int CampusId, DateOnly? CardIssuedOn);
