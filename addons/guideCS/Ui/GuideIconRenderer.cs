using Godot;

namespace GuideCs;

/// <summary>Wrapper for IconRenderers. Must be assigned a GUIDEIconRenderer to function.</summary>
public partial class GuideIconRenderer : GuideResource
{
    public GuideIconRenderer(GodotObject baseIconRenderer) : base(baseIconRenderer) { }

    public GuideIconRenderer()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.IconRendererGdPath); }

    /// <summary>The priority of this icon renderer. Built-in renderers use priority 0, except the controller renderer
    /// which uses -10. Built-in fallback renderer uses priority 100. The smaller the number the higher the priority.</summary>
    public int Priority
    {
        get => BaseGuideObject.Get("priority").AsInt32();
        set => BaseGuideObject.Set("priority", value);
    }

    /// <summary>Tests whether this renderer can render an icon for this input.</summary>
    /// <returns>True if compatible.</returns>
    public bool Supports(GuideInput input, GuideInputFormattingOptions options) => 
        BaseGuideObject.Call("supports", input.BaseGuideObject, options.BaseGuideObject).AsBool();

    /// <summary>Set up the scene so that the given input can be rendered. This will only be called for input where
    /// `supports` has returned true.</summary>
    public void Render(GuideInput input, GuideInputFormattingOptions options) =>
        BaseGuideObject.Call("render", input.BaseGuideObject, options.BaseGuideObject);

    /// <summary>Returns the cache key for the given input. This should be unique for this renderer and the given input.
    /// The same input and options should yield the same cache key for each renderer.</summary>
    public string CacheKey(GuideInput input, GuideInputFormattingOptions options) =>
        BaseGuideObject.Call("cache_key", input.BaseGuideObject, options.BaseGuideObject).ToString();
    
}