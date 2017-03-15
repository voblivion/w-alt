using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveSelectController : MonoBehaviour {
    public SaveItemController saveItem;

    public SaveItemController selectedSaveItem = null;

    void Start() {
        string[] saves = DataManager.listSaves();
        Debug.Log(saves.Length);
        foreach(string save in saves) {
            SaveItemController newSaveItem = Instantiate<SaveItemController>(saveItem);
            newSaveItem.setName(save);
            newSaveItem.saveSelect = this;
            newSaveItem.transform.SetParent(transform, false);
        }

        RectTransform rt = GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = 64 * saves.Length + 4 * (saves.Length - 1);
        rt.sizeDelta = size;
    }

    public void Select(SaveItemController saveItem) {
        SaveItemController old = selectedSaveItem;
        selectedSaveItem = saveItem;
        if(old != null) {
            old.toggle.isOn = false;
        }
    }
}
