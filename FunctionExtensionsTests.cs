using System;
using NUnit.Framework;
using static GenericDelegatesTask.FunctionExtensions;

namespace GenericDelegatesTask.Tests
{
    public class FunctionExtensionsTests
    {
        [Test]
        public void FunctionExtension_CombinePredicates_Should_Combine_String_Predicates()
        {
            var result= CombinePredicatesWithAnd(new Predicate<string>[]
            {
                x => !string.IsNullOrEmpty(x),
                x => x.StartsWith("START"),
                x => x.EndsWith("END"),
                x => x.Contains("#")
            });

            Assert.IsTrue(result("START # END"));
            Assert.IsTrue(result("START######END"));
            Assert.IsTrue(result("START 1111#1111 END"));
            Assert.IsFalse(result("START END"));
            Assert.IsFalse(result(string.Empty));
            Assert.IsFalse(result("START #"));
        }

        [Test]
        public void FunctionExtension_CombinePredicates_Should_Combine_Int_Predicates()
        {
            var result= CombinePredicatesWithAnd(new Predicate<int>[]
            {
                x => x > -10,
                x => x < 10,
                x => x != 0,
                x => x != 1
            });

            Assert.IsTrue(result(2));
            Assert.IsTrue(result(-5));
            Assert.IsTrue(result(9));
            Assert.IsFalse(result(-20));
            Assert.IsFalse(result(0));
            Assert.IsFalse(result(1));
        }

        [Test]
        public void FunctionExtension_CombinePredicates_Should_Combine_Single_Predicates()
        {
            var result= CombinePredicatesWithAnd(new Predicate<int>[]
            {
                x => x > 0
            });

            Assert.IsTrue(result(2));
            Assert.IsTrue(result(5));
            Assert.IsTrue(result(1000));
            Assert.IsFalse(result(-20));
            Assert.IsFalse(result(0));
            Assert.IsFalse(result(-1000));
        }
    }
}