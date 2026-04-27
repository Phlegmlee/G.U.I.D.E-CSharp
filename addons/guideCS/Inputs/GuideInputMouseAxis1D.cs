using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Mouse Axis 1D Input types. Must be assigned a GUIDEInputMouseAxis1D to function.</summary>
public partial class GuideInputMouseAxis1D : GuideInput
{
    
    public enum EGuideInputMouseAxis 
    {
        X,
        Y,
        UNKNOWN
    }
    public GuideInputMouseAxis1D(GodotObject gdInput) : base(gdInput) { }

    public GuideInputMouseAxis1D()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputMouseAxis1dGdPath); }

    /// <summary>The joy axis to sample.</summary>
    public EGuideInputMouseAxis Axis
    {
        get
        {
            var i = BaseGuideObject.Get("axis").AsInt32();
            if (Enum.IsDefined(typeof(EGuideInputMouseAxis), i))
            { return (EGuideInputMouseAxis)i; }

            return EGuideInputMouseAxis.UNKNOWN;
        }

        set
        {
            if (value is EGuideInputMouseAxis.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(EGuideInputMouseAxis.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }
            
            BaseGuideObject.Set("axis", (int)value);
        }
    }
}