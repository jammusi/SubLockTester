using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface I1
    {

    }

    public interface I2: I1
    {

    }

    

    internal class TestRec : Common.Model.Classes.Indicators.IndicatorRecord, I1
    {
        public class TestRecVal
        {
            public string Name { get; set; }
            public string Name2 { get; set; }
            //public Program.color Color { get; set; }
        }
    }

    internal class TestRec2 : Common.Model.Classes.Indicators.IndicatorRecord, I2
    {
        public class TestRecVal
        {
            public string Name { get; set; }
            public string Name2 { get; set; }
            //public Program.color Color { get; set; }
        }
    }

    internal class GenTestRec<T> : Common.Model.Classes.Indicators.IndicatorRecord
    {
        

    }
}
