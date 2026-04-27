using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace GuideCs;

/// <summary>Wrapper class for the InputDetector node. Must be assigned a GUIDEInputDetector to function.</summary>
[GlobalClass, Icon("InputDetector.svg")]
public partial class GuideInputDetector : Node
{
    public enum EJoyIndex {
        // Use -1, so the detected input will match any joystick
        ANY = 0,
        // Use the actual index of the detected joystick.
        DETECTED = 1,
        // Used on failure of evaluation
        UNKNOWN = 99
    }

    public enum EDetectionState {
        // The detector is currently idle.
        IDLE = 0,
        // The detector is currently counting down before starting the detection.
        COUNTDOWN = 3,
        // The detector waits for all abort inputs to be released before starting the detection.
        INPUT_PRE_CLEAR = 4,
        // The detector is currently detecting input.
        DETECTING = 1,
        // The detector has finished detecting but is waiting for input to be released after the detection.
        INPUT_POST_CLEAR = 2,
        // Used on failure of evaluation
        UNKNOWN = 99
    }
    
    /// <summary>The base node that the wrapper talks to. Must be set to an active GUIDEInputDetector in the scene tree
    /// to function. Recommend making the base detector a child of this node so it naturally follows the wrapper.</summary>
    [Export(PropertyHint.NodeType, "GUIDEInputDetector")] public Node BaseGuideDetector;

    public GuideInputDetector()
    {
        // If no node is attached in the editor's export variable and no node is manually attached by the next idle frame,
        // this will automatically create a new node for the wrapper to communicate with.
        Callable.From(ConnectNewDetector).CallDeferred();
            
        return;
        void ConnectNewDetector()
        {
            if (BaseGuideDetector is null)
            {
                var detector = ResourceLoader.Load<GDScript>($"{ResourceLibrary.InputDetector}").New().As<Node>();
                BaseGuideDetector = detector;
                AddChild(detector);
            }
        }
    }

    /// <summary>A countdown between initiating a detection and the actual start of the detection. This is useful because
    /// when the user clicks a button to start a detection, we want to make sure that the player is actually ready (and
    /// not accidentally moves anything). If set to 0, no countdown will be started.</summary>
    public float DetectionCountdownSeconds
    {
        get => BaseGuideDetector.Get("detection_countdown_seconds").AsSingle();
        set => BaseGuideDetector.Set("detection_countdown_seconds", value);
    }
    
    /// <summary>Minimum amplitude to detect any axis.</summary>
    public float MinimumAxisAmplitude
    {
        get => BaseGuideDetector.Get("minimum_axis_amplitude").AsSingle();
        set => BaseGuideDetector.Set("minimum_axis_amplitude", value);
    }

    /// <summary>If any of these inputs is encountered, the detector will treat this as 'abort detection'.<br /><br />
    /// Note: This is a transient list and does not update GUIDE directly. Use <see cref="SetAbortDetectionOn"/> to update
    /// GUIDE after making changes to the returned list.</summary>
    /// <returns>The current list of GuideInputs to detect.</returns>
    public List<GuideInput> GetAbortDetectionOn()
    {
        var baseArray = BaseGuideDetector.Get("abort_detection_on").AsGodotArray<GodotObject>();
        List<GuideInput> wrappedArray = [];

        foreach (var obj in baseArray)
        {
            wrappedArray.Add(Utility.GetCachedOrNew<GuideInput>(obj));
        }

        return wrappedArray;
    }

    /// <summary>Sets the inputs to detect as an 'abort detection'.<br /><br />
    /// Note: This overrides the current list of inputs to detect. If you want to modify the existing list, use
    /// <see cref="GetAbortDetectionOn"/> first.</summary>
    /// <param name="items">List of Inputs to check for.</param>
    public void SetAbortDetectionOn(List<GuideInput> items)
    {
        var baseArray = new Array();
        foreach (var obj in items)
        {
            baseArray.Add(obj.BaseGuideObject);
        }
        
        Utility.ModificationWarning($"{nameof(GuideInputDetector)}.{nameof(SetAbortDetectionOn)}",
            "remote_set_abort_detection_on", BaseGuideDetector);
        
        // Unable to send a custom-typed array through wrapper functions. Navigate to the 'guide_input_detector.gd'
        // file and add the function below:
         
        // func remote_set_abort_detection_on(inputs:Array) -> void:
        //     abort_detection_on.assign(inputs)

        BaseGuideDetector.Call("remote_set_abort_detection_on", baseArray);
    }
    
    /// <summary>Which joy index should be returned for detected joy events.</summary>
    public EJoyIndex UseJoyIndex
    {
        get
        {
            var i = BaseGuideDetector.Get("use_joy_index").AsInt32();
            if (Enum.IsDefined(typeof(EJoyIndex), i))
            { return (EJoyIndex)i; }

            return EJoyIndex.UNKNOWN;
        }

        set
        {
            if (value is EJoyIndex.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(EJoyIndex.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }
            
            BaseGuideDetector.Set("use_joy_index", (int)value);
        }
    }

    /// <summary>Whether trigger buttons on controllers should be detected when then action value type is limited to boolean.</summary>
    public bool AllowTriggersForBooleanActions
    {
        get => BaseGuideDetector.Get("allow_triggers_for_boolean_actions").AsBool();
        set => BaseGuideDetector.Set("allow_triggers_for_boolean_actions", value);
    }
    
    public event Action DetectionStarted
    {
        add => Utility.ModifySignalConnection(BaseGuideDetector, "detection_started", Callable.From(value), true);
        remove => Utility.ModifySignalConnection(BaseGuideDetector, "detection_started", Callable.From(value), false);
    }
    
    private event EventHandler<GuideInput> InputDetectedInvoker;
    /// <summary>Emitted when the input detector detects an input of the given type. If detection was aborted the given
    /// input is null.</summary>
    public event EventHandler<GuideInput> InputDetected
    {
        add
        {
            if (InputDetectedInvoker is null || InputDetectedInvoker.GetInvocationList().Length == 0)
            { 
                Utility.ModifySignalConnection(BaseGuideDetector, "input_detected",
                    Callable.From((Action<GodotObject>)InputDetectedResolver), true); 
            }
            
            InputDetectedInvoker += value;
        }
        
        remove
        {
            InputDetectedInvoker -= value;
            
            if (InputDetectedInvoker is null || InputDetectedInvoker.GetInvocationList().Length == 0)
            {
                Utility.ModifySignalConnection(BaseGuideDetector, "input_detected",
                    Callable.From((Action<GodotObject>)InputDetectedResolver), false); 
            }
        }
    }
    
    /// <summary>Wraps the GuideInput from InputDetected invoke to pass to listeners.</summary>
    private void InputDetectedResolver(GodotObject guideInput)
    {
        InputDetectedInvoker?.Invoke(this, Utility.GetCachedOrNew<GuideInput>(guideInput));
    }

    /// <summary>Checks whether the input detector is currently detecting input.</summary>
    public bool IsDetecting => BaseGuideDetector.Get("is_detecting").AsBool();

    /// <summary>Detects a boolean input type.</summary>
    /// <param name="deviceTypes">Optional: Limits detection to provided DeviceTypes.</param>
    public void DetectBool(List<GuideInput.EDeviceType> deviceTypes = null)
    {
        if (deviceTypes is null)
        {
            BaseGuideDetector.Call("detect_bool");
            return;
        }

        Array<int> dtArray = [];
        foreach (var type in deviceTypes)
        {
            dtArray.Add((int)type);
        }
        
        BaseGuideDetector.Call("detect_bool", dtArray);
    }
    
    /// <summary>Detects a 1D axis input type.</summary>
    /// <param name="deviceTypes">Optional: Limits detection to provided DeviceTypes.</param>
    public void DetectAxis1d(List<GuideInput.EDeviceType> deviceTypes = null)
    {
        if (deviceTypes is null)
        {
            BaseGuideDetector.Call("detect_axis_1d");
            return;
        }

        Array<int> dtArray = [];
        foreach (var type in deviceTypes)
        {
            dtArray.Add((int)type);
        }
        
        BaseGuideDetector.Call("detect_axis_1d", dtArray);
    }
    
    /// <summary>Detects a 2D axis input type.</summary>
    /// <param name="deviceTypes">Optional: Limits detection to provided DeviceTypes.</param>
    public void DetectAxis2d(List<GuideInput.EDeviceType> deviceTypes = null)
    {
        if (deviceTypes is null)
        {
            BaseGuideDetector.Call("detect_axis_2d");
            return;
        }

        Array<int> dtArray = [];
        foreach (var type in deviceTypes)
        {
            dtArray.Add((int)type);
        }
        
        BaseGuideDetector.Call("detect_axis_2d", dtArray);
    }
    
    /// <summary>Detects a 3D axis input type.</summary>
    /// <param name="deviceTypes">Optional: Limits detection to provided DeviceTypes.</param>
    public void DetectAxis3d(List<GuideInput.EDeviceType> deviceTypes = null)
    {
        if (deviceTypes is null)
        {
            BaseGuideDetector.Call("detect_axis_3d");
            return;
        }

        Array<int> dtArray = [];
        foreach (var type in deviceTypes)
        {
            dtArray.Add((int)type);
        }

        BaseGuideDetector.Call("detect_axis_3d", dtArray);
    }

    /// <summary>Detects the given input type.</summary>
    /// <param name="valueType">Action Value type to detect.</param>
    /// <param name="deviceTypes">Optional: If given, will only detect inputs from the specified device types. Otherwise,
    /// will detect inputs from all supported device types.</param>
    public void Detect(GuideAction.EGuideActionValueType valueType, List<GuideInput.EDeviceType> deviceTypes = null)
    {
        if (valueType is GuideAction.EGuideActionValueType.UNKNOWN)
        { GD.PushWarning($"{Enum.GetName(valueType)} is not a valid value and is ignored by GUIDE during calls."); }
        
        var vtInt = (int)valueType;
        
        Array<int> dtArray = [];
        if (deviceTypes is not null)
        {
            foreach (var type in deviceTypes)
            {
                dtArray.Add((int)type);
            }
        }
        
        BaseGuideDetector.Call("detect", vtInt, dtArray);
    }

    /// <summary>Aborts a running detection. Does nothing if no detection is currently running.</summary>
    public void AbortDetection() => BaseGuideDetector.Call("abort_detection");
    
}

