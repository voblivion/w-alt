using UnityEngine;
using System.Collections;

public class BlueSensorController : ItemController {
    void Start() {
        item = new SensorItem("450nm", 5);
    }
}
