using System.Globalization;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PointChecker task2 = new PointChecker()
                .ReadCircleProperties(args[0])
                .CheckPoints(args[1])
                .WriteResult();
        }
    }

    public class PointChecker
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public float radius { get; private set; }
        public List<short> pointsState = new List<short>();

        public PointChecker ReadCircleProperties(string circleFile)
        {
            using (StreamReader sr = new StreamReader(circleFile))
            {
                string line = sr.ReadLine();
                string[] parts = line.Split(' ');
                x = int.Parse(parts[0]);
                y = int.Parse(parts[1]);
                radius = int.Parse(sr.ReadLine());
                return this;
            }
        }

        public PointChecker CheckPoints(string pointsFile)
        {
            using (StreamReader sr = new StreamReader(pointsFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    float pointX = float.Parse(parts[0], CultureInfo.InvariantCulture);
                    float pointY = float.Parse(parts[1], CultureInfo.InvariantCulture);

                    double distance = Math.Sqrt(Math.Pow(pointX - x, 2) + Math.Pow(pointY - y, 2));
                    if (distance == radius)
                    {
                        pointsState.Add(0); 
                    }
                    else if (distance < radius)
                    {
                        pointsState.Add(1); 
                    }
                    else
                    {
                        pointsState.Add(2); 
                    }
                }
            }
            return this;
        }

        public PointChecker WriteResult()
        {
            foreach (short state in pointsState)
            {
                Console.WriteLine(state);
            }
            return this;
        }
    }
}