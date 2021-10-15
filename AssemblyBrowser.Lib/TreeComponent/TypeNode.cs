namespace AssemblyBrowser.Lib.TreeComponent
{
    public class TypeNode:Node
    {
        public TypeNode(string accessModifier, string typeModifier, string classType, string type)
        {
            NodeType = "[type]";
            AccessModifier = accessModifier;
            TypeModifier = typeModifier;
            ClassType = classType;
            Type = type;
        }
    }
}