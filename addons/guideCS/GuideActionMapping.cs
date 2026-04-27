using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace GuideCs;

public partial class GuideActionMapping : GuideResource
{
    public GuideActionMapping(GodotObject gdActionMap) : base(gdActionMap) { }
    
    public GuideActionMapping() { }
    
    /// <summary>The action to be mapped.</summary>
    public GuideAction Action
    {
        get
        {
            var obj = BaseGuideObject.Get("action").AsGodotObject();
            return Utility.GetCachedOrNew<GuideAction>(obj);
        }

        set => BaseGuideObject.Set("action", value.BaseGuideObject);
    }

    /// <summary>Gets the set of input mappings that can trigger the action.<br /><br />
    /// Note: This is a transient list that does not update GUIDE. Use <see cref="SetInputMappings"/> to update GUIDE after
    /// making changes to the returned list.</summary>
    public List<GuideInputMapping> GetInputMappings()
    {
        var baseArray = BaseGuideObject.Get("input_mappings").AsGodotArray<GodotObject>();
        List<GuideInputMapping> wrappedArray = [];
        
        foreach (var obj in baseArray)
        {
            wrappedArray.Add(Utility.GetCachedOrNew<GuideInputMapping>(obj));
        }

        return wrappedArray;
    }

    /// <summary>Sets the input mappings that can trigger the action.<br /><br />
    /// Note: This overrides the current list of mappings. If you want to modify the existing list, use
    /// <see cref="GetInputMappings"/> first.</summary>
    public void SetInputMappings(List<GuideInputMapping> items)
    {
        Array baseArray = [];
        foreach (var item in items)
        {
            baseArray.Add(item.BaseGuideObject);
        }
        
        Utility.ModificationWarning($"{nameof(GuideActionMapping)}.{nameof(SetInputMappings)}", 
            "remote_set_input_mappings", BaseGuideObject);
        
        // Unable to send a custom-typed array through wrapper functions. Navigate to the 'guide_action_mapping.gd'
        // file and add the function below:
        
        // func remote_set_input_mappings(maps:Array) -> void:
        //     input_mappings.assign(maps)
        
        BaseGuideObject.Call("remote_set_input_mappings", baseArray);
    }
}