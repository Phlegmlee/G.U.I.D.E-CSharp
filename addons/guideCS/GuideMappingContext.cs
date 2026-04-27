using System;
using System.Collections.Generic;
using Godot;
using Array = Godot.Collections.Array;

namespace GuideCs;

/// <summary>The GuideCs wrapper for Mapping Contexts. Must be assigned a GUIDEMappingContext to function.</summary>
[GlobalClass, Icon("GuideMappingContextCs.svg")]
public partial class GuideMappingContext : GuideResource
{
    public GuideMappingContext(GodotObject gdContext) : base(gdContext) { }
    
    public GuideMappingContext() { }
    
    /// <summary> Emitted when this mapping context is enabled.</summary>
    public event Action Enabled
    {
        add => Utility.ModifySignalConnection(BaseGuideObject, "enabled", Callable.From(value), true);
        remove => Utility.ModifySignalConnection(BaseGuideObject, "enabled", Callable.From(value), false);
    }

    /// <summary> Emitted when this mapping context is disabled.</summary>
    public event Action Disabled
    {
        add => Utility.ModifySignalConnection(BaseGuideObject, "disabled", Callable.From(value), true);
        remove => Utility.ModifySignalConnection(BaseGuideObject, "disabled", Callable.From(value), false);
    }

    /// <summary> The display name for this mapping context during action remapping.</summary>
    public string DisplayName
    {
        get => BaseGuideObject.Get("display_name").AsString();
        set => BaseGuideObject.Set("display_name", value);
    }

    /// <summary>Returns the mappings in the context. Do yourself a favor and use the G.U.I.D.E. panel to edit these.<br /><br />
    /// Note: This is a transient list that does not update GUIDE. Use <see cref="SetMappings"/> to update GUIDE after
    /// making changes to the returned list.</summary>
    public List<GuideActionMapping> GetMappings()
    {
        var baseArray = BaseGuideObject.Get("mappings").AsGodotArray<GodotObject>();
        List<GuideActionMapping> wrappedArray = [];
        
        foreach (var obj in baseArray)
        {
            wrappedArray.Add(Utility.GetCachedOrNew<GuideActionMapping>(obj));
        }

        return wrappedArray;
    }

    /// <summary>Sets the mappings in the context. Do yourself a favor and use the G.U.I.D.E. panel to edit these.</summary>
    public void SetMappings(List<GuideActionMapping> items)
    {
        Array baseArray = [];
        foreach (var obj in items)
        {
            baseArray.Add(obj.BaseGuideObject);
        }
        
        Utility.ModificationWarning($"{nameof(GuideMappingContext)}.{nameof(SetMappings)}", "remote_set_mappings", BaseGuideObject);
        
        // Unable to send a custom-typed array through wrapper functions. Navigate to the 'guide_mapping_context.gd'
        // file and add the function below:
        
        // func remote_set_mappings(maps:Array) -> void:
        //     mappings.assign(maps)
        
        BaseGuideObject.Call("remote_set_mappings", baseArray);
    }
}