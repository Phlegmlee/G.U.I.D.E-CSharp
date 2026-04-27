using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace GuideCs;

/// <summary>Wrapper for Trigger Combo types. Must be assigned a GUIDETriggerCombo to function.</summary>
public partial class GuideTriggerCombo : GuideTrigger
{
    public enum EActionEventType 
    {
        TRIGGERED = 1,
        STARTED = 2,
        ONGOING = 4,
        CANCELLED = 8,
        COMPLETED = 16,
        UNKNOWN = 99
    }
    
    public GuideTriggerCombo(GodotObject gdInput) : base(gdInput) { }

    public GuideTriggerCombo()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.TriggerComboGdPath); }
    
    /// <summary>If set to true, the combo trigger will print information about state changes to the debug log.</summary>
    public bool EnableDebugPrint
    {
        get => BaseGuideObject.Get("enable_debug_print").AsBool();
        set => BaseGuideObject.Set("enable_debug_print", value);
    }

    /// <summary>Gets the content from the Steps container. To modify GUIDE, use <see cref="SetSteps"/> afterward.</summary>
    public List<GuideTriggerComboStep> GetSteps()
    {
        var baseArray = BaseGuideObject.Get("steps").AsGodotArray<GodotObject>();
        List<GuideTriggerComboStep> wrappedArray = [];
            
        foreach (var obj in baseArray)
        {
            wrappedArray.Add(Utility.GetCachedOrNew<GuideTriggerComboStep>(obj));                
        }

        return wrappedArray;
    }

    /// <summary>Sets the Steps container. To modify the container, use <see cref="GetSteps"/> first.</summary>
    public void SetSteps(List<GuideTriggerComboStep> triggers)
    {
        Array<GodotObject> baseArray = [];
        foreach (var trigger in triggers)
        {
            baseArray.Add(Utility.GetBaseOrNull(trigger));
        }
        
        Utility.ModificationWarning($"{nameof(GuideTriggerCombo)}.{nameof(SetSteps)}", "remote_set_steps",
            BaseGuideObject);
        
        // Unable to send a custom-typed array through wrapper functions. Navigate to the 'guide_trigger_combo.gd'
        // file and add the function below:
         
        // func remote_set_steps(step_items:Array) -> void:
        //     input_mappings.assign(step_items)

        BaseGuideObject.Call("remote_set_steps", baseArray);
    }
    
    /// <summary>Gets the content from the Cancellation Actions container. To modify GUIDE, use
    /// <see cref="SetCancellationActions"/> afterward.</summary>
    public List<GuideTriggerComboCancelAction> GetCancellationActions()
    {
        var baseArray = BaseGuideObject.Get("cancellation_actions").AsGodotArray<GodotObject>();
        List<GuideTriggerComboCancelAction> wrappedArray = [];
            
        foreach (var obj in baseArray)
        {
            wrappedArray.Add(Utility.GetCachedOrNew<GuideTriggerComboCancelAction>(obj));                
        }

        return wrappedArray;
    }

    /// <summary>Sets the Cancellation Actions container. To modify the container, use
    /// <see cref="GetCancellationActions"/> first.</summary>
    public void SetCancellationActions(List<GuideTriggerComboCancelAction> triggers)
    {
        Array<GodotObject> baseArray = [];
        foreach (var trigger in triggers)
        {
            baseArray.Add(Utility.GetBaseOrNull(trigger));
        }
        
        Utility.ModificationWarning($"{nameof(GuideTriggerCombo)}.{nameof(SetCancellationActions)}", 
            "remote_set_cancellation_actions", BaseGuideObject);
        
        // Unable to send a custom-typed array through wrapper functions. Navigate to the 'guide_trigger_combo.gd'
        // file and add the function below:
         
        // func remote_set_cancellation_actions(actions:Array) -> void:
        //     cancellation_actions.assign(actions)

        BaseGuideObject.Call("remote_set_cancellation_actions", baseArray);
    }
    
}