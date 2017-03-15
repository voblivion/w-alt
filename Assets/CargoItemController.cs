using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CargoItemController : MonoBehaviour {
    public InventoryController inventory;
    public Item item;
    public int index;
    public GameObject tooltip;

    // Lifecycle
    void Start() {
        GetComponentInChildren<Text>().text = item.displayedName;
        Sprite sprite = ResourcesManager.get<Sprite>(item.spriteResourcePath);
        GetComponent<Image>().overrideSprite = sprite;
        tooltip.SetActive(false);
    }
    void OnMouseEnter() {
        tooltip.SetActive(true);
    }
    void OnMouseExit() {
        tooltip.SetActive(false);
    }

    // Methods
    public void select(bool value) {
        Debug.Log(value);
        inventory.selectCargoItem(this);
    }
}
