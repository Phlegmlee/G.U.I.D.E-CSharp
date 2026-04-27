using System;
using Godot;

namespace GuideCs;

/// <summary>Wrapper for Input Formatting Options. Must be assigned a GUIDEInputFormattingOptions to function.</summary>
public partial class GuideInputFormattingOptions : GuideResource
{
    public enum EJoyRendering {
        // Renders by detecting the joy type and uses the appropriate icon set for this joy type.  
        DEFAULT = 0,
        // Renders by detecting the joy type but uses the preferred joy type, if it cannot be detected. 
        PREFER_JOY_TYPE = 1,
        // Always renders joy input using the preferred joy type, no matter which type is detected.
        FORCE_JOY_TYPE = 2,
        // Fallback if failed to evaluate
        UNKNOWN = 99
    }
    
    public enum EJoyType {
        // Used for joysticks which are not controllers or as a fallback if no controller type can be determined.
        GENERIC_JOY = 0,
        // Used for Microsoft controllers (e.g. XBox). 
        MICROSOFT_CONTROLLER = 1,
        // Used for Nintendo controllers (e.g. Switch). 
        NINTENDO_CONTROLLER = 2,
        // Used for Sony controllers (e.g. PlayStation). 
        SONY_CONTROLLER = 3,
        // Used for Steam Deck controllers
        STEAM_DECK_CONTROLLER = 4,
        // Fallback if failed to evaluate
        UNKNOWN = 99
    }

    #region StaticLoader

    private static readonly GDScript InputFormattingOptionsStatic;
    static GuideInputFormattingOptions()
    { InputFormattingOptionsStatic = ResourceLoader.Load<GDScript>(ResourceLibrary.InputFormattingOptionsGdPath); }
    
    #endregion StaticLoader
    
    public GuideInputFormattingOptions(GodotObject gdRemapper) : base(gdRemapper) { }

    public GuideInputFormattingOptions() { }

    /// <summary>An input filter that shows all input. This is the default.</summary>
    static Callable InputFilterShowAll => InputFormattingOptionsStatic.Get("INPUT_FILTER_SHOW_ALL").As<Callable>();

    /// <summary> A callable that allows for filtering which parts of an input are included in the formatted output. The
    /// callable takes a formatting context:
    /// <code> options.input_filter = func(context:GUIDEInputFormatter.FormattingContext) -> bool:
    ///     # only show keyboard input
    ///     return context.input.device_type and GUIDEInput.DeviceType.KEYBOARD > 0 </code><br />
    /// If the function returns true, then the given input will be shown in the formatted output, otherwise it will be
    /// ignored.</summary>
    public Callable InputFilter
    {
        get => BaseGuideObject.Get("input_filter").As<Callable>();
        set => BaseGuideObject.Set("input_filter", value);
    }

    public EJoyRendering JoyRendering
    {
        get
        {
            var i = BaseGuideObject.Get("joy_rendering").AsInt32();
            if (Enum.IsDefined(typeof(EJoyRendering), i))
            { return (EJoyRendering)i; }

            return EJoyRendering.UNKNOWN;
        }

        set
        {
            if (value is EJoyRendering.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(EJoyRendering.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }
            
            BaseGuideObject.Set("joy_rendering", (int)value);
        }
    }

    public EJoyType PreferredJoyType
    {
        get
        {
            var i = BaseGuideObject.Get("preferred_joy_type").AsInt32();
            if (Enum.IsDefined(typeof(EJoyType), i))
            { return (EJoyType)i; }

            return EJoyType.UNKNOWN;
        }

        set
        {
            if (value is EJoyType.UNKNOWN)
            {
                GD.PushWarning($"{Enum.GetName(EJoyType.UNKNOWN)} is not a valid value and is ignored by GUIDE during set.");
                return;
            }
            
            BaseGuideObject.Set("preferred_joy_type", (int)value);
        }
    }
}