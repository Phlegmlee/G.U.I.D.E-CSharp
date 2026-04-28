#if TOOLS
using Godot;
using Godot.Collections;

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

        ResourceSaved += OnResourceSaved;

        base._EnterTree();
    }

    private void OnResourceSaved(Resource resource)
    {
        Script resourceScript = (Script)resource.GetScript();
        string savePath = resource.ResourcePath.TrimSuffix(".tres") + "CS.tres";

        if (GetClassName(resourceScript) == "GUIDEMappingContext")
        {
            GuideMappingContext newCSmap = new();
            newCSmap.ConnectBaseGuideResource(resource);
            CreateCSResource(newCSmap, savePath);
        }
        else if (GetClassName(resourceScript) == "GUIDEAction")
        {
            GuideAction newCSAction = new();
            newCSAction.ConnectBaseGuideResource(resource);
            CreateCSResource(newCSAction, savePath);
        }
    }

    private void CreateCSResource(Resource resource, string path)
    {
        Error error = ResourceSaver.Save(resource, path);

        if (error > 0)
        {
            GD.PushWarning("guideCS was unable to create the CS wrapper automatically.");
        }
        GD.Print($"guideCS created {path} and assigned the GUIDE resource to the wrapper.");

    }
    
    private string GetClassName(Script script)
    {
        foreach (Dictionary info in ProjectSettings.GetGlobalClassList())
        {
            if ((string)info["path"] == script.ResourcePath)
            {
                return (string)info["class"];
            }
        }
        return script.GetClass();
    }
}
#endif