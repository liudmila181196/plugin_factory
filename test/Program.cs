using System;
using System.Linq;
using lib;

namespace ds.test.impl
{
    class Program
    {
        static void Main(string[] args)
        {
            Plugins plugins = Plugins.getInstance();
            Console.WriteLine("List of plugins:\n- " + String.Join("\n- ", plugins.GetPluginNames));
            plugins.GetPlugin("Sum").Run(1, 2);
            plugins.GetPlugin("Multi").Run(5, 7);
            plugins.GetPlugin("Subtract").Run(10, 3);
            plugins.GetPlugin("S").Run(1, 2);
        }
    }
}
