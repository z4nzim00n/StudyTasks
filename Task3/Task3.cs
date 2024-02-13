using Newtonsoft.Json;

namespace Task3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ReportCreator task3 = new ReportCreator(args[0], args[1])
            .CreateFile("report.json");
        }
    }

    public class TestsContainer
    {
        public List<TestItem> Tests { get; set; }
    }
    public class TestItem
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public List<TestItem> values { get; set; }
    }

    public class ValuesContainer
    {
        public List<ValueItem> Values { get; set; }
    }
    public class ValueItem
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }




    public class ReportCreator
    {
        private TestsContainer testsData = new TestsContainer();
        private ValuesContainer valueData = new ValuesContainer();


        public ReportCreator(string tests, string values)
        {
            testsData = LoadData<TestsContainer>(tests);
            valueData = LoadData<ValuesContainer>(values);
        }

        private T LoadData<T>(string filePath)
        {
            var jsonStringFromFile = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonStringFromFile);
        }

        public ReportCreator CreateFile(string reportFileName)
        {
            FillValuesRecursive(testsData.Tests);
            var reportJson = JsonConvert.SerializeObject(testsData, Formatting.Indented);
            File.WriteAllText(reportFileName, reportJson);
            return this;
        }

        private void FillValuesRecursive(List<TestItem> items)
        {
            foreach (var item in items)
            {
                var value = valueData.Values.FirstOrDefault(v => v.Id == item.Id);
                if (value != null)
                {
                    item.value = value.Value;
                }

                if (item.values != null && item.values.Any())
                {
                    FillValuesRecursive(item.values);
                }
            }
        }

    }
}
