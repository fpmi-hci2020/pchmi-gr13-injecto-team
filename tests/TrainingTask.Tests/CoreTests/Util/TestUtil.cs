using System;
using System.Collections.Generic;
using System.Reflection;

using NUnit.Framework;

namespace TrainingTask.Tests.CoreTests.Util
{
    public class TestUtil
    {
        public static void AreEqual(object expected, object actual)
        {
            var differences = new List<PropertyInfo>();

            foreach (var property in expected.GetType().GetProperties())
            {
                if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    var value1 = property.GetValue(expected, null);
                    var value2 = property.GetValue(actual, null);

                    if (!value1.Equals(value2))
                    {
                        differences.Add(property);
                    }
                }
            }

            if (differences.Count > 0)
            {
                throw new AssertionException(string.Join(Environment.NewLine, differences));
            }
        }
    }
}