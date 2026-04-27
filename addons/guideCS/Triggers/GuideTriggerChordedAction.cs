using Godot;

namespace GuideCs;

/// <summary>Wrapper for Chorded Action types. Must be assigned a GUIDETriggerChordedAction to function.</summary>
public partial class GuideTriggerChordedAction : GuideTrigger
{
    public GuideTriggerChordedAction(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerChordedAction()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerChordedActionGdPath); }
    
    public GuideAction Action
    {
        get
        {
            var obj = BaseGuideObject.Get("action").AsGodotObject();
            return Utility.GetCachedOrNew<GuideAction>(obj);
        }
        
        set => BaseGuideObject.Set("action", value.BaseGuideObject);
    }
}