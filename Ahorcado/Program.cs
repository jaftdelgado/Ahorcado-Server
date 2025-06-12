using GameServices;
using Services;
using System;
using System.ServiceModel;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost mainServiceHost = new ServiceHost(typeof(MainService)))
            using (ServiceHost ahorcadoServiceHost = new ServiceHost(typeof(GameService)))
            {
                try
                {
                    mainServiceHost.Open();
                    ahorcadoServiceHost.Open();

                    DateTime currentDateTime = DateTime.Now;
                    Console.WriteLine($"AhorcadoServer is running - Start Time: [{currentDateTime}]");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error starting services: {ex.Message}");
                }
                finally
                {
                    mainServiceHost.Abort();
                    ahorcadoServiceHost.Abort();
                    Console.ReadLine();
                }
            }
        }
    }
}
