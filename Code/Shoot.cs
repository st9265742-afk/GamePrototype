using Sandbox;

public sealed class Shoot : Component
{
	/// <summary>
	/// Bullet projectail
	/// </summary>
	[Property] GameObject bullet {  get; set; }

	[Property] public SoundEvent ShootSound { get; set; }

	[Property] public SoundEvent HitSound { get; set; }

	[Property] public SoundEvent SwingSound { get; set; }

	[Property] float Damage { get; set; }

	public TimeSince _timeSinceLastAttack;
	
	protected override void OnUpdate()
	{
		if (Input.Pressed("Attack1"))
		{
			//fireproj();
			firebullet();
		}
		if ( Input.Down( "Attack2" ) )
		{
			if ( _timeSinceLastAttack >= 0.1f)
			{
				_timeSinceLastAttack = 0;
				//fireproj();
				firebullet();
			}
		}
		if (Input.Keyboard.Pressed( "q" ))
		{
			Sound.Play( SwingSound );
			Punch();
		}
	}
	public void fireproj(int speed = 500000)
	{
		GameObject instanse = bullet.Clone( WorldPosition );
		var rb = instanse.GetComponent<Rigidbody>();
		if ( rb.IsValid() )
		{
			Sound.Play( ShootSound );
			rb.ApplyForce( GameObject.Parent.LocalRotation.Forward * speed );
		}
	}

	public void firebullet()
	{
		var startPos = GameObject.WorldPosition;
		var endPos = startPos + GameObject.WorldRotation.Forward * 5000f;

		var tr = Scene.Trace
		.Ray( startPos, endPos )
		.WithCollisionRules( "bullet" )
		.Run();

		DebugOverlay.Trace( tr, 5f );

		if ( tr.Hit )
			{
				Log.Info( $"Hit: {tr.GameObject} at {tr.EndPosition}" );
				Sound.Play( ShootSound );

				if ( tr.GameObject.Components.TryGet<Enemy>( out var enemy ) )
            {
    			enemy.Damage( 6 );
			}
			}
	}

	public void Punch()
	{
		var punchDirection = GameObject.WorldRotation.Forward;
		var startPunch = GameObject.WorldPosition;
		var endPunch = startPunch + punchDirection * 80f;

		var tr = Scene.Trace
		.Ray( startPunch, endPunch )
		.Radius(10f)
		.WithoutTags( "player" )
		.Run();

		if(!tr.Hit) return;

		DebugOverlay.Trace( tr, 5f );

		if ( tr.Hit )
			{
				Log.Info( $"Hit: {tr.GameObject} at {tr.EndPosition}" );
				Sound.Play( HitSound );
			}
		
	}
}
