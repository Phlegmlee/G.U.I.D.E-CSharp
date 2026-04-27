using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Virtual Cursor Modifier types. Must be assigned a GUIDEModifierVirtualCursor to function.</summary>
public partial class GuideModifierVirtualCursor : GuideModifier
{
    public enum EScreenScale 
    {
        // Input is not scaled with input screen size. This means that the cursor will
        // visually move slower on higher resolutions.
        NONE = 0,
        // Input is scaled with the longer axis of the screen size (e.g. width in
        // landscape mode, height in portrait mode). The cursor will move with
        // the same visual speed on all resolutions.
        LONGER_AXIS = 1,
        // Input is scaled with the shorter axis of the screen size (e.g. height in
        // landscape mode, width in portrait mode). The cursor will move with the 
        // same visual speed on all resolutions.
        SHORTER_AXIS = 2,
        // For failure to evaluate
        UNKNOWN = 99
    }
    
    public GuideModifierVirtualCursor(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierVirtualCursor()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierVirtualCursorGdPath); }

    /// <summary>The initial position of the virtual cursor (given in screen relative coordinates).</summary>
    public Vector2 InitialPosition
    {
        get => BaseGuideObject.Get("initial_position").AsVector2();
        set => BaseGuideObject.Set("initial_position", value);
    }
    
    /// <summary>Whether the initial position should be taken from the current mouse position. If true, this has precedence
    /// over the initial_position setting.</summary>
    public bool InitializeFromMousePosition
    {
        get => BaseGuideObject.Get("initialize_from_mouse_position").AsBool();
        set => BaseGuideObject.Set("initialize_from_mouse_position", value);
    }
    
    /// <summary>Whether the virtual cursor's position should be applied to the mouse position when this modifier is
    /// deactivated.</summary>
    public bool ApplyToMousePositionOnDeactivation
    {
        get => BaseGuideObject.Get("apply_to_mouse_position_on_deactivation").AsBool();
        set => BaseGuideObject.Set("apply_to_mouse_position_on_deactivation", value);
    }
    
    /// <summary>The cursor movement speed in pixels.</summary>
    public Vector3 Speed
    {
        get => BaseGuideObject.Get("speed").AsVector3();
        set => BaseGuideObject.Set("speed", value);
    }
    
    /// <summary>Screen scaling to be applied to the cursor movement. This controls whether the cursor movement speed is
    /// resolution dependent or not. If set to anything but [None] then the input value will be multiplied with
    /// the window width/height depending on the setting.</summary>
    public EScreenScale ScreenScale
    {
        get
        {
            var i = BaseGuideObject.Get("screen_scale").AsInt32();
            if (Enum.IsDefined(typeof(EScreenScale), i))
            { return (EScreenScale)i; }

            return EScreenScale.UNKNOWN;
        }

        set
        {
            if (value is EScreenScale.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(EScreenScale.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }
            BaseGuideObject.Set("screen_scale", (int)value);
        }
    }
    
    /// <summary>If true, the cursor movement speed is in pixels per second, otherwise it is in pixels per frame.</summary>
    public bool ApplyDeltaTime
    {
        get => BaseGuideObject.Get("apply_delta_time").AsBool();
        set => BaseGuideObject.Set("apply_delta_time", value);
    }
}