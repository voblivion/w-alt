using UnityEngine;
using System.Collections;

public class IRDrillController : ItemController {
    void Start() {
        ActuatorItem.Layer[] layers = new ActuatorItem.Layer[1];
        layers[0] = ActuatorItem.Layer.IR;
        item = new DrillItem("IR Drill", layers, 1f);
    }
}
