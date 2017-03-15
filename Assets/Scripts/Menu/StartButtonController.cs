using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartButtonController : MonoBehaviour {
    public MenuController menu;
    public InputField playerNameInput;
    public Text errorMessageText;

    private Button button;

    void Start() {
        button = GetComponent<Button>();
    }

    void OnGUI() {
        if(playerNameInput.text == "") {
            button.interactable = false;
            errorMessageText.text = "";
        }
        else if(DataManager.saveExists(playerNameInput.text)) {
            button.interactable = false;
            errorMessageText.text = "Une sauvegarde avec ce nom existe déjà.";
        }
        else {
            button.interactable = true;
            errorMessageText.text = "";
        }
    }

    public void New() {
        menu.game.New(playerNameInput.text);
        SceneManager.LoadScene("Game");
    }
}
