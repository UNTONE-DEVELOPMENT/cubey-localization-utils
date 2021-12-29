using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationSetText : LocaleBehavior
{
    public Text thisText;
    public string[] fields;

    public override void aStart()
    {
        if(thisText == null)
        {
            thisText = this.GetComponent<Text>();
        }
    }

    public override void SetText()
    {
        thisText.text = Localization.GetTextFromCurrentLocale(name, fields, section);
    }
}
