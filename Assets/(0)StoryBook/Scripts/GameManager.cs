using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


    public static GameManager manager;

    private bool[] phase;



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

        phase = new bool[20];
        for (int i = 0; i < phase.Length; i++)
            phase[i] = false;
    }




    //public void activeNext()
    //{
    //    StartCoroutine("activeNextQuad");
    //}
    //IEnumerator activeNextQuad()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    next.SetActive(true);
    //    resetButton.SetActive(true);

    //    phase[0] = true;
    //    Save();
    //}


    //public void inactiveNext()
    //{
    //    //StartCoroutine("inactiveNextQuad");
    //    next.SetActive(false);
    //    resetButton.SetActive(false);
    //    knight_Anim.anim.resetAttack();

    //    phase[0] = false;
    //    Save();
    //}
    //IEnumerator inactiveNextQuad()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    next.SetActive(false);
    //    resetButton.SetActive(false);
    //    knight_Anim.anim.resetAttack();

    //    phase[0] = false;
    //    Save();
    //}




    /// <summary>
    /// 저장
    /// </summary>
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.phase = new bool[20];

        for (int i = 0; i < data.phase.Length; i++)  data.phase[i] = phase[i];

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

            for (int i = 0; i < data.phase.Length; i++)
                phase[i] = data.phase[i];
        }
    }


    public void setPhase(int index, bool b)
    {
        phase[index] = b;
    }


}


[Serializable]
class PlayerData
{
    public bool[] phase;
}
