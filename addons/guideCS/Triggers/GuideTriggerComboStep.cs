using System;
using Godot;
using static GuideCs.GuideTriggerCombo;

namespace GuideCs;

/// <summary>Wrapper for Trigger Combo types. Must be assigned a GUIDETriggerComboStep to function.</summary>
public partial class GuideTriggerComboStep : GuideResource
{
    public GuideTriggerComboStep(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerComboStep()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerComboStepGdPath); }
    
    public GuideAction Action
    {
        get
        {
            var obj = BaseGuideObject.Get("action").AsGodotObject();
            return Utility.GetCachedOrNew<GuideAction>(obj);
        }
        
        set => BaseGuideObject.Set("action", value.BaseGuideObject);
    }
    
    public EActionEventType CompletionEvents
    {
        get
        {
            var i = BaseGuideObject.Get("completion_events").AsInt32();
            if (Enum.IsDefined(typeof(EActionEventType), i))
            { return (EActionEventType)i; }

            return EActionEventType.UNKNOWN;
        }

        set
        {
            if (value is EActionEventType.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(EActionEventType.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }

            BaseGuideObject.Set("completion_events", (int)value);
        }
    }
    
    public float TimeToActuate
    {
        get => BaseGuideObject.Get("time_to_actuate").AsSingle();
        set => BaseGuideObject.Set("time_to_actuate", value);
    }
    
    public bool IsSameAs(GuideTriggerComboStep other) =>
        BaseGuideObject.Call("is_same_as", other.BaseGuideObject).AsBool();
    
}