
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilcrow.Core.Helpers;

namespace Pilcrow.Test.Projects.Core.Helpers
{
    internal class A {}
    internal class B : A {}
    internal class C : A {}
    internal class D : C {}
    internal class E : C {}
    
    [TestClass]
    public class TypeHelperTests
    {
        [TestMethod]
        public void GetSubClassesTest()
        {
            Assert.IsTrue(new List<Type>
            {
                typeof(B),
                typeof(C),
                typeof(D),
                typeof(E)
            }.SequenceEqual(TypeHelper.GetSubClassTypes(typeof(A))));
        }
        
        [TestMethod]
        public void GetDirectSubClassTypes()
        {
            Assert.IsTrue(new List<Type>
            {
                typeof(B),
                typeof(C),
            }.SequenceEqual(TypeHelper.GetDirectSubClassTypes(typeof(A))));
            Assert.IsTrue(new List<Type>
            {
                typeof(D),
                typeof(E),
            }.SequenceEqual(TypeHelper.GetDirectSubClassTypes(typeof(C))));
        }
    }
}
