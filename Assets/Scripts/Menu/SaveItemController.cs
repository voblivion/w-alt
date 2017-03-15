using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveItemController : MonoBehaviour {
    private Text buttonText;
    public Toggle toggle;
    public SaveSelectController saveSelect;

    void Awake() {
        buttonText = GetComponentInChildren<Text>();
        toggle = GetComponent<Toggle>();
    }

    public void setName(string name) {
        this.name = name;
        buttonText.text = name;
    }

    public void Select() {
        saveSelect.Select(this);
    }
}
