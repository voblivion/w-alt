using UnityEngine;
using System.Collections;

public class EnvironmentController : MonoBehaviour {
    public static int minChunkID = 0;
    public static int maxChunkID = 0;
    public static int loadDist = 1;
    public static int unloadDist = 1;

    private GameController gameController;

    // Lifecycle
    void Start() {
        gameController = GetComponentInParent<GameController>();
    }
    void OnEnable() {
        // Charger les chunks
    }

    // Methods
    public void Save() {

    }
}
