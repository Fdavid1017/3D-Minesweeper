using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string mapPath = Application.persistentDataPath + "/map.mms";
    static string highScoresPath = Application.persistentDataPath + "/highscores.hsms";

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

    public static void SaveHighScore(List<Highscore> hsList)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;

        stream = new FileStream(highScoresPath, FileMode.Create);

        //List<Highscore> highscores = new List<Highscore>();

        formatter.Serialize(stream, hsList);
        stream.Close();

        Debug.Log("File saved at: " + highScoresPath);
    }

    public static List<Highscore> LoadHighScores()
    {
        if (File.Exists(highScoresPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(highScoresPath, FileMode.Open);
            List<Highscore> data = formatter.Deserialize(stream) as List<Highscore>;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found at: " + highScoresPath);
            return null;
        }
    }
}
