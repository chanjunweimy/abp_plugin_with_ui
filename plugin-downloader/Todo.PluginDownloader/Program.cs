using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Todo.PluginDownloader
{
    public static class Program
    {
        static void Main(string[] args)
        {
            ExecuteSource(args);
        }

        public static void ExecuteZip(string[] args)
        {
            var program = new PluginHandler();
            program.ExecuteZip(args);
        }

        public static void ExecuteSource(string[] args)
        {
            var program = new PluginHandler();
            program.ExecuteSource(args);
        }
    }
}
