using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace GuideCs;

/// <summary>Wrapper for Input Mappings. Must be assigned a GUIDEInputMapping to function.</summary>
public partial class GuideInputMapping : GuideResource
{
    public GuideInputMapping(GodotObject gdInputRemapper) : base(gdInputRemapper) { }

    public GuideInputMapping()
    { LoadAndConnectBaseGuideResource($"{ResourceLibrary.InputMappingGdPath}"); }
    
    /// <summary>Whether the remapping configuration in this input mapping should override the configuration of the bound
    /// action. Enable this, to give a key a custom name or category for remapping.</summary>
    public bool OverrideActionSettings
    {
        get => BaseGuideObject.Get("override_action_settings").AsBool();
        set => BaseGuideObject.Set("override_action_settings", value);
    }
    
    /// <summary>If true, players can remap this input mapping.<br />
    /// Note: The action to which this input is bound also needs to be remappable for this setting to have an effect.</summary>
    public bool IsRemappable
    {
        get => BaseGuideObject.Get("is_remappable").AsBool();
        set => BaseGuideObject.Set("is_remappable", value);
    }
    
    /// <summary>The display name of the input mapping shown to the player. If empty, the display name of the action is
    /// used.</summary>
    public string DisplayName
    {
        get => BaseGuideObject.Get("display_name").AsString();
        set => BaseGuideObject.Set("display_name", value);
    }
    
    /// <summary>The display category of the input mapping. If empty, the display name of the action is used.</summary>
    public string DisplayCategory
    {
        get => BaseGuideObject.Get("display_category").AsString();
        set => BaseGuideObject.Set("display_category", value);
    }
    
    /// <summary>The input to be actuated.</summary>
    public GuideInput Input
    {
        get
        {
            var obj = BaseGuideObject.Get("input").AsGodotObject();
            return Utility.GetCachedOrNew<GuideInput>(obj);
        }
        
        set => BaseGuideObject.Set("input", value.BaseGuideObject);
    }

    /// <summary>Returns a list of modifiers that preprocess the actuated input before it is fed to the triggers.<br /><br />
    /// Note: This is a transient list that does not update GUIDE. Use <see cref="SetModifiers"/> to update GUIDE after
    /// making changes to the returned dictionary.</summary>
    public List<GuideModifier> GetModifiers()
    {
        var baseArray = BaseGuideObject.Get("modifiers").AsGodotArray<GodotObject>();
        var wrappedList = new List<GuideModifier>();

        foreach (var obj in baseArray)
        {
            wrappedList.Add(Utility.GetCachedOrNew<GuideModifier>(obj));
        }

        return wrappedList;
    }

    /// <summary>Sets the list of modifiers.<br/><br/>
    /// Note: This overrides the current list of modifiers. If you want to modify the existing list, use
    /// <see cref="GetModifiers"/> first.</summary>
    public void SetModifiers(List<GuideModifier> items)
    {
        var baseArray = new Array();
        foreach (var item in items)
        {
            baseArray.Add(item.BaseGuideObject);
        }
        
        Utility.ModificationWarning($"{nameof(GuideInputMapping)}.{nameof(SetModifiers)}", 
            "remote_set_modifiers", BaseGuideObject);
        
        // Unable to send a custom-typed array through wrapper functions. Navigate to the 'guide_input_mapping.gd'
        // file and add the function below:
        
        // func remote_set_modifiers(mods:Array) -> void:
        //     modifiers.assign(mods)
        
        BaseGuideObject.Call("remote_set_modifiers", baseArray);
    }

    /// <summary>Returns a list of triggers that could trigger the mapped action.<br /><br />
    /// Note: This is a transient list that does not update GUIDE. Use <see cref="SetTriggers"/> to update GUIDE after
    /// making changes to the returned list.</summary>
    public List<GuideTrigger> GetTriggers()
    {
        var baseArray = BaseGuideObject.Get("triggers").AsGodotArray<GodotObject>();
        var wrappedList = new List<GuideTrigger>();

        foreach (var obj in baseArray)
        {
            wrappedList.Add(Utility.GetCachedOrNew<GuideTrigger>(obj));
        }

        return wrappedList;
    }
    
    /// <summary>Sets the list of triggers that could trigger the mapped action.<br /><br />
    /// Note: This overrides the current list of triggering events. If you want to modify the existing list, use
    /// <see cref="GetTriggers"/> first.</summary>
    public void SetTriggers(List<GuideTrigger> items)
    {
        var baseArray = new Array();
        foreach (var obj in items)
        {
            baseArray.Add(obj.BaseGuideObject);
        }
        
        Utility.ModificationWarning($"{nameof(GuideInputMapping)}.{nameof(SetTriggers)}", 
            "remote_set_triggers", BaseGuideObject);
        
        // Unable to send a custom-typed array through wrapper functions. Navigate to the 'guide_input_mapping.gd'
        // file and add the function below:
        
        // func remote_set_triggers(trigs:Array) -> void:
        //     triggers.assign(trigs)
        
        BaseGuideObject.Call("remote_set_triggers", baseArray);
    }
}