//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;

//public class Program
//{
//    private static T DeepClone<T>(T objectToClone)
//    {
//        using (MemoryStream memoryStream = new MemoryStream())
//        {
//            IFormatter formatter = new BinaryFormatter();
//            formatter.Serialize(memoryStream, objectToClone);
//            memoryStream.Seek(0, SeekOrigin.Begin);
//            return (T)formatter.Deserialize(memoryStream);
//        }
//    }

//    private static (List<T1> list1, List<T2> list2) _AdjustMTFListsFromTop<T1, T2, BaseType>(
//        List<T1> list1, List<T2> list2, Type typeToClone)
//        where T1 : BaseType
//        where T2 : BaseType
//    {
//        List<T1> clonedList1 = DeepClone(list1, typeToClone);
//        List<T2> clonedList2 = DeepClone(list2, typeToClone);

//        if (clonedList1.Count > clonedList2.Count)
//        {
//            // Adjust clonedList1
//        }
//        else
//        {
//            // Adjust clonedList2
//        }

//        return (clonedList1, clonedList2);
//    }

//    private static List<T> DeepClone<T>(List<T> listToClone, Type typeToClone)
//    {
//        List<T> clonedList = new List<T>();
//        foreach (var item in listToClone)
//        {
//            if (item.GetType() == typeToClone)
//            {
//                clonedList.Add(DeepClone(item));
//            }
//            else
//            {
//                clonedList.Add(item);
//            }
//        }
//        return clonedList;
//    }

//    public static void Main(string[] args)
//    {
//        List<DerivedType1> originalList1 = new List<DerivedType1>
//        {
//            // Initialize DerivedType1 objects
//        };

//        List<DerivedType2> originalList2 = new List<DerivedType2>
//        {
//            // Initialize DerivedType2 objects
//        };

//        Type typeToClone = typeof(DerivedType1);
//        var adjustedLists = _AdjustMTFListsFromTop(
//            originalList1, originalList2, typeToClone);

//        // Adjusted lists are in adjustedLists.list1 and adjustedLists.list2
//    }
//}

//[Serializable]
//public class BaseType
//{
//    // Common properties for BaseType
//}

//[Serializable]
//public class DerivedType1 : BaseType
//{
//    // DerivedType1 properties
//}

//[Serializable]
//public class DerivedType2 : BaseType
//{
//    // DerivedType2 properties
//}
