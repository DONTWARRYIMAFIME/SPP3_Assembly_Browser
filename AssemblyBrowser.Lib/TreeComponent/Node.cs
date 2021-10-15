using System;
using System.Collections.Generic;

namespace AssemblyBrowser.Lib.TreeComponent
{
    public abstract class Node:INode
    {
        public string Optional { get; set; }
        public string NodeType { get; set; }
        public string AccessModifier { get; set; }
        public string TypeModifier { get; set; }
        public string ClassType { get; set; }
        public string Type { get; set; }
        public string ReturnType { get; set; }
        public string Name { get; set; }
        public List<INode> Nodes { get; } = new();
        public void AddNode(INode node)
        {
            Nodes.Add(node);
        }
        
        public void AddRange(IEnumerable<INode> nodes)
        {
            Nodes.AddRange(nodes);
        }
    }
}