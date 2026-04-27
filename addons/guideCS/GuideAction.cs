using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Actions. Must be assigned a GUIDEAction to function.</summary>
[GlobalClass, Icon("GuideActionCs.svg")]
public partial class GuideAction : GuideResource
{
    public enum EGuideActionValueType {
        BOOL = 0,
        AXIS_1D = 1,
        AXIS_2D = 2,
        AXIS_3D = 3,
        UNKNOWN = 99
    }
    
    public GuideAction(GodotObject gdAction) : base(gdAction) { }
    
    public GuideAction() { }
    
    /// <summary>The name of this action. Required when this action should be used as Godot action. Also displayed in the
    /// debugger.</summary>
    public string Name
    {
        get => BaseGuideObject.Get("name").AsString();
        set => BaseGuideObject.Set("name", value);
    }
    
    /// <summary>The action value type. Returns UNKNOWN if there was a failure of evaluation.</summary>
    public EGuideActionValueType ActionValueType
    {
        get
        {
            var i = BaseGuideObject.Get("action_value_type").AsInt32();
            if (Enum.IsDefined(typeof(EGuideActionValueType), i))
            { return (EGuideActionValueType)i; }

            return EGuideActionValueType.UNKNOWN;
        }

        set
        {
            if (value is EGuideActionValueType.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(EGuideActionValueType.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }
            
            BaseGuideObject.Set("action_value_type", (int)value);
        }
    }

    /// <summary>If this action triggers, lower-priority actions cannot trigger if they share input with this action unless
    /// these actions are chorded with this action.</summary>
    public bool BlockLowerPriorityActions
    {
        get => BaseGuideObject.Get("block_lower_priority_actions").AsBool();
        set => BaseGuideObject.Set("block_lower_priority_actions", value);
    }
    
    /// <summary>If true, then this action will be emitted into Godot's built-in action system. This can be helpful to
    /// interact with code using this system, like Godot's UI system. Actions will be emitted on trigger and completion
    /// (e.g. button down and button up).</summary>
    public bool EmitAsGodotActions
    {
        get => BaseGuideObject.Get("emit_as_godot_actions").AsBool();
        set => BaseGuideObject.Set("emit_as_godot_actions", value);
    }
    
    /// <summary>If true, players can remap this action. To be remappable, make sure that a name and the action type are
    /// properly set.</summary>
    public bool IsRemappable
    {
        get => BaseGuideObject.Get("is_remappable").AsBool();
        set => BaseGuideObject.Set("is_remappable", value);
    }
    
    /// <summary>The display name of the action shown to the player.</summary>
    public string DisplayName
    {
        get => BaseGuideObject.Get("display_name").AsString();
        set => BaseGuideObject.Set("display_name", value);
    }
    
    /// <summary>The display category of the action shown to the player.</summary>
    public string DisplayCategory
    {
        get => BaseGuideObject.Get("display_category").AsString();
        set => BaseGuideObject.Set("display_category", value);
    }

    /// <summary>Emitted every frame while the action is triggered.</summary>
    public event Action Triggered
    {
        add => Utility.ModifySignalConnection(BaseGuideObject, "triggered", Callable.From(value), true);
        remove => Utility.ModifySignalConnection(BaseGuideObject, "triggered", Callable.From(value), false);
    }
    
    /// <summary>Emitted the first frame that the action is triggered.</summary>
    public event Action JustTriggered
    {
        add => Utility.ModifySignalConnection(BaseGuideObject, "just_triggered", Callable.From(value), true);
        remove => Utility.ModifySignalConnection(BaseGuideObject, "just_triggered", Callable.From(value), false);
    }

    /// <summary>Emitted when the action starts evaluating.</summary>
    public event Action Started
    {
        add => Utility.ModifySignalConnection(BaseGuideObject, "started", Callable.From(value), true);
        remove => Utility.ModifySignalConnection(BaseGuideObject, "started", Callable.From(value), false);
    }
    
    /// <summary>Emitted every frame while the action is still evaluating.</summary>
    public event Action Ongoing
    {
        add => Utility.ModifySignalConnection(BaseGuideObject, "ongoing", Callable.From(value), true);
        remove => Utility.ModifySignalConnection(BaseGuideObject, "ongoing", Callable.From(value), false);
    }
    
    /// <summary>Emitted when the action finished evaluating.</summary>
    public event Action Completed
    {
        add => Utility.ModifySignalConnection(BaseGuideObject, "completed", Callable.From(value), true);
        remove => Utility.ModifySignalConnection(BaseGuideObject, "completed", Callable.From(value), false);
    }
    
    /// <summary>Emitted when the action was cancelled.</summary>
    public event Action Cancelled
    {
        add => Utility.ModifySignalConnection(BaseGuideObject, "cancelled", Callable.From(value), true);
        remove => Utility.ModifySignalConnection(BaseGuideObject, "cancelled", Callable.From(value), false);
    }

    /// <summary>Returns the value of the action as a bool.</summary>
    /// <returns>True if the X value is not zero.</returns>
    public bool ValueBool => BaseGuideObject.Get("value_bool").AsBool();

    /// <summary>Returns the value of the action as a float.</summary>
    /// <returns>The (X) value of the action.</returns>
    public float ValueAxis1d => BaseGuideObject.Get("value_axis_1d").AsSingle();

    /// <summary>Returns the value of the action as a Vector2.</summary>
    /// <returns>The (X, Y) value of the action.</returns>
    public Vector2 ValueAxis2d => BaseGuideObject.Get("value_axis_2d").AsVector2();

    /// <summary>Returns the value of the action as a Vector3.</summary>
    /// <returns>The (X, Y, Z) value of the action.</returns>
    public Vector3 ValueAxis3d => BaseGuideObject.Get("value_axis_3d").AsVector3();

    /// <summary>The amount of time since the action started evaluating.</summary>
    /// <returns>Time in seconds.</returns>
    public float ElapsedSeconds => BaseGuideObject.Get("elapsed_seconds").AsSingle();
    
    /// <summary>The ratio of the elapsed time to the hold time as a percentage of the assigned hold time that has passed.
    /// If the action has no hold time, this will be 0 when the action is not triggered and 1 when the action is triggered.
    /// </summary>
    /// <returns>Percentage of hold time that has passed between 0 (just activated) and 1 (reached hold time).</returns>
    public float ElapsedRatio => BaseGuideObject.Get("elapsed_ratio").AsSingle();

    /// <summary>The amount of seconds elapsed since the action was triggered.</summary>
    /// <returns>Time in seconds.</returns>
    public float TriggeredSeconds => BaseGuideObject.Get("triggered_seconds").AsSingle();

    /// <summary>Returns whether the action is currently triggered. Can be used for a polling style input.</summary>
    /// <returns>True if action is currently in a triggered state.</returns>
    public bool IsTriggered() => BaseGuideObject.Call("is_triggered").AsBool();
    
    /// <summary>Returns whether the action is currently complete. Can be used for a polling style input.</summary>
    /// <returns>True if action is currently in a completed state.</returns>
    public bool IsCompleted() => BaseGuideObject.Call("is_completed").AsBool();
    
    /// <summary>Returns whether the action is currently ongoing. Can be used for a polling style input.</summary>
    /// <returns>True if action is currently in an ongoing state.</returns>
    public bool IsOngoing() => BaseGuideObject.Call("is_ongoing").AsBool();
    
}

