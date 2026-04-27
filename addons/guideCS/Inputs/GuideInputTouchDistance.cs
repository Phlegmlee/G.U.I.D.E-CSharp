using Godot;

namespace GuideCs;

/// <summary>Wrapper for Touch Distance Input types. Must be assigned a GUIDEInputTouchDistance to function.</summary>
public partial class GuideInputTouchDistance : GuideInput
{
    public GuideInputTouchDistance(GodotObject gdInput) : base(gdInput) { }

    public GuideInputTouchDistance()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputTouchDistanceGdPath); }

    
    // Has no unique public calls
}