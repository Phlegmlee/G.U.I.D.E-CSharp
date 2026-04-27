using Godot;

namespace GuideCs;

/// <summary>Wrapper base for Modifier classes. Does nothing on its own, use with inherited class.</summary>
public partial class GuideModifier : GuideResource
{
    public GuideModifier(GodotObject gdGuideModifier) : base(gdGuideModifier) { }
    
    public GuideModifier() { }

    /// <summary>Returns whether this modifier is the same as the 'other' modifier. This is used to determine if a modifier
    /// can be reused during context switching.</summary>
    public bool IsSameAs(GuideModifier other) => BaseGuideObject.Call("is_same_as", other.BaseGuideObject).AsBool();
    
}