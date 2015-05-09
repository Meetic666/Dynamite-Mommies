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

	int m_CurrentDirection = 1;

	Movement m_Player;

	void Start()
	{
		m_Player = GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.25f)
		{
			m_CurrentDirection = (int) Mathf.Sign (Input.GetAxis ("Horizontal"));
		}

		m_FiringTimer -= Time.deltaTime;

		if(m_FiringTimer <= 0.0f && Input.GetMouseButton(0))
		{
			GameObject newProjectile = null;

			if(m_CurrentType == ProjectileType.e_Flower)
			{
				m_FiringTimer = 1.0f / m_FlowerFiringRate;
				newProjectile = (GameObject) Instantiate(m_FlowerPrefab, transform.position + transform.right * m_CurrentDirection * m_ProjectileSpawnOffset, Quaternion.identity);
			}
			else
			{
				m_FiringTimer = 1.0f / m_TamponFiringRate;
				newProjectile = (GameObject) Instantiate(m_TamponPrefab, transform.position + transform.right * m_CurrentDirection * m_ProjectileSpawnOffset, Quaternion.identity);
			}

			newProjectile.GetComponent<Projectile>().SetDirection(m_CurrentDirection);
			newProjectile.GetComponent<Projectile>().m_Speed += Mathf.Abs (m_Player.HorizontalSpeed);
		}

		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			m_CurrentType = (m_CurrentType == ProjectileType.e_Flower) ? ProjectileType.e_Tampon : ProjectileType.e_Flower;
		}
	}
}
