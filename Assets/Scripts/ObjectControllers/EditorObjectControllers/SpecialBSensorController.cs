using UnityEngine;
using System.Collections;

public class SpecialBSensorController : ItemController {
    void Start() {
        item = new SensorItem("!", 10);
    }
}
