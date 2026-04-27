using System.Collections.Generic;
using Godot;

namespace GuideCs;

/// <summary>Runs a full suite of tests on all calls for GuideCs to ensure all interop calls to GUIDE are functioning. The
/// test node needs to be added to the scene tree somewhere to test internal nodes. Ensure
/// <see cref="Utility.ModifySignalConnection"/> warnings are enabled before running.</summary>
public partial class GuideCsUnitTest : Node
{
    private bool _testSuccess = true;

    private bool TestSuccess
    {
        get  => _testSuccess;
        set
        {
            if (!value)
            {
                // Put a line break here to catch any errors.
                GD.Print("GuideCS Unit Test hit a failure point. Set Breakpoint here and step back to troubleshoot.");
                _testSuccess = false;
                return;
            }

            GD.Print("GuideCS Unit Test completed successfully.");
            _testSuccess = true;
        }
    }

    public override void _Ready()
    {
        // This loads a new node at the resource path and will need redirected if GuideCS is not installed to the addons folder
        var detector = (GuideInputDetector)ResourceLoader.
            Load<CSharpScript>("res://addons/GuideCs/Remapping/GuideInputDetector.cs").New();
        // Run with CallDeferred to set up the detector node after ready.
        Callable.From(() => AddChild(detector)).CallDeferred();
        
        Callable.From(() => RunTestSuite(detector)).CallDeferred();
    }

    /// <summary>Runs full test on all wrapper calls.</summary>
    public void RunTestSuite(GuideInputDetector detector)
    {
        TestGuideMain();
        TestInputs();
        TestModifiers();
        TestRemapping(detector);
        TestTriggers();
        TestUi();
        
        if (TestSuccess) TestSuccess = true;
    }
    
    void TestGuideMain()
    {
        GuideGlobal();
        Action();
        ActionMapping();
        InputMapping();
        MappingContext();

        return;
        void GuideGlobal()
        {
            // Tests in this section may report failures if run in a project with existing GUIDE resources set up since
            // the Guide global will look at all content activated through the global process (i.e. MappingContexts).
            Guide.InjectInput(new InputEventMouseMotion());
            Guide.SetRemappingConfig(new GuideRemappingConfig());
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.MappingContextGdPath).New();
            var w = Utility.CreateWrapper<GuideMappingContext>(obj);
                
            Guide.InputMappingsChanged += OnSignal; // no error message
            Guide.InputMappingsChanged -= OnSignal; // no error message

            var t1x = Guide.IsMappingContextEnabled(w); // false
            if (t1x) TestSuccess = false;
                
            var t2x = Guide.GetEnabledMappingContexts(); // count = 0
            if (t2x.Count != 0) TestSuccess = false;

            Guide.EnableMappingContext(w, true, 10);
            var t1y = Guide.IsMappingContextEnabled(w); // true
            if (!t1y) TestSuccess = false;
                
            var t2y = Guide.GetEnabledMappingContexts(); // count = 1
            if (t2y.Count != 1) TestSuccess = false;

            Guide.DisableMappingContext(w);
            var t1z = Guide.IsMappingContextEnabled(w); // false
            if (t1z) TestSuccess = false;
            
            var t2z = Guide.GetEnabledMappingContexts(); // count = 0
            if (t2z.Count != 0) TestSuccess = false;
            
            var obj2 = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.MappingContextGdPath).New();
            var w2 = Utility.CreateWrapper<GuideMappingContext>(obj2);
            
            var obj3 = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.MappingContextGdPath).New();
            var w3 = Utility.CreateWrapper<GuideMappingContext>(obj3);

            List<GuideMappingContext> list = [w2, w3];
            
            Guide.EnableMappingContext(w);
            var t3x = Guide.SetEnabledMappingContexts(list); // count = 1
            if (t3x.Count != 1) TestSuccess = false;

            var t3y = Guide.GetEnabledMappingContexts(); // count = 2
            if (t3y.Count != 2) TestSuccess = false;

        }
        
        void Action()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionGdPath).New();
            var w = Utility.CreateWrapper<GuideAction>(obj);

            var t1x = w.Name; // ""
            if (t1x != "") TestSuccess = false;
            w.Name = "TestOne";
            var t1y = w.Name; // "TestOne"
            if (t1y != "TestOne") TestSuccess = false;

            var t2x = w.ActionValueType; // BOOL
            if (t2x != GuideAction.EGuideActionValueType.BOOL) TestSuccess = false;
            
            var t2y = w.ActionValueType; // BOOL
            if (t2y != GuideAction.EGuideActionValueType.BOOL) TestSuccess = false;
            
            w.ActionValueType = GuideAction.EGuideActionValueType.AXIS_2D;
            var t2z = w.ActionValueType; // AXIS_2D
            if (t2z != GuideAction.EGuideActionValueType.AXIS_2D) TestSuccess = false;

            var t3x = w.BlockLowerPriorityActions; // true
            if (!t3x) TestSuccess = false;
            
            w.BlockLowerPriorityActions = !t3x;
            var t3y = w.BlockLowerPriorityActions; // false
            if (t3y) TestSuccess = false;

            var t4x = w.EmitAsGodotActions; // false
            if (t4x) TestSuccess = false;
            
            w.EmitAsGodotActions = !t4x;
            var t4y = w.EmitAsGodotActions; // true
            if (!t4y) TestSuccess = false;

            var t5x = w.IsRemappable; // false
            if (t5x) TestSuccess = false;
            
            w.IsRemappable = !t5x;
            var t5y = w.IsRemappable; // true
            if (!t5y) TestSuccess = false;

            var t6x = w.DisplayName; // ""
            if (t6x != "") TestSuccess = false;
            
            w.DisplayName = "TestTwo";
            var t6y = w.DisplayName; // "TestTwo"
            if (t6y != "TestTwo") TestSuccess = false;

            var t7x = w.DisplayCategory; // ""
            if (t7x != "") TestSuccess = false;
            
            w.DisplayCategory = "TestThree";
            var t7y = w.DisplayCategory; // "TestThree"
            if (t7y != "TestThree") TestSuccess = false;

            w.Triggered += OnSignal; // no warning
            w.Triggered -= OnSignal; // no warning

            w.JustTriggered += OnSignal; // no warning
            w.JustTriggered -= OnSignal; // no warning

            w.Started += OnSignal; // no warning
            w.Started -= OnSignal; // no warning

            w.Ongoing += OnSignal; // no warning
            w.Ongoing -= OnSignal; // no warning

            w.Completed += OnSignal; // no warning
            w.Completed -= OnSignal; // no warning

            w.Cancelled += OnSignal; // no warning
            w.Cancelled -= OnSignal; // no warning

            var t8x = w.ValueBool; // false
            if (t8x) TestSuccess = false;
            
            var t9x = w.ValueAxis1d; // 0;
            if (t9x != 0) TestSuccess = false;
            
            var t10x = w.ValueAxis2d; // (0,0)
            if (!t10x.IsEqualApprox(Vector2.Zero)) TestSuccess = false;
            
            var t11x = w.ValueAxis3d; // (0,0,0)
            if (!t11x.IsEqualApprox(Vector3.Zero)) TestSuccess = false;
            
            var t12x = w.ElapsedSeconds; // 0
            if (t12x != 0) TestSuccess = false;
            
            var t13x = w.ElapsedRatio; // 0
            if (t13x != 0) TestSuccess = false;
            
            var t14x = w.TriggeredSeconds; // 0
            if (t14x != 0) TestSuccess = false;
            
            var t15x = w.IsTriggered(); // false
            if (t15x) TestSuccess = false;
            
            var t16x = w.IsCompleted(); // true
            if (!t16x) TestSuccess = false;
            
            var t17x = w.IsOngoing(); // false
            if (t17x) TestSuccess = false;
        }

        void ActionMapping()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionMappingGdPath).New();
            var w = Utility.CreateWrapper<GuideActionMapping>(obj);

            var t1x = w.Action; // null
            if (t1x.BaseGuideObject is not null) TestSuccess = false;
            
            var actObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionGdPath).New();
            var act = Utility.CreateWrapper<GuideAction>(actObj);
            
            w.Action = act;
            var t1y = w.Action; // 'act' wrapper
            if (t1y.BaseGuideObject != act.BaseGuideObject) TestSuccess = false;

            var t2x = w.GetInputMappings(); // count = 0
            if (t2x.Count != 0) TestSuccess = false;
            
            var mapObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputMappingGdPath).New();
            List<GuideInputMapping> imap = [ Utility.CreateWrapper<GuideInputMapping>(mapObj) ]; 
            
            w.SetInputMappings(imap);
            var t2y = w.GetInputMappings(); // count = 1
            if (t2y.Count != 1) TestSuccess = false;
        }

        void InputMapping()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputMappingGdPath).New();
            var w = Utility.CreateWrapper<GuideInputMapping>(obj);

            var t1x = w.OverrideActionSettings; // false
            if (t1x) TestSuccess = false;
            
            w.OverrideActionSettings = !t1x;
            var t1y = w.OverrideActionSettings; // true
            if (!t1y) TestSuccess = false;

            var t2x = w.IsRemappable; // false
            if (t2x) TestSuccess = false;
            
            w.IsRemappable = !t2x;
            var t2y = w.IsRemappable; // true
            if (!t2y) TestSuccess = false;

            var t3x = w.DisplayName; // ""
            if (t3x != "") TestSuccess = false;
            
            w.DisplayName = "TestOne";
            var t3y = w.DisplayName; // "TestOne"
            if (t3y != "TestOne") TestSuccess = false;
            
            var t4x = w.DisplayCategory; // ""
            if (t4x != "") TestSuccess = false;
            
            w.DisplayCategory = "TestTwo";
            var t4y = w.DisplayCategory; // "TestTwo"
            if (t4y != "TestTwo") TestSuccess = false;

            var t5x = w.Input; // null
            if (t5x.BaseGuideObject != null) TestSuccess = false;
            
            var keyObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputKeyGdPath).New();
            var inp = Utility.CreateWrapper<GuideInput>(keyObj);
            
            w.Input = inp;
            var t5y = w.Input; // 'inp' wrapper
            if (t5y.BaseGuideObject != inp.BaseGuideObject) TestSuccess = false;

            var t6x = w.GetModifiers(); // count = 0
            if (t6x.Count != 0) TestSuccess = false;
            
            var modObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ModifierDeadzoneGdPath).New();
            List<GuideModifier> mods = [ Utility.CreateWrapper<GuideModifier>(modObj) ]; 
            w.SetModifiers(mods);
            var t6y = w.GetModifiers(); // count = 1
            if (t6y.Count != 1) TestSuccess = false;

            var t7x = w.GetTriggers(); // count = 0
            if (t7x.Count != 0) TestSuccess = false;
            
            var trigObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerPulseGdPath).New();
            List<GuideTrigger> trigs = [ Utility.CreateWrapper<GuideTrigger>(trigObj) ]; 
            
            w.SetTriggers(trigs);
            var t7y = w.GetTriggers(); // count = 1
            if (t7y.Count != 1) TestSuccess = false;
        }

        void MappingContext()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.MappingContextGdPath).New();
            var w = Utility.CreateWrapper<GuideMappingContext>(obj);
            
            w.Enabled += OnSignal; // no error message
            w.Enabled -= OnSignal; // no error message
            
            w.Disabled += OnSignal; // no error message
            w.Disabled -= OnSignal; // no error message

            var t1x = w.DisplayName; // ""
            if (t1x != "") TestSuccess = false;
            
            w.DisplayName = "TestOne";
            var t1y = w.DisplayName; // "TestOne"
            if (t1y != "TestOne") TestSuccess = false;

            var t2x = w.GetMappings(); // count = 0
            if (t2x.Count != 0) TestSuccess = false;
            
            var modObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionMappingGdPath).New();
            List<GuideActionMapping> maps = [ Utility.CreateWrapper<GuideActionMapping>(modObj) ]; 
            
            w.SetMappings(maps);
            var t2y = w.GetMappings(); // count = 1
            if (t2y.Count != 1) TestSuccess = false;
        }
    }

    private void TestInputs()
    {
        InputAction();
        InputAny();
        InputJoyAxis1D(); 
        InputJoyAxis2D();
        InputJoyButton(); 
        InputKey(); 
        InputMouseAxis1D();
        InputMouseButton(); 
        InputTouchAngle();
        InputTouchAxis1D();
        InputTouchPosition();

        return;
        void InputAction()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputActionGdPath).New();
            var w = Utility.CreateWrapper<GuideInputAction>(obj);
            var actObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionGdPath).New();
            var act = Utility.CreateWrapper<GuideAction>(actObj);
            
            // Start normal tests
            w.Action = act;
            var t1x = w.Action; // 'act' wrapper
            if (t1x.BaseGuideObject != act.BaseGuideObject) TestSuccess = false;

            // Tests inherited GuideInput functions once here
            var diffObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputActionGdPath).New();
            var diff = Utility.CreateWrapper<GuideInputAction>(diffObj);
            var diffActObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionGdPath).New();
            var diffAct = Utility.CreateWrapper<GuideAction>(diffActObj);
            diff.Action = diffAct;
            
            var t2x = w.DeviceType; // NONE
            if (t2x != GuideInput.EDeviceType.NONE) TestSuccess = false;

            var t3x = w.IsSameAs(diff); // false
            if (t3x) TestSuccess = false;

            var t3y = w.IsSameAs(w); // true
            if (!t3y) TestSuccess = false;
        }

        void InputAny()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputAnyGdPath).New();
            var w = Utility.CreateWrapper<GuideInputAny>(obj);

            var t1x = w.MouseButtons; // false
            if (t1x) TestSuccess = false;

            w.MouseButtons = !t1x;
            var t1y = w.MouseButtons; // true
            if (!t1y) TestSuccess = false;
            
            var t2x = w.MouseMovement; // false
            if (t2x) TestSuccess = false;

            w.MouseMovement = !t2x;
            var t2y = w.MouseMovement; // true
            if (!t2y) TestSuccess = false;

            var t3x = w.MinimumMouseMovementDistance; // 1.0
            if (!Mathf.IsEqualApprox(t3x, 1)) TestSuccess = false;

            w.MinimumMouseMovementDistance = t3x * 2;
            var t3y = w.MinimumMouseMovementDistance; // 2.0
            if (!Mathf.IsEqualApprox(t3y, t3x * 2)) TestSuccess = false;
            
            var t4x = w.JoyButtons; // false
            if (t4x) TestSuccess = false;

            w.JoyButtons = !t4x;
            var t4y = w.JoyButtons; // true
            if (!t4y) TestSuccess = false;
            
            var t5x = w.JoyAxes; // false
            if (t5x) TestSuccess = false;

            w.JoyAxes = !t5x;
            var t5y = w.JoyAxes; // true
            if (!t5y) TestSuccess = false;
            
            var t6x = w.MinimumJoyAxisActuationStrength; // 0.2
            if (!Mathf.IsEqualApprox(t6x, 0.2f)) TestSuccess = false;

            w.MinimumJoyAxisActuationStrength = t6x * 2;
            var t6y = w.MinimumJoyAxisActuationStrength; // 0.4
            if (!Mathf.IsEqualApprox(t6y, t6x * 2)) TestSuccess = false;
            
            var t7x = w.Keyboard; // false
            if (t7x) TestSuccess = false;

            w.Keyboard = !t7x;
            var t7y = w.Keyboard; // true
            if (!t7y) TestSuccess = false;
            
            var t8x = w.Touch; // false
            if (t8x) TestSuccess = false;

            w.Touch = !t8x;
            var t8y = w.Touch; // true
            if (!t8y) TestSuccess = false;
        }

        void InputJoyAxis1D()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputJoyAxis1DGdPath).New();
            var w = Utility.CreateWrapper<GuideInputJoyAxis1D>(obj);
            
            // Tests inherited GuideInputJoyBase functions once here
            var t1x = w.JoyIndex; // -1
            if (t1x != -1) TestSuccess = false;

            w.JoyIndex = 2;
            var t1y = w.JoyIndex; // 2
            if (t1y != 2) TestSuccess = false;
            
            // Start normal tests
            var t2x = w.Axis; // LeftX
            if (t2x != JoyAxis.LeftX) TestSuccess = false;

            w.Axis = JoyAxis.TriggerLeft;
            var t2y = w.Axis; // TriggerLeft
            if (t2y != JoyAxis.TriggerLeft) TestSuccess = false;
        }

        void InputJoyAxis2D()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputJoyAxis2DGdPath).New();
            var w = Utility.CreateWrapper<GuideInputJoyAxis2D>(obj);

            var t1x = w.X; // LeftX
            if (t1x != JoyAxis.LeftX) TestSuccess = false;

            w.X = JoyAxis.TriggerLeft;
            var t1y = w.X; // TriggerLeft
            if (t1y != JoyAxis.TriggerLeft) TestSuccess = false;
            
            var t2x = w.Y; // LeftY
            if (t2x != JoyAxis.LeftY) TestSuccess = false;

            w.Y = JoyAxis.TriggerRight;
            var t2y = w.Y; // TriggerRight
            if (t2y != JoyAxis.TriggerRight) TestSuccess = false;
        }

        void InputJoyButton()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputJoyButtonGdPath).New();
            var w = Utility.CreateWrapper<GuideInputJoyButton>(obj);

            var t1x = w.Button; // A
            if (t1x != JoyButton.A) TestSuccess = false;

            w.Button = JoyButton.Touchpad;
            var t1y = w.Button; // Touchpad
            if (t1y != JoyButton.Touchpad) TestSuccess = false;
        }

        void InputKey()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputKeyGdPath).New();
            var w = Utility.CreateWrapper<GuideInputKey>(obj);

            var t1x = w.Key; // None
            if (t1x != Key.None) TestSuccess = false;
            
            w.Key = Key.Backspace;
            var t1y = w.Key; // Backspace
            if (t1y != Key.Backspace) TestSuccess = false;
            
            var t2x = w.Shift; // false
            if (t2x) TestSuccess = false;

            w.Shift = !t2x;
            var t2y = w.Shift; // true
            if (!t2y) TestSuccess = false;
            
            var t3x = w.Control; // false
            if (t3x) TestSuccess = false;

            w.Control = !t3x;
            var t3y = w.Control; // true
            if (!t3y) TestSuccess = false;
            
            var t4x = w.Alt; // false
            if (t4x) TestSuccess = false;

            w.Alt = !t4x;
            var t4y = w.Alt; // true
            if (!t4y) TestSuccess = false;
            
            var t5x = w.Meta; // false
            if (t5x) TestSuccess = false;

            w.Meta = !t5x;
            var t5y = w.Meta; // true
            if (!t5y) TestSuccess = false;
            
            var t6x = w.AllowAdditionalModifiers; // true
            if (!t6x) TestSuccess = false;

            w.AllowAdditionalModifiers = !t6x;
            var t6y = w.AllowAdditionalModifiers; // false
            if (t6y) TestSuccess = false;
        }
        
        void InputMouseAxis1D()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputMouseAxis1dGdPath).New();
            var w = Utility.CreateWrapper<GuideInputMouseAxis1D>(obj);
            
            var t1x = w.Axis; // X
            if (t1x != GuideInputMouseAxis1D.EGuideInputMouseAxis.X) TestSuccess = false;

            w.Axis = GuideInputMouseAxis1D.EGuideInputMouseAxis.Y;
            var t1y = w.Axis; // Y
            if (t1y != GuideInputMouseAxis1D.EGuideInputMouseAxis.Y) TestSuccess = false;

        }
        
        // void InputMouseAxis2D() has no calls to test

        void InputMouseButton()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputMouseButtonGdPath).New();
            var w = Utility.CreateWrapper<GuideInputMouseButton>(obj);

            var t1x = w.Button; // Left
            if (t1x != MouseButton.Left) TestSuccess = false;

            w.Button = MouseButton.WheelUp;
            var t1y = w.Button; // WheelUp
            if (t1y != MouseButton.WheelUp) TestSuccess = false;
        }
        
        // void InputMousePosition() has no calls to test
        
        void InputTouchAngle()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputTouchAngleGdPath).New();
            var w = Utility.CreateWrapper<GuideInputTouchAngle>(obj);

            var t1x = w.Unit; // RADIANS
            if (t1x != GuideInputTouchAngle.EAngleUnit.RADIANS) TestSuccess = false;
            
            w.Unit = GuideInputTouchAngle.EAngleUnit.DEGREES;
            var t1y = w.Unit; // DEGREES
            if (t1y != GuideInputTouchAngle.EAngleUnit.DEGREES) TestSuccess = false;
        }
        
        void InputTouchAxis1D()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputTouchAxis1dGdPath).New();
            var w = Utility.CreateWrapper<GuideInputTouchAxis1D>(obj);
            
            var t1x = w.Axis; // X
            if (t1x != GuideInputTouchAxis1D.EGuideInputTouchAxis.X) TestSuccess = false;
            
            w.Axis = GuideInputTouchAxis1D.EGuideInputTouchAxis.Y;
            var t1y = w.Axis; // Y
            if (t1y != GuideInputTouchAxis1D.EGuideInputTouchAxis.Y) TestSuccess = false;
        }
        
        // void InputTouchAxis2D() has no calls to test
        
        // void InputTouchBase() has no calls to test
        
        // void InputTouchDistance() has no calls to test
        
        void InputTouchPosition()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputTouchPositionGdPath).New();
            var w = Utility.CreateWrapper<GuideInputTouchPosition>(obj);
            
            // Tests inherited GuideInputTouchBase functions once here
            var t1x = w.FingerCount; // 1
            if (t1x != 1) TestSuccess = false;
            
            w.FingerCount = 2;
            var t1y = w.FingerCount; // 2
            if (t1y != 2) TestSuccess = false;

            // GuideInputTouchPosition has no calls to test
        }
    }
    
    private void TestModifiers()
    {
        Modifer3dCoordinates();
        Modifier8WayDirection();
        ModifierCanvasCoordinates();
        ModifierCurve();
        ModifierDeadzone();
        ModifierInputSwizzle();
        ModifierMapRange();
        ModiferNegate();
        ModifierPositiveNegative();
        ModifierScale();
        ModifierVirtualCursor();

        return;
        void Modifer3dCoordinates()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.Modifier3dCoordinatesGdPath).New();
            var w = Utility.CreateWrapper<GuideModifier3dCoordinates>(obj);
            
            // Tests inherited GuideModifier functions once here
            var obj2 = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.Modifier3dCoordinatesGdPath).New();
            var diff = Utility.CreateWrapper<GuideModifier3dCoordinates>(obj2);
            
            var t1x = w.IsSameAs(diff); // true
            if (!t1x) TestSuccess = false;

            diff.CollideWithAreas = true;
            var t1y = w.IsSameAs(diff); // false
            if (t1y) TestSuccess = false;
            
            // Start normal tests
            var t2x = w.MaxDepth; // 1000.0
            if (!Mathf.IsEqualApprox(t2x, 1000)) TestSuccess = false;

            w.MaxDepth = t2x * 2;
            var t2y = w.MaxDepth; // 2000.0
            if (!Mathf.IsEqualApprox(t2y, t2x * 2)) TestSuccess = false;

            var t3x = w.CollideWithAreas; // false
            if (t3x) TestSuccess = false;

            w.CollideWithAreas = !t3x;
            var t3y = w.CollideWithAreas; // true
            if (!t3y) TestSuccess = false;

            var t4x = w.CollisionMask; // 0
            if (t4x != 0) TestSuccess = false;

            w.CollisionMask = 10;
            var t4y = w.CollisionMask; // 10
            if (t4y != 10) TestSuccess = false;
        }
        
        void Modifier8WayDirection()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.Modifier8WayDirectionGdPath).New();
            var w = Utility.CreateWrapper<GuideModifier8WayDirection>(obj);

            var t1x = w.Direction; // EAST
            if (t1x != GuideModifier8WayDirection.EGuideDirection.EAST) TestSuccess = false;
            
            w.Direction = GuideModifier8WayDirection.EGuideDirection.NORTH;
            var t1y = w.Direction; // NORTH
            if (t1y != GuideModifier8WayDirection.EGuideDirection.NORTH) TestSuccess = false;
        }
        
        void ModifierCanvasCoordinates()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ModifierCanvasCoordinatesGdPath).New();
            var w = Utility.CreateWrapper<GuideModifierCanvasCoordinates>(obj);

            var t1x = w.RelativeInput; // false
            if (t1x) TestSuccess = false;

            w.RelativeInput = !t1x;
            var t1y = w.RelativeInput; // true
            if (!t1y) TestSuccess = false;
        }
        
        void ModifierCurve()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ModifierCurveGdPath).New();
            var w = Utility.CreateWrapper<GuideModifierCurve>(obj);
            var diffCurve = new Curve();
            diffCurve.AddPoint(new Vector2(0.25f, 0.25f));
            diffCurve.AddPoint(new Vector2(0.75f, 0.75f));

            var defCurve = w.Curve; // default curve (used for compare)
            w.Curve = diffCurve;
            var t1x = w.Curve; // != default curve after set
            if (t1x == defCurve) TestSuccess = false;

            var t2x = w.X; // true
            if (!t2x) TestSuccess = false;
            
            w.X = !t2x;
            var t2y = w.X; // false
            if (t2y) TestSuccess = false;

            var t3x = w.Y; // true
            if (!t3x) TestSuccess = false;
            
            w.Y = !t3x;
            var t3y = w.Y; // false
            if (t3y) TestSuccess = false;
            
            var t4x = w.Z; // true
            if (!t4x) TestSuccess = false;
            
            w.Z = !t4x;
            var t4y = w.Z; // false
            if (t4y) TestSuccess = false;

            Curve tCurve = null;
            tCurve = GuideModifierCurve.DefaultCurve();
            if (tCurve is null) TestSuccess = false;
        }
        
        void ModifierDeadzone()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ModifierDeadzoneGdPath).New();
            var w = Utility.CreateWrapper<GuideModifierDeadzone>(obj);

            var t1x = w.LowerThreshold; // 0.2
            if (Mathf.IsEqualApprox(t1x, 0.2)) TestSuccess = false;

            w.LowerThreshold = t1x * 2;
            var t1y = w.LowerThreshold; // 0.4
            if (!Mathf.IsEqualApprox(t1y, t1x * 2)) TestSuccess = false;

            var t2x = w.UpperThreshold; // 1.0
            if (!Mathf.IsEqualApprox(t2x, 1)) TestSuccess = false;
            
            w.UpperThreshold = t2x / 2;
            var t2y = w.UpperThreshold; // 0.5
            if (!Mathf.IsEqualApprox(t2y, t2x / 2)) TestSuccess = false;
        }
        
        void ModifierInputSwizzle()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ModifierInputSwizzleGdPath).New();
            var w = Utility.CreateWrapper<GuideModifierInputSwizzle>(obj);

            var t1x = w.Order; // YXZ
            if (t1x != GuideModifierInputSwizzle.EGuideInputSwizzleOperation.YXZ) TestSuccess = false;

            w.Order = GuideModifierInputSwizzle.EGuideInputSwizzleOperation.ZYX;
            var t1y = w.Order; // ZYX
            if (t1y != GuideModifierInputSwizzle.EGuideInputSwizzleOperation.ZYX) TestSuccess = false;
        }
        
        // void ModifierMagnitude() has no calls to test
        
        void ModifierMapRange()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ModifierMapRangeGdPath).New();
            var w = Utility.CreateWrapper<GuideModifierMapRange>(obj);

            var t1x = w.ApplyClamp; // true
            if (!t1x) TestSuccess = false;
            
            w.ApplyClamp = !t1x;
            var t1y = w.ApplyClamp; // false
            if (t1y) TestSuccess = false;

            var t2x = w.InputMin; // 0
            if (!Mathf.IsEqualApprox(t2x, 0)) TestSuccess = false;

            w.InputMin = 0.1f;
            var t2y = w.InputMin; // 0.1
            if (!Mathf.IsEqualApprox(t2y, 0.1f)) TestSuccess = false;
            
            var t3x = w.InputMax; // 1
            if (!Mathf.IsEqualApprox(t3x, 1f)) TestSuccess = false;

            w.InputMax = 0.9f;
            var t3y = w.InputMax; // 0.9
            if (!Mathf.IsEqualApprox(t3y, 0.9f)) TestSuccess = false;
            
            var t4x = w.OutputMin; // 0
            if (!Mathf.IsEqualApprox(t4x, 0f)) TestSuccess = false;

            w.OutputMin = 0.1f;
            var t4y = w.OutputMin; // 0.1
            if (!Mathf.IsEqualApprox(t4y, 0.1f)) TestSuccess = false;
            
            var t5x = w.OutputMax; // 1
            if (!Mathf.IsEqualApprox(t5x, 1f)) TestSuccess = false;

            w.OutputMax = 0.9f;
            var t5y = w.OutputMax; // 0.9
            if (!Mathf.IsEqualApprox(t5y, 0.9f)) TestSuccess = false;
            
            var t6x = w.X; // true
            if (!t6x) TestSuccess = false;
            
            w.X = !t6x;
            var t6y = w.X; // false
            if (t6y) TestSuccess = false;
            
            var t7x = w.Y; // true
            if (!t7x) TestSuccess = false;
            
            w.Y = !t7x;
            var t7y = w.Y; // false
            if (t7y) TestSuccess = false;
            
            var t8x = w.Z; // true
            if (!t8x) TestSuccess = false;
            
            w.Z = !t8x;
            var t8y = w.Z; // false
            if (t8y) TestSuccess = false;
        }
        
        void ModiferNegate()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary. ModifierNegateGdPath).New();
            var w = Utility.CreateWrapper<GuideModifierNegate>(obj);
            
            var t1x = w.X; // true
            if (!t1x) TestSuccess = false;
            
            w.X = !t1x;
            var t1y = w.X; // false
            if (t1y) TestSuccess = false;
            
            var t2x = w.Y; // true
            if (!t2x) TestSuccess = false;
            
            w.Y = !t2x;
            var t2y = w.Y; // false
            if (t2y) TestSuccess = false;
            
            var t3x = w.Z; // true
            if (!t3x) TestSuccess = false;
            
            w.Z = !t3x;
            var t3y = w.Z; // false
            if (t3y) TestSuccess = false;
        }
        
        // void ModifierNormalize() has no calls to test
        
        void ModifierPositiveNegative()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ModifierPositiveNegativeGdPath).New();
            var w = Utility.CreateWrapper<GuideModifierPositiveNegative>(obj);

            var t1x = w.Range; // POSITIVE
            if (t1x != GuideModifierPositiveNegative.ELimitRange.POSITIVE) TestSuccess = false;

            w.Range = GuideModifierPositiveNegative.ELimitRange.NEGATIVE;
            var t1y = w.Range; // NEGATIVE
            if (t1y != GuideModifierPositiveNegative.ELimitRange.NEGATIVE) TestSuccess = false;
            
            var t2x = w.X; // true
            if (!t2x) TestSuccess = false;
            
            w.X = !t2x;
            var t2y = w.X; // false
            if (t2y) TestSuccess = false;
            
            var t3x = w.Y; // true
            if (!t3x) TestSuccess = false;
            
            w.Y = !t3x;
            var t3y = w.Y; // false
            if (t3y) TestSuccess = false;
            
            var t4x = w.Z; // true
            if (!t4x) TestSuccess = false;
            
            w.Z = !t4x;
            var t4y = w.Z; // false
            if (t4y) TestSuccess = false;
        }
        
        void ModifierScale()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ModifierScaleGdPath).New();
            var w = Utility.CreateWrapper<GuideModifierScale>(obj);

            var t1x = w.Scale; // (1, 1, 1)
            if (!t1x.IsEqualApprox(Vector3.One)) TestSuccess = false;

            w.Scale = Vector3.Zero;
            var t1y = w.Scale; // (0, 0, 0)
            if (!t1y.IsEqualApprox(Vector3.Zero)) TestSuccess = false;
            
            var t2x = w.ApplyDeltaTime; // false
            if (t2x) TestSuccess = false;
            
            w.ApplyDeltaTime = !t2x;
            var t2y = w.ApplyDeltaTime; // true
            if (!t2y) TestSuccess = false;
        }
        
        void ModifierVirtualCursor()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ModifierVirtualCursorGdPath).New();
            var w = Utility.CreateWrapper<GuideModifierVirtualCursor>(obj);

            var t1x = w.InitialPosition; // (0.5, 0.5)
            if (!t1x.IsEqualApprox(new Vector2(0.5f, 0.5f))) TestSuccess = false;
            
            w.InitialPosition = Vector2.Zero;
            var t1y = w.InitialPosition; // ( 0, 0)
            if (!t1y.IsEqualApprox(Vector2.Zero)) TestSuccess = false;
            
            var t2x = w.InitializeFromMousePosition; // false
            if (t2x) TestSuccess = false;
            
            w.InitializeFromMousePosition = !t2x;
            var t2y = w.InitializeFromMousePosition; // true
            if (!t2y) TestSuccess = false;
            
            var t3x = w.ApplyToMousePositionOnDeactivation; // false
            if (t3x) TestSuccess = false;
            
            w.ApplyToMousePositionOnDeactivation = !t3x;
            var t3y = w.ApplyToMousePositionOnDeactivation; // true
            if (!t3y) TestSuccess = false;
            
            var t4x = w.Speed; // (1, 1, 1)
            if (!t4x.IsEqualApprox(Vector3.One)) TestSuccess = false;

            w.Speed = Vector3.Zero;
            var t4y = w.Speed; // (0, 0, 0)
            if (!t4y.IsEqualApprox(Vector3.Zero)) TestSuccess = false;
            
            var t5x = w.ScreenScale; // LONGER_AXIS
            if (t5x != GuideModifierVirtualCursor.EScreenScale.LONGER_AXIS) TestSuccess = false;

            w.ScreenScale = GuideModifierVirtualCursor.EScreenScale.SHORTER_AXIS;
            var t5y = w.ScreenScale; // SHORTER_AXIS
            if (t5y != GuideModifierVirtualCursor.EScreenScale.SHORTER_AXIS) TestSuccess = false;
            
            var t6x = w.ApplyDeltaTime; // true
            if (!t6x) TestSuccess = false;
            
            w.ApplyDeltaTime = !t6x;
            var t6y = w.ApplyDeltaTime; // false
            if (t6y) TestSuccess = false;
        }

        // void ModifierWindowRelative() has no calls to test
    }
    
    private void TestRemapping(GuideInputDetector detector)
    {
        InputDetector();
        Remapper();
        RemappingConfig();

        return;
        void InputDetector()
        {
            var t1x = detector.DetectionCountdownSeconds; // 0.5
            if (!Mathf.IsEqualApprox(t1x, 0.5)) TestSuccess = false;

            detector.DetectionCountdownSeconds = 1;
            var t1y = detector.DetectionCountdownSeconds; // 1.0
            if (!Mathf.IsEqualApprox(t1y, 1f)) TestSuccess = false;
            
            var t2x = detector.MinimumAxisAmplitude; // 0.2
            if (!Mathf.IsEqualApprox(t2x, 0.2f)) TestSuccess = false;

            detector.MinimumAxisAmplitude = 1;
            var t2y = detector.MinimumAxisAmplitude; // 1.0
            if (!Mathf.IsEqualApprox(t2y, 1f)) TestSuccess = false;

            var t3x = detector.GetAbortDetectionOn(); // count = 0
            if (t3x.Count != 0) TestSuccess = false;

            var inpObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputMouseButtonGdPath).New();
            List<GuideInput> inputs = [ Utility.CreateWrapper<GuideInput>(inpObj) ]; 
            detector.SetAbortDetectionOn(inputs);
            var t3y = detector.GetAbortDetectionOn(); // count = 1
            if (t3y.Count != 1) TestSuccess = false;

            var t4x = detector.UseJoyIndex; // ANY
            if (t4x != GuideInputDetector.EJoyIndex.ANY) TestSuccess = false;

            detector.UseJoyIndex = GuideInputDetector.EJoyIndex.DETECTED;
            var t5y = detector.UseJoyIndex; // DETECTED
            if (t5y != GuideInputDetector.EJoyIndex.DETECTED) TestSuccess = false;

            var t6x = detector.AllowTriggersForBooleanActions; // true
            if (!t6x) TestSuccess = false;
            
            detector.AllowTriggersForBooleanActions = !t6x;
            var t6y = detector.AllowTriggersForBooleanActions; // false
            if (t6y) TestSuccess = false;

            detector.DetectionStarted += OnSignal;
            detector.DetectionStarted -= OnSignal;

            void OnDetected(object sender, GuideInput e) { }
            detector.InputDetected += OnDetected;
            detector.InputDetected -= OnDetected;

            var t7x = detector.IsDetecting; // false
            if (t7x) TestSuccess = false;

            List<GuideInput.EDeviceType> types = [ GuideInput.EDeviceType.KEYBOARD ];
            detector.DetectBool();
            detector.AbortDetection();
            
            detector.DetectBool(types);
            detector.AbortDetection();
            
            detector.DetectAxis1d();
            detector.AbortDetection();
            
            detector.DetectAxis1d(types);
            detector.AbortDetection();
            
            detector.DetectAxis2d();
            detector.AbortDetection();
            
            detector.DetectAxis2d(types);
            detector.AbortDetection();
            
            detector.DetectAxis3d();
            detector.AbortDetection();
            
            detector.DetectAxis3d(types);
            detector.AbortDetection();
            
            detector.Detect(GuideAction.EGuideActionValueType.BOOL);
            detector.AbortDetection();
            
            detector.Detect(GuideAction.EGuideActionValueType.AXIS_1D, types);
            detector.AbortDetection();
        }
        
        void Remapper()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.RemapperGdPath).New();
            var w = Utility.CreateWrapper<GuideRemapper>(obj);

            void OnChanged(object sender, ItemChangedArgs e) { }
            w.ItemChanged += OnChanged;
            w.ItemChanged -= OnChanged;
            
            // creates Remapping Config
            var cfgObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.RemappingConfigGdPath).New();
            var remap = Utility.CreateWrapper<GuideRemappingConfig>(cfgObj);
            
            // creates GuideInputKey
            var iKey = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputKeyGdPath).New();
            var inpKey = Utility.CreateWrapper<GuideInputKey>(iKey);
            inpKey.Key = Key.A;
            
            // creates remappable InputMapping list
            var iMapObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputMappingGdPath).New();
            var inpMap = Utility.CreateWrapper<GuideInputMapping>(iMapObj);
            inpMap.Input = inpKey;
            inpMap.IsRemappable = true;
            List<GuideInputMapping> inpMaps = [ inpMap ];
            
            // creates Action
            var actObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionGdPath).New();
            var act = Utility.CreateWrapper<GuideAction>(actObj);
            act.IsRemappable = true;
            act.DisplayCategory = "TestOne";
            act.DisplayName = "TestTwo";
            
            // creates ActionMapping
            var aMapObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionMappingGdPath).New();
            var actMap = Utility.CreateWrapper<GuideActionMapping>(aMapObj);
            actMap.SetInputMappings(inpMaps);
            actMap.Action = act;
            List<GuideActionMapping> actMaps = [ actMap ];
            
            // creates MappingContext
            var mcObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.MappingContextGdPath).New();
            var mc = Utility.CreateWrapper<GuideMappingContext>(mcObj);
            mc.SetMappings(actMaps);
            List<GuideMappingContext> mcs = [ mc ];
            
            w.Initialize(mcs, remap);

            var t1x = w.GetMappingConfig(); // returns a copy so just not null
            if (t1x is null) TestSuccess = false;
            
            w.SetCustomData(99, "ninety nine");
            var t2x = w.GetCustomData(99, "bob").AsString(); // "one"
            if (t2x != "ninety nine") TestSuccess = false;
            
            w.RemoveCustomData(99);
            var t2y = w.GetCustomData(99, "bob").AsString(); // "bob"
            if (t2y != "bob") TestSuccess = false;

            var t3x = w.GetRemappableItems(); // count = 1
            if (t3x.Count != 1) TestSuccess = false;
            
            var cfgItem = t3x[0];
            
            var t4x = w.GetInputCollisions(cfgItem, inpKey); // count = 0
            if (t4x.Count != 0) TestSuccess = false;

            var t5x = w.GetBoundInputOrNull(cfgItem); // 'inpKey' wrapper
            if (t5x.BaseGuideObject != inpKey.BaseGuideObject) TestSuccess = false;
            
            w.SetBoundInput(cfgItem, inpKey);

            var t6x = w.GetDefaultInput(cfgItem);
            if (t6x != inpKey) TestSuccess = false;

            w.RestoreDefaultFor(cfgItem);
            
            // ConfigItem tests

            void OnCfgItemChanged(object sender, GuideInput e) { }
            cfgItem.Changed += OnCfgItemChanged;
            cfgItem.Changed -= OnCfgItemChanged;

            var t7x = cfgItem.DisplayCategory; // "TestOne"
            if (t7x != "TestOne") TestSuccess = false;

            var t8x = cfgItem.DisplayName; // "TestTwo"
            if (t8x != "TestTwo") TestSuccess = false;
            
            var t9x = cfgItem.IsRemappable; // True
            if (!t9x) TestSuccess = false;

            var t10x = cfgItem.ValueType; // BOOL
            if (t10x != GuideAction.EGuideActionValueType.BOOL) TestSuccess = false;

            var t11x = cfgItem.Context; // 'mc' wrapper
            if (t11x.BaseGuideObject != mc.BaseGuideObject) TestSuccess = false;
            
            var mcObjDiff = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.MappingContextGdPath).New();
            var mcDiff = Utility.CreateWrapper<GuideMappingContext>(mcObjDiff);
            
            cfgItem.Context = mcDiff;
            var t11y = cfgItem.Context; // 'mcDiff' wrapper
            if (t11y.BaseGuideObject != mcDiff.BaseGuideObject) TestSuccess = false;

            var t12x = cfgItem.Action; // not null
            if (t12x.BaseGuideObject is null) TestSuccess = false;
            
            cfgItem.Action = act;
            var t12y = cfgItem.Action; // 'act' wrapper
            if (t12y.BaseGuideObject != act.BaseGuideObject) TestSuccess = false;

            var t13x = cfgItem.Index; // 0
            if (t13x != 0) TestSuccess = false;

            cfgItem.Index = 2;
            var t13y = cfgItem.Index; // 2
            if (t13y != 2) TestSuccess = false;

            var t14x = cfgItem.IsSameAs(cfgItem); // True
            if (!t14x) TestSuccess = false;
        }
        
        void RemappingConfig()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.RemappingConfigGdPath).New();
            var w = Utility.CreateWrapper<GuideRemappingConfig>(obj);

            var t1x = w.GetRemappedInputs(); // count = 0
            if (t1x.Count != 0) TestSuccess = false;

            var remapDict = t1x.Duplicate();
            remapDict.Add(99, "ninty nine");
            w.SetRemappedInputs(remapDict);
            var t1y = w.GetRemappedInputs(); // [ 99, "ninty nine" ]
            if (t1y[99].AsString() != "ninty nine") TestSuccess = false;

            var t2x = w.GetCustomData(); // count = 0
            if (t2x.Count != 0) TestSuccess = false;
            
            var dataDict = t2x.Duplicate();
            dataDict.Add(99, "ninty nine");
            w.SetCustomData(dataDict);
            var t2y = w.GetCustomData(); // [ 99, "ninty nine" ]
            if (t2y[99].AsString() != "ninty nine") TestSuccess = false;
        }
    }
    
    private void TestTriggers()
    {
        TriggerChordedAction();
        TriggerCombo();
        TriggerComboCancelAction();
        TriggerComboStep();
        TriggerHold();
        TriggerPulse();
        TriggerStability();
        TriggerTap();
        
        return;
        void TriggerChordedAction()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerChordedActionGdPath).New();
            var w = Utility.CreateWrapper<GuideTriggerChordedAction>(obj);
            
            // Tests inherited GuideTrigger functions once here
            var t1x = w.ActuationThreshold; // 0.5
            if (!Mathf.IsEqualApprox(t1x, 0.5f)) TestSuccess = false;

            w.ActuationThreshold = 0.8f;
            var t1y = w.ActuationThreshold; // 0.8
            if (!Mathf.IsEqualApprox(t1y, 0.8f)) TestSuccess = false;
            
            var actObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionGdPath).New();
            var act = Utility.CreateWrapper<GuideAction>(actObj);
            
            var diffObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerChordedActionGdPath).New();
            var diff = Utility.CreateWrapper<GuideTriggerChordedAction>(diffObj);
            diff.Action = act;

            var t2x = w.IsSameAs(diff); // false
            if (t2x) TestSuccess = false;

            var t2y = w.IsSameAs(w); // true
            if (!t2y) TestSuccess = false;

            // Start normal tests
            var t3x = w.Action; // null
            if (t3x.BaseGuideObject != null) TestSuccess = false;

            w.Action = act;
            var t3y = w.Action;
            if (t3y.BaseGuideObject != act.BaseGuideObject) TestSuccess = false;
        }

        void TriggerCombo()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerComboGdPath).New();
            var w = Utility.CreateWrapper<GuideTriggerCombo>(obj);

            var t1x = w.EnableDebugPrint; // false
            if (t1x) TestSuccess = false;

            w.EnableDebugPrint = !t1x;
            var t1y = w.EnableDebugPrint; // true
            if (!t1y) TestSuccess = false;

            var t2x = w.GetSteps(); // count = 0
            if (t2x.Count != 0) TestSuccess = false;
            
            var stpObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerComboStepGdPath).New();
            var step = Utility.CreateWrapper<GuideTriggerComboStep>(stpObj);
            List<GuideTriggerComboStep> steps = [ step ];

            w.SetSteps(steps);
            var t2y = w.GetSteps(); // count = 1
            if (t2y.Count != 1) TestSuccess = false;

            var t3x = w.GetCancellationActions(); // count = 0
            if (t3x.Count != 0) TestSuccess = false;
            
            var cnlObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerComboCancelActionGdPath).New();
            var cancel = Utility.CreateWrapper<GuideTriggerComboCancelAction>(cnlObj);
            List<GuideTriggerComboCancelAction> cancels = [ cancel ];
            
            w.SetCancellationActions(cancels);
            var t3y = w.GetCancellationActions(); // count = 1
            if (t3y.Count != 1) TestSuccess = false;
        }
        
        void TriggerComboCancelAction()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerComboCancelActionGdPath).New();
            var w = Utility.CreateWrapper<GuideTriggerComboCancelAction>(obj);
            
            var actObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionGdPath).New();
            var act = Utility.CreateWrapper<GuideAction>(actObj);

            var t1x = w.Action; // null
            if (t1x.BaseGuideObject is not null) TestSuccess = false;
            
            w.Action = act;
            var t1y = w.Action; // 'act' wrapper
            if (t1y.BaseGuideObject != act.BaseGuideObject) TestSuccess = false;

            var t2x = w.CompletionEvents; // TRIGGERED
            if (t2x != GuideTriggerCombo.EActionEventType.TRIGGERED) TestSuccess = false;

            w.CompletionEvents = GuideTriggerCombo.EActionEventType.COMPLETED;
            var t2y = w.CompletionEvents; // COMPLETED
            if (t2y != GuideTriggerCombo.EActionEventType.COMPLETED) TestSuccess = false;
            
            var objDiff = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerComboCancelActionGdPath).New();
            var diff = Utility.CreateWrapper<GuideTriggerComboCancelAction>(objDiff);

            var t3x = w.IsSameAs(diff); // false
            if (t3x) TestSuccess = false;
            
            var t3y = w.IsSameAs(w); // true
            if (!t3y) TestSuccess = false;
        }
        
        void TriggerComboStep()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerComboStepGdPath).New();
            var w = Utility.CreateWrapper<GuideTriggerComboStep>(obj);

            var actObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionGdPath).New();
            var act = Utility.CreateWrapper<GuideAction>(actObj);

            var t1x = w.Action; // null
            if (t1x.BaseGuideObject is not null) TestSuccess = false;
            
            w.Action = act;
            var t1y = w.Action; // 'act' wrapper
            if (t1y.BaseGuideObject != act.BaseGuideObject) TestSuccess = false;
            
            var t2x = w.CompletionEvents; // TRIGGERED
            if (t2x != GuideTriggerCombo.EActionEventType.TRIGGERED) TestSuccess = false;

            w.CompletionEvents = GuideTriggerCombo.EActionEventType.COMPLETED;
            var t2y = w.CompletionEvents; // COMPLETED
            if (t2y != GuideTriggerCombo.EActionEventType.COMPLETED) TestSuccess = false;
            
            var t3x = w.TimeToActuate; // 0.5
            if (!Mathf.IsEqualApprox(t3x, 0.5f)) TestSuccess = false;

            w.TimeToActuate = 0.8f;
            var t3y = w.TimeToActuate; // 0.8
            if (!Mathf.IsEqualApprox(t3y, 0.8f)) TestSuccess = false;
            
            var objDiff = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerComboStepGdPath).New();
            var diff = Utility.CreateWrapper<GuideTriggerComboStep>(objDiff);

            var t4x = w.IsSameAs(diff); // false
            if (t4x) TestSuccess = false;
            
            var t4y = w.IsSameAs(w); // true
            if (!t4y) TestSuccess = false;
        }
        
        // void TriggerDown has no calls to test
        
        // void TriggerHair has no calls to test
        
        void TriggerHold()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerHoldGdPath).New();
            var w = Utility.CreateWrapper<GuideTriggerHold>(obj);
            
            var t1x = w.HoldThreshold; // 1
            if (!Mathf.IsEqualApprox(t1x, 1f)) TestSuccess = false;

            w.HoldThreshold = 0.5f;
            var t1y = w.HoldThreshold; // 0.5
            if (!Mathf.IsEqualApprox(t1y, 0.5f)) TestSuccess = false;
            
            var t2x = w.IsOneShot; // false
            if (t2x) TestSuccess = false;

            w.IsOneShot = !t2x;
            var t2y = w.IsOneShot; // true
            if (!t2y) TestSuccess = false;
        }
        
        // void TriggerPressed has no calls to test
        
        void TriggerPulse()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerPulseGdPath).New();
            var w = Utility.CreateWrapper<GuideTriggerPulse>(obj);
            
            var t1x = w.TriggerOnStart; // true
            if (!t1x) TestSuccess = false;

            w.TriggerOnStart = !t1x;
            var t1y = w.TriggerOnStart; // false
            if (t1y) TestSuccess = false;
            
            var t2x = w.InitialDelay; // 0.3
            if (!Mathf.IsEqualApprox(t2x, 0.3f)) TestSuccess = false;

            w.InitialDelay = 1f;
            var t2y = w.InitialDelay; // 1
            if (!Mathf.IsEqualApprox(t2y, 1f)) TestSuccess = false;
            
            var t3x = w.PulseInterval; // 0.1
            if (!Mathf.IsEqualApprox(t3x, 0.1f)) TestSuccess = false;

            w.PulseInterval = 1f;
            var t3y = w.PulseInterval; // 1
            if (!Mathf.IsEqualApprox(t3y, 1f)) TestSuccess = false;
            
            var t4x = w.MaxPulses; // 0
            if (t4x != 0) TestSuccess = false;

            w.MaxPulses = 1;
            var t4y = w.MaxPulses; // 1
            if (t4y != 1) TestSuccess = false;
        }
        
        // void TriggerReleased has no calls to test
        
        void TriggerStability()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerStabilityGdPath).New();
            var w = Utility.CreateWrapper<GuideTriggerStability>(obj);

            var t1x = w.MaxDeviation; // 1.0
            if (!Mathf.IsEqualApprox(t1x, 1f)) TestSuccess = false;
            
            w.MaxDeviation = 2f;
            var t1y = w.MaxDeviation; // 2
            if (!Mathf.IsEqualApprox(t1y, 2f)) TestSuccess = false;

            var t2x = w.TriggerWhen; // INPUT_IS_STABLE
            if (t2x != GuideTriggerStability.ETriggerWhen.INPUT_IS_STABLE) TestSuccess = false;

            w.TriggerWhen = GuideTriggerStability.ETriggerWhen.INPUT_CHANGES;
            var t2y = w.TriggerWhen; // INPUT_CHANGES
            if (t2y != GuideTriggerStability.ETriggerWhen.INPUT_CHANGES) TestSuccess = false;
        }
        
        void TriggerTap()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TriggerTapGdPath).New();
            var w = Utility.CreateWrapper<GuideTriggerTap>(obj);

            var t1x = w.TapThreshold; // 0.2
            if (!Mathf.IsEqualApprox(t1x, 0.2f)) TestSuccess = false;
            
            w.TapThreshold = 0.5f;
            var t1y = w.TapThreshold; // 0.5
            if (!Mathf.IsEqualApprox(t1y, 0.5f)) TestSuccess = false;
        }
    }
    
    private void TestUi()
    {
        IconRenderer();
        InputFormatter();
        InputFormattingOptions();
        TextProvider();

        return;
        void IconRenderer()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.IconRendererGdPath).New();
            var w = Utility.CreateWrapper<GuideIconRenderer>(obj);

            var t1x = w.Priority; // 0
            if (t1x != 0) TestSuccess = false;

            w.Priority = 3;
            var t1y = w.Priority; // 3
            if (t1y != 3) TestSuccess = false;
            
            var inpObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputKeyGdPath).New();
            var inpKey = Utility.CreateWrapper<GuideInput>(inpObj);
            
            var optObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputFormattingOptionsGdPath).New();
            var formOpt = Utility.CreateWrapper<GuideInputFormattingOptions>(optObj);
            
            var t2x = w.Supports(inpKey, formOpt); // false
            if (t2x) TestSuccess = false;
            
            w.Render(inpKey, formOpt);

            // This will throw an error as we are not making a custom renderer. Can uncomment to verify call works if
            // desired.
            // var t3x = w.CacheKey(inpKey, formOpt); // "i-forgot-the-cache-key"
            // if (t3x != "i-forgot-the-cache-key") TestSuccess = false;
        }

        async void InputFormatter()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputFormatterGdPath).New();
            var w = Utility.CreateWrapper<GuideInputFormatter>(obj);

            var t1x = w.FormattingOptions; // not null
            if (t1x.BaseGuideObject is null) TestSuccess = false;
            
            var optObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputFormattingOptionsGdPath).New();
            var formOpt = Utility.CreateWrapper<GuideInputFormattingOptions>(optObj);

            w.FormattingOptions = formOpt;
            var t1y = w.FormattingOptions; // 'formOpt' wrapper
            if (t1y.BaseGuideObject != formOpt.BaseGuideObject) TestSuccess = false;
            
            GuideInputFormatter.Cleanup();
            
            var rendObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.IconRendererGdPath).New();
            var rend = Utility.CreateWrapper<GuideIconRenderer>(rendObj);
            
            GuideInputFormatter.AddIconRenderer(rend);
            GuideInputFormatter.RemoveIconRenderer(rend);
            
            var textObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TextProviderGdPath).New();
            var text = Utility.CreateWrapper<GuideTextProvider>(textObj);
            
            GuideInputFormatter.AddTextProvider(text);
            GuideInputFormatter.RemoveTextProvider(text);

            var t2x = GuideInputFormatter.ForActiveContexts(); // not null
            if (t2x.BaseGuideObject is null) TestSuccess = false;
            
            var mcObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.MappingContextGdPath).New();
            var mc = Utility.CreateWrapper<GuideMappingContext>(mcObj);
            
            var t3x = GuideInputFormatter.ForContext(mc); // not null
            if (t3x.BaseGuideObject is null) TestSuccess = false;
            
            var actObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.ActionGdPath).New();
            var act = Utility.CreateWrapper<GuideAction>(actObj);

            var t4x = await w.ActionAsRichTextAsync(act); // ""
            if (t4x != "") TestSuccess = false;
            
            var t5x = w.ActionAsText(act); // ""
            if (t5x != "") TestSuccess = false;
            
            var keyObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputKeyGdPath).New();
            var inpKey = Utility.CreateWrapper<GuideInputKey>(keyObj);
            inpKey.Key = Key.A;
            
            var t6x = await w.InputAsRichtextAsync(inpKey); // contains "[img]"
            if (!t6x.Contains("[img]")) TestSuccess = false;
           
            var t7x = w.InputAsText(inpKey); // "[A]"
            if (t7x != "[A]") TestSuccess = false;
        }
        
        void InputFormattingOptions()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputFormattingOptionsGdPath).New();
            var w = Utility.CreateWrapper<GuideInputFormattingOptions>(obj);

            var t1x = w.InputFilter; // not null, cant generically test. will just look for error
            w.InputFilter = new Callable(this, "OnSignal");

            var t2x = w.JoyRendering; // DEFAULT
            if (t2x != GuideInputFormattingOptions.EJoyRendering.DEFAULT) TestSuccess = false;

            w.JoyRendering = GuideInputFormattingOptions.EJoyRendering.PREFER_JOY_TYPE;
            var t2y = w.JoyRendering; // PREFER_JOY_TYPE
            if (t2y != GuideInputFormattingOptions.EJoyRendering.PREFER_JOY_TYPE) TestSuccess = false;
            
            var t3x = w.PreferredJoyType; // GENERIC_JOY
            if (t3x != GuideInputFormattingOptions.EJoyType.GENERIC_JOY) TestSuccess = false;

            w.PreferredJoyType = GuideInputFormattingOptions.EJoyType.SONY_CONTROLLER;
            var t3y = w.PreferredJoyType; // SONY_CONTROLLER
            if (t3y != GuideInputFormattingOptions.EJoyType.SONY_CONTROLLER) TestSuccess = false;
        }
        
        void TextProvider()
        {
            var obj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.TextProviderGdPath).New();
            var w = Utility.CreateWrapper<GuideTextProvider>(obj);
            
            var t1x = w.Priority; // 0
            if (t1x != 0) TestSuccess = false;

            w.Priority = 3;
            var t1y = w.Priority; // 3
            if (t1y != 3) TestSuccess = false;
            
            var inpObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputKeyGdPath).New();
            var inpKey = Utility.CreateWrapper<GuideInput>(inpObj);
            
            var optObj = (GodotObject)ResourceLoader.Load<GDScript>(ResourceLibrary.InputFormattingOptionsGdPath).New();
            var formOpt = Utility.CreateWrapper<GuideInputFormattingOptions>(optObj);
            
            var t2x = w.Supports(inpKey, formOpt); // false
            if (t2x) TestSuccess = false;
            
            var t2y = w.GetText(inpKey, formOpt); // "not implemented"
            if (t2y != "not implemented") TestSuccess = false;
        }
    }
    
    
    
    private void OnSignal() { }
}