using NUnit.Framework;
using System;
using System.Collections.Generic;
using Package.Building.Extensions;
using System.Text;
using System.Collections.Specialized;
using System.Linq;

namespace Package.Tests.Unit.Building
{

    [TestFixture]
    public class ExtensionsTests
    {        

        [Test]     
        public void ListExtensionsReplaceItems_AnyNull_List() 
        {
            for (int i = 0; i < 3; i++)
            {
                BitVector32 mask = new BitVector32(i);
                List<string> instance = mask[1] ? new List<string>() : null;
                IEnumerable<string> items = mask[2] ? new List<string>() : null;
                string item =  null;
                instance.ReplaceItems(items, item);

                Assert.AreEqual(0, instance?.Count ?? 0);
            }            
        }

        [TestCase(new string[] { "some1", "some2", "some3" }, new string[] { "some1, some2"}, "some4" )]
        [TestCase(new string[] { "some1", "some2", "some3", "some4" }, new string[] { "some1, some1" }, "some1")]
        [TestCase(new string[] { "some1", "some2", "some3", "some4" }, new string[] { "some4, some3" }, "some1")]
        [TestCase(new string[] { "some1", "some2", "some3", "some4" }, new string[] { }, "some1")]
        [TestCase(new string[] { }, new string[] { }, "some1")]
        [TestCase(new string[] { }, new string[] { "some2", "some3", "some4" }, "some1")]
        [TestCase(new string[] { }, new string[] { "some1"}, "some1")]
        [TestCase(new string[] { "some1" }, new string[] { "some1" }, "some1")]
        [TestCase(new string[] { "some1" }, new string[] { "some1" }, "some4")]
        public void ListExtensionsReplaceItems_Items_List(string[] instance, string[] items, string item)
        {
            List<string> listInstance = new List<string>(instance);
            IEnumerable<string> enumerableItems = new List<string>(items);
            bool anyListItems = listInstance.Count > 0,
                anyEnumerableItems = enumerableItems.Count() > 0,
                itemContainsInEnumrable = enumerableItems.Contains(item);
            listInstance.ReplaceItems(enumerableItems, item);
            
            foreach (var enumerableItem in enumerableItems)
                if (enumerableItem != item)
                    Assert.IsFalse(listInstance.Contains(enumerableItem));
            
            Assert.IsTrue(listInstance.Contains(item));
            if (itemContainsInEnumrable)
                Assert.AreEqual(listInstance.Count(a => a == item), 1);
        }

    }

}
