﻿using System;

namespace Tree_Generic
{
  public class Program
  {
    static void Main(string[] args) 
    {
      var testTree = new Tree<int>();
      testTree.Add(20);
      testTree.Add(30);
      testTree.Add(10);
      testTree.Add(5);
      testTree.Add(9);
      testTree.Count();

      int[] arr = testTree.ConvertToArray();

      for (int i = 0; i < arr.Length; i++)
      {
        Console.WriteLine(arr[i]);
      }    
    }
  }
}