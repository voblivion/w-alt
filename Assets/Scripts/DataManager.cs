using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class DataManager {
    /// <summary>
    /// Create a new save with given name.
    /// </summary>
    public static IEnumerator createSave(string name) {
        // La sauvegarde ne doit pas exister
        if(DataManager.saveExists(name)) {
            throw new Exception("A save with this name already exists.");
        }

        // Créer le dossier de sauvegarde
        string saveDirectory = baseSaveDirectory(name);
        Directory.CreateDirectory(saveDirectory);
        // Créer le joueur
        WWW playerData = new WWW("file:///" + defaultSaveDirectory() + "/player.txt");
        yield return playerData;
        Debug.Log(playerData.text);
        
        string playerFile = saveDirectory + "/player.txt";
        File.WriteAllText(playerFile, playerData.text);
    }
    /// <summary>
    /// Check wether or not a save with given name exists.
    /// </summary>
    public static bool saveExists(string name) {
        return Directory.Exists(baseSaveDirectory(name));
    }
    /// <summary>
    /// List all saves.
    /// </summary>
    /// <returns></returns>
    public static string[] listSaves() {
        DirectoryInfo savesDirectory = new DirectoryInfo(Application.persistentDataPath + "/saves/");
        DirectoryInfo[] savesDirectories = savesDirectory.GetDirectories();
        string[] saves = new string[savesDirectories.Length];
        for(int k = 0; k < saves.Length; ++k) {
            saves[k] = savesDirectories[k].Name;
        }
        return saves;
    }
    /// <summary>
    /// Computes base save directory for given save name.
    /// </summary>
    public static string baseSaveDirectory(string name) {
        return Application.persistentDataPath + "/saves/" + name;
    }
    /// <summary>
    /// Returns location path of default save directory.
    /// </summary>
    public static string defaultSaveDirectory() {
        return Application.streamingAssetsPath + "/saves/default";
    }
}
