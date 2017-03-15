using UnityEngine;
using System.Collections;

public class IRASensorController : ItemController {
    void Start() {
        item = new SensorItem("1000nm", 6);
    }
}
