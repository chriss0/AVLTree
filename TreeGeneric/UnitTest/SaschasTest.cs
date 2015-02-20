using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tree_Generic;

namespace UnitTest
{
  class SaschasTest
  {
    [TestClass]
    public class AVLUnitTest
    {
      private static void RangeEqual<T>(IEnumerable<T> expected, IEnumerable<T> result)
      {
        var resultEnum = result.GetEnumerator();
        foreach (var expectedValue in expected)
        {
          Assert.IsTrue(resultEnum.MoveNext());
          Assert.AreEqual<T>(expectedValue, resultEnum.Current);
        }
        Assert.IsFalse(resultEnum.MoveNext());
      }

      static int MaxHeight<T>(Tree<T> tree) where T : IComparable<T>
      {
        int count = tree.Count();
        return (int)Math.Floor(1.44 * Math.Log(count + 2, 2) - .328);
      }

      [TestMethod]
      public void EmptyTree()
      {
        var tree = new Tree<int>();

        Assert.AreEqual(0, tree.Count());
        Assert.AreEqual(0, tree.CalculateHeight());
        Assert.IsFalse(tree.Contains(42));
        Assert.AreEqual(0, tree.ConvertToArray().Length);
      }

      [TestMethod]
      public void Add()
      {
        var tree = new Tree<int>();

        var values = new int[] { 4, 1, 5, 3, 2 };

        foreach (int value in values)
          tree.Add(value);
        Array.Sort(values);

        Assert.AreEqual(values.Length, tree.Count());
        Assert.IsTrue(tree.CalculateHeight() <= MaxHeight(tree));

        RangeEqual(values, tree.ConvertToArray());

        tree.Add(1);
        Assert.AreEqual(values.Length, tree.Count());
        RangeEqual(values, tree.ConvertToArray());
      }

      [TestMethod]
      public void Contains()
      {
        var tree = new Tree<int>();

        var values = new int[] { 4, 1, 5, 3, 2 };

        foreach (int value in values)
          tree.Add(value);

        foreach (int value in values)
          Assert.IsTrue(tree.Contains(value));

        Assert.IsFalse(tree.Contains(-1));
        Assert.IsFalse(tree.Contains(9));
      }

      [TestMethod]
      public void Remove()
      {
        var tree = new Tree<int>();

        var values = new int[] { 4, 1, 5, 3, 2 };
        foreach (int value in values)
          tree.Add(value);
        Array.Sort(values);

        tree.Remove(7);
        RangeEqual(values, tree.ConvertToArray());

        tree.Remove(1);
        RangeEqual(new int[] { 2, 3, 4, 5 }, tree.ConvertToArray());
        Assert.IsTrue(tree.CalculateHeight() <= MaxHeight(tree));
        tree.Remove(3);
        RangeEqual(new int[] { 2, 4, 5 }, tree.ConvertToArray());
        Assert.IsTrue(tree.CalculateHeight() <= MaxHeight(tree));
        tree.Remove(5);
        RangeEqual(new int[] { 2, 4 }, tree.ConvertToArray());
        Assert.IsTrue(tree.CalculateHeight() <= MaxHeight(tree));
        tree.Add(3);
        tree.Add(8);
        tree.Add(6);
        tree.Add(7);
        RangeEqual(new int[] { 2, 3, 4, 6, 7, 8 }, tree.ConvertToArray());
        Assert.IsTrue(tree.CalculateHeight() <= MaxHeight(tree));
        tree.Remove(4);
        RangeEqual(new int[] { 2, 3, 6, 7, 8 }, tree.ConvertToArray());
        Assert.IsTrue(tree.CalculateHeight() <= MaxHeight(tree));
        tree.Add(9);
        RangeEqual(new int[] { 2, 3, 6, 7, 8, 9 }, tree.ConvertToArray());
        Assert.IsTrue(tree.CalculateHeight() <= MaxHeight(tree));
        tree.Remove(8);
        RangeEqual(new int[] { 2, 3, 6, 7, 9 }, tree.ConvertToArray());
        Assert.IsTrue(tree.CalculateHeight() <= MaxHeight(tree));
        tree.Remove(6);
        RangeEqual(new int[] { 2, 3, 7, 9 }, tree.ConvertToArray());
        Assert.IsTrue(tree.CalculateHeight() <= MaxHeight(tree));
        tree.Add(20);
        tree.Remove(9);
        tree.Add(15);
        tree.Add(25);
        tree.Add(10);
        tree.Remove(7);
        RangeEqual(new int[] { 2, 3, 10, 15, 20, 25 }, tree.ConvertToArray());
        Assert.IsTrue(tree.CalculateHeight() <= MaxHeight(tree));

        foreach (int value in tree.ConvertToArray())
        {
          tree.Remove(value);
        }
        Assert.AreEqual(0, tree.Count());
      }
    }

  }
}
