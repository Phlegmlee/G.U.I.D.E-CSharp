using Godot;
using Godot.Collections;

namespace GuideCs;

/// <summary>Wrapper for Remapping Configs. Must be assigned a GUIDERemappingConfig to function.</summary>
public partial class GuideRemappingConfig : GuideResource
{
    public GuideRemappingConfig(GodotObject gdRemapConfig) : base(gdRemapConfig) { }

    public GuideRemappingConfig()
    { LoadAndConnectBaseGuideResource(ResourceLibrary.RemappingConfigGdPath); }

    /// <summary>Gets a dictionary with remapped inputs. Structure is:
    /// <code>
    /// {
    ///     mapping_context : {
    ///         action : {
    ///             index : bound input
    ///             ...
    ///         }, ...
    /// }</code><br />
    /// The bound input can be NULL which means that this was deliberately unbound.<br /><br />
    /// Note: This is a raw dictionary whose contents are not wrapped.<br />
    /// If you need to interact with the items in this dictionary, use <see cref="Utility.GetCachedOrNew"/> to wrap them.<br /><br />
    /// Note: This is a transient dictionary that does not update GUIDE. Use <see cref="SetRemappedInputs"/> to update
    /// GUIDE after making changes to the returned dictionary.</summary>
    public Dictionary GetRemappedInputs() => BaseGuideObject.Get("remapped_inputs").AsGodotDictionary();

    /// <summary>Sets the GUIDE Dictionary with remapped inputs. Structure should be:
    /// <code>
    /// {
    ///     mapping_context : {
    ///         action : {
    ///             index : bound input
    ///             ...
    ///         }, ...
    /// }</code><br />
    /// Note: This dictionary must contain the base GUIDE resources, not the wrapped resources.</summary>
    public void SetRemappedInputs(Dictionary items) => BaseGuideObject.Set("remapped_inputs", items);
    
    /// <summary>Gets the current dictionary for additional custom data to store (e.g. modifier settings, etc.).<br />
    /// Note: This data is completely under application control and is the responsibility of the application to ensure that
    /// this data is serializable and gets applied at the necessary point in time.</summary>
    public Dictionary GetCustomData() => BaseGuideObject.Get("custom_data").AsGodotDictionary();
    
    /// <summary>Sets the current dictionary for additional custom data to store (e.g. modifier settings, etc.).<br />
    /// Note: This data is completely under application control and is the responsibility of the application to ensure that
    /// this data is serializable and gets applied at the necessary point in time.</summary>
    public void SetCustomData(Dictionary items) => BaseGuideObject.Set("custom_data", items);
    
}