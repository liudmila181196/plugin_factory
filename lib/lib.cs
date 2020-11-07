using System;
using System.Collections.Generic;
using System.Linq;


namespace lib
{
    /// <summary>
    /// The IPlugin interface defines methods for plugins
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Return name of plugin
        /// </summary>
        public string PluginName { get; }
        /// <summary>
        /// Return version of plugin
        /// </summary>
        string Version { get; }
        /// <summary>
        /// Return image of plugin
        /// </summary>
        System.Drawing.Image Image { get; }
        /// <summary>
        /// Return description of plugin
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Method where defines operation of plugin
        /// </summary>
        int Run(int input1, int input2);
    }
    /// <summary>
    /// The PluginFactory interface.
    /// </summary>
    interface PluginFactory
    {
        /// <summary>
        /// Return count of plugins
        /// </summary>
        int PluginsCount { get; }
        /// <summary>
        /// Return array of plugins names
        /// </summary>
        string[] GetPluginNames { get; }
        /// <summary>
        /// Find plugin by name.
        /// </summary>
        /// <returns>
        /// Plugin with given name.
        /// </returns>
        IPlugin GetPlugin(string pluginName);
    }
    /// <summary>
    /// The Plugins class.
    /// This class can create new plugins, return plugin by name, list of names and count of plugins.
    /// </summary>
    public class Plugins : PluginFactory
    {
        /// <summary>
        /// Store single object of Plugins class (Necessary to realize Singlton pattern)
        /// </summary>
        private static Plugins instance;
        /// <summary>
        /// List of all plugins.
        /// </summary>
        private static List<IPlugin> listOfPlugins = new List<IPlugin>();
        /// <summary>
        /// Default constuctor for Plugins class. (Necessary to realize Singlton pattern)
        /// </summary>
        private Plugins() { }
        /// <summary>
        /// Return count of plugins
        /// </summary>
        public int PluginsCount { private set;  get; }
        /// <summary>
        /// Return array of plugins names
        /// </summary>
        public string[] GetPluginNames { private set; get; }
        /// <summary>
        /// Realize Singlton pattern. Allow to initialize only one object of this class. Also there initialized plugins.
        /// </summary>
        /// <returns>
        /// Plugins object
        /// </returns>
        public static Plugins getInstance()
        {
            if (instance == null)
            {
                instance = new Plugins();
                instance.GetPluginNames = new string[1];
                instance.PluginsCount = 0;
                AddPlugin(new Sum("Sum", "1.0", "Sum of two numbers"));
                AddPlugin(new Multiplication("Multi", "1.0", "Multiplication of two numbers"));
                AddPlugin(new Subtraction("Subtract", "1.0", "Subtraction of two numbers"));
            }
            return instance;
        }
        /// <summary>
        /// Find plugin by name.
        /// </summary>
        /// <returns>
        /// Plugin with given name.
        /// </returns>
        public IPlugin GetPlugin(string pluginName)
        {
            
            if (instance.GetPluginNames.Contains(pluginName))
            {
                return listOfPlugins.ElementAt(Array.IndexOf(instance.GetPluginNames, pluginName));
            }
            else
            {
                Console.WriteLine("Can not found plugin with name <" + pluginName+">, return the last one");
                return listOfPlugins.ElementAt(0);
            }
        }
        /// <summary>
        /// Method to add plugin in plugin list
        /// </summary>
        private static void AddPlugin(IPlugin plugin)
        {
            if (!String.IsNullOrWhiteSpace(plugin.PluginName)&&plugin!=null)
            {
                listOfPlugins.Add(plugin);
                instance.GetPluginNames = new string[++instance.PluginsCount];
                for (int i=0; i < instance.PluginsCount; i++)
                {
                    instance.GetPluginNames[i] = listOfPlugins.ElementAt(i).PluginName;
                }
                
            }
            else Console.WriteLine("Name can not be empty");
        }
        /// <summary>
        /// Abstract class PluginTemp realise IPlugin interface
        /// </summary>
        private abstract class PluginTemp : IPlugin
        {
            public string PluginName { private set; get; }
            public string Version { get; }
            public System.Drawing.Image Image { get; }
            public string Description { get; }
            /// <summary>
            /// Constructor for children classes with given values
            /// </summary>
            public PluginTemp(string name, string version, string description)
            {
                this.PluginName = name;
                this.Version = version;
                this.Description = description;
            }
            /// <summary>
            /// Abstract method Run for children classes, which will do something
            /// </summary>
            public abstract int Run(int input1, int input2);
        }
        /// <summary>
        /// Class Sum realise PluginTemp class, which realise the IPlugin interface.
        /// Has method Run, which adds two integers.
        /// </summary>
        private class Sum : PluginTemp
        {
            /// <summary>
            /// Default constructor for Sum class.
            /// </summary>
            /// <returns>
            /// The Sum object with default values.
            /// </returns>
            public Sum() : this("Sum", "0", "Description") { }
            /// <summary>
            /// Constructor for Sum class.
            /// </summary>
            /// <returns>
            /// The Sum object with a given values.
            /// </returns>
            public Sum(string name, string version, string description) : base(name, version, description) { }
            /// <summary>
            /// Adds two integers and returns the result.
            /// </summary>
            /// <returns>
            /// The sum of two integers and print it in console with information of object.
            /// </returns>
            /// <param name="input1">An integer precision number.</param>
            /// <param name="input2">An integer precision number.</param>
            public override int Run(int input1, int input2)
            {
                Console.WriteLine("Using plugin <" + this.PluginName + "> v" + this.Version + "\n" + input1 + " + " + input2 + " = " + (input1 + input2));
                return input1 + input2;
            }
        }
        /// <summary>
        /// Class Multiplication realise PluginTemp class, which realise the IPlugin interface.
        /// Has method Run, which multiply two integers.
        /// </summary>
        private class Multiplication : PluginTemp
        {
            /// <summary>
            /// Default constructor for Multiplication class.
            /// </summary>
            /// <returns>
            /// The Multiplication object with default values.
            /// </returns>
            public Multiplication() : this("Multiplication", "0", "Description") { }
            /// <summary>
            /// Constructor for Multiplication class.
            /// </summary>
            /// <returns>
            /// The Multiplication object with a given values.
            /// </returns>
            public Multiplication(string name, string version, string description) : base(name, version, description) { }
            /// <summary>
            /// Multiply two integers and returns the result.
            /// </summary>
            /// <returns>
            /// The multiply of two integers and print it in console with information of object.
            /// </returns>
            /// <param name="input1">An integer precision number.</param>
            /// <param name="input2">An integer precision number.</param>
            public override int Run(int input1, int input2)
            {
                Console.WriteLine("Using plugin <" + this.PluginName + "> v" + this.Version + "\n" + input1 + " * " + input2 + " = " + (input1 * input2));
                return input1 * input2;
            }
        }
        /// <summary>
        /// Class Subtraction realise PluginTemp class, which realise the IPlugin interface.
        /// Has method Run, which subtract two integers.
        /// </summary>
        private class Subtraction : PluginTemp
        {
            /// <summary>
            /// Default constructor for Subtraction class.
            /// </summary>
            /// <returns>
            /// The Subtraction object with default values.
            /// </returns>
            public Subtraction() : this("Subtraction", "0", "Description") { }
            /// <summary>
            /// Constructor for Subtraction class.
            /// </summary>
            /// <returns>
            /// The Subtraction object with a given values.
            /// </returns>
            public Subtraction(string name, string version, string description) : base(name, version, description) { }
            /// <summary>
            /// Subtract two integers and returns the result.
            /// </summary>
            /// <returns>
            /// The subtract of two integers and print it in console with information of object.
            /// </returns>
            /// <param name="input1">An integer precision number.</param>
            /// <param name="input2">An integer precision number.</param>
            public override int Run(int input1, int input2)
            {
                Console.WriteLine("Using plugin <" + this.PluginName + "> v" + this.Version + "\n" + input1 + " - " + input2 + " = " + (input1 - input2));
                return input1 - input2;
            }
        }
    }
    
}


