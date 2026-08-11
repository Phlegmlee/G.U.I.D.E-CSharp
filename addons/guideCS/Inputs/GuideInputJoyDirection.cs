using Godot;
using System;

namespace GuideCs;

/// <summary>Wrapper for Joy Direction Input type. Must be assigned a GUIDEInputJoyDirection to function.</summary>
public partial class GuideInputJoyDirection : GuideInputJoyBase
{
	public GuideInputJoyDirection(GodotObject gdInput) : base(gdInput) { }

	public GuideInputJoyDirection()
	{ LoadAndConnectBaseGuideResource(ResourceLibrary.InputJoyDirGdPath); }

	/// <summary>The direction of the joy axis.</summary>
	public enum Direction
	{
		/// <summary>Positive direction, right for horizontal, down for vertical.</summary>
		Positive,
		/// <summary>Negative direction, left for horizontal, up for vertical.</summary>
		Negative,
	}

	/// <summary>The joy axis to sample.</summary>
	public JoyAxis Axis
	{
		get
		{
			var i = BaseGuideObject.Get("axis").AsInt64();
			if (Enum.IsDefined(typeof(JoyAxis), i))
			{ return (JoyAxis)i; }

			return JoyAxis.Invalid;
		}

		set => BaseGuideObject.Set("axis", (long)value);
	}

	/// <summary>Direction of the joy axis.</summary>
	public Direction Dir
	{
		get
		{
			var d = BaseGuideObject.Get("direction").AsInt16();
			return (Direction)d;
		}

		set => BaseGuideObject.Set("direction", (int)value);
	}

	/// <summary>The minimum axis value that must be reached to consider the input actuated.</summary>
	public float ActuationThreshold
	{
		get
		{
			var a = (float)BaseGuideObject.Get("actuation_threshold");
			return a;
		}

		set => BaseGuideObject.Set("actuation_threshold", value);
	}
}
