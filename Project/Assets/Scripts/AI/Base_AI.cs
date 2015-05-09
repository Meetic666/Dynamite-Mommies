using UnityEngine;
using System.Collections;

public abstract class Base_AI : MonoBehaviour 
{
	protected int m_Health;
	protected float m_MovementSpeed;

	protected enum States
	{
		e_Idle = 0,
		e_Patrol,
		e_Attack,
		e_Dead,
		e_SpecialOne
	}
	protected States m_CurrentState = States.e_Idle;

	protected abstract void ChangeStateTo (States newState);
	protected abstract void TakeDamage (int dmg);

	protected virtual void DealDamage (int dmg)
	{
	}

	protected virtual void TriggerPatrol ()
	{
		m_CurrentState = States.e_Patrol;
	}

	protected virtual void TriggerIdle ()
	{
		m_CurrentState = States.e_Idle;
	}

	protected virtual void TriggerAttack ()
	{
		m_CurrentState = States.e_Attack;
	}

	protected virtual void TriggerSpecialOne ()
	{
		m_CurrentState = States.e_SpecialOne;
	}

	protected virtual void KillSelf()
	{
		m_CurrentState = States.e_Dead;
	}
}
