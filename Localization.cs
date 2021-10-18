using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public static string[] folders = { "game", "general" };
    public static string[] locales = { "en_GB", "ja_JP" };
    public static void LoadLocales()
    {
        localeStrings = new List<localeString>();
        foreach(string folder in folders)
        {
            foreach(string locale in locales)
            {
                TextAsset thisLocaleFile = Resources.Load<TextAsset>("locales/" + folder + "/" + locale + "");
                Debug.Log("locales/" + folder + "/" + locale + "");
                string[] lines = thisLocaleFile.text.Split(
    new string[] { "\r\n", "\r", "\n" },
    StringSplitOptions.None
);
                foreach (string line in lines)
                {
                    if (line.Contains("="))
                    {
                        string key = "";
                        string value = "";
                        Debug.Log(line);
                        string[] split = line.Split('=');
                        key = split[0];
                        value = split[1].Replace("\n", "").Replace("\r", "").Substring(1, split[1].Length - 2);

                        localeString thisString;
                        thisString.locale = locale;
                        thisString.section = folder;

                        value = value.Replace("\\n", "\n");
                        thisString.content = value;
                        thisString.name = key;

                        localeStrings.Add(thisString);
                    }
                }
            }
        }
    }

    public static void SetLocale(string locale)
    {
        currentLocale = locale;
        foreach(LocaleBehavior obj in localeBehaviors)
        {
            obj.ChangeLocale(locale);
        }
    }

    public static string ProcessLine(string content, string[] fields)
    {
        if(fields.Length > 0)
        {
            int index = 0;
            foreach(string field in fields)
            {
                content = content.Replace("{" + index.ToString() + "}", fields[index]);
                index++;
            }
            return content;
        }
        else
        {
            return content;
        }
        return null;
    }

    public static string GetText(string locale, string name, string[] fields, string file = "game")
    {
        foreach(localeString thisString in localeStrings)
        {
            if(thisString.section == file)
            {
                if (thisString.name == name)
                {
                    if (thisString.locale == locale)
                    {
                        return ProcessLine(thisString.content, fields);
                    }
                }
            }
        }

        // if we cant find the string in the requested language we'll fall back to en_GB
        foreach (localeString thisString in localeStrings)
        {
            if (thisString.section == file)
            {
                if (thisString.name == name)
                {
                    if(thisString.locale == "en_GB")
                    {
                        return ProcessLine(thisString.content, fields);
                    }
                }
            }
        }

        // oh fuck. it doesn't exist. let's return null
        return null;
    }
}