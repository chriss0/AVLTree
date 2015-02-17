using System;

namespace Tree_Generic
{
  /// <summary>
  /// Represents a node of a tree.
  /// </summary>
  /// <typeparam name="T">DataType of the value</typeparam>
  class Node<T> where T : IComparable<T>
  {
    private T _value;
    private Node<T> _right;
    private Node<T> _left;
    private int _weight;

    /// <summary>
    /// Gets/sets the nodes weight.
    /// </summary>
    public int Weight
    {
      get { return _weight; }
      private set { _weight = value; }
    }

    /// <summary>
    /// Value of the node.
    /// </summary>
    public T Value
    {
      get { return _value; }
      private set { _value = value; }
    }

    /// <summary>
    /// Node stored on the right side.
    /// </summary>
    public Node<T> Right
    {
      get { return _right; }
      set { _right = value; }
    }

    /// <summary>
    /// Node stored on the left side.
    /// </summary>
    public Node<T> Left
    {
      get { return _left; }
      set { _left = value; }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="value">Value to store in the node</param>
    public Node(T value)
    {
      Value = value;
    }
  }
}