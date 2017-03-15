using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour {
    public GameController game;

    public void ToMenu() {
        SceneManager.LoadScene("Menu");
    }
    public void ToDesktop() {
        Application.Quit();
    }
    public void Continue() {
        // TODO
    }
}
