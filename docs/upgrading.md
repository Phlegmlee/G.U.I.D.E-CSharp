---
layout: default
---

## Updating the Plugin

Updating G.U.I.D.E-CSharp currently must be done manually, automatic updates may be added in the future.

_NOTE: ALL STEPS SHOULD BE DONE FROM WITHIN THE EDITOR OR WITH THE EDITOR OPEN_

1. **Deactivate Plugin**
1. Delete guideCS folder
1. Install new version (manual or asset lib)
1. Use the project updater to fix UID issues if updating from a version <= 0.3.6
	- `Project -> Tools -> Upgrade Project Files` this automatically opens and saves every scene, resource and script.
1. Build project
1. Enable plugin
1. Ensure GUIDE global is above GuideCS global
1. Restart Project

---

Report any [issues](https://github.com/Phlegmlee/G.U.I.D.E-CSharp/issues).

