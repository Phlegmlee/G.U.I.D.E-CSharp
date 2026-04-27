using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper base for Input classes. Does nothing on its own, use with inherited class.</summary>
public partial class GuideInput : GuideResource
{
    public enum EDeviceType
    {
        // The input originates from no device (e.g. virtual inputs).
        NONE = 0,
        // The input originates from a keyboard.
        KEYBOARD = 1,
        // The input originates from a mouse.
        MOUSE = 2,
        // The input originates from a joystick / gamepad.
        JOY = 4,
        // The input originates from a touch device.
        TOUCH = 8,
        // Fallback if failed to evaluate
        UNKNOWN = 99
    }
    
    public GuideInput(GodotObject gdInput) : base(gdInput) { }

    public GuideInput() { }

    ///<summary>The type of device from which this input originates.<br />
    /// Note: This can also be a combination of devices (e.g. for the 'any' input).<br />
    /// Returns UNKNOWN if there is a failure to evaluate through the wrapper.</summary>
    public EDeviceType DeviceType
    {
        get
        {
            var i = BaseGuideObject.Get("device_type").AsInt32();
            if (Enum.IsDefined(typeof(EDeviceType), i))
            { return (EDeviceType)i; }

            return EDeviceType.UNKNOWN;
        }
    }

    public bool IsSameAs(GuideInput other) => BaseGuideObject.Call("is_same_as", other.BaseGuideObject).AsBool();
}