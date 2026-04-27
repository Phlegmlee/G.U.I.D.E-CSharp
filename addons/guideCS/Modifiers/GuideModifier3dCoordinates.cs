using Godot;

namespace GuideCs;

/// <summary>Wrapper for 3D Coordinate Modifier types. Must be assigned a GUIDEModifier3DCoordinates to function.</summary>
public partial class GuideModifier3dCoordinates : GuideModifier
{
    public GuideModifier3dCoordinates(GodotObject gdInput) : base(gdInput) { }

    public GuideModifier3dCoordinates()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.Modifier3dCoordinatesGdPath); }
    
    /// <summary>The maximum depth of the ray cast used to detect the 3D position.</summary>
    public float MaxDepth
    {
        get => BaseGuideObject.Get("max_depth").AsSingle();
        set => BaseGuideObject.Set("max_depth", value);
    }
    
    /// <summary>Whether the rays cast should collide with areas.</summary>
    public bool CollideWithAreas
    {
        get => BaseGuideObject.Get("collide_with_areas").AsBool();
        set => BaseGuideObject.Set("collide_with_areas", value);
    }
    
    /// <summary>Collision mask to use for the ray cast.</summary>
    public int CollisionMask
    {
        get => BaseGuideObject.Get("collision_mask").AsInt32();
        set => BaseGuideObject.Set("collision_mask", value);
    }
}