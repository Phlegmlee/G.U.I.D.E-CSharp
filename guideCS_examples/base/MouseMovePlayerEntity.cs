#nullable enable

using Godot;

namespace GuideCs.Examples;

public partial class MouseMovePlayerEntity : CharacterBody2D
{
	private const float Speed = 300.0f;

	[Export] private GuideMappingContext BaseContext = null!;

	private const string mouseMove = "uid://ctu2xbqsbej44";
	private GuideAction mouseInput = null!;

	public override void _Ready()
	{
		Guide.EnableMappingContext(BaseContext);

		var baseObj = (GodotObject)ResourceLoader.Load(mouseMove);
		var wrappedAction = Utility.CreateWrapper<GuideAction>(baseObj);

		mouseInput = wrappedAction;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity;

		Vector2 direction = mouseInput.ValueAxis2d.Normalized();
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
		}
		else
		{
			velocity = Vector2.Zero;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
