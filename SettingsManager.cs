// SettingsManager
// Shepherd Zhu
// 取代PlayerPrefs做配置读写，同时兼容老版本使用PlayerPrefs的情况。
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager instance;
    public static SettingsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SettingsManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "SettingsManager";
                    instance = obj.AddComponent<SettingsManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    public static string settingsPath;
    private SettingsData settingsData = new SettingsData();

    private void Awake()
    {
        LoadSettings();
    }

	/// <summary>
	/// 读取配置json，Awake的时候调一次就行了
	/// </summary>
    private void LoadSettings()
    {
	    settingsPath = Path.Combine(Application.persistentDataPath, "settings.json");
        if (File.Exists(settingsPath))
        {
            string json = File.ReadAllText(settingsPath);
            settingsData = JsonConvert.DeserializeObject<SettingsData>(json);
        }
        else
        {
            settingsData = new SettingsData();
        }
    }

	/// <summary>
	/// 写入配置json，理论上每次调用诸如SetInt时都得写一次。
	/// </summary>
    private void SaveSettings()
    {
        string json = JsonConvert.SerializeObject(settingsData);
        File.WriteAllText(settingsPath, json);
    }

    public void SetInt(string key, int value)
    {
	    if (settingsData == null)
	    {
			LoadSettings();
	    }

        settingsData.SetInt(key, value);
        SaveSettings();
    }

    public int GetInt(string key, int defaultValue = 0)
    {
	    if (settingsData == null)
	    {
		    LoadSettings();
	    }

        if (settingsData.HasKey(key))
        {
            return settingsData.GetInt(key, defaultValue);
        }
        else
        {
            if (PlayerPrefs.HasKey(key))
            {
                int value = PlayerPrefs.GetInt(key, defaultValue);
                settingsData.SetInt(key, value);
                PlayerPrefs.DeleteKey(key);
                SaveSettings();
                return value;
            }
            else
            {
                return defaultValue;
            }
        }
    }

    public void SetFloat(string key, float value)
    {
	    if (settingsData == null)
	    {
		    LoadSettings();
	    }

        settingsData.SetFloat(key, value);
        SaveSettings();
    }

    public float GetFloat(string key, float defaultValue = 0f)
    {
	    if (settingsData == null)
	    {
		    LoadSettings();
	    }

        if (settingsData.HasKey(key))
        {
            return settingsData.GetFloat(key, defaultValue);
        }
        else
        {
            if (PlayerPrefs.HasKey(key))
            {
                float value = PlayerPrefs.GetFloat(key, defaultValue);
                settingsData.SetFloat(key, value);
                PlayerPrefs.DeleteKey(key);
                SaveSettings();
                return value;
            }
            else
            {
                return defaultValue;
            }
        }
    }

    public void SetString(string key, string value)
    {
	    if (settingsData == null)
	    {
		    LoadSettings();
	    }

        settingsData.SetString(key, value);
        SaveSettings();
    }

    public string GetString(string key, string defaultValue = "")
    {
	    if (settingsData == null)
	    {
		    LoadSettings();
	    }

        if (settingsData.HasKey(key))
        {
            return settingsData.GetString(key, defaultValue);
        }
        else
        {
            if (PlayerPrefs.HasKey(key))
            {
                string value = PlayerPrefs.GetString(key, defaultValue);
                settingsData.SetString(key, value);
                PlayerPrefs.DeleteKey(key);
                SaveSettings();
                return value;
            }
            else
            {
                return defaultValue;
            }
        }
    }

    public void DeleteAll()
    {
	    if (settingsData == null)
	    {
		    LoadSettings();
	    }

        PlayerPrefs.DeleteAll();
        if (File.Exists(settingsPath))
        {
            File.Delete(settingsPath);
        }
        settingsData = new SettingsData();
    }

    public void DeleteKey(string key)
    {
	    if (settingsData == null)
	    {
		    LoadSettings();
	    }

        PlayerPrefs.DeleteKey(key);
        if (settingsData.HasKey(key))
        {
            settingsData.RemoveKey(key);
            SaveSettings();
        }
    }

    public bool HasKey(string key)
    {
	    if (settingsData == null)
	    {
		    LoadSettings();
	    }

        return PlayerPrefs.HasKey(key) || settingsData.HasKey(key);
    }
}

[System.Serializable]
public class SettingsData
{
    public Dictionary<string, int> intValues;
    public Dictionary<string, float> floatValues;
    public Dictionary<string, string> stringValues;

    public SettingsData()
    {
        intValues = new Dictionary<string, int>();
        floatValues = new Dictionary<string, float>();
        stringValues = new Dictionary<string, string>();
    }

    public void SetInt(string key, int value)
    {
        if (intValues.ContainsKey(key))
        {
            intValues[key] = value;
        }
        else
        {
            intValues.Add(key, value);
        }
    }

    public int GetInt(string key, int defaultValue)
    {
        if (intValues.ContainsKey(key))
        {
            return intValues[key];
        }
        else
        {
            return defaultValue;
        }
    }

    public void SetFloat(string key, float value)
    {
        if (floatValues.ContainsKey(key))
        {
            floatValues[key] = value;
        }
        else
        {
            floatValues.Add(key, value);
        }
    }

    public float GetFloat(string key, float defaultValue)
    {
        if (floatValues.ContainsKey(key))
        {
            return floatValues[key];
        }
        else
        {
            return defaultValue;
        }
    }

    public void SetString(string key, string value)
    {
        if (stringValues.ContainsKey(key))
        {
            stringValues[key] = value;
        }
        else
        {
            stringValues.Add(key, value);
        }
    }

    public string GetString(string key, string defaultValue)
    {
        if (stringValues.ContainsKey(key))
        {
            return stringValues[key];
        }
        else
        {
            return defaultValue;
        }
    }

    public bool HasKey(string key)
    {
        return intValues.ContainsKey(key) || floatValues.ContainsKey(key) || stringValues.ContainsKey(key);
    }

    public void RemoveKey(string key)
    {
        if (intValues.ContainsKey(key))
        {
            intValues.Remove(key);
        }
        else if (floatValues.ContainsKey(key))
        {
            floatValues.Remove(key);
        }
        else if (stringValues.ContainsKey(key))
        {
            stringValues.Remove(key);
        }
    }
}
