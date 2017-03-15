using UnityEngine;
using System.Collections;

public class UVCSensorController : ItemController {
    void Start() {
        item = new SensorItem("200nm", 0);
    }
}
