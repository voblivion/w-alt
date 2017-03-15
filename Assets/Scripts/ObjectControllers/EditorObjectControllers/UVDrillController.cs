using UnityEngine;
using System.Collections;

public class UVDrillController : ItemController {
    void Start() {
        ActuatorItem.Layer[] layers = new ActuatorItem.Layer[1];
        layers[0] = ActuatorItem.Layer.UV;
        item = new DrillItem("UV Drill", layers, 1f);
    }
}
