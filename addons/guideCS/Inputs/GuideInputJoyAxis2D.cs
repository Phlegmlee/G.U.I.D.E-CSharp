using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Joy Axis 2D Input types. Must be assigned a GUIDEInputJoyAxis2D to function.</summary>
public partial class GuideInputJoyAxis2D : GuideInputJoyBase
{
    public GuideInputJoyAxis2D(GodotObject gdInput) : base(gdInput) { }

    public GuideInputJoyAxis2D()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputJoyAxis2DGdPath); }

    /// <summary>The joy axis to sample for x input.</summary>
    public JoyAxis X
    {
        get
        {
            var i = BaseGuideObject.Get("x").AsInt64();
            if (Enum.IsDefined(typeof(JoyAxis), i))
            { return (JoyAxis)i; }

            return JoyAxis.Invalid;
        }

        set => BaseGuideObject.Set("x", (long)value);
    }

    /// <summary>The joy axis to sample for y input.</summary>
    public JoyAxis Y
    {
        get
        {
            var i = BaseGuideObject.Get("y").AsInt64();
            if (Enum.IsDefined(typeof(JoyAxis), i))
            {
                return (JoyAxis)i;
            }

            return JoyAxis.Invalid;
        }

        set => BaseGuideObject.Set("y", (long)value);
    }
}