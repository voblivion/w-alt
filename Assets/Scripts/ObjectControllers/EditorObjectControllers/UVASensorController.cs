using UnityEngine;
using System.Collections;

public class UVASensorController : ItemController {
    void Start() {
        item = new SensorItem("350nm", 2);
    }
}
