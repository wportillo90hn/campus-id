using System.Text.Json;

List<Campus> campuses =
[
    new Campus(1, "CU", "123 Main St"),
    new Campus(2, "CURLA", "456 Elm St"),
    new Campus(3, "CURC", "789 Oak St")
];

string peopleFile = "people.json";
List<Person> people = [];

if (File.Exists(peopleFile))
{
    using FileStream file = File.OpenRead(peopleFile);
    people = await JsonSerializer.DeserializeAsync<List<Person>>(file) ?? [];
}

int option;
do
{
    Console.WriteLine("***** Campus Identity Menu *****");
    Console.WriteLine("--------------------------------");
    Console.WriteLine("1. List.");
    Console.WriteLine("2. Add.");
    Console.WriteLine("3. Find by name.");
    Console.WriteLine("0. Salir.");
    Console.WriteLine("---------------------------------");
    int.TryParse(Console.ReadLine(), out option);

    switch (option)
    {
        case 1:
            ShowList(people);
            break;

        case 2:
            AddPerson();
            break;

        case 3:
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";
            ShowList(people.Where(p => p.FullName.Contains(name)));
            break;

        case 0:
            Console.WriteLine("See ya!");
            break;

        default:
            Console.WriteLine("Option not valid!");
            break;
    }
    Console.ReadKey();
    Console.Clear();
} while (option != 0);

if (people != null)
{
    using FileStream file = File.Create(peopleFile);

    await JsonSerializer.SerializeAsync(file, people);
}


void ShowList(IEnumerable<Person> people)
{
    if (people != null) foreach (Person p in people)
            Console.WriteLine($"FullName: {p.FullName}, Type: {p.Type}, Campus: {p.CampusId}");
}

void AddPerson()
{
    Person person = new Person();
    person.Id = people.Count > 0 ? people.Max(p => p.Id) + 1 : 1;
    do
    {
        Console.Write("Introduce Full Name: ");
        person.FullName = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(person.FullName))
        {
            Console.WriteLine("Full Name invalid.");
        }
    }
    while (string.IsNullOrWhiteSpace(person.FullName));

    var personTypes = "";
    foreach (var pt in Enum.GetValues<PersonType>())
    {
        personTypes += (int)pt + " " + pt + ", ";
    }

    Console.Write("Introduce Type: " + personTypes);
    do
    {
        PersonType result;
        if (Enum.TryParse<PersonType>(Console.ReadLine() ?? "", true, out result))
        {
            if (Enum.IsDefined(typeof(PersonType), result))
            {
                person.Type = result;
                break;

            }
        }
        Console.WriteLine("Type invalid!");
    } while (true);

    Console.Write("Introduce Campus: ");
    campuses.ForEach(c => Console.Write($"{c.Id}. {c.Name}, "));
    do
    {
        string campus = Console.ReadLine() ?? "";
        int id;
        int.TryParse(campus, out id);
        Campus? result = campuses.Where(c => c.Id == id || string.Equals(c.Name, campus)).FirstOrDefault();
        if (result != null)
        {
            person.CampusId = result.Id;
            break;
        }
        Console.WriteLine("Center invalid!");
    } while (true);
    people.Add(person);
    Console.WriteLine("Person added to the list!");
}
public enum PersonType
{
    Student,
    Faculty,
    Staff
}



record Campus(int Id, string Name, string Location);
class Person
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public PersonType Type { get; set; }
    public int CampusId { get; set; }
    public DateOnly? CardIssuedOn { get; set; }
}
