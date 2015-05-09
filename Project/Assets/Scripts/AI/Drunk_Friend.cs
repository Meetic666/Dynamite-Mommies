using UnityEngine;
using System.Collections;

public class Drunk_Friend : Base_AI 
{
	private float m_DelayUntilStumble;
	private RaycastHit m_RayHit;

	public float m_InitialMovementSpeed;
	public float m_MaxStepHeight;

	void Start()
	{
		m_MovementSpeed = m_InitialMovementSpeed;
	}

	void Update()
	{
		if(Physics2D.Raycast(transform.position + (transform.right * 0.6f), -Vector2.up, m_MaxStepHeight))
		{
			transform.position += (transform.right * m_MovementSpeed);
		}
		else
		{
			TurnAround();
		}
	}

	void TurnAround()
	{
		transform.Rotate (Vector3.up * 180);
	}

	protected override void ChangeStateTo(Base_AI.States newState)
	{
		switch(newState)
		{
			case States.e_Idle:
			{
				break;
			}
			case States.e_Patrol:
			{
				break;
			}
			case States.e_Attack:
			{
				break;
			}
			case States.e_Dead:
			{
				break;
			}
			case States.e_SpecialOne:
			{
				break;
			}
		default:
			break;
		}

		m_CurrentState = newState;
	}

	protected override void TakeDamage(int dmg)
	{
		m_Health -= dmg;

		if(m_Health <= 0)
		{
			KillSelf();
		}
	}

	protected override void KillSelf()
	{
		base.KillSelf ();
	}
}
