using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToContinueButtonController : MonoBehaviour {
    private Button button;

    void Start() {
        button = GetComponent<Button>();
        OnGUI();
    }

    void OnGUI() {
        string[] saves = DataManager.listSaves();
        if(saves.Length > 0) {
            button.interactable = true;
        }
        else {
            button.interactable = false;
        }
    }
}
