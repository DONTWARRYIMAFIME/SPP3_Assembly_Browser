using System.Collections.Generic;

namespace AssemblyBrowser.Lib.TreeComponent
{
    public class PropertyNode:Node
    {
        public PropertyNode(string accessModifier, string type, string name, IEnumerable<INode> nodes)
        {
            NodeType = "[property]";
            AccessModifier = accessModifier;
            Type = type;
            Name = name;
            AddRange(nodes);
        }
    }
}