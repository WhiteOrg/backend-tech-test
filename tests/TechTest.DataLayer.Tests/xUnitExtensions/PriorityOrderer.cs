using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TechTest.DataLayer.Tests.xUnitExtensions
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PriorityAttribute : Attribute
    {
        public PriorityAttribute(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; private set; }
    }

    /// <summary>
    /// To use place the following
    /// <code>[TestCaseOrderer("Boxi.Tests.xUnitExtensions.PriorityOrderer", "Boxi.Tests")]</code>
    /// at the top of the TestClass.
    ///
    /// Will execute tests in Priority Order, if multiple tests are marked with same Priority, then it will execute them in alphabetical order by methodname.
    /// If a Fact/Theory isn't marked with the attribute then it executes in method name order.
    /// </summary>
    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

            foreach (var testCase in testCases)
            {
                var priority = 0;

                foreach (var attr in testCase.TestMethod.Method.GetCustomAttributes(typeof(PriorityAttribute)
                    .AssemblyQualifiedName))
                {
                    priority = attr.GetNamedArgument<int>("Priority");
                }

                GetOrCreate(sortedMethods, priority).Add(testCase);
            }

            foreach (var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
            {
                list.Sort((x, y) =>
                    StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
                foreach (var testCase in list)
                {
                    yield return testCase;
                }
            }
        }

        private static TValue GetOrCreate<TKey, TValue>(SortedDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : new()
        {
            if (dictionary.TryGetValue(key, out var result))
            {
                return result;
            }

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }
}