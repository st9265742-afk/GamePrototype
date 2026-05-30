using Sandbox;

public sealed class Classes : Component
{
	[Property] GameObject Char1 {  get; set; }
	[Property] GameObject Char2 {  get; set; }
	[Property] GameObject Char3 {  get; set; }
	[Property] public Shoot OtherScript { get; set; }
	protected override void OnUpdate()
	{
		if (Input.Pressed("Slot1"))
		{
			//var myComponent = Components.Get<MyCustomComponent>();
			Log.Info( "Class 1" );
		}

		if (Input.Pressed("Slot2"))
		{
			Log.Info( "Class 2" );
		}

		if (Input.Pressed("Slot3"))
		{
			Log.Info( "Class 3" );
		}
	}
}
