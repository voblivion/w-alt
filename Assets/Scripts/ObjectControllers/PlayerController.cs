using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A RelativeObjectController for player.
/// </summary>
public class PlayerController : RelativeObjectController {
    [NonSerialized]
    public bool inventoryChanged = true;
    [SerializeField]
    private string[] w_inventory;
    [NonSerialized]
    public List<Item> inventory = new List<Item>();
    [SerializeField]
    private string w_shell = "{\"type\":0,\"data\":\"\"}";
    [NonSerialized]
    public ShellItem shell;
    [SerializeField]
    private string w_drill = "{\"type\":0,\"data\":\"\"}";
    [NonSerialized]
    public DrillItem drill;
    [SerializeField]
    private string w_hand = "{\"type\":0,\"data\":\"\"}";
    [NonSerialized]
    public BlockItem hand;
    [SerializeField]
    private string w_redSensor = "{\"type\":0,\"data\":\"\"}";
    [NonSerialized]
    public SensorItem redSensor;
    [SerializeField]
    private string w_greenSensor = "{\"type\":0,\"data\":\"\"}";
    [NonSerialized]
    public SensorItem greenSensor;
    [SerializeField]
    private string w_blueSensor = "{\"type\":0,\"data\":\"\"}";
    [NonSerialized]
    public SensorItem blueSensor;

    private GameController gameController;

    // Lifecycle
    void Start() {
        gameController = GetComponentInParent<GameController>();
        if(inventory.Count == 0) {
            redSensor = new SensorItem("650nm", 3);
            greenSensor = new SensorItem("550nm", 4);
            blueSensor = new SensorItem("450nm", 5);
            inventoryChanged = true;
        }
    }

    // Methods
    public void Save() {

    }
    public void setShell(ShellItem shell) {
        this.shell = shell;
        foreach(ActuatorItem.Layer layer in Enum.GetValues(typeof(ActuatorItem.Layer))) {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(layer.ToString()), LayerMask.NameToLayer("Player"));
        }
        foreach(ActuatorItem.Layer layer in shell.layers) {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(layer.ToString()), LayerMask.NameToLayer("Player"), false);
        }
    }
    public void unsetShell() {
        this.shell = null;
        foreach(ActuatorItem.Layer layer in Enum.GetValues(typeof(ActuatorItem.Layer))) {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(layer.ToString()), LayerMask.NameToLayer("Player"));
        }
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("RGB"), LayerMask.NameToLayer("Player"), false);
    }

    // Wrapping
    public override void onBeforeWrap() {
        base.onBeforeWrap();
        w_inventory = new string[inventory.Count];
        for(int k = 0; k < inventory.Count; ++k) {
            w_inventory[k] = WrappedItem.wrap(inventory[k]);
        }
        w_shell = WrappedItem.wrap(shell);
        w_drill = WrappedItem.wrap(drill);
        w_hand = WrappedItem.wrap(hand);
        w_redSensor = WrappedItem.wrap(redSensor);
        w_greenSensor = WrappedItem.wrap(greenSensor);
        w_blueSensor = WrappedItem.wrap(blueSensor);
    }
    public override void onAfterUnwrap() {
        base.onAfterUnwrap();
        // Unwrap inventory
        inventory = new List<Item>(w_inventory.Length);
        foreach(string wrappedItem in w_inventory) {
            inventory.Add(JsonUtility.FromJson<WrappedItem>(wrappedItem).unwrap());
        }
        // Unwrap shell
        if(w_shell != null) {
            shell = WrappedItem.unwrap(w_shell) as ShellItem;
        }
        // Unwrap drill
        if(w_drill != null) {
            drill = WrappedItem.unwrap(w_drill) as DrillItem;
        }
        // Unwrap hand
        if(w_hand != null) {
            hand = WrappedItem.unwrap(w_hand) as BlockItem;
        }
        // Unwrap sensors
        if(w_redSensor != null) {
            redSensor = WrappedItem.unwrap(w_redSensor) as SensorItem;
        }
        if(w_redSensor != null) {
            greenSensor = WrappedItem.unwrap(w_greenSensor) as SensorItem;
        }
        if(w_redSensor != null) {
            blueSensor = WrappedItem.unwrap(w_blueSensor) as SensorItem;
        }
    }
    public override WrappedObjectController wrap() {
        onBeforeWrap();
        return new WrappedObjectController(WrappedObjectController.Type.Player, JsonUtility.ToJson(this));
    }
}
