using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour 
{
	Animator m_Animator;

	bool IsShooting;

	MovementState m_CurrentState;

	void Start()
	{
		m_Animator = GetComponent<Animator>();
	}

	public void SetState(MovementState state)
	{
		if(state != m_CurrentState)
		{
			m_CurrentState = state;

			string animationName = GetAnimationName();

			m_Animator.Play (animationName);
		}
	}

	public void SetShooting(bool isShooting)
	{
		if(isShooting != IsShooting)
		{
			IsShooting = isShooting;

			string animationName = GetAnimationName ();

			m_Animator.Play(animationName, 0, m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
		}
	}

	string GetAnimationName()
	{
		string animationName = "";
		
		switch(m_CurrentState)
		{
		case MovementState.e_Dead:
			break;
			
		case MovementState.e_Falling:
			animationName = "Mommy_Jump";
			break;
			
		case MovementState.e_Floating:
			animationName = "Mommy_Float";
			break;
			
		case MovementState.e_Idle:
			animationName = "Mommy_Idle";
			break;
			
		case MovementState.e_Jumping:
			animationName = "Mommy_Jump";
			break;
			
		case MovementState.e_Running:
			animationName = "MommyRun";
			break;
		}
		
		if(IsShooting)
		{
			animationName += "Shoot";
		}
		
		return animationName;
	}
}
