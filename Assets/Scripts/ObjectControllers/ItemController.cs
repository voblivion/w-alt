using UnityEngine;
using System;
using System.Collections;

/* TODO ... Abstract ? */

/// <summary>
/// A RelativeObjectController for floating items.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ItemController : RelativeObjectController {
    [NonSerialized]
    public Item item;
    [SerializeField]
    private string w_item;

    // Serialization
    public override void onBeforeWrap() {
        base.onBeforeWrap();
        w_item = WrappedItem.wrap(item);
    }
    public override void onAfterUnwrap() {
        base.onAfterUnwrap();
        item = WrappedItem.unwrap(w_item);
    }
    public override WrappedObjectController wrap() {
        onBeforeWrap();
        return new WrappedObjectController(WrappedObjectController.Type.Item, JsonUtility.ToJson(this));
    }

    // Lifecycle
    void Start() {}
    void OnTriggerEnter2D(Collider2D other) {
        PlayerController player = other.GetComponent<PlayerController>() as PlayerController;
        if(player != null) {
            player.inventory.Add(item);
            player.inventoryChanged = true;
            Destroy(this.gameObject);
        }
    }
}
