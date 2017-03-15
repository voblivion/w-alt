using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CargoController : MonoBehaviour {
    public InventoryController inventory;
    public CargoItemController cargoItem;

    // Methods
    public void setItems(List<Item> items) {
        foreach(CargoItemController cargoItem in GetComponentsInChildren<CargoItemController>()) {
            Destroy(cargoItem.gameObject);
        }
        for(int k = 0; k < items.Count; ++k) {
            Item item = items[k];
            CargoItemController newCargoItem = Instantiate<CargoItemController>(cargoItem);
            newCargoItem.inventory = inventory;
            newCargoItem.item = item;
            newCargoItem.index = k;
            newCargoItem.transform.SetParent(transform, false);
        }
    }
}
