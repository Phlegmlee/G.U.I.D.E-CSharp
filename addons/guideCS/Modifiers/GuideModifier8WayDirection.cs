using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for 8-Way Direction Modifier types. Must be assigned a GUIDEModifier8WayDirection to function.</summary>
public partial class GuideModifier8WayDirection : GuideModifier
{
    public enum EGuideDirection 
    {
        EAST = 0, 
        NORTH_EAST = 1,
        NORTH = 2, 
        NORTH_WEST = 3,
        WEST = 4, 
        SOUTH_WEST = 5,
        SOUTH = 6, 
        SOUTH_EAST = 7,
        UNKNOWN = 99
    }
    
    public GuideModifier8WayDirection(GodotObject gdInput) : base(gdInput) { }

    public GuideModifier8WayDirection()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.Modifier8WayDirectionGdPath); }
    
    /// <summary>The direction in which the input should point.</summary>
    public EGuideDirection Direction
    {
        get
        {
            var i = BaseGuideObject.Get("direction").AsInt32();
            if (Enum.IsDefined(typeof(EGuideDirection), i))
            { return (EGuideDirection)i; }

            return EGuideDirection.UNKNOWN;
        }

        set
        {
            if (value is EGuideDirection.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(EGuideDirection.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }
            
            BaseGuideObject.Set("direction", (int)value);
        }
    }
}