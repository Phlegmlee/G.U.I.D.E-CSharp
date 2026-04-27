using System;
using System.Collections.Generic;

namespace GuideCs;

/// <summary>Lookup table for base GUIDE script locations.</summary>
public static class ResourceLibrary
{
    /// <summary>If you install GUIDE to a different directory, change this to point to the base folder where it is
    /// installed.</summary>
    private const string GuideInstallDirectory = "res://addons/guideCS/guide";
    
    // Inputs
    public const string InputActionGdPath = $"{GuideInstallDirectory}/inputs/guide_input_action.gd";
    public const string InputAnyGdPath = $"{GuideInstallDirectory}/inputs/guide_input_any.gd";
    public const string InputJoyAxis1DGdPath = $"{GuideInstallDirectory}/inputs/guide_input_joy_axis_1d.gd";
    public const string InputJoyAxis2DGdPath = $"{GuideInstallDirectory}/inputs/guide_input_joy_axis_2d.gd";
    public const string InputJoyButtonGdPath = $"{GuideInstallDirectory}/inputs/guide_input_joy_button.gd";
    public const string InputKeyGdPath = $"{GuideInstallDirectory}/inputs/guide_input_key.gd";
    public const string InputMouseAxis1dGdPath = $"{GuideInstallDirectory}/inputs/guide_input_mouse_axis_1d.gd";
    public const string InputMouseAxis2dGdPath = $"{GuideInstallDirectory}/inputs/guide_input_mouse_axis_2d.gd";
    public const string InputMouseButtonGdPath = $"{GuideInstallDirectory}/inputs/guide_input_mouse_button.gd";
    public const string InputMousePositionGdPath = $"{GuideInstallDirectory}/inputs/guide_input_mouse_position.gd";
    public const string InputTouchAngleGdPath = $"{GuideInstallDirectory}/inputs/guide_input_touch_angle.gd";
    public const string InputTouchAxis1dGdPath = $"{GuideInstallDirectory}/inputs/guide_input_touch_axis_1d.gd";
    public const string InputTouchAxis2dGdPath = $"{GuideInstallDirectory}/inputs/guide_input_touch_axis_2d.gd";
    public const string InputTouchDistanceGdPath = $"{GuideInstallDirectory}/inputs/guide_input_touch_distance.gd";
    public const string InputTouchPositionGdPath = $"{GuideInstallDirectory}/inputs/guide_input_touch_position.gd";
    
    // Modifiers
    public const string Modifier3dCoordinatesGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_3d_coordinates.gd";
    public const string Modifier8WayDirectionGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_8_way_direction.gd";
    public const string ModifierCanvasCoordinatesGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_canvas_coordinates.gd";
    public const string ModifierCurveGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_curve.gd";
    public const string ModifierDeadzoneGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_deadzone.gd";
    public const string ModifierInputSwizzleGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_input_swizzle.gd";
    public const string ModifierMagnitudeGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_magnitude.gd";
    public const string ModifierMapRangeGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_map_range.gd";
    public const string ModifierNegateGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_negate.gd";
    public const string ModifierNormalizeGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_normalize.gd";
    public const string ModifierPositiveNegativeGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_positive_negative.gd";
    public const string ModifierScaleGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_scale.gd";
    public const string ModifierVirtualCursorGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_virtual_cursor.gd";
    public const string ModifierWindowRelativeGdPath = $"{GuideInstallDirectory}/modifiers/guide_modifier_window_relative.gd";
    
    // Remapping
    public const string RemapperGdPath = $"{GuideInstallDirectory}/remapping/guide_remapper.gd";
    public const string RemappingConfigGdPath = $"{GuideInstallDirectory}/remapping/guide_remapping_config.gd";
    public const string InputDetector = $"{GuideInstallDirectory}/remapping/guide_input_detector.gd";
    
    // Triggers
    public const string TriggerGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger.gd";
    public const string TriggerChordedActionGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_chorded_action.gd";
    public const string TriggerComboGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_combo.gd";
    public const string TriggerComboCancelActionGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_combo_cancel_action.gd";
    public const string TriggerComboStepGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_combo_step.gd";
    public const string TriggerDownGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_down.gd";
    public const string TriggerHairGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_hair.gd";
    public const string TriggerHoldGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_hold.gd";
    public const string TriggerPressedGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_pressed.gd";
    public const string TriggerPulseGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_pulse.gd";
    public const string TriggerReleasedGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_released.gd";
    public const string TriggerStabilityGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_stability.gd";
    public const string TriggerTapGdPath = $"{GuideInstallDirectory}/triggers/guide_trigger_tap.gd";
    
    // Ui
    public const string InputFormatterGdPath = $"{GuideInstallDirectory}/ui/guide_input_formatter.gd";
    public const string IconRendererGdPath = $"{GuideInstallDirectory}/ui/guide_icon_renderer.gd";
    public const string TextProviderGdPath = $"{GuideInstallDirectory}/ui/guide_text_provider.gd";
    public const string InputFormattingOptionsGdPath = $"{GuideInstallDirectory}/ui/guide_input_formatting_options.gd";
    
    // Guide
    public const string PluginCfgPath = $"{GuideInstallDirectory}/plugin.cfg";
    public const string InputMappingGdPath = $"{GuideInstallDirectory}/guide_input_mapping.gd";
    public const string ActionGdPath = $"{GuideInstallDirectory}/guide_action.gd";
    public const string MappingContextGdPath = $"{GuideInstallDirectory}/guide_mapping_context.gd";
    public const string ActionMappingGdPath = $"{GuideInstallDirectory}/guide_action_mapping.gd";
    
    public static readonly Dictionary<string, Type> GuideInputTypes = new()
    {
        {"", typeof(GuideInput) },
        {"Action", typeof(GuideInputAction) },
        {"Any Input", typeof(GuideInputAny) },
        {"Joy Axis 1D", typeof(GuideInputJoyAxis1D) },
        {"Joy Axis 2D", typeof(GuideInputJoyAxis2D) },
        {"Joy Button", typeof(GuideInputJoyButton) },
        {"Key", typeof(GuideInputKey) },
        {"Mouse Axis 1D", typeof(GuideInputMouseAxis1D) },
        {"Mouse Axis 2D", typeof(GuideInputMouseAxis2D) },
        {"Mouse Button", typeof(GuideInputMouseButton) },
        {"Mouse Position", typeof(GuideInputMousePosition) },
        {"Touch Angle", typeof(GuideInputTouchAngle) },
        {"Touch Axis1D", typeof(GuideInputTouchAxis1D) },
        {"Touch Axis2D", typeof(GuideInputTouchAxis2D) },
        {"Touch Distance", typeof(GuideInputTouchDistance) },
        {"Touch Position", typeof(GuideInputTouchPosition) },
    };
    
    public static readonly Dictionary<string, Type> GuideModifierTypes = new()
    {
        {"", typeof(GuideModifier) },
        {"3D coordinates", typeof(GuideModifier3dCoordinates) },
        {"8-way direction", typeof(GuideModifier8WayDirection) },
        {"Canvas coordinates", typeof(GuideModifierCanvasCoordinates) },
        {"Curve", typeof(GuideModifierCurve) },
        {"Deadzone", typeof(GuideModifierDeadzone) },
        {"Input Swizzle", typeof(GuideModifierInputSwizzle) },
        {"Magnitude", typeof(GuideModifierMagnitude) },
        {"Map Range", typeof(GuideModifierMapRange) },
        {"Negate", typeof(GuideModifierNegate) },
        {"Normalize", typeof(GuideModifierNormalize) },
        {"Positive/Negative", typeof(GuideModifierPositiveNegative) },
        {"Scale", typeof(GuideModifierScale) },
        {"Virtual Cursor", typeof(GuideModifierVirtualCursor) },
        {"Window relative", typeof(GuideModifierWindowRelative) },
    };
    
    public static readonly Dictionary<string, Type> GuideTriggerTypes = new()
    {
        {"GUIDETrigger", typeof(GuideTrigger) },
        {"Chorded Action", typeof(GuideTriggerChordedAction) },
        {"Combo", typeof(GuideTriggerCombo) },
        {"Down", typeof(GuideTriggerDown) },
        {"Hair", typeof(GuideTriggerHair) },
        {"Hold", typeof(GuideTriggerHold) },
        {"Pressed", typeof(GuideTriggerPressed) },
        {"Pulse", typeof(GuideTriggerPulse) },
        {"Released", typeof(GuideTriggerReleased) },
        {"Stability", typeof(GuideTriggerStability) },
        {"Tap", typeof(GuideTriggerTap) },
    };
    
    
    
}