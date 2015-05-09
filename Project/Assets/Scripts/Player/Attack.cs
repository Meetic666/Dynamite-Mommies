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

	public int m_FiringRate;
	float m_FiringTimer;

	public float m_ProjectileSpawnOffset;

	int m_CurrentDirection = 1;
	
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
			m_FiringTimer = 1.0f / m_FiringRate;

			GameObject newProjectile = null;

			if(m_CurrentType == ProjectileType.e_Flower)
			{
				newProjectile = (GameObject) Instantiate(m_FlowerPrefab, transform.position + transform.right * m_CurrentDirection * m_ProjectileSpawnOffset, Quaternion.identity);
			}
			else
			{
				newProjectile = (GameObject) Instantiate(m_TamponPrefab, transform.position + transform.right * m_CurrentDirection * m_ProjectileSpawnOffset, Quaternion.identity);
			}

			newProjectile.GetComponent<Projectile>().SetDirection(m_CurrentDirection);
		}

		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			m_CurrentType = (m_CurrentType == ProjectileType.e_Flower) ? ProjectileType.e_Tampon : ProjectileType.e_Flower;
		}
	}
}
