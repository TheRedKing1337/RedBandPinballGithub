using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LevelInfo //this class stores all the table info for easy use in other scripts, it gets the info from a text file for easy editing, LoadInfo() has to be called on game startup
{
    public static table[] tables;
    private static string path = "Assets/Resources/levelInfo.txt";

    public static void LoadInfo()
    {
        StreamReader reader = new StreamReader(path);
        string rawData = reader.ReadToEnd();        

        string[] info = rawData.Split('|');

        tables = new table[int.Parse(info[0])];
        for(int i=0;i<tables.Length;i++)
        {
            tables[i] = new table();
        }

        for (int i = 1; i < info.Length; i++)
        {
            string[] finalInfo = info[i].Split('/');

            tables[i-1].name = finalInfo[0];
            tables[i-1].icon = finalInfo[1];
            string[] colorData = finalInfo[2].Split(',');
            tables[i-1].color = new Color(float.Parse(colorData[0]), float.Parse(colorData[1]), float.Parse(colorData[2]), float.Parse(colorData[3]));
            tables[i-1].filePath = finalInfo[3];
            tables[i-1].info = finalInfo[4];
        }
    }

    public class table
    {
        //--for the scroll menu
        public string name;
        public string icon;
        public Color color;

        //--for the infoPanel
        public string filePath;
        public string info;
    }
}
