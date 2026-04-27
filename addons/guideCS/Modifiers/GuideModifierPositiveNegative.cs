using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Positive/Negative Modifier types. Must be assigned a GUIDEModifierPositiveNegative to function.</summary>
public partial class GuideModifierPositiveNegative : GuideModifier
{
    public enum ELimitRange 
    {
        POSITIVE = 1,
        NEGATIVE = 2,
        UNKNOWN = 99
    }
    
    public GuideModifierPositiveNegative(GodotObject gdInput) : base(gdInput) { }

    public GuideModifierPositiveNegative()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.ModifierPositiveNegativeGdPath); }
    
    /// <summary>The range of numbers to which the input should be limited.</summary>
    public ELimitRange Range
    {
        get
        {
            var i = BaseGuideObject.Get("range").AsInt32();
            if (Enum.IsDefined(typeof(ELimitRange), i))
            { return (ELimitRange)i; }

            return ELimitRange.UNKNOWN;
        }

        set
        {
            if (value is ELimitRange.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(ELimitRange.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }
            
            BaseGuideObject.Set("range", (int)value);
        }
    }
    
    /// <summary>Whether the X axis should be affected.</summary>
    public bool X
    {
        get => BaseGuideObject.Get("x").AsBool();
        set => BaseGuideObject.Set("x", value);
    }
    
    /// <summary>Whether the Y axis should be affected.</summary>
    public bool Y
    {
        get => BaseGuideObject.Get("y").AsBool();
        set => BaseGuideObject.Set("y", value);
    }
    
    /// <summary>Whether the Z axis should be affected.</summary>
    public bool Z
    {
        get => BaseGuideObject.Get("z").AsBool();
        set => BaseGuideObject.Set("z", value);
    }
}