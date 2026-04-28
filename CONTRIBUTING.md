# Contribution Guide

_For contributions to G.U.I.D.E please see [G.U.I.D.E/CONTRIBUTING](https://github.com/godotneers/G.U.I.D.E/blob/main/CONTRIBUTING.md)._

---

The following checklist is for contributions to the C# wrapper portion:

- Before you start working on a feature or fix, **please create an [issue](https://github.com/Phlegmlee/G.U.I.D.E-CSharp/issues) first**. This way we can discuss the implementation and any potential side effects.
- In your PR please make sure you **only change the files that are necessary** for your fix or feature. Changes to unrelated files make it harder to review and merge your PR.
  - An example, you open the project in a version of Godot > 4.2, this will create new `.import` files, `.uid` files, etc. These are unnessessary to commit. Only commit the files that YOU changed.
- If you have multiple fixes or features, please **create a separate PR for each**. This makes it easier to review and merge them, and problems with one fix/feature will not block the other fixes/features from being merged.
- Please make sure your code is **formatted according to the [GDScript style guide](https://docs.godotengine.org/en/stable/getting_started/scripting/gdscript/gdscript_styleguide.html)** and the **[C# style guide](https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_style_guide.html)**. It may not be your preferred style, but we really don't want to have multiple styles mixed in the codebase.
- Please **rebase your PR on the latest on the `main`** branch before submitting it. This makes it easier to compare the changes and avoids merge conflicts. 
- Your contribution must be **licensed under the MIT license**. This is the same license as this project uses. See the [LICENSE](LICENSE) file for details.
