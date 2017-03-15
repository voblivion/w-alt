using UnityEngine;
using System.Collections;

public class IRBSensorController : ItemController {
    void Start() {
        item = new SensorItem("2000nm", 7);
    }
}
