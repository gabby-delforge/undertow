using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StarController : MonoBehaviour {
	public StarData starData;
    private string starDataFileName = "starData";
    public static StarController sc;
    void Awake()
    {
        if (sc == null) {
            sc = this;
            DontDestroyOnLoad(gameObject);
		    LoadStarData();
        } else if (sc != this) {
            Destroy(gameObject);
        }
	}

    public int TotalStarsInLevel(int levelId)
    {
        return starData.allStars[levelId].stars.Count; 
    }

    public int CollectedStarsInLevel(int levelId)
    {
        return starData.allStars[levelId].CountStars();
    }

	public void ReachStar(int levelId, int starId)
	{
		starData.ReachStar (levelId, starId);
	}

	public bool CheckStar (int levelId, int starId) {
		return starData.CheckStar (levelId, starId);
	}

    public void ResetStars () {
        print ("Reset all stars");
        starData.ResetStars();
        SaveStarData ();
    }
    
    public void CollectAllStars () {
        print ("Collected all stars");
        starData.CollectAllStars();
        SaveStarData ();
    }

    public int GetLevelIndex ( int levelId)
    {
        int i;
        for (i = 0; i < starData.allStars.Count; i++)
        {
            if (starData.allStars[i].levelId == levelId)
            {
                break;
            }
        }
        return i;
    }

	public void LoadStarData()
	{
        //Path.Combine combines strings into a file path
        //Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        #if UNITY_EDITOR
        TextAsset txtAst = Resources.Load(starDataFileName) as TextAsset;
        string dataAsJson = txtAst.text;
        
        #else
        string path = Application.persistentDataPath + "/starData";
        string dataAsJson;
        if (File.Exists (path)) {
            dataAsJson = File.ReadAllText (path);
        } else {
            TextAsset txtAst = Resources.Load(starDataFileName) as TextAsset;
            dataAsJson = txtAst.text;
        }
        #endif
       
		starData = JsonUtility.FromJson<StarData>(dataAsJson);
	}

	public void SaveStarData()
	{
        
        TextAsset txtAst = Resources.Load(starDataFileName) as TextAsset;
		string dataAsJson = JsonUtility.ToJson (starData);
        #if UNITY_EDITOR
        File.WriteAllText(AssetDatabase.GetAssetPath(txtAst), dataAsJson);
        EditorUtility.SetDirty(txtAst);
        #else
        File.WriteAllText(Application.persistentDataPath + "/starData", dataAsJson);
        #endif
        
        Refresh();

    }

    void Refresh()
    {
        #if UNITY_EDITOR
        System.Threading.Thread.Sleep(100);
        AssetDatabase.Refresh();
        #endif
    }




}
