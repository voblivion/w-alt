using UnityEngine;
using System.Collections;

public class SingletonController<Type> : MonoBehaviour {
    public static SingletonController<Type> instance;
    public bool isPersistant;

    public virtual void Awake() {
        if(isPersistant) {
            if(!instance) {
                instance = this as SingletonController<Type>;
            }
            else {
                DestroyObject(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        else {
            instance = this as SingletonController<Type>;
        }
    }
}