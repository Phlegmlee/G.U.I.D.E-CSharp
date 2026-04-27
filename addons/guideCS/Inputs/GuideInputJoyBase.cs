using Godot;

namespace GuideCs;

/// <summary>Wrapper base for Joy classes. Does nothing on its own, use with inherited class.</summary>
public partial class GuideInputJoyBase : GuideInput
{
    public GuideInputJoyBase(GodotObject gdInput) : base(gdInput) { }

    public GuideInputJoyBase() { }

    /// <summary>The index of the connected joy pad to check.<br /> 
    /// Any connected joy pad = -1<br />
    /// First connected joy pad = 0<br />
    /// Second connected joy pad = 1<br />
    /// Third connected joy pad = 2<br />
    /// Fourth connected joy pad = 3<br />
    /// First virtual joy pad = -2<br />
    /// Second virtual joy pad = -3</summary>
    public int JoyIndex
    {
        get => BaseGuideObject.Get("joy_index").AsInt32(); 
        set => BaseGuideObject.Set("joy_index", value);
    }
}