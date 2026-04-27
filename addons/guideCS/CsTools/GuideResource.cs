using System;
using System.Collections.Generic;
using Godot;

namespace GuideCs;

/// <summary>Base class for holding wrapped Guide Resource types. Does nothing on its own; use with an inherited type.</summary>
public partial class GuideResource : Resource
{
    public GuideResource()
    { VerifyBaseResource(); }
    
    public GuideResource(GodotObject obj)
    { ConnectBaseGuideResource(obj); }
    
    private ulong _cacheLookupId;
    private GodotObject _baseGuideObject;
    /// <summary>The GUIDE object file that the wrapper talks to. Can only be set if not already set.</summary>
    [Export] public GodotObject BaseGuideObject
    {
        get => _baseGuideObject;
        private set
        {
            if (_baseGuideObject is not null)
            { return; }
            
            _baseGuideObject = value;
            CacheResource(this);
        }
    }

    
    /// <summary>Allows manual connection of a GUIDE object to the wrapper. For safety, this can only be preformed once.
    /// <br /><br />
    /// WARNING: There is no type safety for GDScript object classes. Ensure you are passing a path to the correct GUIDE
    /// resource type.</summary>
    /// <param name="path">Path of the GUIDE resource to load. Use resource path, user path or UID.</param>
    /// <returns>True if the connection was successful.</returns>
    public bool LoadAndConnectBaseGuideResource(string path)
    {
        var obj = ResourceLoader.Load<GodotObject>(path);
        if (obj is GDScript script)
        { obj = script.New().AsGodotObject(); }

        return BaseGuideObject is null && ConnectBaseGuideResource(obj);
    }

    /// <summary>Allows manual connection of a loaded GUIDE object into the wrapper. For safety, this can only be preformed
    /// once.<br /><br />
    /// WARNING: There is no type safety for GDScript object classes. Ensure you are passing a path to the correct GUIDE
    /// resource type.</summary>
    /// <param name="obj">GUIDE object to connect.</param>
    /// <returns>True if the connection was successful.</returns>
    public bool ConnectBaseGuideResource(GodotObject obj)
    {
        if (BaseGuideObject is not null)
        { return false; }

        if (obj is null)
        { return false; }
        
        BaseGuideObject = obj;
        return true;
    }

    /// <summary>Helper function to verify this resource has a linked base GUIDE object by the next idle frame.</summary>
    [System.Diagnostics.Conditional("DEBUG")]
    private void VerifyBaseResource()
    {
        Callable.From(Verify).CallDeferred();
        
        return;
        void Verify()
        {
            if (BaseGuideObject is null)
            {
                GD.PushWarning($"{GetType().Name} created with no base GUIDE resource.");
            }
        }
    }

    #region ResourceCache
    
    private static Dictionary<ulong, WeakReference<GuideResource>> _lookupCache = [];

    /// <summary>Attempts to return a wrapped resource from the cache by its base GUIDE object.</summary>
    /// <param name="obj">Base GUIDE object to lookup.</param>
    /// <typeparam name="T">Type of GuideResource to return.</typeparam>
    public static T GetWrappedResourceByBase<T>(GodotObject obj) where T : GuideResource
    {
              
        // If the object is null, 
        if (obj is null)
        { return null; }
        
        // If the cache does not contain a match
        if (!_lookupCache.TryGetValue(obj.GetInstanceId(), out var weakRef))
        { return null; }

        // If the referenced object does not exist
        if (!weakRef.TryGetTarget(out var wrapped))
        { return null; }
        
        // If the wrapper is of the desired type
        if (wrapped is T typed)
        { return typed; }
        
        // If the wrapper exists but is not of the requested type.
        GD.PushWarning($"Entry found but not of type {typeof(T).Name}.");
        return null;
    }
    
    /// <summary>Attempts to cache a resource reference into the lookup table.</summary>
    /// <returns>True if the reference did not exist and was successfully added.</returns>
    private static bool CacheResource(GuideResource wrappedResource)
    {
        var id = wrappedResource.BaseGuideObject.GetInstanceId();
        if (_lookupCache.TryGetValue(id, out var weakRef))
        {
            if (weakRef.TryGetTarget(out var wrap))
            { return false; }
            
            weakRef.SetTarget(wrappedResource);
            return true;
        }
        
        _lookupCache.Add(id, new WeakReference<GuideResource>(wrappedResource));
        wrappedResource._cacheLookupId = id;
        return true;
    }
    
    /// <summary>Attempts to remove a resource from the cache.</summary>
    /// <returns>True if the resource existed and was removed.</returns>
    private static bool RemoveResource(GuideResource wrappedResource)
    {
        return _lookupCache.Remove(wrappedResource._cacheLookupId);
    }
    
    #endregion ResourceCache

    ~GuideResource()
    {
        RemoveResource(this);
    }

}