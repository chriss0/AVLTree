﻿using System;

namespace Tree_Generic
{
  /// <summary>
  /// Represents a tree data structure
  /// </summary>
  /// <typeparam name="T">DataType of the tree</typeparam>
  public class Tree<T> where T : IComparable<T>
  {
    private Node<T> _root;

    /// <summary>
    /// Checks each node by its <paramref name="balanceFactor"/> if balancing is needed. //TODO: Is it legal to comment this way as "balanceFactor" is never assigned
    /// </summary>
    /// <param name="current">The current <paramref name="node"/></param>
    private void AutoBalanceTree(Node<T> current)
    {
      if(current.Left != null)
      CheckForRotateMethod(current.Left);

      if (current.Right != null)
        CheckForRotateMethod(current.Right);

      return;
    }

    /// <summary>
    /// Checks wheter autobalancing is needed and if so, which rotate-method is to be performed
    /// </summary>
    /// <param name="current">The current node that is checked</param>
    private void CheckForRotateMethod(Node<T> current)
    {
      if (LeftLeft(current))
      {
        RotateLeftLeft(current, current.Left);
      }
      else if (LeftRight(current))
      {
        RotateLeftRight(current, current.Left);
      }
      //else if (RightRight)
      //{

      //}
      //else if(RightLeft)
      //{

      //}
    }

    private void RotateLeftRight(Node<T> parent, Node<T> child)
    {
      Node<T> nodeToAdd = child.Right.Left;
      Node<T> current = parent.Left;

      parent.Left = child.Right;
      parent.Left.Left = child;
      child.Right = nodeToAdd;
      child = parent.Left;

      RotateLeftLeft(parent, child);
    }

    /// <summary>
    /// Rotates the found Node leftwards
    /// </summary>
    /// <param name="parent">Node parent</param>
    /// <param name="child">Node child</param>
    private void RotateLeftLeft(Node<T> parent, Node<T> child)
    {
      Node<T> nodeToAdd = child.Right;

      if (parent == _root)
      {
        _root = child;
        child.Right = parent;
        parent.Left = nodeToAdd;
      }
      else
      {
        child.Right = parent;
        parent.Left = nodeToAdd;
      }
    }

    /// <summary>
    /// Returns true if the child of <paramref name="parent"/> has a longer left branch than its right branch.
    /// </summary>
    /// <param name="current">T</param>
    /// <returns>Whether or not the left-left-case is true</returns>
    private bool LeftLeft(Node<T> parent)
    {
      if (CalculateHeight(parent.Left) > CalculateHeight(parent.Right) && (CalculateBalanceFactor(parent.Left) == 0 || CalculateBalanceFactor(parent.Left) == 1))
        return true;
      else
        return false;
    }

    /// <summary>
    /// Returns true if the child of <paramref name="parent"/> has a longer right branch than its left branch.
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    private bool LeftRight(Node<T> parent)
    {
      if (CalculateHeight(parent.Left) > CalculateHeight(parent.Right) && CalculateBalanceFactor(parent.Left) > 1)
        return true;
      else
        return false;
    }

    /// <summary>
    /// Calculates the balance factor for the given <paramref name="node"/>.
    /// </summary>
    /// <param name="node">Subtree to calculate balance factor for.</param>
    /// <returns>Balance factor.</returns>
    private int CalculateBalanceFactor(Node<T> node)
    {
      if (node == null)
        return 0;
      else
        return CalculateHeight(node.Right) - CalculateHeight(node.Left);
    }

    /// <summary>
    /// Counts the number of tree levels of <paramref name="node"/>.
    /// </summary>
    /// <param name="node">The subtree to count.</param>
    /// <returns>Number of levels including <paramref name="node"/>.</returns>
    private int CalculateHeight(Node<T> node)
    {
      if (node == null)
        return 0;
      else
        return 1 + Math.Max(CalculateHeight(node.Left), CalculateHeight(node.Right));
    }

    /// <summary>
    /// Calls GetNodeToCalculate() to find the <paramref name="node"/> of which the balancefactor is to be calculated.
    /// </summary>
    /// <param name="value">The value of the <paramref name="node"/> of which the balancefactor is wanted</param>
    /// <returns>The balancefactor of the node</returns>
    public int CalculateBalanceFactor(T value)
    {
      int balanceFactor = GetNodeToCalculate(_root, value);
      return balanceFactor;
    }

    /// <summary>
    /// Finds the <paramref name="node"/> of which the balancefactor is to be calculated.
    /// </summary>
    /// <param name="current">The current <paramref name="node"/> that is checked by its value.</param>
    /// <param name="value">The value that is searched for.</param>
    /// <returns></returns>
    private int GetNodeToCalculate(Node<T> current, T value)
    {
      if (current == null)
        return 0;

      if (value.CompareTo(current.Value) == 1 && current.Right != null)
        return GetNodeToCalculate(current.Right, value);
      else if (value.CompareTo(current.Value) == 0)
        return CalculateBalanceFactor(current);
      else if (value.CompareTo(current.Value) == -1 && current.Left != null)
        return GetNodeToCalculate(current.Left, value);
      else
        return 0;
    }

    /// <summary>
    /// Converts the tree to an array.
    /// </summary>
    /// <returns>Array with all values of the tree</returns>
    public T[] ConvertToArray()
    {
      T[] valueArray = new T[Count()];
      int i = 0;
      WriteToArray(_root, ref i, valueArray);
      return valueArray;
    }

    /// <summary>
    /// Stores the values of all nodes in an array.
    /// </summary>
    /// <param name="current">The current Node</param>
    /// <param name="i">Variable to count up the arrays index</param>
    /// <param name="valueArray">The array to store the values in</param>
    private void WriteToArray(Node<T> current, ref int i, T[] valueArray)
    {
      if (current.Left != null)
        WriteToArray(current.Left, ref i, valueArray);

      valueArray[i] = current.Value;
      i++;

      if (current.Right != null)
        WriteToArray(current.Right, ref i, valueArray);
    }

    /// <summary>
    /// Calls the private FindNodeToDelete().
    /// </summary>
    /// <param name="value">Value of the node that is to be remove from the tree</param>
    public void Remove(T value)
    {
      FindNodeToDelete(_root, value, null);
    }

    /// <summary>
    /// Compares the value that is to be removed with the value of the current node.
    /// Calls itself with current.Right if the value that is to be removed is bigger than the value 
    /// of the current node.
    /// Calls itself with current.Left if the value that is to be removed is smaller than the value
    /// of the current node.
    /// Calls RemoveFoundValue() if the value of the current node equals the value that is to be removed.
    /// </summary>
    /// <param name="current">The current node that is compared to value</param>
    /// <param name="value">The value of the node that has to be deleted</param>
    private void FindNodeToDelete(Node<T> current, T value, Node<T> parent)
    {
      if (DoesTreeContainValue(_root, value))
      {
        if (value.CompareTo(current.Value) < 0)
          FindNodeToDelete(current.Left, value, current);

        else if (value.CompareTo(current.Value) > 0)
          FindNodeToDelete(current.Right, value, current);
        else
          RemoveFoundValue(current, parent, value);
      }
      else
        return;
    }

    /// <summary>
    /// Checks if there are further nodes on the current node that is to be deleted.
    /// Calls the related Method for each case.
    /// </summary>
    /// <param name="current">Node that is to be deleted</param>
    /// <param name="parent">Parentnode of the node that is to be deleted</param>
    /// <param name="value">Value of the node that is to be deleted</param>
    private void RemoveFoundValue(Node<T> current, Node<T> parent, T value)
    {
      if (current.Right != null && current.Left == null) //If there is only a node on the right side of current.
        Add(parent, current.Right);

      else if (current.Right == null && current.Left != null) //If there is only a node on the left side of current.
        Add(parent, current.Left);
      else if (current.Right != null && current.Left != null) //If there is a node on both sides of current .
        HookIn(parent, current, value);
      else //If there is no node on either side of current.
      {
        if (value.CompareTo(parent.Value) > 0)
        {
          current = null;
          parent.Right = current;
        }
        else if (value.CompareTo(parent.Value) == 0)
          throw new Exception("Unexpected matching");
        else
        {
          current = null;
          parent.Left = current;
        }
      }
    }

    /// <summary>
    /// Adds the nodes that are on the node that is to be deleted to the
    /// correct side of parent.
    /// Stores the branch that has not yet been added and calls
    /// AddOriginalBranch() to hook it to the correct branch of the new tree.
    /// </summary>
    /// <param name="parent">Parentnode of the current node</param>
    /// <param name="nodeToDelete">The node that is to be deleted</param>
    /// <param name="value">The value of the node that is to be deleted</param>
    private void HookIn(Node<T> parent, Node<T> nodeToDelete, T value)
    {
      Node<T> nodeToAdd = nodeToDelete.Right;

      if (parent == null)
      {
        _root = nodeToDelete.Left;
        Add(_root, nodeToAdd);
        return;
      }

      if (value.CompareTo(parent.Value) > 0)
      {
        parent.Right = nodeToDelete.Left;
        nodeToDelete = nodeToDelete.Left;
        Add(nodeToDelete, nodeToAdd);
      }
      else if (value.CompareTo(parent.Value) < 0)
      {
        parent.Left = nodeToDelete.Left;
        nodeToDelete = nodeToDelete.Left;
        Add(nodeToDelete, nodeToAdd);
      }
      else
        throw new Exception("Unexpected matching values!");
    }

    /// <summary>
    /// Calls the private Add() method - needed due to protection level.
    /// </summary>
    /// <param name="element">Element that is to be added by user</param>
    public void Add(T element)
    {
      if (DoesTreeContainValue(_root, element))
        throw new ArgumentException("A node with this value already exists!");
      else
      {
        Node<T> nodeToAdd = new Node<T>(element);
        Add(_root, nodeToAdd);
      }
    }

    /// <summary>
    /// Checks if there is already a _root, calls AddLeft() or AddRight()
    /// depending on whether the element is bigger or smaller than _root.Value.
    /// </summary>
    /// <param name="current">Current node that is compared to</param>
    /// <param name="element">Element to add to the tree</param>
    private void Add(Node<T> current, Node<T> nodeToAdd)
    {
      if (_root == null)
        _root = nodeToAdd;
      else
      {
        if (nodeToAdd.Value.CompareTo(current.Value) >= 0)
          AddRight(current, nodeToAdd);
        else
          AddLeft(current, nodeToAdd);
      }

      AutoBalanceTree(_root); //--------------------------------------------------------------------------------------------------------------------------------

    }

    /// <summary>
    /// Calls the private DoesTreeContainValue() due to protecion level.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <returns>true if the tree contains the <paramref name="value"/></returns>
    public bool Contains(T value)
    {
      return DoesTreeContainValue(_root, value);
    }

    /// <summary>
    /// Checks whether the tree contains the <paramref name="value"/>.
    /// </summary>
    /// <param name="current">The current node that is checked</param>
    /// <param name="value">Value to check</param>
    /// <returns>true if the tree contains the <paramref name="value"/></returns>
    private bool DoesTreeContainValue(Node<T> current, T value)
    {
      if (current == null)
        return false;

      if (value.CompareTo(current.Value) == 1 && current.Right != null)
        return DoesTreeContainValue(current.Right, value);
      else if (value.CompareTo(current.Value) == 0)
        return true;
      else if (value.CompareTo(current.Value) == -1 && current.Left != null)
        return DoesTreeContainValue(current.Left, value);
      else
        return false;
    }

    /// <summary>
    /// Calls the private Count() - needed due to protection level.
    /// </summary>
    /// <returns>Number of nodes in the tree</returns>
    public int Count()
    {
      return Count(_root, 0);
    }

    /// <summary>
    /// Counts all elements in the tree.
    /// </summary>
    /// <param name="current"></param>
    /// <returns>Number of nodes in the tree</returns>
    private int Count(Node<T> current, int numberOfElements)
    {
      if (current.Left != null)
        numberOfElements = Count(current.Left, numberOfElements);

      numberOfElements += 1;

      if (current.Right != null)
        numberOfElements = Count(current.Right, numberOfElements);

      return numberOfElements;
    }

    /// <summary>
    /// Checks if the current node has a node to its left .
    /// Adds a new node with value "element" if there is no left node.
    /// If there is a node on the left side of current: Calls Add again with current.Left
    /// to check whether AddLeft() or AddRight() has to be called next.
    /// </summary>
    /// <param name="current">Current Node that is compared to</param>
    /// <param name="element">Value that is to be added</param>
    private void AddLeft(Node<T> current, Node<T> nodeToAdd)
    {
      if (current.Left != null)
        Add(current.Left, nodeToAdd);
      else
        current.Left = nodeToAdd;
    }

    /// <summary>
    /// Checks if the current node has a node to its right .
    /// Adds a new node with value "element" if there is no right node.
    /// If there is a node on the right side of current: Calls Add() again with current.Right
    /// to check whether AddLeft() or AddRight() has to be called next.
    /// </summary>
    /// <param name="current">Current Node that is compared to</param>
    /// <param name="element">Value that is to be added</param>
    private void AddRight(Node<T> current, Node<T> nodeToAdd)
    {
      if (current.Right != null)
        Add(current.Right, nodeToAdd);
      else
        current.Right = nodeToAdd;
    }
  }
}