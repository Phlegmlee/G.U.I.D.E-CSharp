using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Mouse Position Input types. Must be assigned a GUIDEInputMousePosition to function.</summary>
public partial class GuideInputMousePosition : GuideInput
{
    public GuideInputMousePosition(GodotObject gdInput) : base(gdInput) { }

    public GuideInputMousePosition()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputMousePositionGdPath); }

    // Has no unique public calls
}