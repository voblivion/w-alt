using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerNameController : MonoBehaviour {
    private InputField inputField;

    void Start() {
        inputField = GetComponent<InputField>();
        inputField.onValidateInput += delegate (string input, int charIndex, char addedChar) { return AuthorizeCharacter(addedChar); };
    }

    private char AuthorizeCharacter(char chr) {
        if((chr < 'a' || chr > 'z')
            && (chr < 'A' || chr > 'Z')
            && (chr < '0' || chr > '9')
            && chr != '_'
            && chr != '-'
            && chr != ' ') {
            chr = '\0';
        }
        return chr;
    }
}
