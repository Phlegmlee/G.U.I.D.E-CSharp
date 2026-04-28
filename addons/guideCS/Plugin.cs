#if TOOLS
using Godot;

namespace GuideCs;

[Tool]
public partial class Plugin : EditorPlugin
{
    private string PluginPath => GetScript().As<CSharpScript>().ResourcePath.GetBaseDir();
    private EditorInterface editorInterface;
    
    private CanvasLayer _scene;
    private ConfigFile _csCfg;
    
    public override void _EnablePlugin()
    {
        AddAutoloadSingleton("GuideCs", $"{PluginPath}/Guide.cs");
        editorInterface.SetPluginEnabled($"{PluginPath}/guide", true);
        base._EnablePlugin();
    }
    
    public override void _DisablePlugin()
    {
        RemoveAutoloadSingleton("GuideCs");
        editorInterface.SetPluginEnabled($"{PluginPath}/guide", false);
        base._DisablePlugin();
    }

    public override void _EnterTree()
    {
        editorInterface = (EditorInterface)Engine.GetSingleton("EditorInterface");

        var cfgLoader = new ConfigFile();
        cfgLoader.Load($"{PluginPath}/plugin.cfg");

        base._EnterTree();
    }
}
#endif