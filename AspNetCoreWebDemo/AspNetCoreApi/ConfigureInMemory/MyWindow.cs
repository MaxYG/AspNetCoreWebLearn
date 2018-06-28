using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApi.ConfigureInMemory
{
    public class MyWindow
    {
        public Dictionary<string, string> GetDictionary()
        {
            var dict = new Dictionary<string, string>
            {
                {"Profile:MachineName", "Rick"},
                {"App:MainWindow:Height", "10"},
                {"App:MainWindow:Width", "11"},
                {"App:MainWindow:Top", "12"},
                {"App:MainWindow:Left", "13"}
            };
            return dict;
        }
    }
    public class MyWindowDemo
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
    }

    public class AppSettings
    {
        public Window Window { get; set; }
        public Connection Connection { get; set; }
        public Profile Profile { get; set; }
    }

    public class Window
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class Connection
    {
        public string Value { get; set; }
    }

    public class Profile
    {
        public string Machine { get; set; }
    }
}
