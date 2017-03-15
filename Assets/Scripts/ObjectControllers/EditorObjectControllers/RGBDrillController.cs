using UnityEngine;
using System.Collections;

public class RGBDrillController : ItemController {
    void Start() {
        ActuatorItem.Layer[] layers = new ActuatorItem.Layer[1];
        layers[0] = ActuatorItem.Layer.RGB;
        item = new DrillItem("RGB Drill", layers, 1f);
    }
}
