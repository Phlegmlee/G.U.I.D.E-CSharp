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
        if (cfgLoader.Load($"{PluginPath}/plugin.cfg") is Error.Ok)
        {
            if (cfgLoader.GetValue("plugin", "suppressCheck", "false").AsString() != "true")
            {
                _csCfg = cfgLoader;
                CheckVersionCompatibility();
            }
        }
        
        base._EnterTree();
    }
    
    private void CheckVersionCompatibility()
    {
        string displayText;

        if (!FileAccess.FileExists(ResourceLibrary.PluginCfgPath))
        {
            displayText = $"Unable to verify GuideCs wrapper compatibility: cannot find G.U.I.D.E. config file. Ensure G.U.I.D.E. is installed and the install directory is correct in the '{nameof(ResourceLibrary)}.cs' file.";
            LoadDialogueBox();
            return;
        }

        var gdCfg = new ConfigFile();
        if (gdCfg.Load(ResourceLibrary.PluginCfgPath) != Error.Ok)
        { return; }
        
        var csVersion = _csCfg.GetValue("plugin", "version", "").AsString();
        var gdVersion = gdCfg.GetValue("plugin", "version", "").AsString();
        if (gdVersion == "" || csVersion == "")
        {
            displayText = "Unable to verify GuideCs wrapper compatibility with G.U.I.D.E.: cannot parse version values in one or both config files.";
            LoadDialogueBox();
            return;
        }
            
        if (gdVersion != csVersion)
        {
            displayText = $"GuideCs version [{csVersion}] should match G.U.I.D.E. version [{gdVersion}] but found mismatch. Recommend matching versions to ensure highest compatibility.";
            LoadDialogueBox();
        }

        return;
        void LoadDialogueBox()
        {
            _scene = ResourceLoader.Load<PackedScene>($"{PluginPath}/CsTools/VerificationScene.tscn").Instantiate<CanvasLayer>();
            if (_scene is null)
            { return; }

            _scene.GetNode<Button>("%Button").Pressed += OnVerifyOkPressed;
            _scene.GetNode<Label>("%Label").Text = displayText;
            EditorInterface.Singleton.GetBaseControl().AddChild(_scene);
        }
    }

    private void OnVerifyOkPressed()
    {
        var suppress = _scene.GetNode<CheckBox>("%CheckBox").ButtonPressed;
        if (suppress)
        {
            _csCfg.SetValue("plugin", "suppressCheck", "true");
            _csCfg.Save($"{PluginPath}/plugin.cfg");
        }

        _scene.Visible = false;
        _scene.GetNode<Button>("%Button").Pressed -= OnVerifyOkPressed;
        _scene.QueueFree();
    }
}
#endif