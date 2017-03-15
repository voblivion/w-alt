using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourcesManager {
    private static Dictionary<string, Object> loaded = new Dictionary<string, Object>();
    public static Object get(string resource) {
        if(!loaded.ContainsKey(resource)) {
            loaded[resource] = Resources.Load(resource);
        }
        return loaded[resource];
    }
    public static T get<T>(string resource) where T : Object {
        if(!loaded.ContainsKey(resource)) {
            T res = Resources.Load<T>(resource);
            loaded[resource] = res;
            return res;
        }
        return loaded[resource] as T;
    }
}
