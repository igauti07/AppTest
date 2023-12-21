using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        // Specify the path to your JSON file
        string jsonFilePath = "/Users/goutham/Downloads/flavors.json";

        // Read JSON data from the file
        string jsonData = File.ReadAllText(jsonFilePath);

        // Deserialize JSON data into a List of IceCreamEntry
        var iceCreamData = JsonConvert.DeserializeObject<List<IceCreamEntry>>(jsonData);

        var sortedList = new List<IceCreamEntry>();
        foreach (var item in iceCreamData)
        {
            string[] flavors = { item.FlavorOne, item.FlavorTwo, item.FlavorThree };
            Array.Sort(flavors);
            sortedList.Add(new IceCreamEntry
            {
                FlavorOne = flavors[0],
                FlavorTwo = flavors[1],
                FlavorThree = flavors[2]
            });
        }

        var filtered = sortedList.GroupBy(n => new { n.FlavorOne, n.FlavorTwo, n.FlavorThree })
                   .Select(x => new
                   {
                       x.Key.FlavorOne,
                       x.Key.FlavorTwo,
                       x.Key.FlavorThree,
                       count = x.Count()
                   })
                   .ToList();
        Console.WriteLine("Combination                 Times Eaten");
        for (int i = 0; i < filtered.Count; i++)
            Console.WriteLine(string.Format("{0},{1},{2} --> {3}",
                                filtered[i].FlavorOne,
                                filtered[i].FlavorTwo,
                                filtered[i].FlavorThree,
                                filtered[i].count));
    }
}

// Define a class to represent each entry in the JSON array
public class IceCreamEntry
{
    public string FlavorOne { get; set; }
    public string FlavorTwo { get; set; }
    public string FlavorThree { get; set; }
}