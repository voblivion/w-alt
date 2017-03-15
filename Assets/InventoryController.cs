using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryController : MonoBehaviour {
    public PlayerController player;
    public GameObject wrap;
    public Text indication;
    public string defaultIndicationText;
    public EquipmentController drill;
    public EquipmentController shell;
    public EquipmentController hand;
    public EquipmentController redSensor;
    public EquipmentController greenSensor;
    public EquipmentController blueSensor;
    public CargoController cargo;

    private CargoItemController selectedCargoItem;

    void Start() {
        wrap.SetActive(false);
    }

    void Update() {
        if(Input.GetButtonDown("Inventory")) {
            wrap.SetActive(!wrap.activeSelf);
        }
        if(wrap.activeSelf) {
            if(player.inventoryChanged) {
                drill.setEquipment(player.drill);
                shell.setEquipment(player.shell);
                hand.setEquipment(player.hand);
                redSensor.setEquipment(player.redSensor);
                greenSensor.setEquipment(player.greenSensor);
                blueSensor.setEquipment(player.blueSensor);
                cargo.setItems(player.inventory);
                player.inventoryChanged = false;
            }
        }
    }

    // Methods
    public void selectEquipment(EquipmentController equipment) {
        // Put object in inventory
        if(equipment == drill && player.drill != null) {
            player.inventory.Add(player.drill);
            player.drill = null;
        }
        else if(equipment == shell && player.shell != null) {
            player.inventory.Add(player.shell);
            player.unsetShell();
        }
        else if(equipment == hand && player.hand != null) {
            player.inventory.Add(player.hand);
            player.hand = null;
        }
        else if(equipment == redSensor && player.redSensor != null) {
            player.inventory.Add(player.redSensor);
            player.redSensor = null;
        }
        else if(equipment == greenSensor && player.greenSensor != null) {
            player.inventory.Add(player.greenSensor);
            player.greenSensor = null;
        }
        else if(equipment == blueSensor && player.blueSensor != null) {
            player.inventory.Add(player.blueSensor);
            player.blueSensor = null;
        }
        // Unset equipment in GUI
        equipment.setEquipment(null);
        // Mark inventory to "changed"
        player.inventoryChanged = true;

        if(selectedCargoItem != null) {
            // Move equipment from player inventory to its new place
            if(equipment == drill && selectedCargoItem.item is DrillItem) {
                player.inventory.RemoveAt(selectedCargoItem.index);
                player.drill = selectedCargoItem.item as DrillItem;
            }
            else if(equipment == shell && selectedCargoItem.item is ShellItem) {
                player.inventory.RemoveAt(selectedCargoItem.index);
                player.setShell(selectedCargoItem.item as ShellItem);
            }
            else if(equipment == hand && selectedCargoItem.item is BlockItem) {
                player.inventory.RemoveAt(selectedCargoItem.index);
                player.hand = selectedCargoItem.item as BlockItem;
            }
            else if(equipment == redSensor && selectedCargoItem.item is SensorItem) {
                player.inventory.RemoveAt(selectedCargoItem.index);
                player.redSensor = selectedCargoItem.item as SensorItem;
            }
            else if(equipment == greenSensor && selectedCargoItem.item is SensorItem) {
                player.inventory.RemoveAt(selectedCargoItem.index);
                player.greenSensor = selectedCargoItem.item as SensorItem;
            }
            else if(equipment == blueSensor && selectedCargoItem.item is SensorItem) {
                player.inventory.RemoveAt(selectedCargoItem.index);
                player.blueSensor = selectedCargoItem.item as SensorItem;
            }
            else {
                return;
            }
            // Set equipment in GUI
            equipment.setEquipment(selectedCargoItem.item);
            // Unselect item
            selectedCargoItem = null;
            // Mark inventory to "changed"
            player.inventoryChanged = true;
        }
    }
    public void selectCargoItem(CargoItemController cargoItem) {
        if(selectedCargoItem == null) {
            // Select item
            selectedCargoItem = cargoItem;
            // Update color of selected item
            ColorBlock colors = selectedCargoItem.GetComponent<Toggle>().colors;
            colors.normalColor = new Color32(255, 0, 255, 255);
            selectedCargoItem.GetComponent<Toggle>().colors = colors;
            // Set indication
            indication.text = "Choisir le nouvel emplacement de l'objet sélectionné.";
        }
        else {
            // Swap elements
            Item tmp = player.inventory[selectedCargoItem.index];
            player.inventory[selectedCargoItem.index] = player.inventory[cargoItem.index];
            player.inventory[cargoItem.index] = tmp;
            // Mark inventory to "changed"
            player.inventoryChanged = true;
            // Update color of selected item
            ColorBlock colors = selectedCargoItem.GetComponent<Toggle>().colors;
            colors.normalColor = new Color32(255, 255, 255, 255);
            selectedCargoItem.GetComponent<Toggle>().colors = colors;
            // Unselect item
            selectedCargoItem = null;
            // Set indication
            indication.text = "Sélectionner un objet pour interragir avec.";
        }
    }
}
