using System.Collections.Generic;
using AssemblyBrowser.Lib.TreeComponent;
using NUnit.Framework;

namespace AssemblyBrowser.Test
{
    public class Tests
    {
        private const string PathToDll = "../../../../Test/TestProject.dll";
        private readonly List<INode> _nodes = Lib.AssemblyBrowser.GetAssemblyInfo(PathToDll);
        
        [Test]
        public void TestNamespaces()
        {
            Assert.AreEqual(2, _nodes.Count);
            Assert.AreEqual("TestProject", _nodes[0].Name);
            Assert.AreEqual("TestProject.NamespaceTest", _nodes[1].Name);
        }

        [Test]
        public void TestClassType()
        {
            var type = _nodes[0].Nodes[0];
            Assert.AreEqual("[type]", type.NodeType);
            Assert.AreEqual("public", type.AccessModifier);
            Assert.AreEqual("class", type.ClassType);
        }
        
        [Test]
        public void TestEnumType()
        {
            var type = _nodes[1].Nodes[0];
            Assert.AreEqual("[type]", type.NodeType);
            Assert.AreEqual("public", type.AccessModifier);
            Assert.AreEqual("sealed", type.TypeModifier);
            Assert.AreEqual("enum", type.ClassType);
        }
        
        [Test]
        public void TestStructureType()
        {
            var type = _nodes[1].Nodes[1];
            Assert.AreEqual("[type]", type.NodeType);
            Assert.AreEqual("public", type.AccessModifier);
            Assert.AreEqual("sealed", type.TypeModifier);
            Assert.AreEqual("structure", type.ClassType);
        }

        [Test]
        public void TestAssemblyConstructors()
        {
            var constructor = _nodes[0].Nodes[0].Nodes[0];
            Assert.AreEqual("[constructor]", constructor.NodeType);
        }
        
        [Test]
        public void TestAssemblyDestructor()
        {
            var constructor = _nodes[0].Nodes[0].Nodes[1];
            Assert.AreEqual("[destructor]", constructor.NodeType);
        }
        
        [Test]
        public void TestAssemblyProperty()
        {
            var property = _nodes[0].Nodes[1].Nodes[0];
            Assert.AreEqual("[property]", property.NodeType);
        }
        
        [Test]
        public void TestAssemblyPropertyAccessors()
        {
            var property = _nodes[0].Nodes[1].Nodes[0];
            var accessors = property.Nodes;
            Assert.AreEqual("[accessor]", accessors[0].NodeType);
            Assert.AreEqual("get_int1", accessors[0].Name);
            Assert.AreEqual("[accessor]", accessors[1].NodeType);
            Assert.AreEqual("set_int1", accessors[1].Name);
        }

        [Test]
        public void TestAssemblyField()
        {
            var field = _nodes[0].Nodes[3].Nodes[0];
            Assert.AreEqual("[field]", field.NodeType);
            Assert.AreEqual("public", field.AccessModifier);
            Assert.AreEqual("static", field.TypeModifier);
            Assert.AreEqual("List<String>", field.Type);
            Assert.AreEqual("Strings", field.Name);
            Assert.AreEqual(0, field.Nodes.Count);
        }
        
        [Test]
        public void TestAssemblyMethod()
        {
            var method = _nodes[0].Nodes[3].Nodes[1];
            Assert.AreEqual("[method]", method.NodeType);
        }
        
        [Test]
        public void TestAssemblyMethodParameters()
        {
            var method = _nodes[0].Nodes[3].Nodes[1];
            var parameters = method.Nodes;
            Assert.AreEqual("[param]", parameters[0].NodeType);
        }
        
        [Test]
        public void TestAssemblyExtensionMethods()
        {
            var extensionMethod1 = _nodes[0].Nodes[5].Nodes[4];
            var extensionMethod2 = _nodes[0].Nodes[5].Nodes[5];
            Assert.AreEqual("[extension]", extensionMethod1.Optional);
            Assert.AreEqual("[extension]", extensionMethod2.Optional);
        }
        
    }
}