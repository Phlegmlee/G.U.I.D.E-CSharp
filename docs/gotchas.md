---
layout: default
---

## Gotchas
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
### Go Back -> [Usage](usage.html)
