using UnityEngine;
using System.Collections;

public enum ProjectileType
{
	e_Tampon,
	e_Flower
}

public class Attack : MonoBehaviour 
{
	ProjectileType m_CurrentType;

	public GameObject m_TamponPrefab;
	public GameObject m_FlowerPrefab;

	public int m_FlowerFiringRate;
	public int m_TamponFiringRate;
	float m_FiringTimer;

	public float m_ProjectileSpawnOffset;
	public float m_VerticalPjectileSpawnOffset;

	int m_CurrentDirection = 1;

	Movement m_Player;

	Animator m_Animator;

	AnimationManager m_AnimationManager;

	void Start()
	{
		m_Player = GetComponent<Movement>();

		m_Animator = GetComponentInChildren<Animator>();
		m_AnimationManager = GetComponentInChildren<AnimationManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Mathf.Abs (m_Player.HorizontalSpeed) > m_Player.m_MaxSpeedForIdle)
		{
			m_CurrentDirection = (int) Mathf.Sign (m_Player.HorizontalSpeed);
		}

		m_FiringTimer -= Time.deltaTime;

		if(Input.GetMouseButton(0))
		{
			m_Animator.SetBool ("IsShooting", true);
			
			m_AnimationManager.SetShooting(true);

			if(m_FiringTimer <= 0.0f)
			{
				GameObject newProjectile = null;

				if(m_CurrentType == ProjectileType.e_Flower)
				{
					m_FiringTimer = 1.0f / m_FlowerFiringRate;
					newProjectile = (GameObject) Instantiate(m_FlowerPrefab, transform.position + transform.right * m_CurrentDirection * m_ProjectileSpawnOffset + transform.up * m_VerticalPjectileSpawnOffset, Quaternion.identity);
				}
				else
				{
					m_FiringTimer = 1.0f / m_TamponFiringRate;
					newProjectile = (GameObject) Instantiate(m_TamponPrefab, transform.position + transform.right * m_CurrentDirection * m_ProjectileSpawnOffset + transform.up * m_VerticalPjectileSpawnOffset, Quaternion.identity);
				}

				newProjectile.GetComponent<Projectile>().SetDirection(m_CurrentDirection);
				newProjectile.GetComponent<Projectile>().m_Speed += Mathf.Abs (m_Player.HorizontalSpeed);
			}
		}
		else
		{
			m_Animator.SetBool ("IsShooting", false);

			m_AnimationManager.SetShooting(false);
		}

		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			m_CurrentType = (m_CurrentType == ProjectileType.e_Flower) ? ProjectileType.e_Tampon : ProjectileType.e_Flower;
		}
	}
}
