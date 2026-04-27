using Godot;

namespace GuideCs;

/// <summary>Wrapper for Trigger Pulse types. Must be assigned a GUIDETriggerPulse to function.</summary>
public partial class GuideTriggerPulse : GuideTrigger
{
    public GuideTriggerPulse(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerPulse()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerPulseGdPath); }
    
    /// <summary>If true, the trigger will trigger immediately when the input is actuated. Otherwise, the trigger will
    /// wait for the initial delay.</summary>
    public bool TriggerOnStart
    {
        get => BaseGuideObject.Get("trigger_on_start").AsBool();
        set => BaseGuideObject.Set("trigger_on_start", value);
    }
    
    /// <summary>The delay after the initial actuation before pulsing begins.</summary>
    public float InitialDelay
    {
        get => BaseGuideObject.Get("initial_delay").AsSingle();
        set => BaseGuideObject.Set("initial_delay", value);
    }
    
    /// <summary>The interval between pulses. Set to 0 to pulse every frame.</summary>
    public float PulseInterval
    {
        get => BaseGuideObject.Get("pulse_interval").AsSingle();
        set => BaseGuideObject.Set("pulse_interval", value);
    }
    
    /// <summary>Maximum number of pulses. If &lt;= 0, the trigger will pulse indefinitely.</summary>
    public int MaxPulses
    {
        get => BaseGuideObject.Get("max_pulses").AsInt32();
        set => BaseGuideObject.Set("max_pulses", value);
    }
    
}