using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroupIDs : MonoBehaviour {
    public List<int> groupIDs;
    public static Dictionary<int, Color> IDToColor = new Dictionary<int, Color> {
        {0, new Color(196/255f, 191/255f, 38/255f)},
        {1, new Color(63/255f,68/255f, 139/255f)},
        {2, new Color(201/255f,35/255f,67/255f)},
        {3, new Color(122/255f, 229/255f, 221/255f)}
    };
    InputHandler ih;
    BoolEvent myToggles;
    [SerializeField]
	public bool isActive = false;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
    }

    public void Awake()
    {
        myToggles = new BoolEvent(); 
    }

    public void Start()
    {
        SetColor();
        if (groupIDs.Count > 0)
        {
            if (ih == null)
            {
                ih = InputHandler.ih;
            }
            ih.AddToInputListener(Toggle, groupIDs);
        }
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void OnEnable() //This should only effect MENUHeads
    {
        myToggles.Invoke(isActive);
    }

    public void SetActive(bool _isActive)
    {
        isActive = _isActive;
        myToggles.Invoke(isActive);
        
    }

    public void AddToListener(UnityAction<bool> call)
    {
        myToggles.AddListener(call);
        call(isActive);
    }

    public void Toggle()
    {
        SetActive(!isActive);
    }

    void SetColor () {
        Color c = GroupIDs.IDToColor[groupIDs[0]];
        GameObject gem = transform.Find("Gem").gameObject;
        SpriteGlow.SpriteGlowEffect glow = GetComponent<SpriteGlow.SpriteGlowEffect>();
        if (gem != null) {
            gem.GetComponent<SpriteRenderer>().color = c;
            SpriteGlow.SpriteGlowEffect gemGlow = gem.GetComponent<SpriteGlow.SpriteGlowEffect>();
            if (gemGlow != null)
                gemGlow.GlowColor = c;

        }

        if (glow != null) 
            glow.GlowColor = c;

    }
}
