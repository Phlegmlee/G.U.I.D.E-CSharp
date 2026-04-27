using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Input Swizzle Modifier types. Must be assigned a GUIDEModifierInputSwizzle to function.</summary>
public partial class GuideModifierInputSwizzle : GuideModifier
{
    public enum EGuideInputSwizzleOperation {
        // Swap X and Y axes.
        YXZ,
        // Swap X and Z axes.
        ZYX,
        // Swap Y and Z axes.
        XZY,
        // Y to X, Z to Y, X to Z.
        YZX,
        // Y to Z, Z to X, X to Y.
        ZXY,
        // Return on failure of evaluation
        UNKNOWN
    }
    
    public GuideModifierInputSwizzle(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierInputSwizzle()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierInputSwizzleGdPath); }
    
    /// <summary>The direction in which the input should point.</summary>
    public EGuideInputSwizzleOperation Order
    {
        get
        {
            var i = BaseGuideObject.Get("order").AsInt32();
            if (Enum.IsDefined(typeof(EGuideInputSwizzleOperation), i))
            { return (EGuideInputSwizzleOperation)i; }

            return EGuideInputSwizzleOperation.UNKNOWN;
        }

        set
        {
            if (value is EGuideInputSwizzleOperation.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(EGuideInputSwizzleOperation.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }
            
            BaseGuideObject.Set("order", (int)value);
        }
    }
}