namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            PathFinder task1 = new PathFinder(args[0], args[1])
                .FindPath()
                .WritePath();
        }
    }

    public class PathFinder
    {
        private readonly int n, m;
        private readonly List<int> result = new List<int>();

        public PathFinder(string n, string m)
        {
            this.n = int.Parse(n);
            this.m = int.Parse(m);
        }

        public PathFinder FindPath()
        {
            int num = 1;
            do
            {
                result.Add(num);
                num = (num + m - 2) % n + 1;
            } while (num != 1);

            return this;
        }

        public PathFinder WritePath()
        {
            foreach (int part in result)
            {
                Console.Write(part);
            }

            return this;
        }
    }
}