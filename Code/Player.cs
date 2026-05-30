using Sandbox;

public sealed class Player : Component
{
	public static Player Instance {get; private set;}
	protected override void OnUpdate()
	{
		Instance = this;
	}
}
