using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Data
{
    public bool on_off;
    public int channel;
    public int volume;
}

public class buttonClick : MonoBehaviour
{
    public void Start()
    {
        Data data = new Data();
        data.on_off = false;
        data.channel = 0;
        data.volume = 0;
    }

    public void buttonOnClick(Data data)
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        switch(clickObject.name)
        {
            case "TV_ON":
                Debug.Log("TV ON");
                break;
            case "TV_OFF":
                Debug.Log("TV OFF");
                break;

            case "VolUP":
                data.volume += 1;
                Debug.Log("VolUP");
                break;

            case "VolDOWN":
                data.volume -= 1;
                Debug.Log("VolDOWN");
                break;

            case "ChUP":
                data.channel += 1;
                Debug.Log("ChUP");
                break;
            
            case "ChDOWN":
                data.channel -= 1;
                Debug.Log("ChDOWN");
                break;
        }

        string tv_info = JsonUtility.ToJson(data);
        //File.WriteAllText(Application.dataPath + "/tvInfo.json", tv_info);

        Debug.Log(tv_info);
    }
}
