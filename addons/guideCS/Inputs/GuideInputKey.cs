using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Key Input types. Must be assigned a GUIDEInputKey to function.</summary>
public partial class GuideInputKey : GuideInput
{
    public GuideInputKey(GodotObject gdInput) : base(gdInput) { }

    public GuideInputKey()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputKeyGdPath); }
    
    /// <summary>The physical keycode of the key.</summary>
    public Key Key
    {
        get
        {
            var i = BaseGuideObject.Get("key").AsInt64();
            if (Enum.IsDefined(typeof(Key), i))
            { return (Key)i; }

            return Key.Unknown;
        }

        set => BaseGuideObject.Set("key", (long)value);
    }
    
    /// <summary>Whether 'shift' must be pressed.</summary>
    public bool Shift
    {
        get => BaseGuideObject.Get("shift").AsBool(); 
        set => BaseGuideObject.Set("shift", value);
    }
    
    /// <summary>Whether 'control' must be pressed.</summary>
    public bool Control
    {
        get => BaseGuideObject.Get("control").AsBool(); 
        set => BaseGuideObject.Set("control", value);
    }
    
    /// <summary>Whether 'alt' must be pressed.</summary>
    public bool Alt
    {
        get => BaseGuideObject.Get("alt").AsBool(); 
        set => BaseGuideObject.Set("alt", value);
    }

    /// <summary>Whether 'meta/win/cmd' must be pressed.</summary>
    public bool Meta
    {
        get => BaseGuideObject.Get("meta").AsBool(); 
        set => BaseGuideObject.Set("meta", value);
    }
    
    /// <summary>Whether this input should fire if additional modifier keys are currently pressed.</summary>
    public bool AllowAdditionalModifiers
    {
        get => BaseGuideObject.Get("allow_additional_modifiers").AsBool(); 
        set => BaseGuideObject.Set("allow_additional_modifiers", value);
    }
}