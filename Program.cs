using HarvestFarm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml.Serialization;

class Program
{
    public static void Main()
    {
        
        Player player = new Player("TBB", 100);

        
        List<Product> products = new List<Product>
        {
            //new Wheat(30, 50, 3, 5, 5),    
            //new Tomato(40, 90, 4, 6, 7),   
            new Sunflower(20, 60, 2, 4, 3) 
        };

        //List<Product> chosenProducts = new List<Product>();

        //Console.WriteLine("Choose your plant or enter 0 to end");
        //for (int i = 0; i < products.Count; i++)
        //{
        //    Console.WriteLine($"{i + 1}. {products[i].GetType().Name} (Cost: {products[i].Cost}, Value: {products[i].Value})");
        //}

        //int totalCost = 0;


        //while (true)
        //{
        //    Console.WriteLine("Choose the plant you want from 1 to 3");
        //    string input = Console.ReadLine();
        //    int choice;
        //    bool validChoice = int.TryParse(input, out choice);

        //    if (!validChoice || choice < 0 || choice > products.Count)
        //    {
        //        Console.WriteLine("Choose again!");
        //        continue;
        //    }
        //    if (choice == 0)
        //    {
        //        break;
        //    }

        //    Product selectedProduct = products[choice - 1];


        //    int productTotalCost = selectedProduct.Cost + selectedProduct.FertilizerCost + selectedProduct.WaterCost;


        //    if (totalCost + productTotalCost > player.Reward)
        //    {
        //        Console.WriteLine("Not enough points to buy this");
        //        continue;
        //    }


        //    chosenProducts.Add(selectedProduct);
        //    totalCost += productTotalCost;

        //    Console.WriteLine($"{selectedProduct.GetType().Name} has been added to your inventory.");
        //    Console.WriteLine("\nList of your choice");
        //    foreach (Product product in chosenProducts)
        //    {
        //        Console.WriteLine(product.GetType().Name);
        //    }
        //}


        //if (totalCost > 0)
        //{
        //    player.DeductReward(totalCost); 
        //    Console.WriteLine($"You have spent {totalCost} points. Your remaining points: {player.Reward}");
        //}
        //else
        //{
        //    Console.WriteLine("You haven't chosen any products.");
        //    return; 
        //}


        //foreach (Product product in chosenProducts)
        //{
        //    product.Seed();


        //    if (product is ICare careableProduct)
        //    {
        //        careableProduct.Feed();
        //        careableProduct.ProvideWater();
        //    }

        //    product.Harvest();


        //    int profit = product.Value; 
        //    player.AddReward(profit);
        //    Console.WriteLine($"You earned {profit} points from {product.GetType().Name}. Total points: {player.Reward}");
        //}

        //Console.WriteLine($"Final total points: {player.Reward}");

        //Serialization

        //Json
        string jsonString = JsonSerializer.Serialize(products);
        File.WriteAllText("data.dat", jsonString);



        string newJsonString = File.ReadAllText("data.dat");
        Console.WriteLine("Data serialized to data.dat");

        List<Product> Plist = JsonSerializer.Deserialize<List<Product>>(newJsonString);
        Plist.Add(new Wheat(30, 50, 3, 5, 5));
        Plist.Add(new Tomato(40, 90, 4, 6, 7));

        string jsonUpdated = JsonSerializer.Serialize(Plist);
        File.WriteAllText("data.dat", jsonUpdated);

        string finalJson = File.ReadAllText("data.dat");
        List<Product> finalJsonList = JsonSerializer.Deserialize<List<Product>>(finalJson);
        foreach (Product product in finalJsonList)
        {
            Console.WriteLine($"{product.GetType().Name} - Cost: {product.Cost}, Value: {product.Value}");
        }

        //xml
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Product>));
        using (FileStream fileStream = new FileStream("data.xml", FileMode.Create, FileAccess.Write))
        {
            xmlSerializer.Serialize(fileStream, products);
        }
        Console.WriteLine("Data serialized to data.xml");

        
        using (FileStream fileStream = new FileStream("data.xml", FileMode.Open, FileAccess.Read))
        {
            List<Product> newProductList = (List<Product>)xmlSerializer.Deserialize(fileStream);

            
            Console.WriteLine("Product List after deserialization:");
            foreach (var product in newProductList)
            {
                Console.WriteLine($"{product.GetType().Name} - Cost: {product.Cost}, Value: {product.Value}");
            }
        }

        //Binary
        DataContractSerializer binarySerializer = new DataContractSerializer(typeof(List<Product>));

        string binaryFilePath = "data.bin";

        using (FileStream fs = new FileStream(binaryFilePath, FileMode.Create))
        {
            binarySerializer.WriteObject(fs, products);
        }

        
        List<Product> newProducts;
        using (FileStream fs = new FileStream(binaryFilePath, FileMode.Open))
        {
            newProducts = (List<Product>)binarySerializer.ReadObject(fs);
        }

        
        Console.WriteLine("\nNew Product List after Deserialization:");
        foreach (Product product in newProducts)
        {
            Console.WriteLine($"{product.GetType().Name}: Cost = {product.Cost}, Value = {product.Value}");
        }

        
        newProducts.Add(new Wheat(50, 100, 5, 7, 10));
        newProducts.Add(new Tomato(60, 120, 6, 8, 9));

        
        using (FileStream fs = new FileStream(binaryFilePath, FileMode.Create))
        {
            binarySerializer.WriteObject(fs, newProducts);
        }

        
        using (FileStream fs = new FileStream(binaryFilePath, FileMode.Open))
        {
            newProducts = (List<Product>)binarySerializer.ReadObject(fs);
        }

        Console.WriteLine("\nUpdated Product List :");
        foreach (var product in newProducts)
        {
            Console.WriteLine($"{product.GetType().Name}: Cost = {product.Cost}, Value = {product.Value}");
        }
    }
}
