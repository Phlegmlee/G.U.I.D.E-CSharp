using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Touch Angle Input types. Must be assigned a GUIDEInputTouchAngle to function.</summary>
public partial class GuideInputTouchAngle : GuideInput
{
    // Unit in which the angle should be provided
    public enum EAngleUnit {
        // Angle is provided in radians
        RADIANS = 0,
        // Angle is provided in degrees.
        DEGREES = 1,
        // Return when evaluation fails.
        UNKNOWN = 99
    }
    
    public GuideInputTouchAngle(GodotObject gdInput) : base(gdInput) { }

    public GuideInputTouchAngle()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputTouchAngleGdPath); }

    /// <summary>The unit in which the angle should be provided.</summary>
    public EAngleUnit Unit
    {
        get
        {
            var i = BaseGuideObject.Get("unit").AsInt32();
            if (Enum.IsDefined(typeof(EAngleUnit), i))
            { return (EAngleUnit)i; }

            return EAngleUnit.UNKNOWN;
        }

        set
        {
            if (value is EAngleUnit.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(EAngleUnit.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }
            
            BaseGuideObject.Set("unit", (int)value);
        }
    }
}