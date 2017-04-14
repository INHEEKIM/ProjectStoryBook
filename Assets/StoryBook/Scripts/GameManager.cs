using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


    public static GameManager manager;

    public List<bool> phase;

    void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);  //don't destroy!!! other Scene
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }

        //처음 켰을 때 초기화.
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        for (int i = 0; i < data.phase.Count; i++)
            data.phase[i] = false;

    }


    /// <summary>
    /// 저장
    /// </summary>
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();

        for (int i = 0; i < data.phase.Count; i++)  data.phase[i] = phase[i];

        bf.Serialize(file, data);
        file.Close();
    }
    /// <summary>
    /// 불러오기
    /// </summary>
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < data.phase.Count; i++)
                phase[i] = data.phase[i];
        }
    }





}


[Serializable]
class PlayerData
{
    public List<bool> phase;
}
