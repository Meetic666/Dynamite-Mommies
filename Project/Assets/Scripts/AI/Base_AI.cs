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
		e_SpecialOne,
		e_SpecialTwo,
		e_SpecialThree
	}
	protected States m_CurrentState = States.e_Idle;

	protected virtual void ChangeStateTo (States newState)
	{
		if(m_CurrentState != newState)
		{
			switch(newState)
			{
			case States.e_Idle:
			{
				TriggerIdle ();
				
				break;
			}
			case States.e_Patrol:
			{
				TriggerPatrol();
				
				break;
			}
			case States.e_Attack:
			{
				TriggerAttack();
				
				break;
			}
			case States.e_Dead:
			{
				TriggerDeath();
				
				break;
			}
			case States.e_SpecialOne:
			{
				TriggerSpecialOne();
				
				break;
			}
			default:
				break;
			}
		}
	}
	
	protected virtual void DealDamage (int dmg)
	{

	}

	protected virtual void TriggerIdle ()
	{
		m_CurrentState = States.e_Idle;
	}

	protected virtual void TriggerPatrol ()
	{
		m_CurrentState = States.e_Patrol;
	}

	protected virtual void TriggerAttack ()
	{
		m_CurrentState = States.e_Attack;
	}

	protected virtual void TriggerDeath()
	{
		m_CurrentState = States.e_Dead;
	}

	protected virtual void TriggerSpecialOne ()
	{
		m_CurrentState = States.e_SpecialOne;
	}
}
