using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {

    public void LoadData(GameObjectData data)
    {
        LoadBase(data);
        if (data is ToggleableGameObjectData)
            LoadToggleable((ToggleableGameObjectData)data);
        if (data is PufferMovementObjectData)
            LoadMoving((PufferMovementObjectData)data);
    }

    public GameObjectData GenerateSaveData()
    {
        GameObjectData toSave = new GameObjectData();
        if (GetComponent<GroupIDs>() != null)
        {
            toSave = new ToggleableGameObjectData();
        }
        if (GetComponent<PufferMovement>() != null)
        {
            toSave = new PufferMovementObjectData();
        }
        SaveBase(toSave);
        if(toSave is ToggleableGameObjectData)
            SaveToggleable((ToggleableGameObjectData)toSave);
        if(toSave is PufferMovementObjectData) 
            SaveMoving((PufferMovementObjectData)toSave);
        return toSave;
    }
    
    void LoadBase(GameObjectData data)
    {
        transform.position = new Vector3(data.xPos, data.yPos, 0);
        transform.localScale = new Vector3(data.xScale, data.yScale, 1);
        transform.rotation = Quaternion.Euler(0,0,data.zRotation);
		Star s = GetComponent<Star> ();
		if (s != null)
			s.starId = data.starId;
    }

    void LoadToggleable(ToggleableGameObjectData data)
    {
        GroupIDs gid = GetComponent<GroupIDs>();
        if (gid != null)
        {
            gid.groupIDs = data.groupIDs;
            gid.isActive = data.active;
        }
        /*GenericHead gh = GetComponent<GenericHead>();
        if (gh != null)
        {
            gh.headID = data.headID;
            gh.Load();
        }*/
        ToggleSprite ts = GetComponent<ToggleSprite>();
        if (ts != null)
        {
            ts.SpriteID = data.spriteID;
        }
        Gravity g = GetComponent<Gravity>();
        if (g != null)
        {
            g.SetMass(data.mass);
        }

        ToggleMovement tg = GetComponent<ToggleMovement>();
        if (tg != null) {
            tg.amplitudeX = data.toggleAmplitudeX;
            tg.amplitudeY = data.toggleAmplitudeY;
            tg.speedX = data.toggleSpeedX;
            tg.speedY = data.toggleSpeedY;
        }
    }

    void LoadMoving(PufferMovementObjectData data)
    {
        PufferMovement mo = GetComponent<PufferMovement>();
        if (mo != null)
        {
            mo.amplitudeX = data.amplitudeX;
            mo.amplitudeY = data.amplitudeY;
            mo.speedX = data.speedX;
            mo.speedY = data.speedY;
        }
    }

    void SaveBase(GameObjectData data)
    {
        data.xPos = transform.position.x;
        data.yPos = transform.position.y;
        data.xScale = transform.localScale.x;
        data.yScale = transform.localScale.y;
        data.zRotation = transform.eulerAngles.z;
        print (data.zRotation);
        Star s = GetComponent<Star> ();
		if (s != null)
			data.starId = s.starId;
    }

    void SaveToggleable(ToggleableGameObjectData data)
    {
        GroupIDs gid = GetComponent<GroupIDs>();
        if (gid != null)
        {
            data.groupIDs = gid.groupIDs;
            data.active = gid.isActive;
        }
        /*GenericHead gh = GetComponent<GenericHead>();
        if (gh != null)
        {
            data.headID = gh.headID;
        }*/
        ToggleSprite ts = GetComponent<ToggleSprite>();
        if (ts != null)
        {
            data.spriteID = ts.SpriteID;
        }
        Gravity g = GetComponent<Gravity>();
        if (g != null)
        {
            data.mass = g.GetMass();
        }
        ToggleMovement tg = GetComponent<ToggleMovement>();
        if (tg != null) {
            data.toggleAmplitudeX = tg.amplitudeX;
            data.toggleAmplitudeY = tg.amplitudeY;
            data.toggleSpeedX = tg.speedX;
            data.toggleSpeedY = tg.speedY;
        }
    }

    void SaveMoving(PufferMovementObjectData data)
    {

        PufferMovement mo = GetComponent<PufferMovement>();
        if (mo != null)
        {
            data.amplitudeX = mo.amplitudeX;
            data.amplitudeY = mo.amplitudeY;
            data.speedX = mo.speedX;
            data.speedY = mo.speedY;
        }
    }
    
}
