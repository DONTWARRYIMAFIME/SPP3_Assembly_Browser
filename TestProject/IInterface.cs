using System.Collections.Generic;

namespace TestProject
{
    public interface IInterface<T, U>
    {
        public T Generate();
        
        public U GenerateInstance();

        public string GetString<TR, TU, TT>();
    }
}