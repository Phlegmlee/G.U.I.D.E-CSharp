using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Joy Button Input types. Must be assigned a GUIDEInputJoyButton to function.</summary>
public partial class GuideInputJoyButton : GuideInputJoyBase
{
    public GuideInputJoyButton(GodotObject gdInput) : base(gdInput) { }

    public GuideInputJoyButton()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputJoyButtonGdPath); }
    
    /// <summary>Controller button assigned to this input.</summary>
    public JoyButton Button
    {
        get
        {
            var i = BaseGuideObject.Get("button").AsInt64();
            if (Enum.IsDefined(typeof(JoyButton), i))
            { return (JoyButton)i; }

            return JoyButton.Invalid;
        }

        set => BaseGuideObject.Set("button", (long)value);
    }
}