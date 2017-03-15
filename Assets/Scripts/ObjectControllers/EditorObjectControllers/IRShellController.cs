using UnityEngine;
using System.Collections;

public class IRShellController : ItemController {
    void Start() {
        ActuatorItem.Layer[] layers = new ActuatorItem.Layer[1];
        layers[0] = ActuatorItem.Layer.IR;
        item = new ShellItem("IR Shell", layers);
    }
}
