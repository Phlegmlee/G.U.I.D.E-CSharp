using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

namespace GuideCs;

public static class Utility
{
    
    /// <summary>Utility for calling Godot functions with the 'await' keyword. This does not call the GUIDE function
    /// directly and requires a remote callable function to be added to GUIDE.</summary>
    /// <param name="obj">GUIDE object to call the async task on.</param>
    /// <param name="functionName">String name of the added remote function to call.</param>
    /// <param name="args">Optional args to pass to the async function.</param>
    /// <typeparam name="T">Return type of the GUIDE awaited function.</typeparam>
    /// <returns>A task that can be awaited in Cs.</returns>
    public static async Task<T> CallAsync<T>(GodotObject obj, string functionName, params Variant[] args)
    {
        var tcs = new TaskCompletionSource<T>();
        
        var callback = Callable.From((T ret) => tcs.SetResult(ret));
        obj.Call(functionName, args.Prepend(callback).ToArray());
        return await tcs.Task;
    }
    
    /// <summary>Helper function for safely connecting and disconnecting signals between the Guide CS wrapper and the GUIDE
    /// GDScript.</summary>
    /// <param name="target">GUIDE object with the signal connection to modify.</param>
    /// <param name="signalName">Name of the signal to modify.</param>
    /// <param name="callback">Callable data to pass to the Godot signal (typically the function to trigger on signal
    /// invoke).</param>
    /// <param name="connect">If True, will attempt to connect to the signal, otherwise will attempt to disconnect.</param>
    public static void ModifySignalConnection(GodotObject target, string signalName, Callable callback, bool connect)
    {
        if (connect)
        {
            if (target.IsConnected(signalName, callback))
            {
                // GD.PushWarning($"Unable to connect signal: {target} {signalName}: signal already connected.");
                return;
            }

            target.Connect(signalName, callback);  
        }

        else
        {
            if (!target.IsConnected(signalName, callback))
            {
                // GD.PushWarning($"Unable to disconnect signal: {target} {signalName}: signal does not exist or not connected.");
                return;
            }
            
            target.Disconnect(signalName, callback);
        }
    }
    
    /// <summary>Attempts to find an existing wrapped resource from the cache and, if not found, creates a new wrapped
    /// resource.</summary>
    /// <param name="obj">Base GUIDE object to look up.</param>
    /// <typeparam name="T">Type of wrapped resource that is desired.</typeparam>
    /// <returns>Wrapped resource of type T.</returns>
    public static T GetCachedOrNew<T>(GodotObject obj) where T : GuideResource
    {
        var res = GuideResource.GetWrappedResourceByBase<T>(obj);
        return res ?? CreateWrapper<T>(obj);
    }
    
    /// <summary>Generates a new wrapper of at least type T. Attempts to wrap as most derived class where available. This
    /// will create a wrapper of the derived class but will always return as type T regardless.</summary>
    /// <param name="obj">Base GUIDE object to wrap.</param>
    /// <typeparam name="T">Class type to wrap as. Must be of at least a root class of the object.<br />
    /// Example: If 'obj' is InputJoyButton, T can be GuideInputJoyButton, GuideInputJoyBase, or GuideInput.</typeparam>
    /// <returns>New GuideResource of type T.</returns>
    public static T CreateWrapper<T>(GodotObject obj) where T : GuideResource
    {
        Type type = null;
        
        // Skip derive check if object is null
        if (obj is null) { }
        
        // GuideInput types
        else if (typeof(T).IsAssignableFrom(typeof(GuideInput)))
        {
            var name = obj.Call("_editor_name").AsString();
            if (!ResourceLibrary.GuideInputTypes.TryGetValue(name, out type))
            {
                GD.PushWarning($"Unknown {nameof(GuideInput)} type.");
            }
        }
        
        // GuideModifier types
        else if (typeof(T).IsAssignableFrom(typeof(GuideModifier)))
        {
            var name = obj.Call("_editor_name").AsString();
            if (!ResourceLibrary.GuideModifierTypes.TryGetValue(name, out type))
            {
                GD.PushWarning($"Unknown {nameof(GuideModifier)} type.");
            }
        }
        
        // GuideTrigger types
        else if (typeof(T).IsAssignableFrom(typeof(GuideTrigger)))
        {
            var name = obj.Call("_editor_name").AsString();
            if (!ResourceLibrary.GuideTriggerTypes.TryGetValue(name, out type))
            {
                GD.PushWarning($"Unknown {nameof(GuideTrigger)} type.");
            }
        }

        type ??= typeof(T);
    
        return Activator.CreateInstance(type, obj) as T;
    }

    /// <summary>Helper function to retrieve the base GUIDE object from a wrapper or null. Useful when the wrapped resource
    /// itself could be null but the function that wants the object out of the wrapper can handle null inputs.</summary>
    public static GodotObject GetBaseOrNull(GuideResource wrapper)
    {
        GodotObject obj = null;
        if (wrapper is not null)
        {
            obj = wrapper.BaseGuideObject;
        }
        
        return obj;
    }

    /// <summary>Used to give a warning that base GUIDE GDScript files require a modification for the specified call to
    /// function properly.</summary>
    /// <param name="where">Name of the call where the warning occured.</param>
    /// <param name="funcName">Name of the GDScript function to verify.</param>
    /// <param name="target">GUIDE object to check for the new GDScript.</param>
    [System.Diagnostics.Conditional("DEBUG")]
    public static void ModificationWarning(string where, string funcName, GodotObject target)
    {
        if (!target.HasMethod(funcName))
        {
            GD.PushWarning
            (
                $"This GuideCs call requires direct modification to the base GUIDE GDScript files to function properly " +
                $"but is missing the required addition. Navigate to '{where}' to verify the requirements for this call."
            );
            
        }
    }
}