using System.Threading.Channels;

namespace MiniConsoleAppProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            ManagementApp app = new ManagementApp();
            app.Run();
            

        }
    }
}
