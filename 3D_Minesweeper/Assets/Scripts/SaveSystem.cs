using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string mapPath = Application.persistentDataPath + "/map.mms";
    public static string settingsPath = Application.persistentDataPath + "/settings.sts";

    public static void SaveMap(MapGenerations map)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(mapPath, FileMode.Create);

        MapData data = new MapData(MapGenerations.xSize, MapGenerations.zSize, map.map, map.GetRevealedBool(), GameUIHelper.playTime, MapGenerations.bombCount, MapGenerations.difficulity, map.GetFlaggedMap());

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Map file saved at: " + mapPath);
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
            Debug.LogError("Map save file not found at: " + mapPath);
            return null;
        }
    }

    public static void SaveSettings(SettingsUiHelper settings)
    {
        Debug.Log(settingsPath);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(settingsPath, FileMode.Create);

        SettingsData data = new SettingsData(settings);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Settings file saved at: " + mapPath);
    }

    public static SettingsData LoadSettings()
    {
        if (File.Exists(settingsPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(settingsPath, FileMode.Open);
            SettingsData data = formatter.Deserialize(stream) as SettingsData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Settings save file not found at: " + settingsPath);
            return null;
        }
    }
}
