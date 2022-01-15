using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TechTest.DataLayer.Tests.xUnitExtensions
{
    /// <summary>
    /// To use place the following
    /// <code>[TestCaseOrderer("Boxi.Tests.xUnitExtensions.AlphabeticalOrderer", "Boxi.Tests")]</code>
    /// at the top of the TestClass.
    /// Will execute the test case methods in alphabetical order.
    /// </summary>
    public class AlphabeticalOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var result = testCases.ToList();
            result.Sort((x, y) => 
                StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
            return result;
        }
    }
}