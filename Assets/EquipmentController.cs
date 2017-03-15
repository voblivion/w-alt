using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentController : MonoBehaviour {
    public InventoryController inventory;
    public Image image;
    public Text text;
    public GameObject tooltip;
    public Item item;

    // Lifecycle
    void Start() {
        tooltip.SetActive(false);
    }
    void OnMouseEnter() {
        tooltip.SetActive(true && item != null);
    }
    void OnMouseExit() {
        tooltip.SetActive(false);
    }

    // Methods
    public void setEquipment(Item item) {
        this.item = item;

        if(item == null) {
            setName("");
            setSprite(null);
        }
        else {
            setName(item.displayedName);
            setSprite(ResourcesManager.get<Sprite>(item.spriteResourcePath));
        }
    }
    public void select() {
        inventory.selectEquipment(this);
    }

    private void setName(string name) {
        text.text = name;
    }
    private void setSprite(Sprite sprite) {
        image.overrideSprite = sprite;
    }
    
}
