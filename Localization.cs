using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    public static string[] locales = { "en_GB", "nl_NL", "it_IT", "ro_RO", "de_DE" };
    public static void LoadLocales()
    {
        localeStrings = new List<localeString>();
        foreach(string folder in folders)
        {
            foreach(string locale in locales)
            {
                TextAsset thisLocaleFile = Resources.Load<TextAsset>("locales/" + folder + "/" + locale + "");
                Debug.Log("loading locale: " + folder + "/" + locale);
                string[] splitter = new string[] { "\r\n", "\r", "\n" };
                string[] lines = Regex.Split(thisLocaleFile.text, "\n|\r|\r\n");
                foreach (string line in lines)
                {
                    if (line.Contains("="))
                    {
                        string key = "";
                        string value = "";
                        string[] split = line.Split(new[] { '=' }, 2);
                        key = split[0];
                        value = split[1].Substring(1, split[1].Length - 3);

                        localeString thisString;
                        thisString.locale = locale;
                        thisString.section = folder;

                        value = value.Replace("\\n", "\n");
                        //value = value.Replace("/", "/");
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
            obj.ChangeLocale();
        }
    }

    public static string ProcessLine(string content, string[] fields)
    {
        if(fields.Length > 0)
        {
            int index = 0;
            foreach(string field in fields)
            {
                content = content.Replace("{username}", "Cubey");
                content = content.Replace("{" + index.ToString() + "}", fields[index]);
                index++;
            }
            return content;
        }
        else
        {
            return content;
        }
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

    public static string GetTextFromCurrentLocale(string name, string[] fields, string file = "game")
    {
        return GetText(currentLocale, name, fields, file);
    }
}
