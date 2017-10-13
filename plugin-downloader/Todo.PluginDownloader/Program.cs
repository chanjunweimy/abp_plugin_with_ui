using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Todo.PluginDownloader
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Execute(args);
        }

        public static void Execute(string[] args)
        {
            var program = new PluginHandler();
            program.Execute(args);
        }
    }
}
