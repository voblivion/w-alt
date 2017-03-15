using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class VisionController : MonoBehaviour {
    private MultiSpriteManager msm;
    private PlayerController player;
    public float defaultFrequency = -1;

    float lastR;
    float lastG;
    float lastB;

    // Lifecycle
    void Start() {
        msm = MultiSpriteManager.Instance;
        msm.UpdateSprites(defaultFrequency, defaultFrequency, defaultFrequency);
        player = GetComponent<PlayerController>();
    }
    void Update() {
        float r = player.redSensor != null ? player.redSensor.frequency : defaultFrequency;
        float g = player.greenSensor != null ? player.greenSensor.frequency : defaultFrequency;
        float b = player.blueSensor != null ? player.blueSensor.frequency : defaultFrequency;
        if(lastR != r || lastG != g || lastB != b) {
            lastR = r;
            lastG = g;
            lastB = b;
            Debug.Log(r + "-" + g + "-" + b);
            msm.UpdateSprites(r, g, b);
        }
    }
}
