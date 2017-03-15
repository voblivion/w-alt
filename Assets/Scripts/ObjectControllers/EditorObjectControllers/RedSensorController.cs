using UnityEngine;
using System.Collections;

public class RedSensorController : ItemController {
    void Start() {
        item = new SensorItem("650nm", 3);
    }
}
