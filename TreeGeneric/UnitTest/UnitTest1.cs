using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tree_Generic;

namespace UnitTest
{
  /// <summary>
  /// Class to test the tree.
  /// </summary>
  [TestClass]
  public class TreeTest
  {
    /// <summary>
    /// Creates a new tree, so the user does not need to 
    /// implement a new one in each test method.
    /// </summary>
    /// <returns>The tree that can be used for testing</returns>
    public Tree<int> InitializeIntTree()
    {
      var testTree = new Tree<int>();
      testTree.Add(5);
      testTree.Add(8);
      testTree.Add(3);
      testTree.Add(4);
      testTree.Add(1);
      testTree.Add(51);
      testTree.Add(81);
      testTree.Add(25);
      testTree.Add(10);

      return testTree;
    }

    ///// <summary>
    ///// Checks whether the balancefactors of each <paramref name="node"/> have been calculated correctly.
    ///// </summary>
    //[TestMethod]
    //public void BalanceFactorTest()
    //{
    //  Tree<int> balanceTree = InitializeIntTree();
    //  Assert.AreEqual(2, balanceTree.CalculateBalanceFactor(5));
    //  Assert.AreEqual(3, balanceTree.CalculateBalanceFactor(8));
    //  Assert.AreEqual(-1, balanceTree.CalculateBalanceFactor(51));
    //  Assert.AreEqual(0, balanceTree.CalculateBalanceFactor(1));
    //  Assert.AreEqual(0, balanceTree.CalculateBalanceFactor(4));
    //  Assert.AreEqual(0, balanceTree.CalculateBalanceFactor(3));
    //  Assert.AreEqual(-1, balanceTree.CalculateBalanceFactor(25));
    //  Assert.AreEqual(0, balanceTree.CalculateBalanceFactor(10));
    //}

    /// <summary>
    /// Checks wether there have values been added to the tree.
    /// </summary>
    [TestMethod]
    public void AddTest()
    {
      Tree<int> tree = InitializeIntTree();
      Assert.IsNotNull(tree.Count());
    }

    /// <summary>
    /// Checks wether the array-length and the values stored in it are correct.
    /// </summary>
    [TestMethod]
    public void ArrayTest()
    {
      Tree<int> testTree = InitializeIntTree();
      int[] arr = testTree.ConvertToArray();
      Assert.AreEqual(9, arr.Length);
      Assert.AreEqual(5, arr[3]);
    }

    /// <summary>
    /// Tests if an exception is thrown when the same value is added.
    /// </summary>
    [TestMethod, ExpectedException(typeof(ArgumentException))]
    public void AddExistingTest()
    {
      var tree = InitializeIntTree();
      Assert.IsTrue(tree.Contains(81));
      tree.Add(81);
    }

    /// <summary>
    /// Checks wheter the count method returns the correct
    /// number of nodes that are contained in the tree.
    /// </summary>
    [TestMethod]
    public void CountTest()
    {
      Tree<int> tree = InitializeIntTree();
      Assert.AreEqual(9, tree.Count());
    }

    /// <summary>
    /// Checks whether the Add() added all values correctly.
    /// </summary>
    [TestMethod]
    public void ContainsTest()
    {
      Tree<int> tree = InitializeIntTree();
      Assert.IsTrue(tree.Contains(5));
      Assert.IsTrue(tree.Contains(8));
      Assert.IsTrue(tree.Contains(3));
      Assert.IsTrue(tree.Contains(1));
      Assert.IsTrue(tree.Contains(51));
      Assert.IsTrue(tree.Contains(81));
      Assert.IsTrue(tree.Contains(25));
      Assert.IsTrue(tree.Contains(10));
      Assert.IsFalse(tree.Contains(0));
      Assert.IsFalse(tree.Contains(7));
      Assert.IsFalse(tree.Contains(100));
      Assert.IsFalse(tree.Contains(9));
    }


    /// <summary>
    /// Removes half of the values and tests if they were removed correctly.
    /// </summary>
    [TestMethod]
    public void RemoveTest()
    {
      Tree<int> tree = InitializeIntTree();

      Assert.IsTrue(tree.Contains(10));
      Assert.IsTrue(tree.Contains(5));
      Assert.IsTrue(tree.Contains(1));
      Assert.IsTrue(tree.Contains(81));
      Assert.IsFalse(tree.Contains(111));
      Assert.AreEqual(9, tree.Count());

      try
      {
        tree.Remove(7);
      }
      catch (Exception ex)
      {
        Assert.Fail(ex.GetType().ToString() + " was thrown: " + ex.Message);
      }
      Assert.AreEqual(9, tree.Count());

      tree.Remove(5);
      Assert.IsTrue(tree.Contains(10));
      Assert.IsFalse(tree.Contains(5));
      Assert.IsTrue(tree.Contains(1));
      Assert.IsTrue(tree.Contains(81));
      Assert.IsFalse(tree.Contains(111));
      Assert.AreEqual(8, tree.Count());

      tree.Remove(10);
      Assert.IsFalse(tree.Contains(10));
      Assert.IsFalse(tree.Contains(5));
      Assert.IsTrue(tree.Contains(1));
      Assert.IsTrue(tree.Contains(81));
      Assert.IsFalse(tree.Contains(111));
      Assert.AreEqual(7, tree.Count());

      tree.Remove(1);
      Assert.IsFalse(tree.Contains(10));
      Assert.IsFalse(tree.Contains(5));
      Assert.IsFalse(tree.Contains(1));
      Assert.IsTrue(tree.Contains(81));
      Assert.IsFalse(tree.Contains(111));
      Assert.AreEqual(6, tree.Count());

      tree.Remove(81);
      Assert.IsFalse(tree.Contains(10));
      Assert.IsFalse(tree.Contains(5));
      Assert.IsFalse(tree.Contains(1));
      Assert.IsFalse(tree.Contains(81));
      Assert.IsFalse(tree.Contains(111));
      Assert.AreEqual(5, tree.Count());

      Assert.IsTrue(tree.Contains(25));
      Assert.IsTrue(tree.Contains(51));
      Assert.IsTrue(tree.Contains(3));
      Assert.IsTrue(tree.Contains(8));
    }
  }
}