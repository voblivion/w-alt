using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class GroundChecker : MonoBehaviour {
    public bool debug;

    // Attributes
    private HashSet<Collider2D> triggers = new HashSet<Collider2D>();
    private Collider2D cd;

    public bool isTriggered {
        get { return triggers.Count > 0; }
    }

    public Collider2D Collider {
        get { return cd; }
    }

    public HashSet<Collider2D> Triggers {
        get { return triggers; }
    }

    void Start() {
        cd = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(!collider.isTrigger) {
            triggers.Add(collider);
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        triggers.Remove(collider);
    }

    void Update() {
        debug = isTriggered;
        HashSet<Collider2D> toRemove = new HashSet<Collider2D>();
        foreach(Collider2D trigger in triggers) {
            if(!trigger) {
                toRemove.Add(trigger);
            }
        }
        foreach(Collider2D trigger in toRemove) {
            triggers.Remove(trigger);
        }
    }
}
