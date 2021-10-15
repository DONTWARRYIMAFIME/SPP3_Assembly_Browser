namespace AssemblyBrowser.Lib.TreeComponent
{
    public class ParameterNode:Node
    {
        public ParameterNode(string typeModifier, string type, string name)
        {
            NodeType = "[param]";
            TypeModifier = typeModifier;
            Type = type;
            Name = name;
        }
    }
}