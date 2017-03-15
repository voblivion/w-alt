using UnityEngine;
using System.Collections;

public class UVBSensorController : ItemController {
    void Start() {
        item = new SensorItem("300nm", 1);
    }
}
