using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Trigger Stability types. Must be assigned a GUIDETriggerStability to function.</summary>
public partial class GuideTriggerStability : GuideTrigger
{
    public enum ETriggerWhen 
    {
        // Input must be stable
        INPUT_IS_STABLE,
        // Input must change
        INPUT_CHANGES,
        // On failure to evaluate
        UNKNOWN
    }
    
    public GuideTriggerStability(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerStability()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerStabilityGdPath); }
    
    /// <summary>The maximum amount that the input can change after actuation before it is considered "changed".</summary>
    public float MaxDeviation
    {
        get => BaseGuideObject.Get("max_deviation").AsSingle();
        set => BaseGuideObject.Set("max_deviation", value);
    }
    
    /// <summary>When should the trigger trigger?</summary>
    public ETriggerWhen TriggerWhen
    {
        get
        {
            var i = BaseGuideObject.Get("trigger_when").AsInt32();
            if (Enum.IsDefined(typeof(ETriggerWhen), i))
            { return (ETriggerWhen)i; }

            return ETriggerWhen.UNKNOWN;
        }

        set
        {
            if (value is ETriggerWhen.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(ETriggerWhen.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }

            BaseGuideObject.Set("trigger_when", (int)value);
        }
    }
    
}