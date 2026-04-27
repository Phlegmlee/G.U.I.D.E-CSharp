using System;
using System.Collections.Generic;
using static GuideCs.GuideResource;
using Godot;
using Godot.Collections;

namespace GuideCs;

/// <summary>The GuideCs base node. Works similarly to the GUIDE addon. Needs to be loaded in the editor's
/// [ Project -> Project Settings -> Globals ] list after (below) the base GUIDE plugin.</summary>
public partial class Guide : Node
{
    /// <summary>The base GUIDE node autoloaded from the plugin's Global assignment. This assumes the GUIDE Global has not
    /// been renamed. If it was, adjust the GetNode assignment path in the Ready function to reflect the new name.</summary>
    private static Node _guideAutoload;
    
    public override void _Ready()
    {
        _guideAutoload = GetNode("/root/GUIDE");
        ProcessMode = ProcessModeEnum.Always;
        
        base._Ready();
    }

    /// <summary>This is emitted whenever input mappings change (due to mapping contexts being enabled/disabled,
    /// remapping configs being re-applied, or joystick devices being connected/disconnected).<br/>
    /// This is useful for updating UI prompts.</summary>
    public static event Action InputMappingsChanged
    {
        add => Utility.ModifySignalConnection(_guideAutoload, "input_mappings_changed", 
            Callable.From(value), true);
        remove => Utility.ModifySignalConnection(_guideAutoload, "input_mappings_changed", 
            Callable.From(value), false);
    }

    /// <summary>Injects input into GUIDE. GUIDE will call this automatically but can also be used to manually inject input
    /// for GUIDE to handle.<br /><br />
    /// Does not react to Godot's built-in events.<br /> <br />
    /// The input state is the sole consumer of input events. It will notify GUIDEInputs when relevant input events happen.
    /// This way we don't need to process input events multiple times and, at the same time, always have the full picture
    /// of the input state.</summary>
    /// <param name="event">Godot InputEvent to inject.</param>
    public static void InjectInput(InputEvent @event)
    {
        _guideAutoload.Call("inject_input", @event);
    }
    
    /// <summary>Applies an input remapping config. This will override all input bindings in the currently loaded mapping
    /// contexts with the bindings from the configuration. Note that GUIDE will not track changes to the remapping config.
    /// If your remapping config changes, you will need to call this method again.</summary>
    public static void SetRemappingConfig(GuideRemappingConfig config)
    {
        _guideAutoload.Call("set_remapping_config", config.BaseGuideObject);
    }

    /// <summary>Enables the given context. If no priority is given, defaults to 0.</summary>
    /// <param name="mappingContext">MappingContext resource.</param>
    /// <param name="disableOthers">Optional: If True, all other currently enabled mapping contexts will be disabled.</param>
    /// <param name="priority">Optional: Priority of the MappingContext. Lower numbers have higher priority.</param>
    /// <param name="defer">Optional: If True, will automatically wrap the change in a CallDeferred. Useful when
    /// changing a mapping context from an Action that needs to finish work before being disabled by a context change.</param>
    public static void EnableMappingContext(GuideMappingContext mappingContext, bool disableOthers = false, int priority = 0,
        bool defer = false)
    {
        if (defer)
        { Callable.From(ContextChange).CallDeferred(); }
        
        else ContextChange();
        
        return;
        void ContextChange()
        {
            _guideAutoload.Call("enable_mapping_context", mappingContext.BaseGuideObject, disableOthers, priority);
        }
    }

    /// <summary>Disables the given mapping context.</summary>
    /// <param name="mappingContext">MappingContext to disable.</param>
    /// <param name="defer">Optional: If True, will automatically wrap the change in a CallDeferred. <br />
    /// Useful when changing a mapping context from an Action that needs to finish work before being disabled by a context
    /// change.</param>
    public static void DisableMappingContext(GuideMappingContext mappingContext, bool defer = false)
    {
        if (defer)
        { Callable.From(ContextChange).CallDeferred(); }
        
        else ContextChange();
        
        return;
        void ContextChange()
        {
            _guideAutoload.Call("disable_mapping_context", mappingContext.BaseGuideObject);
        }
    }

    /// <summary>Replaces the currently enabled mapping contexts with a new set of contexts. This is more efficient than
    /// calling <see cref="EnableMappingContext"/> multiple times as it only updates the internal caches once. All
    /// contexts are enabled with priority 0, with contexts later in the array having higher precedence when merging inputs
    /// (due to later timestamps). </summary>
    /// <param name="contexts">Mapping Contexts to set to become the currently active contexts.</param>
    /// <returns>Contexts that were active before this call.</returns>
    public static List<GuideMappingContext> SetEnabledMappingContexts(List<GuideMappingContext> contexts)
    {
         Array <GodotObject> baseArray = [];
         foreach (var context in contexts)
         {
             baseArray.Add(context.BaseGuideObject);
         }
         
         Utility.ModificationWarning($"{nameof(Guide)}.{nameof(SetEnabledMappingContexts)}", 
             "remote_set_enabled_mapping_contexts", _guideAutoload);
        
         // Unable to send a custom-typed array through wrapper functions. Navigate to the 'guide.gd' file and add the
         // function below:
         
         // func remote_set_enabled_mapping_contexts(context_items:Array) -> Array:
         //     var typed_array: Array[GUIDEMappingContext] = []
         //     typed_array.assign(context_items)
         //     return set_enabled_mapping_contexts(typed_array)

         List<GuideMappingContext> wrappedList = [];
         var returnArray = _guideAutoload.Call("remote_set_enabled_mapping_contexts", baseArray).AsGodotArray<GodotObject>();

         foreach (var obj in returnArray)
         {
             wrappedList.Add(Utility.GetCachedOrNew<GuideMappingContext>(obj));
         }
         
         return wrappedList;
    }

    /// <summary>Returns whether the given mapping context is currently enabled.</summary>
    /// <param name="mappingContext">MappingContext to check.</param>
    /// <returns>True if mapping context is currently enabled.</returns>
    public static bool IsMappingContextEnabled(GuideMappingContext mappingContext)
    {
        return _guideAutoload.Call("is_mapping_context_enabled", 
            mappingContext.BaseGuideObject).AsBool();
    }

    /// <summary>Returns the currently enabled mapping contexts.</summary>
    public static List<GuideMappingContext> GetEnabledMappingContexts()
    {
        var baseArray = _guideAutoload.Call("get_enabled_mapping_contexts").AsGodotArray<GodotObject>();
        var wrappedList = new List<GuideMappingContext>();
        
        foreach (var obj in baseArray)
        {
            wrappedList.Add(Utility.GetCachedOrNew<GuideMappingContext>(obj));
        }

        return wrappedList;
    }
}