//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public static class InputManager 
//{
//    static Dictionary<string, KeyCode> keyMapping;
//    static string[] keyMaps = new string[5]
//    {
//        "PLayer1L",
//        "PLayer1R",
//        "PLayer2L",
//        "PLayer2R",
//        "Return"
//    };
//    static KeyCode[] defaults = new KeyCode[5]
//    {
//        KeyCode.Z,
//        KeyCode.M,
//        KeyCode.LeftArrow,
//        KeyCode.RightArrow,
//        KeyCode.Escape
//    };

//    public static void LoadInfo()
//    {
//        InitializeDictionary();
//    }

//    private static void InitializeDictionary()
//    {
//        keyMapping = new Dictionary<string, KeyCode>();
//        for (int i = 0; i < keyMaps.Length; ++i)
//        {
//            keyMapping.Add(keyMaps[i], defaults[i]);
//        }
//    }

//    public static void SetKeyMap(int index, KeyCode key)
//    {
//        keyMapping[GetKey(index)] = key;
//    }

//    public static bool GetKeyDown(string keyMap)
//    {
//        return Input.GetKeyDown(keyMapping[keyMap]);
//    }

//    private static string GetKey(int index)
//    {
//        return keyMapping.ElementAt(index).Key;
//    }
//}
