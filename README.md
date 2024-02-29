## SettingsManager

SettingsManager is a Unity script that provides a convenient way to manage and persist configuration settings in a game. It replaces the usage of PlayerPrefs and supports backward compatibility for projects that previously used PlayerPrefs.

### Features

- Stores configuration settings in a JSON file in the persistent data path.
- Supports setting and getting integer, float, and string values.
- Provides methods to delete all settings or specific keys.
- Handles backward compatibility by automatically migrating values from PlayerPrefs to the JSON file.

### Usage

1. Attach the SettingsManager script to any game object in your Unity project.
2. Use the following methods to set and get configuration values:

   - `SetInt(string key, int value)`: Sets an integer value for the specified key.
   - `GetInt(string key, int defaultValue = 0)`: Gets the integer value associated with the key. If the key is not found, it checks PlayerPrefs for the value. If still not found, it returns the provided default value.
   - `SetFloat(string key, float value)`: Sets a float value for the specified key.
   - `GetFloat(string key, float defaultValue = 0f)`: Gets the float value associated with the key. If the key is not found, it checks PlayerPrefs for the value. If still not found, it returns the provided default value.
   - `SetString(string key, string value)`: Sets a string value for the specified key.
   - `GetString(string key, string defaultValue = "")`: Gets the string value associated with the key. If the key is not found, it checks PlayerPrefs for the value. If still not found, it returns the provided default value.

3. To delete all settings, use the `DeleteAll()` method. This will remove all PlayerPrefs data and delete the JSON file.
4. To delete a specific key, use the `DeleteKey(string key)` method. This will remove the key from PlayerPrefs and the JSON file.
5. Use the `HasKey(string key)` method to check if a key exists in either PlayerPrefs or the JSON file.

### Example

```csharp
// Set an integer value
SettingsManager.Instance.SetInt("score", 100);

// Get the integer value with a default value
int score = SettingsManager.Instance.GetInt("score", 0);

// Set a float value
SettingsManager.Instance.SetFloat("volume", 0.5f);

// Get the float value with a default value
float volume = SettingsManager.Instance.GetFloat("volume", 1f);

// Set a string value
SettingsManager.Instance.SetString("playerName", "John");

// Get the string value with a default value
string playerName = SettingsManager.Instance.GetString("playerName", "Unknown");

// Delete all settings
SettingsManager.Instance.DeleteAll();

// Delete a specific key
SettingsManager.Instance.DeleteKey("score");

// Check if a key exists
bool hasKey = SettingsManager.Instance.HasKey("score");
```

### Notes

- The settings are stored in a file named "settings.json" in the persistent data path of the application.
- The SettingsManager uses a singleton pattern to ensure there is only one instance in the scene.
- The script automatically loads the settings from the JSON file on startup and saves any changes when values are set.
- The JSON file is overwritten each time the settings are saved, so make sure to back up any important data before deleting or modifying the file.

---
Feel free to modify and extend the SettingsManager script according to your specific needs.
