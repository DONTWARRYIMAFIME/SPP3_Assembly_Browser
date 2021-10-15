using System;
using System.Collections.Generic;

namespace TestProject
{
    public class Class1
    {
        private string _string1;
        private int _int1;

        public Class1(string string1, int int1)
        {
            _string1 = string1;
            _int1 = int1;
        }
    }
    
    public abstract class Class2
    {
        public int int1 { get; set; }
        protected int int2 { get; private set; }
        private int int3 { get; }
    }
    
    public sealed class Class3
    {
        public string String1 { get; set; }
        public string String2 { get; private set; }
        public string String3 { get; }
        
    }
    
    static class Class4
    {
        public static List<string> Strings = new();

        public static void Add(string node)
        {
            Strings.Add(node);
        }
        
        public static void Remove(string node)
        {
            Strings.Remove(node);
        }

    }

}