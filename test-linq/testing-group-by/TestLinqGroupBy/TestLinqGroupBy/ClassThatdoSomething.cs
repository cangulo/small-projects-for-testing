using System.Collections.Generic;

namespace TestLinqGroupBy
{
    public interface IClassThatdoSomething
    {
        void DoStuff(IEnumerable<int> input);
    }
    public class ClassThatdoSomething : IClassThatdoSomething
    {
        public void DoStuff(IEnumerable<int> input)
        {
            
        }
    }
}
