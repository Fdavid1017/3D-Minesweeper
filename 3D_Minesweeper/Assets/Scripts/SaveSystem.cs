using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string mapPath = Application.persistentDataPath + "/map.mms";

    public static void SaveMap(MapGenerations map)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(mapPath, FileMode.Create);

        MapData data = new MapData(MapGenerations.xSize, MapGenerations.zSize, map.map, map.GetRevealedBool(), GameUIHelper.playTime, MapGenerations.bombCount, MapGenerations.difficulity, map.GetFlaggedMap());

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("File saved at: " + mapPath);
    }

    public static MapData LoadMap()
    {
        if (File.Exists(mapPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(mapPath, FileMode.Open);
            MapData data = formatter.Deserialize(stream) as MapData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found at: " + mapPath);
            return null;
        }
    }
}
