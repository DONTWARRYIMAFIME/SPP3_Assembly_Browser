namespace AssemblyBrowser.Lib.TreeComponent
{
    public class FieldNode:Node
    {
        public FieldNode(string accessModifier, string typeModifier, string type, string name)
        {
            NodeType = "[field]";
            AccessModifier = accessModifier;
            TypeModifier = typeModifier;
            Type = type;
            Name = name;
        }
    }
}