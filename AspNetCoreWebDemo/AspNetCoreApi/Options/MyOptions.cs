using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApi.Options
{
    public class MyOptions
    {
        public MyOptions()
        {
            Option1 = "value1_from_ctor";
        }

        public string Option1 { get; set; }
        public int Option2 { get; set; } = 5;
    }

    public class MyOptionsWithDelegateConfig
    {
        public MyOptionsWithDelegateConfig()
        {
            Option1 = "value1_from_ctor";
        }

        public string Option1 { get; set; }
        public int Option2 { get; set; } = 5;
    }

    public class MySnapshotOptions
    {
        public MySnapshotOptions()
        {
            Option1 = "value1_from_ctor";
        }

        public string Option1 { get; set; }
        public int Option2 { get; set; } = 5;
    }
}
