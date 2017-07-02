using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pilcrow.Core.Helpers;
using Pilcrow.Tests.Core;

namespace Pilcrow.Tests.Projects.Core.Helpers
{
    internal class A {}
    internal class B : A {}
    internal class C : A {}
    internal class D : C {}
    internal class E : C {}
    
    internal interface IF {}
    internal class F : IF {}
    internal class G : F {}
    internal class H : F {}
    internal class I : IF {}
    
    [TestClass]
    public class TypeHelperTests
    {
        [TestMethod]
        public void GetSubClassTypesTest()
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
        public void GetDirectSubClassTypesTest()
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
        
        [TestMethod]
        public void GetLeafSubClassTypesTest()
        {
            Assert.IsTrue(new List<Type>
            {
                typeof(B),
                typeof(D),
                typeof(E),
            }.SequenceEqual(TypeHelper.GetLeafSubClassTypes(typeof(A))));
        }
        
        [TestMethod]
        public void GetImplementingTypesTest()
        {
            Assert.IsTrue(new List<Type>
            {
                typeof(F),
                typeof(G),
                typeof(H),
                typeof(I)
            }.SequenceEqual(TypeHelper.GetImplementingTypes(typeof(IF))));
        }
    }
}
