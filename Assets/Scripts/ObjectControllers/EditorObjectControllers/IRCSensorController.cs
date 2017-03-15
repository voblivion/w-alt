using UnityEngine;
using System.Collections;

public class IRCSensorController : ItemController {
    void Start() {
        item = new SensorItem("5000nm", 8);
    }
}
