using System;

namespace Tree_Generic
{
  public class Program
  {
    static void Main(string[] args) 
    {
      var testTree = new Tree<int>();
      testTree.Add(30);
      testTree.Add(25);
      testTree.Add(35);
      testTree.Add(36);
      testTree.Add(34);
      testTree.Add(20);
      testTree.Add(15);
      testTree.Add(24);
      testTree.Add(23);

      testTree.Count();

      int[] arr = testTree.ConvertToArray();

      for (int i = 0; i < arr.Length; i++)
      {
        Console.WriteLine(arr[i]);
      }    
    }
  }
}