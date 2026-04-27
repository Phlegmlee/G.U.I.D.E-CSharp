using Godot;

namespace GuideCs;

/// <summary>Wrapper for Input Action types. Must be assigned a GUIDEInputAction to function.</summary>
public partial class GuideInputAction : GuideInput
{
    public GuideInputAction(GodotObject gdInput) : base(gdInput) { }

    public GuideInputAction()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.InputActionGdPath); }

    /// <summary>The action that this input should mirror. This is live tracked, so any change in the action will update
    /// the input.</summary>
    public GuideAction Action
    {
        get
        {
            var baseObj = BaseGuideObject.Get("action").AsGodotObject();
            return Utility.GetCachedOrNew<GuideAction>(baseObj);
        }
        
        set => BaseGuideObject.Set("action", value.BaseGuideObject);
    }
}