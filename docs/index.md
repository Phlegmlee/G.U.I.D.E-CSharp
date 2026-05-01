---
layout: default
---

G.U.I.D.E (Godot Unified Input Detection Engine) is an extension for the Godot Engine that allows you to easily use input from multiple sources, such as keyboard, mouse, gamepad and touch in a unified way. Gone are the days, where mouse input was handled differently from joysticks and touch was a totally different beast. No matter where the input comes from - your game code works the same way.

_Note: This version utilises a C# Wrapper originally made by @DFGameDev. If you do not need C# support for G.U.I.D.E, visit the main repository [HERE](https://github.com/godotneers/G.U.I.D.E)._

---

G.U.I.D.E-CSharp has full functionality on its own, see the [changelog](CHANGES.md) for current version information.

**This plugin includes:**
- The C# wrapper
- The full guide plugin as a [sub-plugin](https://docs.godotengine.org/en/stable/tutorials/plugins/editor/making_plugins.html#using-sub-plugins).
    - The [GUIDE](https://github.com/godotneers/G.U.I.D.E) portion included in this plugin will be kept up to date with releases.

## Quick Start

1. Make sure you are using the **C# version of Godot** (Godot-Mono/GodotSharp) and it is **at least version 4.2** or greater.
1. Aquire the plugin using one of the following:
	- Use the asset browser within Godot (Search "unified")
	- [Godot Asset Store](https://godotengine.org/asset-library/asset/5104)
	- [Repository Releases](https://github.com/Phlegmlee/G.U.I.D.E-CSharp/releases)
1. Build your project, C# is a compiled language so you must build the C# portion of the plugin.
    <details>
    <img width="444" height="155" alt="image" src="https://github.com/user-attachments/assets/622f33e7-6839-4783-acc3-8e1c8f7de46b" /><br>
    <p>If build option is missing:</p>
    <img width="435" height="147" alt="image" src="https://github.com/user-attachments/assets/8abbf9c9-d116-469f-ad8b-dafdd0692f8c" /><br>
    <p>Use Project > Tools > C# > Create C# Solution</p>
    <img width = "550" height="155" alt="image" src="assets/images/godotcs_create_solution.png">
    <p>After the solution is created, the build option should show up.</p>
    </details>
1. Enable GUIDE-CSharp in your Project > Project Setting > Plugins list.
    <details>
    <img width="609" height="234" alt="plugin-enabled" src="https://github.com/user-attachments/assets/fcfd4e19-aac5-4382-b074-1fd00e740493" />
    </details>
1. Restart your project.
    <details>
    <img width="400" height="400" alt="plugin-enabled" src="assets/images/reload_project.png" />
    </details>

## Usage

G.U.I.D.E-CSharp is designed to follow all the normal behavior of G.U.I.D.E, so most of what you do with the wrapper is exactly the same other than using CamelCase instead of snake_case.

```cs
// GDScript
    GUIDE.enable_mapping_context();

// C#
    Guide.EnableMappingContext();
```

C# Editor resources are handled with a duplicate resource type, denoted by the **blood orange** color variation and a C# icon.

<img width="50" height="50" alt="image" src="assets/images/GuideActionCs.svg" />
<img width="50" height="50" alt="image" src="assets/images/GuideMappingContextCs.svg" />

Anything done directly in the editor, use GUIDE resources as normal.

Anything done in C#, use a GuideCs resource.

## Workflow Tutorial

1. Create a `GUIDEAction` or `GUIDEMappingContext`.
    <details>
      <img width="349" height="423" alt="image" src="https://github.com/user-attachments/assets/d45c3379-4858-472f-a83d-93cc73961eeb" />
    </details>
1. The matching C# wrapper is created automatically.
    <details>
      <img width="343" height="514" alt="image" src="https://github.com/user-attachments/assets/7ee170e4-76e8-471f-af4b-e2f81f135182" />
    </details>
1. The C# wrapper takes the `GUIDEAction` or `GUIDEMappingContext` as an export parameter. This is assigned automatically.
    <details>
      <img width="877" height="560" alt="image" src="https://github.com/user-attachments/assets/5fdbcb79-21ff-4593-bfa9-cb091c3cafb9" />
    </details>
1. In your C# script, create your `[Export]` variables as GuideCs types.
    <details>
      <img width="436" height="307" alt="image" src="https://github.com/user-attachments/assets/489268df-5cc0-46ce-b929-2d24c3e31949" />
    </details>
1. Assign them in the editor with your GuideCs wrappers:
    <details>
      <img width="874" height="745" alt="image" src="https://github.com/user-attachments/assets/b323ebaa-b95c-45de-a9b0-530fba3f6c54" />
    </details>
1. Use them in C# as you would in GDScript, keeping in mind the differences with C#. See the [C# documentation](https://docs.godotengine.org/en/4.6/tutorials/scripting/c_sharp/index.html).
    <details>
      <img width="426" height="478" alt="image" src="https://github.com/user-attachments/assets/6443f462-9849-45c6-822a-8d2195d0380d" />
    </details>

## Code Only Examples


### No Exports

If you don't use `[Export]` properties and would prefer to load and manage your C# resources through code, you can also do so by using the `Utility.CreateWrapper()` function to generate wrapped resources:

```cs
    public Dictionary<string, GuideAction> WrappedActions;
    public override void _Ready()
    {
        Dictionary<string, string> guideActions = new ()
        {
            {"Jump", "uid://cm76dijjo3wm4"},
            {"Shoot", "uid://cki32mfnd6v7k"},
            {"walk", "uid://c0quxwokh6bm4"},
        };

        foreach (var kvp in guideActions)
        {
            var guideBaseObject = (GodotObject)ResourceLoader.Load<GDScript>(kvp.Value).New();
            var wrappedAction = Utility.CreateWrapper<GuideAction>(guideBaseObject);
            WrappedActions.TryAdd(kvp.Key, wrappedAction);
        }
        
        base._Ready();
    }
```
### Direct Creation
These examples are **NOT** the only way, there are much better ways, keeping the code examples more readable was priority here.

#### You can also directly create the wrapper depending on how you prefer to manage your resources.
```cs
    GuideResource.ConnectBaseGuideResource()

    // or
    
    GuideResource.LoadAndConnectBaseGuideResource() 
```

#### If you loaded your own object(s), use connect.
```cs
public partial class Main : Node2D
{
  public List<GodotObject> BaseGuideMappingContextsLoaded = [];

  public override void _Ready()
  {
    var wrappedMappingContext = new GuideMappingContext();
    wrappedMappingContext.ConnectBaseGuideResource(BaseGuideMappingContextsLoaded[0]);
  }
}
```

#### If you want GuideCs to load the object(s) for you, use load and connect.
```cs
public partial class Main : Node2D
{
  public List<string> BaseGuideMappingContextPaths = [];

  public override void _Ready()
  {
    var wrappedMappingContext = new GuideMappingContext();
    wrappedMappingContext.LoadAndConnectBaseGuideResource(BaseGuideMappingContextPaths[0]);
  }
}
```

#### You can also create a new object and pass the loaded GUIDE resource as a constructor parameter.
```cs
public partial class Main : Node2D
{
  public override void _Ready()
  {
    var loadedGuideObject = (GodotObject)ResourceLoader.Load<GDScript>("uid or path").New();
    wrappedMappingContext = new GuideMappingContext(loadedGuideObject);
  }
}
```

**Note: GuideCs cannot verify the GUIDE object type:**
- `get_class()` and `is_class()` return the Godot inherited class **not** `class_name`.
- This means that G.U.I.D.E-CSharp references everything as a `GodotObject`.
- You can load the GUIDE content as anything needed using casting without issue.


## Issues/Gotchas
Current problems that have yet to be solved or can't be solved.


- Being an interop layer, not everything translates smoothly from C# to GDScript and vice-versa. There are several functions that require you to modify GUIDE files and add an extra function for things to translate correctly. These are all checked on invoke and will provide an error warning if the required modifications are missing. 
  <details>
    <summary>These are the files that need modification:</summary>
    <img width="268" height="278" alt="image" src="https://github.com/user-attachments/assets/4a330e8c-34ad-45b1-9d94-2b149059d984" />
  </details>

- To require minimal modification to GUIDE, all innumerable interactions (Arrays, Dictionaries, etc.) were reduced to `Get` and `Set` operations. This is not ideal but, to make interop work with things like `.Add()`, `.Remove()`, `.Sort()`, etc. would require creating a variety of observable list managers. The value did not outweigh the potential bug introduction so it was omitted.

- `[GlobalScope]` versions of all GuideCs objects were not created (yet?), as none of them are `[Tool]` and currently don't see much value in doubling the entire resource list when most people are unlikely to create these resources in-editor. 
  - Currently only `GuideAction`, `GuideMappingContext` and `GuideInputDetector` are available in the editor by default. If you want any of the other types to be available in the editor, just add the `[GlobalScope]` tag to the C# script (and restart the editor) to be able to select them in the New > Resource list.

- Not all GUIDE content has been wrapped. Renderers, text providers and icon makers were not wrapped as, I suspect, if you want to do any work in that space, you'll be writing custom content anyway. I'll add those wrappers in the future if that sentiment changes.

- GuideCs caches all wrappers in a weakref library and reuses them where possible. However, the cache will not retain wrappers and they will be freed by garbage collection if they are not referenced elsewhere. Make sure to store wrappers you plan to reuse. Don't store unnecessarily (you don't need to store every ConfigItem) but, for example, storing only the base GUIDEActions will cause the wrapper to constantly regenerate Action wrappers when you try to do calls which will add a small but persistent performance cost.

- GuideCs assumes you are installing addons to the `res://addons/` folder. If you install to a different directory, navigate to the `/GuideCs/CsTools/ResourceLibrary.cs` file and update the `GuideInstallDirectory` path to point at the directory you installed GUIDE to. This will also most likley break the base guide plugin as well.

- GuideCs comes with a UnitTest script which will run every wrapped call and validate there are no issues. Generally, you will not need this but, if you want to use this with a different version of GUIDE or just want to make sure everything is connected properly, add and run this function on a node in your game script:
  - This needs to be added as a child to your scene to be able to test the InputDetector node. It also assumes GuideCs was installed to the addons folder. Update the path if that is not the case.

```cs
var tester = (Node)ResourceLoader.Load<CSharpScript>("res://addons/GuideCs/CsTools/GuideCsUnitTest.cs").New();
AddChild(tester);

```
