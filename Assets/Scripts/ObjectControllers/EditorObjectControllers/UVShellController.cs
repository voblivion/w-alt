using UnityEngine;
using System.Collections;

public class UVShellController : ItemController {
	void Start () {
        ActuatorItem.Layer[] layers = new ActuatorItem.Layer[1];
        layers[0] = ActuatorItem.Layer.UV;
        item = new ShellItem("UV Shell", layers);
    }
}
