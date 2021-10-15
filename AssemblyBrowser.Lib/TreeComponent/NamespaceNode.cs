namespace AssemblyBrowser.Lib.TreeComponent
{
    public class NamespaceNode:Node
    {
        public NamespaceNode(string name)
        {
            NodeType = "[namespace]";
            Name = name;
        }
    }
}