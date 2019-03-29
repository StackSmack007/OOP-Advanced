using System;

namespace GenericScale
{
   public class StartUp
    {
        static void Main()
        {
            Scale<int> scalesOfJustice = new Scale<int>(12, 3);
            Console.WriteLine(scalesOfJustice.GetHeavier());
            Scale<string> scalesOfJustice2 = new Scale<string>("Asen", "Mais");
            Console.WriteLine(scalesOfJustice2.GetHeavier());
        }
    }
}
