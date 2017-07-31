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
    internal interface IJ : IF {}
    internal class F : IF {}
    internal class G : F {}
    internal class H : F {}
    internal class I : IF {}
    internal class J : IJ {}
    
    [TestClass]
    public class TypeHelperTests
    {
        [TestMethod]
        public void GetSubTypesTest()
        {
            Assert.IsTrue(new List<Type>
            {
                typeof(B),
                typeof(C),
                typeof(D),
                typeof(E)
            }.SequenceEqual(TypeHelper.GetSubTypes(typeof(A))));
        }
        
        [TestMethod]
        public void GetDirectSubTypesTest()
        {
            Assert.IsTrue(new List<Type>
            {
                typeof(B),
                typeof(C),
            }.SequenceEqual(TypeHelper.GetDirectSubTypes(typeof(A))));
            Assert.IsTrue(new List<Type>
            {
                typeof(D),
                typeof(E),
            }.SequenceEqual(TypeHelper.GetDirectSubTypes(typeof(C))));
        }
        
        [TestMethod]
        public void GetLeafSubTypesTest()
        {
            Assert.IsTrue(new List<Type>
            {
                typeof(B),
                typeof(D),
                typeof(E),
            }.SequenceEqual(TypeHelper.GetLeafSubTypes(typeof(A))));
        }
        
        [TestMethod]
        public void GetImplementingTypesTest()
        {
            Assert.IsTrue(new List<Type>
            {
                typeof(F),
                typeof(G),
                typeof(H),
                typeof(I),
                typeof(J)
            }.SequenceEqual(TypeHelper.GetImplementingTypes(typeof(IF))));
        }
    }
}
