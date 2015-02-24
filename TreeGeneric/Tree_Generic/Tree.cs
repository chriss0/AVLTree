using System;

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
    /// Gets/sets the _root. 
    /// Only needed for testing purposes.
    /// </summary>
    public Node<T> Root
    {
      get { return _root; }
      private set { _root = value; }
    }

    /// <summary>
    /// Checks each node by its balanceFactor if balancing is needed.
    /// </summary>
    /// <param name="current">The current node.</param>
    private void AutoBalanceTree(Node<T> previous, Node<T> current)
    {
      if (_root == null)
        return;

      if (current.Left != null)
        AutoBalanceTree(current, current.Left);

      CheckForRotateMethod(current, previous);

      if (current.Right != null)
        AutoBalanceTree(current, current.Right);

      return;
    }

    /// <summary>
    /// Checks wheter autobalancing is needed and if so, which rotate-method is to be performed.
    /// </summary>
    /// <param name="current">The current node that is checked.</param>
    private void CheckForRotateMethod(Node<T> current, Node<T> previous)
    {
      if (LeftLeft(current))
      {
        RotateLeftLeft(current, current.Left, previous);
      }
      else if (LeftRight(current))
      {
        RotateLeftRight(current, current.Left, previous);
      }
      else if (RightRight(current))
      {
        RotateRightRight(current, current.Right, previous);
      }
      else if (RightLeft(current))
      {
        RotateRightLeft(current, current.Right, previous);
      }
    }

    /// <summary>
    /// Returns true if the child of <paramref name="parent"/> has a longer left branch than its right branch.
    /// </summary>
    /// <param name="current">The Current node.</param>
    /// <returns>Whether or not the left-left-case is true.</returns>
    private bool LeftLeft(Node<T> parent) //Correct
    {
      if (CalculateBalanceFactor(parent) == 2 && CalculateHeight(parent.Left) > CalculateHeight(parent.Right) &&
         (CalculateBalanceFactor(parent.Left) == 0 || CalculateBalanceFactor(parent.Left) == 1))
        return true;
      else
        return false;
    }

    /// <summary>
    /// Returns true if the child of <paramref name="parent"/> has a longer right branch than its left branch.
    /// </summary>
    /// <param name="parent">The Parent node.</param>
    /// <returns>Wheter or not the left-right.case is true.</returns>
    private bool LeftRight(Node<T> parent) //Check
    {
      if (CalculateBalanceFactor(parent) == 2 && (CalculateBalanceFactor(parent.Left) == -1 || CalculateBalanceFactor(parent.Left) == 0) &&
         CalculateHeight(parent.Left) > CalculateHeight(parent.Right))
        return true;
      else
        return false;
    }

    /// <summary>
    /// Returns true if the child of <paramref name="parent"/> has a longer right branch than its left branch. 
    /// </summary>
    /// <param name="parent">The parent node.</param>
    /// <returns>Whether or not the right-right-case is true.</returns>
    private bool RightRight(Node<T> parent) //Check
    {
      if (CalculateBalanceFactor(parent) == -2 && CalculateHeight(parent.Right) > CalculateHeight(parent.Left) &&
         (CalculateBalanceFactor(parent.Right) == -1 || CalculateBalanceFactor(parent.Right) == 0))
        return true;
      else
        return false;
    }

    /// <summary>
    /// Returns true if the child of <paramref name="parent"/> has a  longer left branch than its right branch.
    /// </summary>
    /// <param name="parent">The parent node.</param>
    /// <returns>Wheter or not the right-left-case is true.</returns>
    private bool RightLeft(Node<T> parent)
    {
      if (CalculateBalanceFactor(parent) == -2 && (CalculateBalanceFactor(parent.Right) == 1 || CalculateBalanceFactor(parent.Right) == 0) &&
        CalculateHeight(parent.Right) > CalculateHeight(parent.Left))
        return true;
      else
        return false;
    }

    /// <summary>
    /// Rotates the found node on the right branch rightwards.
    /// </summary>
    /// <param name="parent">The parent node.</param>
    /// <param name="child">The child node.</param>
    private void RotateRightLeft(Node<T> parent, Node<T> child, Node<T> grandParent)
    {
      Node<T> current = child.Left;
      Node<T> nodeToAdd = current.Right;
      parent.Right = child.Left;
      child.Left = null;
      parent.Right.Right = child;
      child.Left = nodeToAdd;
    }

    /// <summary>
    /// Rotates the found node on the right branch leftwards.
    /// </summary>
    /// <param name="parent">The parent node.</param>
    /// <param name="child">The child node.</param>
    private void RotateRightRight(Node<T> parent, Node<T> child, Node<T> grandParent)
    {
      Node<T> nodeToAdd = child.Left;

      if (parent == _root || grandParent == null)
      {
        _root = child;
        parent.Right = null;
        _root.Left = parent;
        parent.Right = nodeToAdd;
      }
      else if (parent == grandParent.Right)
      {
        grandParent.Right = child;
        child.Left = parent;
        parent.Right = nodeToAdd;
      }
      else if (parent == grandParent.Left)
      {
        grandParent.Left = child;
        child.Left = parent;
        parent.Right = nodeToAdd;
      }
    }

    /// <summary>
    /// Rotates the found value on the left branch leftwards.
    /// </summary>
    /// <param name="parent">The parent node.</param>
    /// <param name="child">The child node.</param>
    private void RotateLeftRight(Node<T> parent, Node<T> child, Node<T> grandParent)
    {
      Node<T> current = child.Right;
      Node<T> nodeToAdd = current.Left;
      parent.Left = child.Right;
      child.Right = null;
      parent.Left.Left = child;
      child.Right = nodeToAdd;
    }

    //<summary>
    //Rotates the found Node on the left branch rightwards.
    //</summary>
    //<param name="parent">The parent node.</param>
    //<param name="child">The child node.</param>
    private void RotateLeftLeft(Node<T> parent, Node<T> child, Node<T> grandParent)
    {
      Node<T> nodeToAdd = child.Right;

      if (parent == _root || grandParent == null)
      {
        _root = child;
        parent.Left = null;
        _root.Right = parent;
        parent.Left = nodeToAdd;
      }
      else if (parent == grandParent.Right)
      {
        grandParent.Right = child;
        child.Right = parent;
        parent.Left = nodeToAdd;
      }
      else if (parent == grandParent.Left)
      {
        grandParent.Left = child;
        child.Right = parent;
        parent.Left = nodeToAdd;
      }
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
        return CalculateHeight(node.Left) - CalculateHeight(node.Right);
    }

    /// <summary>
    /// Calculates the height of the current tree.
    /// </summary>
    /// <returns>Height of the tree.</returns>
    public int CalculateHeight()
    {
      if (_root == null)
        return 0;
      else
        return CalculateHeight(_root);
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
    /// Converts the tree to an array.
    /// </summary>
    /// <returns>Array with all values of the tree.</returns>
    public T[] ConvertToArray()
    {
      if (_root == null)
        return new T[0];

      T[] valueArray = new T[Count()];
      int i = 0;
      WriteToArray(_root, ref i, valueArray);
      return valueArray;
    }

    /// <summary>
    /// Stores the values of all nodes in an array.
    /// </summary>
    /// <param name="current">The current Node.</param>
    /// <param name="i">Variable to count up the arrays index.</param>
    /// <param name="valueArray">The array to store the values in.</param>
    private void WriteToArray(Node<T> current, ref int i, T[] valueArray)
    {
      if (current.Left != null)
        WriteToArray(current.Left, ref i, valueArray);

      valueArray[i++] = current.Value;

      if (current.Right != null)
        WriteToArray(current.Right, ref i, valueArray);
    }

    /// <summary>
    /// Removes the node with the <paramref name="value"/>.
    /// </summary>
    /// <param name="value">Value of the node that is to be removed from the tree.</param>
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
    /// <param name="current">The current node that is compared to value.</param>
    /// <param name="value">The value of the node that has to be deleted.</param>
    private void FindNodeToDelete(Node<T> current, T value, Node<T> parent)
    {
      if (DoesTreeContainValue(_root, value))
      {
        if (value.CompareTo(current.Value) < 0)
          FindNodeToDelete(current.Left, value, current);

        else if (value.CompareTo(current.Value) > 0)
          FindNodeToDelete(current.Right, value, current);
        else
          RemoveFoundValue(current, parent);
      }
      else
        return;
    }

    /// <summary>
    /// Removes the found value from the tree and adds the branches to of the removed node
    /// to the correct place in the tree.
    /// </summary>
    /// <param name="current">The node that is to be deleted</param>
    /// <param name="parent">The node that has <paramref name="current"/> as its child.</param>
    private void RemoveFoundValue(Node<T> current, Node<T> parent)
    {
      Node<T> nodeToAdd = current.Right;

      if (current == _root)
        DeleteRoot();

      else if(current == parent.Right)
      {
        parent.Right = current.Left;
        Add(parent, nodeToAdd);
      }
      else
      {
        parent.Left = current.Left;
        Add(parent, nodeToAdd);
      }
    }

    /// <summary>
    /// Deletes the current root and assigns a new node to it.
    /// </summary>
    public void DeleteRoot()
    {
      Node<T> nodeToAdd = _root.Right;
      _root = _root.Left;
      Add(_root, nodeToAdd);      
    }

    /// <summary>
    /// Adds the node with the <paramref name="value"/>.
    /// </summary>
    /// <param name="value">Value that is to be added by user.</param>
    public void Add(T value)
    {
      if (DoesTreeContainValue(_root, value))
        return;
      else
      {
        Node<T> nodeToAdd = new Node<T>(value);
        Add(_root, nodeToAdd);
      }
    }

    /// <summary>
    /// Checks if there is already a _root, calls AddLeft() or AddRight()
    /// depending on whether the element is bigger or smaller than _root.Value.
    /// </summary>
    /// <param name="current">Current node that is compared to.</param>
    /// <param name="element">Element to add to the tree.</param>
    private void Add(Node<T> current, Node<T> nodeToAdd)
    {
      if (_root == null)
        _root = nodeToAdd;

      else if (nodeToAdd == null)
        return;
      else
      {
        if (nodeToAdd.Value.CompareTo(current.Value) >= 0)
          AddRight(current, nodeToAdd);
        else
          AddLeft(current, nodeToAdd);
      }
      AutoBalanceTree(null, _root);
    }

    /// <summary>
    /// Checks whether the tree contains the <paramref name="value"/>.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>true if the tree contains the <paramref name="value"/>.</returns>
    public bool Contains(T value)
    {
      return DoesTreeContainValue(_root, value);
    }

    /// <summary>
    /// Checks whether the tree contains the <paramref name="value"/>.
    /// </summary>
    /// <param name="current">The current node that is checked.</param>
    /// <param name="value">Value to check.</param>
    /// <returns>true if the tree contains the <paramref name="value"/>.</returns>
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
    /// Counts the elements in the tree.
    /// </summary>
    /// <returns>Number of nodes in the tree.</returns>
    public int Count()
    {
      if (_root == null)
        return 0;

      return Count(_root, 0);
    }

    /// <summary>
    /// Counts all elements in the tree.
    /// </summary>
    /// <param name="current">The curreent node that is checked.</param>
    /// <returns>Number of nodes in the tree.</returns>
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
    /// <param name="current">Current Node that is compared to.</param>
    /// <param name="element">Value that is to be added.</param>
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
    /// <param name="current">Current Node that is compared to.</param>
    /// <param name="element">Value that is to be added.</param>
    private void AddRight(Node<T> current, Node<T> nodeToAdd)
    {
      if (current.Right != null)
        Add(current.Right, nodeToAdd);
      else
        current.Right = nodeToAdd;
    }
  }
}