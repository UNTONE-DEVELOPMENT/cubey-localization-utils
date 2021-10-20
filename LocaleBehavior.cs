using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocaleBehavior : MonoBehaviour
{
    public string locale;
    public string name;
    public string section = "game";

    bool added = false;

    public void Start()
    {
        //Debug.Log("adding locale behavior to list");
        //Localization.localeBehaviors.Add(this);
        //Debug.Log("[LOCALEBEHAVIOR] Started");
        //ChangeLocale(Localization.currentLocale);
    }

    public void Update()
    {
        try
        {
            if (added == false)
            {
                Localization.localeBehaviors.Add(this);
                added = true;
                ChangeLocale(Localization.currentLocale);
            }
        }
        catch
        {
            
        }
    }

    public void ChangeLocale(string newLocale)
    {
        locale = newLocale;
        SetText(newLocale);
    }

    public virtual void SetText(string newLocale)
    {

    }
}
