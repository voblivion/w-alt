using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContinueButtonController : MonoBehaviour {
    public SaveSelectController saveSelect;
    private Button button;

    void Start() {
        button = GetComponent<Button>();
        OnGUI();
    }

    void OnGUI() {
        if(saveSelect.selectedSaveItem != null) {
            button.interactable = true;
        }
        else {
            button.interactable = false;
        }
    }
}
