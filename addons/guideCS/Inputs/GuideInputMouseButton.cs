using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Mouse Button Input types. Must be assigned a GUIDEInputMouseButton to function.</summary>
public partial class GuideInputMouseButton : GuideInput
{
    public GuideInputMouseButton(GodotObject gdInput) : base(gdInput) { }

    public GuideInputMouseButton()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputMouseButtonGdPath); }
    
    /// <summary>Mouse button assigned to this input.</summary>
    public MouseButton Button
    {
        get
        {
            var i = BaseGuideObject.Get("button").AsInt64();
            if (Enum.IsDefined(typeof(MouseButton), i))
            { return (MouseButton)i; }

            return MouseButton.None;
        }

        set => BaseGuideObject.Set("button", (long)value);
    }
}