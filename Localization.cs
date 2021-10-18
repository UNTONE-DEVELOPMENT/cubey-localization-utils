using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Localization
{
    public static List<LocaleBehavior> localeBehaviors;
    public static string currentLocale;
    // <section <locale <key value>>>
    public struct localeString
    {
        public string section;
        public string locale;
        public string name;
        public string content;
    }

    public static List<localeString> localeStrings;

    public static void LoadLocales()
    {
        localeStrings = new List<localeString>();

    }

    public static void SetLocale(string locale)
    {
        currentLocale = locale;
        foreach(LocaleBehavior obj in localeBehaviors)
        {
            obj.ChangeLocale(locale);
        }
    }

    public static string GetText(string locale, string name, string[] fields, string file = "game")
    {
        return "test";
    }
}