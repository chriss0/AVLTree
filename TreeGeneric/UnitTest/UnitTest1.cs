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

    /// <summary>
    /// Tests if the rotation for a right-left-case works properly.
    /// </summary>
    [TestMethod]
    public void LeftRightTest()
    {
      //Case that root is not parent:
      var leftRightTree = new Tree<int>();
      leftRightTree.Add(20);
      leftRightTree.Add(9);
      leftRightTree.Add(5);
      leftRightTree.Add(10);
      leftRightTree.Add(30);
      Assert.AreEqual(9, leftRightTree.Root.Value);
      Assert.AreEqual(5, leftRightTree.Root.Left.Value);
      Assert.AreEqual(20, leftRightTree.Root.Right.Value);
      Assert.AreEqual(30, leftRightTree.Root.Right.Right.Value);
      Assert.AreEqual(10, leftRightTree.Root.Right.Left.Value);

      ////Case that root is parent:
      leftRightTree = new Tree<int>();
      leftRightTree.Add(20);
      leftRightTree.Add(10);
      leftRightTree.Add(15);
      Assert.AreEqual(15, leftRightTree.Root.Value);
      Assert.AreEqual(10, leftRightTree.Root.Left.Value);
      Assert.AreEqual(20, leftRightTree.Root.Right.Value);
    }

    [TestMethod]
    public void RightLeftTest()
    {
      //Case that root is parent:
      var rightLeftTree = new Tree<int>();
      rightLeftTree.Add(20);
      rightLeftTree.Add(30);
      rightLeftTree.Add(25);
      Assert.AreEqual(25, rightLeftTree.Root.Value);
      Assert.AreEqual(30, rightLeftTree.Root.Right.Value);
      Assert.AreEqual(20, rightLeftTree.Root.Left.Value);

      //Case that root is not parent:
      rightLeftTree = new Tree<int>();
      rightLeftTree.Add(20);
      rightLeftTree.Add(30);
      rightLeftTree.Add(10);
      rightLeftTree.Add(35);
      rightLeftTree.Add(32);
      Assert.AreEqual(20, rightLeftTree.Root.Value);
      Assert.AreEqual(10, rightLeftTree.Root.Left.Value);
      Assert.AreEqual(32, rightLeftTree.Root.Right.Value);
      Assert.AreEqual(35, rightLeftTree.Root.Right.Right.Value);
      Assert.AreEqual(30, rightLeftTree.Root.Right.Left.Value);
    }

    /// <summary>
    /// Tests if the rotation for a right-right-case works properly.
    /// </summary>
    [TestMethod]
    public void RightRightTest()
    {
      //Case that root is not parent:
      var rightRightTree = new Tree<int>();
      rightRightTree.Add(20);
      rightRightTree.Add(10);
      rightRightTree.Add(30);
      rightRightTree.Add(40);
      rightRightTree.Add(50);
      Assert.AreEqual(20, rightRightTree.Root.Value);
      Assert.AreEqual(10, rightRightTree.Root.Left.Value);
      Assert.AreEqual(40, rightRightTree.Root.Right.Value);
      Assert.AreEqual(50, rightRightTree.Root.Right.Right.Value);
      Assert.AreEqual(30, rightRightTree.Root.Right.Left.Value);

      //Case that root is parent:
      rightRightTree = new Tree<int>();
      rightRightTree.Add(20);
      rightRightTree.Add(30);
      rightRightTree.Add(40);
      Assert.AreEqual(30, rightRightTree.Root.Value);
      Assert.AreEqual(20, rightRightTree.Root.Left.Value);
      Assert.AreEqual(40, rightRightTree.Root.Right.Value);
    }

    /// <summary>
    /// Tests if the rotation for a left-left-case works properly.
    /// </summary>
    [TestMethod]
    public void LeftLeftTest()
    {
      var leftLeftTree = new Tree<int>();

      //Case that root is parent:
      leftLeftTree.Add(20);
      leftLeftTree.Add(10);
      leftLeftTree.Add(5);
      leftLeftTree.Add(30);
      leftLeftTree.Add(4);
      Assert.AreEqual(5, leftLeftTree.Count());
      Assert.AreEqual(10, leftLeftTree.Root.Value);
      Assert.AreEqual(5, leftLeftTree.Root.Left.Value);
      Assert.AreEqual(4, leftLeftTree.Root.Left.Left.Value);
      Assert.AreEqual(20, leftLeftTree.Root.Right.Value);
      Assert.AreEqual(30, leftLeftTree.Root.Right.Right.Value);

      //Case that root is not parent:
      leftLeftTree = new Tree<int>();
      leftLeftTree.Add(20);
      leftLeftTree.Add(10);
      leftLeftTree.Add(5);
      leftLeftTree.Add(30);
      leftLeftTree.Add(4);
      leftLeftTree.Add(6);
      leftLeftTree.Add(3);
      leftLeftTree.Add(2);
      Assert.AreEqual(10, leftLeftTree.Root.Value);
      Assert.AreEqual(5, leftLeftTree.Root.Left.Value);
      Assert.AreEqual(3, leftLeftTree.Root.Left.Left.Value);
      Assert.AreEqual(20, leftLeftTree.Root.Right.Value);
      Assert.AreEqual(6, leftLeftTree.Root.Left.Right.Value);
      Assert.AreEqual(30, leftLeftTree.Root.Right.Right.Value);
      Assert.AreEqual(2, leftLeftTree.Root.Left.Left.Left.Value);
      Assert.AreEqual(4, leftLeftTree.Root.Left.Left.Right.Value);
    }

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