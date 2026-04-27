using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Touch Axis 1D Input types. Must be assigned a GUIDEInputTouchAxis1D to function.</summary>
public partial class GuideInputTouchAxis1D : GuideInputTouchAxisBase
{
    public enum EGuideInputTouchAxis 
    {
        X,
        Y,
        UNKNOWN
    }
    
    public GuideInputTouchAxis1D(GodotObject gdInput) : base(gdInput) { }

    public GuideInputTouchAxis1D()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputTouchAxis1dGdPath); }

    public EGuideInputTouchAxis Axis
    {
        get
        {
            var i = BaseGuideObject.Get("axis").AsInt32();
            if (Enum.IsDefined(typeof(EGuideInputTouchAxis), i))
            { return (EGuideInputTouchAxis)i; }

            return EGuideInputTouchAxis.UNKNOWN;
        }

        set
        {
            if (value is EGuideInputTouchAxis.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(EGuideInputTouchAxis.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }
            
            BaseGuideObject.Set("axis", (int)value);
        }
    }
}