using Newtonsoft.Json;

public class Program
{
    public static void Main(string[] args)
    {
        var values = JsonConvert.DeserializeObject<ValuesData>(File.ReadAllText(args[0]));
        var tests = JsonConvert.DeserializeObject<TestsData>(File.ReadAllText(args[1]));

        var idToValueMap = values.Values.ToDictionary(item => item.Id, item => item.Value);

        UpdateValues(tests.Tests, idToValueMap);

        File.WriteAllText(args[2], JsonConvert.SerializeObject(tests, Formatting.Indented));
    }

    private static void UpdateValues(List<Item> items, Dictionary<int, string> idToValueMap)
    {
        foreach (var item in items)
        {
            if (idToValueMap.TryGetValue(item.Id, out var value))
            {
                item.Value = value;
            }

            if (item.Values != null)
            {
                UpdateValues(item.Values, idToValueMap);
            }
        }
    }
}

public class ValuesData
{
    public List<Item> Values { get; set; }
}

public class TestsData
{
    public List<Item> Tests { get; set; }
}

public class Item
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Value { get; set; }
    public List<Item> Values { get; set; }
}
