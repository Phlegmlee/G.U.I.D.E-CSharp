using Godot;

namespace GuideCs;

/// <summary>Wrapper for Text Providers. Must be assigned a GUIDETextProvider to function.</summary>
public partial class GuideTextProvider : GuideResource
{
    public GuideTextProvider(GodotObject baseTextProvider) : base(baseTextProvider) { }

    public GuideTextProvider()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TextProviderGdPath); }

    /// <summary>The priority of this text provider. The built-in text provider uses priority 0. The smaller the number
    /// the higher the priority.</summary>
    public int Priority
    {
        get => BaseGuideObject.Get("priority").AsInt32();
        set => BaseGuideObject.Set("priority", value);
    }

    /// <summary>Determines if this provider can provide a text for the provided input.</summary>
    /// <returns>True if they are compatible.</returns>
    public bool Supports(GuideInput input, GuideInputFormattingOptions options) =>
        BaseGuideObject.Call("supports", input.BaseGuideObject, options.BaseGuideObject).AsBool();
    

    /// <summary>Provides the text for the given input. Will only be called when the input is supported by this text
    /// provider. Note that for key input this is not supposed to look at the modifiers. This function will be called
    /// separately for each modifier.</summary>
    public string GetText(GuideInput input, GuideInputFormattingOptions options) =>
        BaseGuideObject.Call("get_text", input.BaseGuideObject, options.BaseGuideObject).AsString();
}