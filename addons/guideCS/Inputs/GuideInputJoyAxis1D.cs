using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Joy Axis 1D Input types. Must be assigned a GUIDEInputJoyAxis1D to function.</summary>
public partial class GuideInputJoyAxis1D : GuideInputJoyBase
{
    public GuideInputJoyAxis1D(GodotObject gdInput) : base(gdInput) { }

    public GuideInputJoyAxis1D()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputJoyAxis1DGdPath); }

    /// <summary>The joy axis to sample.</summary>
    public JoyAxis Axis
    {
        get
        {
            var i = BaseGuideObject.Get("axis").AsInt64();
            if (Enum.IsDefined(typeof(JoyAxis), i))
            { return (JoyAxis)i; }

            return JoyAxis.Invalid;
        }

        set => BaseGuideObject.Set("axis", (long)value);
    }
}