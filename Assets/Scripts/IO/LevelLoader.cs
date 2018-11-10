using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelLoader : MonoBehaviour {
    //testing
    public int levelID = -1;
    public List<GameObject> objectPrefabs;
    public Text levelText;
    public void Start()
    {
        if(SceneData.sd!= null)
        {
            levelID = SceneData.sd.levelID;
            ClearLevel();
            LoadLevel();
        }
    }
    public void LoadLevel()
    {
        if (levelID < 0)
            return;
        //string dirPath = Path.Combine(Application.dataPath, Path.Combine("Resources", "XML"));
        TextAsset ta = Resources.Load(Path.Combine("XML", levelID.ToString())) as TextAsset;
        LevelData loaded = LevelData.Load(ta.text);
#if UNITY_EDITOR
#else
       levelText.text = (StarController.sc.GetLevelIndex(levelID)+1).ToString();
#endif
        GameObject go;
        SaveData sd;
        foreach (GameObjectData data in loaded.objects)
        {
#if UNITY_EDITOR
            go = (GameObject)PrefabUtility.InstantiatePrefab(objectPrefabs[data.objID]);
#else
            go = (GameObject)Instantiate(objectPrefabs[data.objID]);
#endif
            sd = go.GetComponent<SaveData>();
            if (sd == null)
            {
                Debug.LogError("SaveData missing on prefab");
            }
            else
            {
                sd.LoadData(data);
            }
        }
        //levelID = -1;
    }

#if UNITY_EDITOR
    public void SaveLevel()
    {
        if (!Application.isEditor)
        {
            return;
        }
        if (levelID < 0)
            return;
        LevelData toSave = new LevelData();
        SaveData[] datas = FindObjectsOfType<SaveData>();
        toSave.objects = new List<GameObjectData>();
        GameObject pref;
        GameObjectData dat;
        foreach (SaveData data in datas)
        {
            Debug.Log(PrefabUtility.GetPrefabParent(data.gameObject));
            pref = (GameObject) PrefabUtility.GetPrefabParent(data.gameObject);
            dat = data.GenerateSaveData();
            if ( pref == null )
            {
                Debug.LogError("No prefab found to save!");
                return;
            }         
            if(objectPrefabs.Count == 0)
            {
                Debug.LogError("Unknown prefab");
                return;
            }
            for (int i = 0; i < objectPrefabs.Count; i++)
            {
                if (objectPrefabs[i]!=null && objectPrefabs[i].name==pref.name)
                {
                    dat.objID = i;
                    break;
                } else if (i == objectPrefabs.Count - 1)
                {
                    Debug.LogError("Unknown prefab, add to list: " + pref.ToString());
                    return;
                }
            }
            toSave.objects.Add(dat);
        }

        string dirPath = Path.Combine(Application.dataPath, Path.Combine("Resources", "XML"));
        toSave.Save(Path.Combine(dirPath, levelID.ToString() + ".xml"));
        Debug.Log("Saved to: " + Path.Combine(dirPath, levelID.ToString() + ".xml"));
        Refresh();
    }
    void Refresh()
    {
        System.Threading.Thread.Sleep(100);
        AssetDatabase.Refresh();
    }

    public void ClearLevel()
    {
        SaveData[] datas = FindObjectsOfType<SaveData>();
        foreach (SaveData data in datas)
        {
            DestroyImmediate(data.gameObject);
        }
    }
#else
    public void ClearLevel()
    {
        SaveData[] datas = FindObjectsOfType<SaveData>();
        foreach (SaveData data in datas)
        {
            Destroy(data.gameObject);
        }
        //GravityManager.gm.ClearLists();
    }

#endif


}

[Serializable]
public class GameObjectData
{
    [XmlAttribute("ObjectID")]
    public int objID;
    [XmlAttribute("XPos")]
    public float xPos;
    [XmlAttribute("YPos")]
    public float yPos;
    [XmlAttribute("XScale")]
    public float xScale;
    [XmlAttribute("YScale")]
    public float yScale;
    [XmlAttribute("ZRotation")]
    public float zRotation;
	[XmlAttribute("StarId")]
	public int starId;
}
[Serializable]
public class ToggleableGameObjectData:GameObjectData
{
    [XmlArray("GroupIDs")]
    [XmlArrayItem("ID")]
    public List<int> groupIDs; //Leave empty if you don't want it to be togglable
    [XmlAttribute("SpriteID")]
    public int spriteID;
    [XmlAttribute("Mass")]
    public float mass;
    [XmlAttribute("IsLethal")]
    public bool lethal;
    [XmlAttribute("IsActive")]
    public bool active;
    [XmlAttribute("HeadID")]
    public int headID;
    [XmlAttribute("ToggleAmplitudeX")]
    public float toggleAmplitudeX;
    [XmlAttribute("ToggleAmplitudeY")]
    public float toggleAmplitudeY;
    [XmlAttribute("ToggleSpeedX")]
    public float toggleSpeedX;
    [XmlAttribute("ToggleSpeedY")]
    public float toggleSpeedY;

}
[Serializable]
public class PufferMovementObjectData:ToggleableGameObjectData
{
    [XmlAttribute("AmplitudeX")]
    public float amplitudeX;
    [XmlAttribute("AmplitudeY")]
    public float amplitudeY;
    [XmlAttribute("SpeedX")]
    public float speedX;
    [XmlAttribute("SpeedY")]
    public float speedY;
}

[Serializable]
[XmlRoot("LevelData")]
public class LevelData
{
    [XmlArray("Objects")]
    [XmlArrayItem("GameObjectData", typeof(GameObjectData))]
    [XmlArrayItem("ToggleableGameObjectData", typeof(ToggleableGameObjectData))]
    [XmlArrayItem("PufferMovementObjectData", typeof(PufferMovementObjectData))]
    public List<GameObjectData> objects;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(LevelData));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static LevelData Load(string xml)
    {
        var serializer = new XmlSerializer(typeof(LevelData));
        using (var stream = new StringReader(xml))
        {
            return serializer.Deserialize(stream) as LevelData;
        }
    }/*
    public static LevelData Load(string path)
    {
        var serializer = new XmlSerializer(typeof(LevelData));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as LevelData;
        }
    }*/

}
