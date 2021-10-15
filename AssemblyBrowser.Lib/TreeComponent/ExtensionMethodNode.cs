using System.Collections.Generic;

namespace AssemblyBrowser.Lib.TreeComponent
{
    public class ExtensionMethodNode:MethodNode
    {
        public ExtensionMethodNode(string accessModifier, string typeModifier, string returnType, string name, IEnumerable<INode> parameters) : base(accessModifier, typeModifier, returnType, name, parameters)
        {
            Optional = "[extension]";
        }
    }
}