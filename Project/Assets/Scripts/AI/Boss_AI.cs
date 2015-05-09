using UnityEngine;
using System.Collections;

public class Boss_AI : MonoBehaviour 
{
	protected enum BossState
	{
		e_Idle = 0,
		e_Scanning,
		e_Attack,
		e_Dead,
		e_SpecialOne,
		e_SpecialTwo
	}
	protected BossState m_CurrentState;


}
