using System;
using System.Collections.Generic;
using Godot;
using static GuideCs.ResourceLibrary;

namespace GuideCs;

/// <summary>Arg data for ItemChanged event.</summary>
public record ItemChangedArgs(ConfigItem ConfigItem, GuideInput GuideInput);

/// <summary>Wrapper for Remappers. Must be assigned a GUIDERemapper to function.</summary>
public partial class GuideRemapper : GuideResource
{
    public GuideRemapper(GodotObject gdRemapper) : base(gdRemapper) { }

    public GuideRemapper()
    { LoadAndConnectBaseGuideResource($"{RemapperGdPath}"); }
    
    private event EventHandler<ItemChangedArgs> ItemChangedInvoker;
    /// <summary> Emitted when the bound input of an item changes.</summary>
    public event EventHandler<ItemChangedArgs> ItemChanged
    {
        add
        {
            if (ItemChangedInvoker is null || ItemChangedInvoker.GetInvocationList().Length == 0)
            { 
                Utility.ModifySignalConnection(BaseGuideObject, "item_changed",
                Callable.From((Action<GodotObject, GodotObject>)ItemChangedResolver), true); 
            }
            
            ItemChangedInvoker += value;
        }
        
        remove
        {
            ItemChangedInvoker -= value;
            
            if (ItemChangedInvoker is null || ItemChangedInvoker.GetInvocationList().Length == 0)
            {
                Utility.ModifySignalConnection(BaseGuideObject, "item_changed",
                Callable.From((Action<GodotObject, GodotObject>)ItemChangedResolver), false); 
            }
        }
    }
    
    /// <summary>Wraps args from ItemChanged return for signal invoke.</summary>
    private void ItemChangedResolver(GodotObject configItem, GodotObject guideInput)
    {
        var wConfig = Utility.GetCachedOrNew<ConfigItem>(configItem);
        var wInput = Utility.GetCachedOrNew<GuideInput>(guideInput);
        
        ItemChangedInvoker?.Invoke(this, new ItemChangedArgs(wConfig, wInput));
    }

    /// <summary>Loads the default bindings as they are currently configured in the mapping contexts and a mapping config
    /// for editing. Note that the given mapping config will not be modified, so editing can be canceled. Call
    /// <see cref="GetMappingConfig"/> to get the modified mapping config.</summary>
    /// <param name="mappingContexts">List of Mapping Contexts to check for rebindable actions.</param>
    /// <param name="remappingConfig">Config file to use for this remapper.</param>
    public void Initialize(List<GuideMappingContext> mappingContexts, GuideRemappingConfig remappingConfig)
    {
        Godot.Collections.Array baseArray = [];
        foreach (var wmc in mappingContexts)
        {
            baseArray.Add(wmc.BaseGuideObject);
        }
        
        Utility.ModificationWarning($"{nameof(GuideRemapper)}.{nameof(Initialize)}", "remote_initialize",
            BaseGuideObject);
        
        // Unable to send a custom-typed array through wrapper functions. Navigate to the 'guide_remapper.gd' file and
        // add the function below:
        
        // func remote_initialize(mapping_contexts:Array, remapping_config:GUIDERemappingConfig) -> void:
        //     var typed_array: Array[GUIDEMappingContext] = []
        //     typed_array.assign(mapping_contexts)
        //     initialize(typed_array, remapping_config)
        
        BaseGuideObject.Call("remote_initialize", baseArray, remappingConfig.BaseGuideObject);
    }
    
    /// <summary>Returns a copy of the mapping config with all modifications applied.</summary>
    public GuideRemappingConfig GetMappingConfig()
    {
        var obj = BaseGuideObject.Call("get_mapping_config").As<GodotObject>();
        return Utility.GetCachedOrNew<GuideRemappingConfig>(obj);
    }
    
    /// <summary>Adds entry in the repository dictionary for additional custom data to store (e.g. modifier settings, etc.) <br />
    /// Note: This data is completely under application control and is the responsibility of the application to ensure that
    /// this data is serializable and gets applied at the necessary point in time.</summary>
    /// <param name="key">New custom data dictionary entry key.</param>
    /// <param name="value">New custom data dictionary entry value.</param>
    public void SetCustomData(Variant key, Variant value) => BaseGuideObject.Call("set_custom_data", key, value);

    /// <summary>Returns custom data stored at [key].</summary>
    /// <param name="key">Key to retrieve entry at.</param>
    /// <param name="default">If the param key does not exist, returns param default, or null if the parameter is omitted.</param>
    /// <returns>Returns a variant containing the entry, if found.</returns>
    public Variant GetCustomData(Variant key, Variant? @default = null)
    {
        // GDScript can handle a null variant but C# cannot. The @default is made nullable so, if it is not null, use it, 
        if (@default is { } v)
        { return BaseGuideObject.Call("get_custom_data", key, v); }
        
        // otherwise we just omit sending the @default arg and let GUIDE use its null default
        return BaseGuideObject.Call("get_custom_data", key);
    }

    /// <summary>Removes the 'key' entry from the custom data dictionary, if it exists.</summary>
    public void RemoveCustomData(Variant key) => BaseGuideObject.Call("remove_custom_data", key);

    /// <summary>Returns all remappable items. Can be filtered by context, display category and/or action.</summary>
    /// <param name="context">Optional: Filters items that are within the specified context.</param>
    /// <param name="displayCategory">Optional: Filters items with the specified display category.</param>
    /// <param name="action">Optional: Filters items of the specified action.</param>
    /// <returns>All configurations that are within the filter constraints.</returns>
    public List<ConfigItem> GetRemappableItems(GuideMappingContext context = null, string displayCategory = "", GuideAction action = null)
    {
        GodotObject mc = null;
        GodotObject act = null;
        List<ConfigItem> wItems = [];
        
        if (context is not null)
        { mc = context.BaseGuideObject; }

        if (action is not null)
        { act = action.BaseGuideObject; }
        
        var baseArray = BaseGuideObject.Call("get_remappable_items", mc, displayCategory, act).AsGodotArray<GodotObject>();
        
        foreach (var obj in baseArray)
        {
            wItems.Add(Utility.GetCachedOrNew<ConfigItem>(obj));
        }
        
        return wItems;
    }

    /// <summary>Returns a list of all collisions in all contexts when this new input would be applied to the config item.</summary>
    /// <param name="item">ConfigItem to check for conflicts.</param>
    /// <param name="input">GuideInput class to examine.</param>
    public List<ConfigItem> GetInputCollisions(ConfigItem item, GuideInput input)
    {
        var baseArray = BaseGuideObject.Call("get_input_collisions", item.BaseGuideObject, 
            input.BaseGuideObject).AsGodotArray<GodotObject>();
        
        List<ConfigItem> wItems = [];
        foreach (var obj in baseArray)
        {
            wItems.Add(Utility.GetCachedOrNew<ConfigItem>(obj));
        }
        
        return wItems;
    }

    /// <summary>Returns the input currently bound to the action in the given context. Can be null if the input is currently
    /// not bound.</summary>
    public GuideInput GetBoundInputOrNull(ConfigItem item)
    {
        var obj = BaseGuideObject.Call("get_bound_input_or_null", item.BaseGuideObject).As<GodotObject>();
        if (obj is null)
        { return null; }
        
        return Utility.GetCachedOrNew<GuideInput>(obj);
    }

    /// <summary> Sets the bound input to the new value for the given config item. Ignores collisions because collision
    /// resolution is highly game specific. Use <see cref="GetInputCollisions"/> to find potential collisions and then
    /// resolve them in a way that suits the game.<br />
    /// Note: Bound input can be set to null, which deliberately unbinds the input. If you want to restore the defaults,
    /// call <see cref="RestoreDefaultFor"/> instead.</summary>
    /// <param name="item">Config to bind.</param>
    /// <param name="input">Input to modify.</param>
    public void SetBoundInput(ConfigItem item, GuideInput input) => BaseGuideObject.Call("set_bound_input", item.BaseGuideObject, Utility.GetBaseOrNull(input));

    /// <summary>Returns the default binding for the given config item.</summary>
    public GuideInput GetDefaultInput(ConfigItem item)
    {
        var obj = BaseGuideObject.Call("get_default_input", item.BaseGuideObject).As<GodotObject>();
        return Utility.GetCachedOrNew<GuideInput>(obj);
    }

    /// <summary>Restores the default binding for the given config item.<br />
    /// Note: This may introduce a conflict if other bindings have bound conflicting input. Call <see cref="GetBoundInputOrNull"/>
    /// for the given item to get the default input and then call <see cref="GetInputCollisions"/>
    /// for that to find out whether you would get a collision.</summary>
    public void RestoreDefaultFor(ConfigItem item) => BaseGuideObject.Call("restore_default_for", item.BaseGuideObject);
}

/// <summary>Subclass for GuideRemapper. Contains data about the action to be remapped.</summary>
public partial class ConfigItem : GuideResource
{
    public ConfigItem(GodotObject baseConfigItem)
    { ConnectBaseGuideResource(baseConfigItem); }

    public ConfigItem() { }

    private event EventHandler<GuideInput> ChangedInvoker;
    /// <summary>Emitted when the input to this item has changed.</summary>
    public new event EventHandler<GuideInput> Changed
    {
        add
        {
            if (ChangedInvoker is null || ChangedInvoker.GetInvocationList().Length == 0)
            { 
                Utility.ModifySignalConnection(BaseGuideObject, "changed",
                Callable.From((Action<GodotObject>)ChangedResolver), true); 
            }
            
            ChangedInvoker += value;
        }
        
        remove
        {
            ChangedInvoker -= value;
            
            if (ChangedInvoker is null || ChangedInvoker.GetInvocationList().Length == 0)
            { 
                Utility.ModifySignalConnection(BaseGuideObject, "changed",
                Callable.From((Action<GodotObject>)ChangedResolver), false);
            }
        }
    }
    
    /// <summary>Wraps the GuideInput from Changed invoke to pass to listeners.</summary>
    private void ChangedResolver(GodotObject guideInput)
    {
        var wr = Utility.GetCachedOrNew<GuideInput>(guideInput);
        ChangedInvoker?.Invoke(this, wr);
    }

    /// <summary>The display category for this config item.</summary>
    public string DisplayCategory => BaseGuideObject.Get("display_category").AsString();

    /// <summary>The display name for this config item.</summary>
    public string DisplayName => BaseGuideObject.Get("display_name").AsString();

    /// <summary>Whether this item is remappable.</summary>
    public bool IsRemappable => BaseGuideObject.Get("is_remappable").AsBool();

    /// <summary>The value type for this config item. Returns UNKNOWN if there was a failure of evaluation.</summary>
    public GuideAction.EGuideActionValueType ValueType
    {
        get
        {
            var i = BaseGuideObject.Get("value_type").AsInt32();
            if (Enum.IsDefined(typeof(GuideAction.EGuideActionValueType), i))
            { return (GuideAction.EGuideActionValueType)i; }
            
            return GuideAction.EGuideActionValueType.UNKNOWN;
        }
    }

    public GuideMappingContext Context
    {
        get
        {
            var obj = BaseGuideObject.Get("context").As<GodotObject>();
            return Utility.GetCachedOrNew<GuideMappingContext>(obj);
        }
        
        set => BaseGuideObject.Set("context", value.BaseGuideObject);
    }
    
    public GuideAction Action
    {
        get
        {
            var obj = BaseGuideObject.Get("action").As<GodotObject>();
            return Utility.GetCachedOrNew<GuideAction>(obj);
        }
        
        set => BaseGuideObject.Set("action", value.BaseGuideObject);
    }
    
    public int Index
    {
        get => BaseGuideObject.Get("index").AsInt32();
        set => BaseGuideObject.Set("index", value);
    }

    /// <summary>Checks whether this config item is the same as some other e.g. refers to the same input mapping.</summary>
    /// <param name="other">ConfigItem to check against.</param>
    /// <returns>True if context, action and index are the same.</returns>
    public bool IsSameAs(ConfigItem other) => BaseGuideObject.Call("is_same_as", other.BaseGuideObject).AsBool();
    
}