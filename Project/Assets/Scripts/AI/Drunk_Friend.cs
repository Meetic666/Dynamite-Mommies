using UnityEngine;
using System.Collections;

public class Drunk_Friend : Base_AI, Health_System<int>
{
	//Unique
	private float m_DelayUntilStumble;
	private RaycastHit m_RayHit;

	//Movement
	public float m_InitialMovementSpeed;

	//Detection
	public float m_MaxStepHeight;
	public float m_PlayerDetectionRadius;
	public float m_PlayerDetectionDistance;

	//Prefabs/Projectiles
	public float m_UpwardProjectileForce;
	public GameObject m_WeaponPrefab;

	void Start()
	{
		m_MovementSpeed = m_InitialMovementSpeed;

		m_CurrentState = States.e_Patrol;
	}

	void Update()
	{
		switch (m_CurrentState) {
		case States.e_Idle:
		{
			break;
		}
		case States.e_Patrol:
		{
			//Ledge Detection
		if(Physics.Raycast(transform.position + (transform.right * 0.6f), -Vector3.up, m_MaxStepHeight))
		{
			transform.position += (transform.right * m_MovementSpeed);
		}
		else
		{
			TurnAround();
		}

		//EnemyDetection
		if(Physics.SphereCast(transform.position + (transform.right * 0.6f), m_PlayerDetectionRadius, transform.right, out m_RayHit, m_PlayerDetectionDistance))
		{
			if(m_RayHit.transform.tag == "Player")
			{
				ChangeStateTo(States.e_Attack);
			}
		}
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

		Debug.DrawRay (transform.position, transform.right, Color.green);

	}

	void TurnAround()
	{
		transform.Rotate (Vector3.up * 180);
	}

	protected override void ChangeStateTo(Base_AI.States newState)
	{
		//Animations and Sounds
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

		base.ChangeStateTo (newState);
	}

	protected override void TriggerAttack ()
	{
		GameObject projectile = (GameObject)Instantiate (m_WeaponPrefab, transform.position + transform.right + (Vector3.up ), transform.rotation);
		if (transform.rotation.y != 0 && transform.rotation.y != 360) 
		{
			projectile.GetComponent<Projectile> ().SetDirection (-1);
		} 
		else
		{
			projectile.GetComponent<Projectile> ().SetDirection (1);
		}
		projectile.GetComponent<Rigidbody> ().AddForce (Vector3.up * m_UpwardProjectileForce);

		base.TriggerAttack ();
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
