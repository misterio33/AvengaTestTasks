using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AvengaTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText(@"C:\Users\Pasha\source\repos\AvengaTest1\AvengaTest1\pizzas.json");

            Dictionary<string, int> toppingsCounter = new Dictionary<string, int>() { };
            List<Pizzas> pizzaConfigurationsList = JsonConvert.DeserializeObject<List<Pizzas>>(json);

            for (int configuration = 0; configuration < pizzaConfigurationsList.Count; configuration++)
            {
                var toppingsList = new List<string>() { };

                for (int ingredient = 0; ingredient <= pizzaConfigurationsList[configuration].Toppings.Length - 1; ingredient++)
                {
                    toppingsList.Add(pizzaConfigurationsList[configuration].Toppings[ingredient]);
                }
                string[] toppingsArray = toppingsList.ToArray();
                Array.Sort(toppingsArray, StringComparer.InvariantCulture);

                string toppingsToString = String.Join(", ", toppingsArray);

                //If such pizza configuration already exists - increase counter by 1
                if (toppingsCounter.ContainsKey(toppingsToString))
                {
                    toppingsCounter[toppingsToString] += 1;
                }
                else
                {
                    toppingsCounter.Add(toppingsToString, 1);
                }
            }

            var orderedDict = (from entry in toppingsCounter
                              orderby entry.Value descending
                              select entry).Take(20) ;

            int placeCounter = 1;
            foreach (KeyValuePair<string, int> toppings in orderedDict)
            {
                Console.WriteLine(
                    $"Place - {placeCounter} \r\n" +
                    $"Configuration: {toppings.Key}\r\n" +
                    $"Value: {toppings.Value}\r\n" +
                    $"--------------------------------\r\n");
                placeCounter++;
            }
        }
    }
}
