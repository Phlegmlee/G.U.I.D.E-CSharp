using System;
using Godot;
using static GuideCs.GuideTriggerCombo;

namespace GuideCs;

/// <summary>Wrapper for Combo Cancel Action types. Must be assigned a GUIDETriggerComboCancelAction to function.</summary>
public partial class GuideTriggerComboCancelAction : GuideResource
{
    public GuideTriggerComboCancelAction(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerComboCancelAction()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerComboCancelActionGdPath); }
    
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

    public bool IsSameAs(GuideTriggerComboCancelAction other) =>
        BaseGuideObject.Call("is_same_as", other.BaseGuideObject).AsBool();

}