using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StarData {
	[System.Serializable]
	public struct LevelStars {
		public int levelId;
		public List<bool> stars;
        public int starRequirement;

        public int CountStars()
        {
            int counter = 0;            
            for (int i = 0; i < stars.Count; i++)
            {
                counter += stars[i] ? 1 : 0;
            }
            return counter;
        }
    }
	public List<LevelStars> allStars;
	public bool CheckStar (int levelId, int starId) {
		foreach (LevelStars s in allStars) {
			if (levelId == s.levelId)
				return s.stars[starId];
		}
		Debug.LogError ("Error: Star not found");
		return false;
	}
	public void ReachStar (int levelId, int starId) {
		foreach (LevelStars s in allStars) {
			if (levelId == s.levelId) {
				s.stars [starId] = true;
				return;
			}
		}
		Debug.LogError ("Error: Star not found");
	}

    public void ResetStars () {
        foreach (LevelStars s in allStars)
            for (int i = 0; i < s.stars.Count; i++) 
                s.stars[i] = false;
    }

    public void CollectAllStars () {
        foreach (LevelStars s in allStars)
            for (int i = 0; i < s.stars.Count; i++) 
                s.stars[i] = true;
    }

    public int TotalStarsGotten()
    {
        int counter = 0;
        foreach (LevelStars s in allStars)
        {
            counter += s.CountStars();
        }
        return counter;
    }


}
