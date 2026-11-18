List <Campus> campuses = new()
{
    new Campus(1, "CU", "123 Main St"),
    new Campus(2, "CURLA", "456 Elm St"),
    new Campus(3, "CURC", "789 Oak St")
};

List<Person> people =
[
    new Person(1, "John Doe", PersonType.Student, 1, new DateOnly(2022, 9, 1)),
    new Person(2, "Jane Smith", PersonType.Faculty, 2, new DateOnly(2021, 8, 15)),
    new Person(3, "Alice Johnson", PersonType.Staff, 3, null),
    new Person(4, "Bob Brown", PersonType.Student, 1, new DateOnly(2023, 1, 10)),
    new Person(5, "Charlie Davis", PersonType.Faculty, 2, null),
    new Person(6, "Diana Evans", PersonType.Student, 3, new DateOnly(2022, 8, 22)),
    new Person(7, "Edward Garcia", PersonType.Staff, 1, new DateOnly(2020, 5, 12)),
    new Person(8, "Fatima Hassan", PersonType.Faculty, 3, new DateOnly(2019, 10, 1)),
    new Person(9, "George Kim", PersonType.Student, 2, null),
    new Person(10, "Hannah Lee", PersonType.Staff, 2, new DateOnly(2024, 2, 5)),
    new Person(11, "Isaac Martinez", PersonType.Student, 1, new DateOnly(2023, 9, 4)),
    new Person(12, "Julia Nguyen", PersonType.Faculty, 1, null),
    new Person(13, "Kevin Patel", PersonType.Student, 3, new DateOnly(2021, 1, 18)),
    new Person(14, "Linda Robinson", PersonType.Staff, 3, new DateOnly(2018, 7, 9)),
    new Person(15, "Marcus Thompson", PersonType.Faculty, 2, new DateOnly(2020, 11, 16)),
    new Person(16, "Nora Wilson", PersonType.Student, 2, null),
    new Person(17, "Oscar Young", PersonType.Staff, 1, new DateOnly(2022, 4, 27)),
    new Person(18, "Priya Shah", PersonType.Faculty, 3, new DateOnly(2017, 8, 14)),
    new Person(19, "Robert Turner", PersonType.Student, 1, new DateOnly(2024, 9, 2)),
    new Person(20, "Sophia White", PersonType.Student, 3, null)
];
public enum PersonType
{
    Student,
    Faculty,
    Staff
}

public record Campus(int Id, string Name, string Location);
public record Person(int Id, string FullName,PersonType Type,int CampusId, DateOnly? CardIssuedon);
