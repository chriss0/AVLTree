using System;

namespace Tree_Generic
{
  public class Program
  {
    static void Main(string[] args) 
    {
      var testTree = new Tree<int>();
      testTree.Add(20);
      testTree.Add(10);
      testTree.Add(5);
      testTree.Add(30);
      testTree.Add(4);
      testTree.Add(6);
      testTree.Add(3);
      testTree.Add(2);
      //testTree.Add(10);

      testTree.Count();

      int[] arr = testTree.ConvertToArray();

      for (int i = 0; i < arr.Length; i++)
      {
        Console.WriteLine(arr[i]);
      }    
    }
  }
}