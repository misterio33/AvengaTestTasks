using Newtonsoft.Json;


namespace AvengaTest1
{
    class Pizzas
    {
        [JsonProperty("toppings")]
        public string[] Toppings { get; set; }
      
    }
}
