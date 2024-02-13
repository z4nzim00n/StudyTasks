namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NumberConverter task4 = new NumberConverter(args[0])
                .ReadNumbersFromFile()
                .CalculateMinimumMoves()
                .WriteResult();
        }
    }

    public class NumberConverter
    {
        private List<int> nums = new List<int>();
        public string fileName { get; private set; }
        private int result = 0;

        public NumberConverter(string file)
        {
            fileName = file;
        }

        public NumberConverter ReadNumbersFromFile()
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        nums.AddRange(line.Split(' ').Select(int.Parse));
                    }
                }
            }
            return this;
        }

        public NumberConverter CalculateMinimumMoves()
        {
            nums.Sort();
            int medianIndex = nums.Count / 2;
            int median = nums[medianIndex];
            foreach (int num in nums)
            {
                result += Math.Abs(num - median);
            }
            return this;
        }

        public NumberConverter WriteResult()
        {
            Console.WriteLine(result);
            return this;
        }
    }
}

