using System.Text.Json;

List<Campus> campuses =
[
    new Campus(1, "CU", "123 Main St"),
    new Campus(2, "CURLA", "456 Elm St"),
    new Campus(3, "CURC", "789 Oak St")
];

string peopleFile = "people.json";
List<Person> people = new List<Person>();

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
    Console.WriteLine("4. People without card.");
    Console.WriteLine("5. Count by type.");
    Console.WriteLine("0. Exit.");
    Console.WriteLine("---------------------------------");
    if (!int.TryParse(Console.ReadLine(), out option)) option = -1;

    switch (option)
    {
        case 1:
            ShowList(people);
            break;

        case 2:
            await AddPerson();
            break;

        case 3:
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";
            ShowList(people.Where(p => p.FullName.Contains(name, StringComparison.OrdinalIgnoreCase)));
            break;

        case 4:
            ShowList(people.Where(p => p.CardIssuedOn == null));
            break;

        case 5:
            foreach (var group in people.GroupBy(g => g.Type)) Console.WriteLine($"{group.Key}: {group.Count()}");
            break;

        case 0:
            Console.WriteLine("See ya!");
            break;

        default:
            Console.WriteLine("Option not valid!");
            break;
    }
    if (option != 0)
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    Console.Clear();
} while (option != 0);

await RegisterPeople();

async Task RegisterPeople()
{
    using FileStream file = File.Create(peopleFile);

    await JsonSerializer.SerializeAsync(file, people);
}

void ShowList(IEnumerable<Person> list)
{
    foreach (Person p in list)
        Console.WriteLine($"FullName: {p.FullName}, Type: {p.Type}, Campus: {campuses.FirstOrDefault(c => c.Id == p.CampusId)?.Name ?? "Unknown"}");
}

async Task AddPerson()
{
    int id = people.Count > 0 ? people.Max(p => p.Id) + 1 : 1;
    string prompt = "Enter full name: ";
    string fullName = Ask(prompt, input => (!string.IsNullOrWhiteSpace(input), input));

    prompt = "Enter person type: ";
    foreach (var pt in Enum.GetValues<PersonType>())
    {
        prompt += (int)pt + " " + pt + ", ";
    }

    PersonType personType = Ask(prompt, input => (Enum.TryParse(input, true, out PersonType pt) && Enum.IsDefined(pt), pt));

    prompt = "Enter Campus: ";
    campuses.ForEach(c => prompt += $"{c.Id}. {c.Name}, ");

    int campusId = Ask(prompt, input =>
{
    bool isNumber = int.TryParse(input, out int tempId);
    var matched = campuses.FirstOrDefault(c =>
        c.Id == tempId ||
        string.Equals(c.Name, input, StringComparison.OrdinalIgnoreCase));

    return (matched != null, matched?.Id ?? 0);
});
    people.Add(new Person(id, fullName, personType, campusId, null));
    await RegisterPeople();
    Console.WriteLine("Person added to the list!");
}

T Ask<T>(string prompt, Func<string, (bool Ok, T Value)> convert)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine() ?? "";
        var (ok, value) = convert(input);
        if (ok) return value;
        Console.WriteLine("Invalid value, try again.");
    }
}
public enum PersonType
{
    Student,
    Faculty,
    Staff
}

record Campus(int Id, string Name, string Location);
record Person(int Id, string FullName, PersonType Type, int CampusId, DateOnly? CardIssuedOn);
