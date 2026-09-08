#nullable enable

using Godot;

namespace GuideCs.Examples;

public partial class PlayerEntity : CharacterBody2D
{
	private const float Speed = 300.0f;

	[Export] private GuideMappingContext BaseContext = null!;
	[Export] private GuideAction Move = null!;

	public override void _Ready()
	{
		Guide.EnableMappingContext(BaseContext);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity;

		Vector2 direction = Move.ValueAxis2d;
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
