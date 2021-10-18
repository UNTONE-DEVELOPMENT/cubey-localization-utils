using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationSetText : LocaleBehavior
{
    public Text thisText;
    public string[] fields;

    public override void SetText(string locale)
    {
        Debug.Log("updating text!!!");
        thisText.text = Localization.GetText(locale, name, fields);
    }
}
