using UnityEngine;
using System.IO;
using System.Collections;

public class GameController : SingletonController<GameController> {
    public string name = null;
    private EnvironmentController environmentController;
    private PlayerController playerController;

    // FIXME tmp
    public int remaining = 3;
    // FIXME end

    // Lifecycle
    void Start() {
        // Récupérer l'environnement et le joueur dnas la scène
        environmentController = GetComponentInChildren<EnvironmentController>();
        environmentController.gameObject.SetActive(false);
        playerController = GetComponentInChildren<PlayerController>();
        //playerController.gameObject.SetActive(false);
    }

    // Methods
    public void Save() {
        environmentController.Save();
        playerController.Save();        
    }
    public IEnumerator Load(string name) {
        this.name = name;

        // Activer l'environnement
        environmentController.gameObject.SetActive(true);

        // Charger et activer le joueur
        string playerData = File.ReadAllText(DataManager.baseSaveDirectory(name) + "/player.txt");
        yield return playerData;
        JsonUtility.FromJsonOverwrite(playerData, playerController);
        playerController.gameObject.SetActive(true);
    }
    public void New(string name) {
        StartCoroutine(NewGame(name));
    }
    public IEnumerator NewGame(string name) {
        // Créer la sauvegarde
        yield return DataManager.createSave(name);

        // Charger la sauvegarde
        yield return Load(name);
    }
    void OnGUI() {
        GUI.Label(new Rect(10, 10, 160, 20), "Noots restant à sauver : " + remaining);
    }
}
