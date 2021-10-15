using System.Collections.Generic;

namespace AssemblyBrowser.Lib.TreeComponent
{
    public class Node:INode
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

        public Node
        (
            string nodeType, 
            string optional = "", 
            string accessModifier = "", 
            string typeModifier = "", 
            string classType = "", 
            string type = "", 
            string returnType = "", 
            string name = "",
            IEnumerable<INode> nodes = null)
        {
            Optional = optional;
            NodeType = nodeType;
            AccessModifier = accessModifier;
            TypeModifier = typeModifier;
            ClassType = classType;
            Type = type;
            ReturnType = returnType;
            Name = name;
            AddRange(nodes);
        }

        public void AddNode(INode node)
        {
            Nodes.Add(node);
        }
        
        public void AddRange(IEnumerable<INode> nodes)
        {
            if (nodes != null)
            {
                Nodes.AddRange(nodes);    
            }
        }
    }
}