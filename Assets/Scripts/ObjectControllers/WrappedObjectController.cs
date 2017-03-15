using UnityEngine;
using System;
using System.Collections;

public class ObjectControllerTypeUnknownException : Exception {
    public ObjectControllerTypeUnknownException() { }
    public ObjectControllerTypeUnknownException(string message) : base(message) { }
    public ObjectControllerTypeUnknownException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>
/// Wrap of an ObjectController for building recognizable json.
/// </summary>
public class WrappedObjectController {
    public enum Type {
        None = 0,
        Chunk = 1,
        Player = 2,
        Noot = 3,
        Village = 4,
        Item = 5,
        Tree = 6,
        DirtBlock = 7,
        RockBlock = 8,
        BedrockBlock = 9,
        BrickBlock = 10,
        WaterBlock = 11,
        GlassBlock = 12
    }

    public Type type;
    public string data;

    public WrappedObjectController(Type type, string data = "") {
        this.type = type;
        this.data = data;
    }

    public static string wrap(ObjectController objectController) {
        WrappedObjectController wrap;
        if(objectController == null) {
            wrap = new WrappedObjectController(WrappedObjectController.Type.None);
        }
        else {
            wrap = objectController.wrap();
        }
        return JsonUtility.ToJson(wrap);
    }
    public static ObjectController unwrap(string wrapString, Transform parent = null) {
        WrappedObjectController wrap = JsonUtility.FromJson<WrappedObjectController>(wrapString);
        return wrap.unwrap(parent);
    }

    public ObjectController unwrap(Transform parent = null) {
        string prefabPath;
        string controllerName;
        switch(type) {
        case Type.None:
            return null;
        case Type.Player:
            prefabPath = "Prefabs/Player";
            controllerName = "PlayerController";
            break;
        case Type.Chunk:
            prefabPath = "Prefabs/Chunk";
            controllerName = "ChunkController";
            break;
        case Type.DirtBlock:
            prefabPath = "Prefabs/DirtBlock";
            controllerName = "DirtBlockController";
            break;
        case Type.RockBlock:
            prefabPath = "Prefabs/RockBlock";
            controllerName = "RockBlockController";
            break;
        case Type.BedrockBlock:
            prefabPath = "Prefabs/BedrockBlock";
            controllerName = "BedrockBlockController";
            break;
        case Type.BrickBlock:
            prefabPath = "Prefabs/BrickBlock";
            controllerName = "BrickBlockController";
            break;
        case Type.WaterBlock:
            prefabPath = "Prefabs/WaterBlock";
            controllerName = "WaterBlockController";
            break;
        case Type.GlassBlock:
            prefabPath = "Prefabs/GlassBlock";
            controllerName = "GlassBlockController";
            break;
        case Type.Tree:
            prefabPath = "Prefabs/Tree";
            controllerName = "TreeController";
            break;
        case Type.Village:
            prefabPath = "Prefabs/Village";
            controllerName = "VillageController";
            break;
        case Type.Noot:
            prefabPath = "Prefabs/Noot";
            controllerName = "NootController";
            break;
        case Type.Item:
            prefabPath = "Prefabs/Item";
            controllerName = "ItemController";
            break;
        default:
            throw new ObjectControllerTypeUnknownException();
        }
        GameObject gameObject = UnityEngine.Object.Instantiate(ResourcesManager.get(prefabPath), parent) as GameObject;
        ObjectController gameObjectController = gameObject.GetComponent(controllerName) as ObjectController;
        Debug.Log(data);
        JsonUtility.FromJsonOverwrite(data, gameObjectController);
        gameObjectController.onAfterUnwrap();
        return gameObjectController;
    }
}
