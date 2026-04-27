using Godot;

namespace GuideCs;

/// <summary>Wrapper for Input 'Any' types. Must be assigned a GUIDEInputAny to function.</summary>
public partial class GuideInputAny : GuideInput
{
    public GuideInputAny(GodotObject gdInput) : base(gdInput) { }

    public GuideInputAny()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputAnyGdPath); }
    
    // Did not include deprecated content.

    /// <summary>Should input from mouse buttons be considered?</summary>
    public bool MouseButtons
    {
        get => BaseGuideObject.Get("mouse_buttons").AsBool();
        set => BaseGuideObject.Set("mouse_buttons", value);
    }
    
    /// <summary>Should input from mouse movement be considered?</summary>
    public bool MouseMovement
    {
        get => BaseGuideObject.Get("mouse_movement").AsBool();
        set => BaseGuideObject.Set("mouse_movement", value);
    }
    
    /// <summary>Should input from mouse movement be considered?</summary>
    public float MinimumMouseMovementDistance
    {
        get => BaseGuideObject.Get("minimum_mouse_movement_distance").AsSingle();
        set => BaseGuideObject.Set("minimum_mouse_movement_distance", value);
    }
    
    /// <summary>Should input from gamepad/joystick buttons be considered?</summary>
    public bool JoyButtons
    {
        get => BaseGuideObject.Get("joy_buttons").AsBool();
        set => BaseGuideObject.Set("joy_buttons", value);
    }
    
    /// <summary>Should input from gamepad/joystick axis be considered?</summary>
    public bool JoyAxes
    {
        get => BaseGuideObject.Get("joy_axes").AsBool();
        set => BaseGuideObject.Set("joy_axes", value);
    }
    
    /// <summary>Minimum strength of a single joy axis actuation before it is considered as actuated.</summary>
    public float MinimumJoyAxisActuationStrength
    {
        get => BaseGuideObject.Get("minimum_joy_axis_actuation_strength").AsSingle();
        set => BaseGuideObject.Set("minimum_joy_axis_actuation_strength", value);
    }
    
    /// <summary>Should input from the keyboard be considered?</summary>
    public bool Keyboard
    {
        get => BaseGuideObject.Get("keyboard").AsBool();
        set => BaseGuideObject.Set("keyboard", value);
    }
    
    /// <summary>Should input from the keyboard be considered?</summary>
    public bool Touch
    {
        get => BaseGuideObject.Get("touch").AsBool();
        set => BaseGuideObject.Set("touch", value);
    }
}