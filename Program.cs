
List<Product> products = new List<Product>{
    new Product("Egg", "Groceries", 1.00m, 2),
    new Product("Orange", "Groceries", 0.50m, 20),
    new Product("Carrot", "Groceries", 0.70m, 4),
    new Product("Coke Cola", "Beverages", 2.50m, 3),
    new Product("Orange Juice", "Beverages", 4.76m, 25),
    new Product("Pepsi", "Beverages", 2.50m, 50),
    new Product("Soap","Cleaning", 1.50m, 10),
    new Product("Shampoo","Cleaning", 5.00m, 5),
};

foreach (var product in products)
{
    Console.WriteLine(Format(product));
}

Product product1 = new("Egg", "Groceries", 1.00m, 2);
Product product2 = product1 with { Price = 1.50m, Stock = 5 };
Product product3 = new("Egg", "Groceries", 1.00m, 2);

Console.WriteLine(Format(product1));
Console.WriteLine(Format(product2));

Console.WriteLine($"Records equal: {product1 == product3}");

ProductClass productClass1 = new("Egg", "Groceries", 1.00m, 2);
ProductClass productClass2 = new("Egg", "Groceries", 1.00m, 2);

Console.WriteLine($"Classes equal: {productClass1 == productClass2}");
Console.WriteLine(product1);
Console.WriteLine(productClass1);

static string Format(Product p) => $"{p.Name} | {p.Category} | L {p.Price:N2} | Stock: {p.Stock}";

public record Product(string Name, string Category, decimal Price, int Stock);

public class ProductClass
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public ProductClass(string name, string category, decimal price, int stock)
    {
        Name = name;
        Category = category;
        Price = price;
        Stock = stock;
    }
}