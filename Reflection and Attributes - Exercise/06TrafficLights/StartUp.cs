namespace _06TrafficLights
{
    using System;
    public class StartUp
    {
        static void Main()
        {
            string[] inputColors = Console.ReadLine().Split();
            TrafficLight trafficLight = new TrafficLight();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < inputColors.Length; j++)
                {

                    inputColors[j] = trafficLight.TogleSignal(inputColors[j]);
                }

                Console.WriteLine(string.Join(" ",inputColors));
            }
        }
    }
}