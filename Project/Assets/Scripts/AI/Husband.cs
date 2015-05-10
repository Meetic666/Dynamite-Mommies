﻿using UnityEngine;
using System.Collections;

public class Husband : Base_AI, Health_System<int> 
{
	//Unique
	public int m_InitialHealth;

	//Attack
	public GameObject[] m_WeaponPrefabs = new GameObject[2];

	//Movement
	public float m_InitialMovementSpeed;

	void Start()
	{
		m_MovementSpeed = m_InitialMovementSpeed;
		m_Health = m_InitialHealth;
		m_CurrentState = States.e_Idle;
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
		m_Health -= dmg;
		
		if(m_Health <= 0)
		{
			ChangeStateTo(States.e_Dead);
		}
	}
}
