# G.U.I.D.E C# - Godot Unified Input Detection Engine C#

G.U.I.D.E is an extension for the Godot Engine that allows you to easily use input from multiple sources, such as keyboard, mouse, gamepad and touch in a unified way. Gone are the days, where mouse input was handled differently from joysticks and touch was a totally different beast. No matter where the input comes from - your game code works the same way.

_Note: This version utilises a C# Wrapper originally made by @DFGameDev. If you do not need C# support for G.U.I.D.E, visit the main repository [HERE](https://github.com/godotneers/G.U.I.D.E)._

---

This is an unofficial project created to make the G.U.I.D.E. system accessible to Godot C# projects. This builds directly off the <a href="https://github.com/godotneers/G.U.I.D.E">Godot Unified Input Detection Engine</a> provided by derkork and is updated in tandem with the original plugin until such a time that a better alternative comes along.

For all documentation: any reference to base G.U.I.D.E. files and systems will be referred as GUIDE (all caps) while any reference to the C# wrapper will be GuideCS or GuideCs (camel case) for simplicity sake.

# Preamble

GuideCs has full functionality on its own, see the [Changelog](CHANGES.md) for current version information. This addon has the C# wrapper, and the full guide addon as a [sub-addon](https://docs.godotengine.org/en/stable/tutorials/plugins/editor/making_plugins.html#using-sub-plugins).

The sub-addon portion of this plugin will be updated to match releases of the main [GUIDE](https://github.com/godotneers/G.U.I.D.E) plugin.

# TEMPORARY DOCS

Until this plugin gets added to the asset browser, this is where the docs live. Future updates may move these to be included in the official G.U.I.D.E documentation, or, some other approach.

# Installation

1. Download, install and run GodotSharp (this is different than the standard Godot editor).
2. Install the GUIDE GDScript addon normally. Follow the nicely done <a href="https://youtu.be/gNiiaNViaUg?si=WB1lNpfeOl3FvG-7">GUIDE tutorial</a> here if you're starting from scratch.
3. Download the matching GuideCs addon for your GUIDE version. Since the wrapper does not add functionality, the version of GuideCs should be the same as the base addon. Mismatched versions mean new calls may not be in the wrapper or it may have calls other versions of GUIDE cannot fulfil. I don't plan to backfill for previous versions and will only rarely be updating the addon to the latest version of GUIDE as this was made for my own personal project but I'll do what I can to keep things in line.
4. Copy the GuideCs zip contents into your addons folder beside the 'guide' addon. GuideCs was built to function like a normal addon but has not been released as an official addon (yet?).
<details>
  <summary>Image</summary>
<img width="533" height="265" alt="2026-03-30_16h58_32" src="https://github.com/user-attachments/assets/bb276b0a-b0b0-444d-abdc-0ad1946f8df6" />
</details>

5. Build your project (important).
<details>
  <summary>Image</summary>
  <img width="444" height="155" alt="image" src="https://github.com/user-attachments/assets/622f33e7-6839-4783-acc3-8e1c8f7de46b" />
</details>

If you do not have the build option, you'll need to add a new C# script to your project first:
<details>
  <summary>Image</summary>
  <img width="435" height="147" alt="image" src="https://github.com/user-attachments/assets/8abbf9c9-d116-469f-ad8b-dafdd0692f8c" /><br>
  
  <img width="558" height="422" alt="image" src="https://github.com/user-attachments/assets/6bf10c3c-2129-4cbc-83ff-d9f1e2427840" /><br>
  
  <img width="528" height="402" alt="image" src="https://github.com/user-attachments/assets/7bc24c0f-ccbf-4f12-b8af-043f844df724" />
</details>

Any script is fine; this will just tell Godot your project is a C# project.

6. Enable GuideCs in your Project > Project Setting > Plugins list.
<details>
  <summary>Image</summary>
  <img width="622" height="236" alt="image" src="https://github.com/user-attachments/assets/32f176ff-5ad7-4b52-8644-0b5c91736145" />
</details>

If you get the error:
"Unable to load addon script from path: 'res://addons/GuideCs/Plugin.cs'."
Restart the editor, rebuild your project and try again. GuideCs was made with Godot 4.6 so there's a possibility other versions of Godot might not work. Its very lightweight though so you can also just manually load the GuideCs autoloader yourself without loading it as an addon if you want (see next step).

7. Navigate to the Project > Project Setting > Globals list and make sure GuideCs is loaded <i>after</i> (below) the GUIDE autoloader. GuideCs captures a reference to GUIDE on load so GUIDE must be loaded first.
<details>
  <summary>Image</summary>
  <img width="854" height="440" alt="image" src="https://github.com/user-attachments/assets/8d0213b9-3f0c-474b-8100-4e89ed398088" />
</details>

8. (Potentially optional) Restart the editor. There is an <a href="https://github.com/godotengine/godot/issues/116169">issue</a> that prevents externally loaded GlobalScope objects from being recognized by the editor until after a restart. Once this is fixed, you likely wont need to do it but, if you don't see the GuideCs resources in the list (denoted by the blood orange versions), try restarting your editor.
9. Enjoy!

# Usage

GuideCs is designed to follow all the normal calls exposed by GUIDE so most of what you do with the wrapper is exactly the same other than using CamelCase instead of snake_case.

So, if you want to call `GUIDE.enable_mapping_context()`, you would simply call `Guide.EnableMappingContext()`.

C# Editor resources are handled with a duplicate resource type denoted by the 'blood orange' color variation in the list.
<details>
  <summary>Image</summary>
  <img width="903" height="296" alt="image" src="https://github.com/user-attachments/assets/f8944be5-4a71-455e-acfd-d9a3e11aa84b" /><br>
  
  <img width="907" height="292" alt="image" src="https://github.com/user-attachments/assets/8b3c1566-ef92-475f-b0ac-6faa81e9fc20" /><br>
</details>

Anything you want to do in the editor, use GUIDE resources. Anything you want to do in C#, use a GuideCs resource that is assigned a base GUIDE object.

For example:
If you want to create some GUIDEActions and GUIDEMappingContexts to follow along with the tutorial, create the resources as normal.
<details>
  <summary>Image</summary>
  <img width="349" height="423" alt="image" src="https://github.com/user-attachments/assets/d45c3379-4858-472f-a83d-93cc73961eeb" />
</details>

Then create a matching C# version:
<details>
  <summary>Image</summary>
  <img width="343" height="514" alt="image" src="https://github.com/user-attachments/assets/7ee170e4-76e8-471f-af4b-e2f81f135182" />
</details>

And assign the base GUIDE resource to the C# wrapper:
<details>
  <summary>Image</summary>
  <img width="877" height="560" alt="image" src="https://github.com/user-attachments/assets/5fdbcb79-21ff-4593-bfa9-cb091c3cafb9" />
</details>

Note: There is no type-safety between GDScript and C# so you will have to assign the correct base type to its associated wrapper.

In your C# script, create your `[Export]` variables as GuideCs types:
<details>
  <summary>Image</summary>
  <img width="436" height="307" alt="image" src="https://github.com/user-attachments/assets/489268df-5cc0-46ce-b929-2d24c3e31949" />
</details>

Assign them in the editor with your GuideCs wrappers:
<details>
  <summary>Image</summary>
  <img width="874" height="745" alt="image" src="https://github.com/user-attachments/assets/b323ebaa-b95c-45de-a9b0-530fba3f6c54" />
</details>

And call them in C# as you would in GDScript!
<details>
  <summary>Image</summary>
  <img width="426" height="478" alt="image" src="https://github.com/user-attachments/assets/6443f462-9849-45c6-822a-8d2195d0380d" />
</details>

If you don't use `[Export]` properties and would prefer to load and manage your C# resources through code, you can also do so by using the `Utility.CreateWrapper()` function to generate wrapped resources:
<details>
  <summary>Image</summary>
  <img width="769" height="542" alt="image" src="https://github.com/user-attachments/assets/dfa64d81-e23a-49d7-8d2e-e439d054604c" />
</details>

<details>
  <summary>Code</summary>

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

</details>

Alternatively, you can also directly create the wrapper using `GuideResource.ConnectBaseGuideResource()` or `GuideResource.LoadAndConnectBaseGuideResource()` depending on how you prefer to manage your resources.

If you loaded your own object:
<details>
  <summary>Image</summary>
  <img width="607" height="335" alt="image" src="https://github.com/user-attachments/assets/8b953760-2375-411e-b05e-9898b5f93662" />
</details>

If you want GuideCs to load the object for you:
<details>
  <summary>Image</summary>
<img width="632" height="319" alt="image" src="https://github.com/user-attachments/assets/90f6e048-7fda-45b1-944a-6cbe3d942e92" />
</details>

You can also simply new-up an object, passing the loaded GUIDE resource as a constructor parameter:
<details>
  <summary>Image</summary>
  <img width="791" height="266" alt="image" src="https://github.com/user-attachments/assets/a248874d-43db-41d1-8f38-4763176777a7" />
</details>

Note: GuideCs cannot verify the GUIDE object type since `get_class()` and `is_class()` return the Godot inherited class rather than a custom `class_name`. Thus, GuideCs references everything as a `GodotObject`. You can load the GUIDE content as anything it can be cast as (node, resource, etc.) if you prefer without issue.

# Issues/Gotchas

-> Being an interop layer, not everything translates smoothly from C# to GDS and vice-versa. There are several functions that require you to modify GUIDE files and add an extra function for things to translate correctly. These are all checked on invoke and will provide an error warning if the required modifications are missing. 
<details>
  <summary>These are the files that need modification:</summary>
  <img width="268" height="278" alt="image" src="https://github.com/user-attachments/assets/4a330e8c-34ad-45b1-9d94-2b149059d984" />
</details>

-> To require minimal modification to GUIDE, all innumerable interactions (Arrays, Dictionaries, etc.) were reduced to `Get` and `Set` operations. This is not ideal but, to make interop work with things like `.Add()`, `.Remove()`, `.Sort()`, etc. would require creating a variety of observable list managers. The value did not outweigh the potential bug introduction so it was omitted.

-> I didn't create `[GlobalScope]` versions of all GuideCs objects as none of them are `[Tool]` and I didn't really see much value in doubling the entire list when most people are unlikely to create these resources in-editor. So currently only GuideAction, GuideMappingContext and GuideInputDetector are available in the editor by default. If you want any of the other types to be available in the editor, just add the `[GlobalScope]` tag to the C# script (and probably restart the editor) to be able to select them in the New > Resource list.

-> Not all GUIDE content has been wrapped. Renderers, text providers and icon makers were not wrapped as, I suspect, if you want to do any work in that space, you'll be writing custom content anyway. I'll add those wrappers in the future if that sentiment changes.

-> GuideCs caches all wrappers in a weakref library and reuses them were possible. However, the cache will not retain wrappers and they will be freed by garbage collection if they are not referenced elsewhere. Make sure to store wrappers you plan to reuse. Don't store unnecessarily (you don't need to store every ConfigItem) but, for example, storing only the base GUIDEActions will cause the wrapper to constantly regenerate Action wrappers when you try to do calls which will add a small but persistent performance cost.

-> GuideCs assumes you are installing addons to the `res://addons/` folder. If you install to a different directory, navigate to the `/GuideCs/CsTools/ResourceLibrary.cs` file and update the `GuideInstallDirectory` path to point at the directory you installed GUIDE to.

-> GuideCs comes with a UnitTest script which will run every wrapped call and validate there are no issues. Generally, you will not need this but, if you want to use this with a different version of GUIDE or just want to make sure everything is connected properly, add and run this function on a node in your game script:

<details>
  <summary>Code</summary>
  This needs to be added as a child to your scene to be able to test the InputDetector node. It also assumes GuideCs was installed to the addons folder. Update the path if that is not the case.
  
      var tester = (Node)ResourceLoader.Load<CSharpScript>("res://addons/GuideCs/CsTools/GuideCsUnitTest.cs").New();
      AddChild(tester);
  
</details>
