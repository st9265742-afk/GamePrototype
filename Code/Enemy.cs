using Sandbox;
using System;

public sealed class Enemy : Component
{
	//[Property] public Dictionary<string, AiAction> actions = new Dictionary<string, AiAction>();
	//[Property] public List<IPriority> priorities = new List<IPriority>();

	//[Property] public GameObject Target {get; set;}
	[Property] public float MoveSpeed {get; set;} = 1f;
	[Property] public SoundEvent HartSound { get; set; }
	[Property] public SoundEvent DeathSound { get; set; }
	[Property] public float Health { get; set; } = 3;

	private static readonly float _radiansToDegrees = 380 / (MathF.PI * 2);
	private Vector3 _Direction {get; set;}

	private float RadiansToDegrees(float radians)
	{
		return radians * _radiansToDegrees;
	}

	private float LookAt()
	{
		float targetAngleRadians = MathF.Atan2(_Direction.y, _Direction.x);
		float angle = RadiansToDegrees(targetAngleRadians);

		return angle;
	}

	public void RotateToTarget()
	{
		float angle = LookAt();

		Transform.Rotation = Rotation.FromYaw(angle);
	}

	public void MoveToTarget()
	{
		Transform.Position += _Direction * MoveSpeed; 
	}

	public void Init()
	{
		_Direction = (Player.Instance.Transform.Position - Transform.Position).WithZ(0).Normal; 
	}

	protected override void OnStart()
	{
		Init();
	}

	protected override void OnFixedUpdate()
	{
		RotateToTarget();
		MoveToTarget();
	}
    public void Damage( float damage )
    {
        Health -= damage;

        Log.Info( $"Enemy HP: {Health}" );

		Sound.Play( HartSound );

        if ( Health <= 0 )
        {
            GameObject.Destroy();
			Sound.Play( DeathSound );
        }
    }
	/*public bool? GetDecision(string action, List<object> context)
	{
		AiAction _action = actions[action];
		if ( _action != null )
		{
			float approval = 0;
			foreach ( IPriority priority in priorities )
			{
				foreach ( object _context in context )
				{
					int approv = priority.GetApproval( _context ).AsInt();

					if ( approv < _action.minRequiredApproval ) return false;
					if ( approv > _action.maxRequiredApproval ) return true;

					approval += approv;
				}
			}

			if ( priorities.Count * context.Count > 0 )
				approval /= priorities.Count * context.Count;

			if ( approval > _action.requiredApproval ) return true;

			return false;
		}

		return null;
	}

	public abstract class AiAction
	{
		// required median approval (equal or above) to get the decision to approve
		public float requiredApproval = 1;

		// Anything below this will cause the decision to immediately dissaprove
		public int minRequiredApproval = -1;

		// Anything above this will cause the decision to immediately approve
		public int maxRequiredApproval = 2;
	}

	public enum PriorityApproval
	{
		StronglyDisapprove = -2,
		Disapprove = -1,
		Impartial = 0,
		Approve = 1,
		StronglyApprove = 2,
	}

	public class IPriority : Object
	{
		public virtual PriorityApproval GetApproval( object context )
		{
			//if(context != null && typeof( AiContext ).IsAssignableFrom( context.GetType() )) return PriorityApproval.Approve;
			return PriorityApproval.Impartial;
		}

		public IPriority()
		{
			
		}
	}*/
}
