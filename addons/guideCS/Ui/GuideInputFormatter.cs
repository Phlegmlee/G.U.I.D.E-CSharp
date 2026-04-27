using System.Threading.Tasks;
using Godot;
using static GuideCs.ResourceLibrary;

namespace GuideCs;

/// <summary>Wrapper class for Input Formatters. Must be assigned a GUIDEInputFormatter to function.</summary>
public partial class GuideInputFormatter : GuideResource
{
    #region StaticLoader

    private static readonly GDScript InputFormatterStatic;
    static GuideInputFormatter()
    { InputFormatterStatic = ResourceLoader.Load<GDScript>(InputFormatterGdPath); }
    
    #endregion StaticLoader
    
    public GuideInputFormatter(GodotObject gdRemapper) : base(gdRemapper) { }

    public GuideInputFormatter()
    { LoadAndConnectBaseGuideResource($"{InputFormatterGdPath}"); }

    /// <summary>The <see cref="GuideInputFormattingOptions"/> that this renderer uses.</summary>
    public GuideInputFormattingOptions FormattingOptions
    {
        get
        {
            var obj = BaseGuideObject.Get("formatting_options").AsGodotObject();
            return Utility.GetCachedOrNew<GuideInputFormattingOptions>(obj);
        }
        
        set => BaseGuideObject.Set("formatting_options", value.BaseGuideObject);
    }

    /// <summary>This will clean up the rendering infrastructure used for generating icons. Note that in a normal game you
    /// will have no need to call this as the infrastructure is needed throughout the run of your game. It might be useful
    /// in tests though, to get rid of spurious warnings about orphaned nodes.</summary>
    public static void Cleanup() => InputFormatterStatic.Call("cleanup");

    /// <summary>Adds an icon renderer for rendering icons.</summary>
    public static void AddIconRenderer(GuideIconRenderer renderer) => 
        InputFormatterStatic.Call("add_icon_renderer", renderer.BaseGuideObject);

    /// <summary>Removes an icon renderer.</summary>
    public static void RemoveIconRenderer(GuideIconRenderer renderer) => 
        InputFormatterStatic.Call("remove_icon_renderer", renderer.BaseGuideObject);
    
    /// <summary>Adds a text provider for rendering text.</summary>
    public static void AddTextProvider(GuideTextProvider provider) => 
        InputFormatterStatic.Call("add_text_provider", provider.BaseGuideObject);

    /// <summary>Removes a text provider.</summary>
    public static void RemoveTextProvider(GuideTextProvider provider) => 
        InputFormatterStatic.Call("remove_text_provider", provider.BaseGuideObject);


    /// <summary>Returns an input formatter that can format actions using the currently active inputs.</summary>
    /// <param name="iconSize">Optional: Square size, in pixels, to format icons to.</param>
    public static GuideInputFormatter ForActiveContexts(int iconSize = 32)
    {
        var obj = InputFormatterStatic.Call("for_active_contexts", iconSize).AsGodotObject();
        return Utility.GetCachedOrNew<GuideInputFormatter>(obj);
    }

    /// <summary>Returns an input formatter that can format actions using the given context.</summary>
    /// <param name="context">Mapping Context to look up inputs from.</param>
    /// <param name="iconSize">Optional: Square size, in pixels, to format icons to.</param>
    public static GuideInputFormatter ForContext(GuideMappingContext context, int iconSize = 32)
    {
        var obj = InputFormatterStatic.Call("for_context", 
            context.BaseGuideObject, iconSize).AsGodotObject();
        return Utility.GetCachedOrNew<GuideInputFormatter>(obj);
    }
    
    /// <summary>Returns BBCode formatted text for use in a <see cref="RichTextLabel"/> or similar.<br />
    /// As this is an async function, calls need to 'await' the results.</summary>
    /// <param name="action">Action to create icons for.</param>
    public async Task<string> ActionAsRichTextAsync(GuideAction action)
    {
        Utility.ModificationWarning($"{nameof(GuideInputFormatter)}.{nameof(ActionAsRichTextAsync)}",
            "remote_action_as_richtext_async", BaseGuideObject);
        
        // For this function to work asynchronously, navigate to the 'guide_input_formatter.gd' file and add the function
        // below:
        
        // func remote_action_as_richtext_async(callback:Callable, action:GUIDEAction):
        //     var ret = await action_as_richtext_async(action)
        //     callback.call(ret)
        
        return await Utility.CallAsync<string>(
            BaseGuideObject, "remote_action_as_richtext_async", action.BaseGuideObject);
    }
    

    /// <summary>Returns the action input as plain text which can be used in any UI component. This is a bit more
    /// light-weight than formatting as icons and returns immediately.</summary>
    /// <param name="action">Action to get text input 'icon' for.</param>
    public string ActionAsText(GuideAction action) => 
        BaseGuideObject.Call("action_as_text", action.BaseGuideObject).AsString();
    
    /// <summary>Returns BBCode formatted text for use in a <see cref="RichTextLabel"/> or similar.<br />
    /// As this is an async function, calls need to 'await' the results.</summary>
    /// <param name="input">Input to create icons for.</param>
    /// <param name="materializeActions">Optional: If true, find and use specific action types, otherwise will use simple
    /// input.</param>
    public async Task<string> InputAsRichtextAsync(GuideInput input, bool materializeActions = true)
    {
        Utility.ModificationWarning($"{nameof(GuideInputFormatter)}.{nameof(InputAsRichtextAsync)}", 
            "remote_input_as_richtext_async", BaseGuideObject);
        
        // For this function to work asynchronously, navigate to the 'guide_input_formatter.gd' file and add the function
        // below:
        
        // func remote_input_as_richtext_async(callback:Callable, input:GUIDEInput, materialize_actions:bool = true):
        //     var ret = await input_as_richtext_async(input, materialize_actions)
        //     callback.call(ret)
        
        return await Utility.CallAsync<string>(
            BaseGuideObject, "remote_input_as_richtext_async", input.BaseGuideObject, materializeActions);
    }

    /// <summary>Returns the action input as plain text which can be used in any UI component. This is a bit more
    /// light-weight than formatting as icons and returns immediately.</summary>
    /// <param name="input">Input to create icons for.</param>
    /// <param name="materializeActions">Optional: If true, find and use specific action types, otherwise will use simple
    /// input.</param>
    public string InputAsText(GuideInput input, bool materializeActions = true) => 
        BaseGuideObject.Call("input_as_text", input.BaseGuideObject, materializeActions).AsString();
    
}

