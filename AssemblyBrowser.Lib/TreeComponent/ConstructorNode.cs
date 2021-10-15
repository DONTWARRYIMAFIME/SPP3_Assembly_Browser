using System.Collections.Generic;

namespace AssemblyBrowser.Lib.TreeComponent
{
    public class ConstructorNode:Node
    {
        public ConstructorNode(string accessModifier, string type, IEnumerable<ParameterNode> parameters)
        {
            NodeType = "[constructor]";
            AccessModifier = accessModifier;
            Type = type;
            AddRange(parameters);
        }
    }
}