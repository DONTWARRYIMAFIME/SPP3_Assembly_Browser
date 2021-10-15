using System.Collections.Generic;

namespace AssemblyBrowser.Lib.TreeComponent
{
    public class MethodNode:Node
    {
        public MethodNode(string accessModifier, string typeModifier, string returnType, string name, IEnumerable<INode> parameters)
        {
            NodeType = "[method]";
            AccessModifier = accessModifier;
            TypeModifier = typeModifier;
            Name = name;
            ReturnType = returnType;
            AddRange(parameters);
        }
    }
}