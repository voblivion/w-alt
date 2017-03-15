using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// A Monobehaviour that can be loaded/saved.
/// </summary>
public abstract class ObjectController : MonoBehaviour {
    public virtual void onBeforeWrap() { }
    public virtual void onAfterUnwrap() { }
    public abstract WrappedObjectController wrap(); 
}
