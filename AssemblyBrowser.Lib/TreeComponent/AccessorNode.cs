namespace AssemblyBrowser.Lib.TreeComponent
{
    public class AccessorNode:Node
    {
        public AccessorNode(string accessModifier, string name)
        {
            NodeType = "[accessor]";
            AccessModifier = accessModifier;
            Name = name;
        }
    }
}