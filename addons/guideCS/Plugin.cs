#if TOOLS
using Godot;

namespace GuideCs;

[Tool]
public partial class Plugin : EditorPlugin
{
	EditorInterface editorInterface;
	
	public override void _EnterTree()
	{
		editorInterface = (EditorInterface)Engine.GetSingleton("EditorInterface");
	}

	public override void _EnablePlugin()
	{
		editorInterface.SetPluginEnabled("guideCS/guide", true);

		base._EnablePlugin();
	}

	public override void _DisablePlugin()
	{
		editorInterface.SetPluginEnabled("addons/guideCS/guide", false);

		base._EnablePlugin();
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
	}
}
#endif
