using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocaleBehavior : MonoBehaviour
{
    public string locale;
    public string name;

    public void Start()
    {
        Debug.Log("adding locale behavior to list");
        Localization.localeBehaviors.Add(this);
        Debug.Log("[LOCALEBEHAVIOR] Started");
        ChangeLocale(Localization.currentLocale);
    }

    public void ChangeLocale(string newLocale)
    {
        Debug.Log("setting locale in LocaleBehavior");
        locale = newLocale;
        SetText(newLocale);
    }

    public virtual void SetText(string newLocale)
    {

    }
}
