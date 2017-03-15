using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ItemTypeUnknownException : Exception {
    public ItemTypeUnknownException() { }
    public ItemTypeUnknownException(string message) : base(message) { }
    public ItemTypeUnknownException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>
/// Wrap of an Item for building recognizable json.
/// </summary>
[Serializable]
public class WrappedItem {
    public enum Type {
        None = 0,
        Shell = 1,
        Drill = 2,
        Sensor = 3,
        IntervalSensor = 4,
        DirtBlock = 7,
        RockBlock = 8,
        BedrockBlock = 9,
        BrickBlock = 10,
        WaterBlock = 11,
        GlassBlock = 12,
    }

    public Type type;
    public string data;

    public WrappedItem(Type type, string data = "") {
        this.type = type;
        this.data = data;
    }

    public static string wrap(Item item) {
        WrappedItem wrap;
        if(item == null) {
            wrap = new WrappedItem(WrappedItem.Type.None);
        }
        else {
            wrap = item.wrap();
        }
        return JsonUtility.ToJson(wrap);
    }
    public static Item unwrap(string wrapString) {
        WrappedItem wrap = JsonUtility.FromJson<WrappedItem>(wrapString);
        return wrap.unwrap();
    }
    public Item unwrap() {
        switch(type) {
        case Type.None:
            return null;
        case Type.DirtBlock:
            return JsonUtility.FromJson<DirtBlockItem>(data);
        case Type.RockBlock:
            return JsonUtility.FromJson<RockBlockItem>(data);
        case Type.Shell:
            return JsonUtility.FromJson<ShellItem>(data);
        case Type.Drill:
            return JsonUtility.FromJson<DrillItem>(data);
        case Type.Sensor:
            return JsonUtility.FromJson<SensorItem>(data);
        case Type.IntervalSensor:
            return JsonUtility.FromJson<IntervalSensorItem>(data);
        default:
            throw new ItemTypeUnknownException(type.ToString());
        }
    }
}

/// <summary>
/// An object the player can hold in his inventory.
/// </summary>
[Serializable]
public abstract class Item {
    public abstract WrappedItem wrap();
    public abstract string name { get; }
    public virtual string displayedName {
        get { return name; }
    }
    public abstract string spriteResourcePath { get; }
}

/// <summary>
/// An Item that can be stacked if many of them are hold.
/// </summary>
[Serializable]
public abstract class StackableItem : Item {
    public uint m_count = 1;

    public StackableItem(uint count) {
        m_count = count;
    }

    public uint count {
        get { return m_count; }
        set { m_count = value; }
    }
    public abstract WrappedItem.Type type { get; }
    public override string displayedName {
        get {
            return name + " (" + count + ")";
        }
    }
}

/// <summary>
/// A StackableItem that can be placed in the environment.
/// </summary>
[Serializable]
public abstract class BlockItem : StackableItem {
    public BlockItem(uint count) : base(count) { }

    public abstract GameObject place();
}

/// <summary>
/// A BlockItem for Dirt.
/// </summary>
[Serializable]
public class DirtBlockItem : BlockItem {
    public DirtBlockItem(uint count) : base(count) { }

    public override WrappedItem wrap() {
        return new WrappedItem(WrappedItem.Type.DirtBlock, JsonUtility.ToJson(this));
    }
    public override string name {
        get { return "Dirt Block"; }
    }
    public override string spriteResourcePath {
        get { return "Sprites/Items/dirt"; }
    }
    public override WrappedItem.Type type {
        get { return WrappedItem.Type.DirtBlock; }
    }
    public override GameObject place() {
        throw new NotImplementedException();
    }
}

/// <summary>
/// A BlockItem for Rock.
/// </summary>
[Serializable]
public class RockBlockItem : BlockItem {
    public RockBlockItem(uint count) : base(count) { }

    public override WrappedItem wrap() {
        return new WrappedItem(WrappedItem.Type.RockBlock, JsonUtility.ToJson(this));
    }
    public override string name {
        get { return "Rock Block"; }
    }
    public override string spriteResourcePath {
        get { return "Sprites/Items/rock"; }
    }
    public override WrappedItem.Type type {
        get { return WrappedItem.Type.RockBlock; }
    }
    public override GameObject place() {
        throw new NotImplementedException();
    }
}

/// <summary>
/// A BlockItem for Dirt.
/// </summary>
[Serializable]
public class BedrockBlockItem : BlockItem {
    public BedrockBlockItem(uint count) : base(count) { }

    public override WrappedItem wrap() {
        return new WrappedItem(WrappedItem.Type.BedrockBlock, JsonUtility.ToJson(this));
    }
    public override string name {
        get { return "Bedrock Block"; }
    }
    public override string spriteResourcePath {
        get { return "Sprites/Items/bderock"; }
    }
    public override WrappedItem.Type type {
        get { return WrappedItem.Type.BedrockBlock; }
    }
    public override GameObject place() {
        throw new NotImplementedException();
    }
}

/// <summary>
/// A BlockItem for Dirt.
/// </summary>
[Serializable]
public class BrickBlockItem : BlockItem {
    public BrickBlockItem(uint count) : base(count) { }

    public override WrappedItem wrap() {
        return new WrappedItem(WrappedItem.Type.BrickBlock, JsonUtility.ToJson(this));
    }
    public override string name {
        get { return "Brick Block"; }
    }
    public override string spriteResourcePath {
        get { return "Sprites/Items/brick"; }
    }
    public override WrappedItem.Type type {
        get { return WrappedItem.Type.BrickBlock; }
    }
    public override GameObject place() {
        throw new NotImplementedException();
    }
}

/// <summary>
/// A BlockItem for Dirt.
/// </summary>
[Serializable]
public class WaterBlockItem : BlockItem {
    public WaterBlockItem(uint count) : base(count) { }

    public override WrappedItem wrap() {
        return new WrappedItem(WrappedItem.Type.WaterBlock, JsonUtility.ToJson(this));
    }
    public override string name {
        get { return "Water Block"; }
    }
    public override string spriteResourcePath {
        get { return "Sprites/Items/water"; }
    }
    public override WrappedItem.Type type {
        get { return WrappedItem.Type.WaterBlock; }
    }
    public override GameObject place() {
        throw new NotImplementedException();
    }
}

/// <summary>
/// A BlockItem for Rock.
/// </summary>
[Serializable]
public class GlassBlockItem : BlockItem {
    public GlassBlockItem(uint count) : base(count) { }

    public override WrappedItem wrap() {
        return new WrappedItem(WrappedItem.Type.GlassBlock, JsonUtility.ToJson(this));
    }
    public override string name {
        get { return "Glass Block"; }
    }
    public override string spriteResourcePath {
        get { return "Sprites/Items/glass"; }
    }
    public override WrappedItem.Type type {
        get { return WrappedItem.Type.GlassBlock; }
    }
    public override GameObject place() {
        throw new NotImplementedException();
    }
}

/// <summary>
/// An Item that canno't be stacked and therefor has a specific name.
/// </summary>
[Serializable]
public abstract class UniqueItem : Item {
    public string m_name;
    public string m_spriteResourcePath;

    public UniqueItem(string name, string spriteResourcePath) {
        m_name = name;
        m_spriteResourcePath = spriteResourcePath;
    }

    public override string name {
        get { return m_name; }
    }
    public override string spriteResourcePath {
        get { return m_spriteResourcePath; }
    }
}

/// <summary>
/// An UniqueItem that can be equiped to interract with the environment.
/// </summary>
[Serializable]
public abstract class ActuatorItem : UniqueItem {
    public enum Layer {
        UV,
        RGB,
        IR
    }

    public Layer[] m_layers;

    public ActuatorItem(string name, string spriteResourcePath, Layer[] layers) : base(name, spriteResourcePath) {
        m_layers = layers;
    }

    public Layer[] layers {
        get { return m_layers; }
    }
}

/// <summary>
/// An ActuatorItem that can be used to collider with parts of the environment.
/// </summary>
[Serializable]
public class ShellItem : ActuatorItem {
    public ShellItem(string name, Layer[] layers) : base(name, "Sprites/Items/shell", layers) { }

    public override WrappedItem wrap() {
        return new WrappedItem(WrappedItem.Type.Shell, JsonUtility.ToJson(this));
    }
}

/// <summary>
/// An ActuatorItem that can be used to drill blocks from the environment.
/// </summary>
[Serializable]
public class DrillItem : ActuatorItem {
    public float m_efficiency;

    public DrillItem(string name, Layer[] layers, float efficiency) : base(name, "Sprites/Items/drill", layers) {
        m_efficiency = efficiency;
    }


    public override WrappedItem wrap() {
        return new WrappedItem(WrappedItem.Type.Drill, JsonUtility.ToJson(this));
    }

    public float efficiency {
        get { return m_efficiency; }
    }
}

/// <summary>
/// An UniqueItem that can be equiped to analyze the environment at certain frequency.
/// </summary>
[Serializable]
public class SensorItem : UniqueItem {
    public float m_frequency;

    public SensorItem(string name, float frequency): base(name, "Sprites/Items/sensor") {
        m_frequency = frequency;
    }
    public SensorItem(float frequency) : base(frequency + "nm", "Sprites/Items/sensor") {
        m_frequency = frequency;
    }


    public override WrappedItem wrap() {
        return new WrappedItem(WrappedItem.Type.Sensor, JsonUtility.ToJson(this));
    }

    public virtual float frequency {
        get { return m_frequency; }
        set { m_frequency = frequency; }
    }
}

/// <summary>
/// A Sensor whom frequency may be chosen according to an interval.
/// </summary>
[Serializable]
public class IntervalSensorItem : SensorItem {
    public float m_fmin;
    public float m_fmax;

    public IntervalSensorItem(string name, float frequency, float fmin, float fmax):
        base(name, frequency) {

    }
    public IntervalSensorItem(float frequency, float fmin, float fmax) :
        base(fmin + "nm-" + fmax + "nm", frequency) {
        m_fmin = fmin;
        m_fmax = fmax;
        // Ensure frequency belongs to [fmin, fmax]
        if(m_frequency < fmin) m_frequency = fmin;
        if(m_frequency > fmax) m_frequency = fmax;
    }


    public override WrappedItem wrap() {
        return new WrappedItem(WrappedItem.Type.IntervalSensor, JsonUtility.ToJson(this));
    }

    public float fmin {
        get { return m_fmin; }
    }
    public float fmax {
        get { return m_fmax; }
    }
    public override float frequency {
        get { return base.frequency; }
        set {
            value = Mathf.Max(m_fmin, Mathf.Min(value, m_fmax));
            base.frequency = value;
        }
    }
}

