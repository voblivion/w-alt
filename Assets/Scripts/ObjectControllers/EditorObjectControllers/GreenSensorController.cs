using UnityEngine;
using System.Collections;

public class GreenSensorController : ItemController {
    void Start() {
        item = new SensorItem("550nm", 4);
    }
}
