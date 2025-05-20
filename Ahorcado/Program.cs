using System;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime currentDateTime = DateTime.Now;
            Console.WriteLine($"AhorcadoServer is running - Start Time: [{currentDateTime}]");
            Console.ReadLine();
        }
    }
}
