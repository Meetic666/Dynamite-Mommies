using UnityEngine;
using System.Collections;

public class Husband : Base_AI, Health_System<int> 
{


	void Start()
	{

	}

	void Update()
	{
		switch(m_CurrentState)
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
		case States.e_SpecialTwo:
		{
			break;
		}
		case States.e_SpecialThree:
		{
			break;
		}
		default:
			break;
		}
	}

	protected override void ChangeStateTo (States newState)
	{
		if(m_CurrentState != newState)
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
			case States.e_SpecialTwo:
			{
				break;
			}
			case States.e_SpecialThree:
			{
				break;
			}
			default:
				break;
			}

			base.ChangeStateTo (newState);
		}
	}

	public void TakeDamage(int dmg)
	{

	}
}
