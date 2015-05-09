using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public int m_Damage;
	public float m_Speed;

	int m_Direction;
	
	public GameObject m_SplatterParticlesPrefab;

	public string m_TagToIgnore;

	Renderer m_Renderer;

	public float m_RotationSpeed;

	void Start()
	{
		m_Renderer = GetComponentInChildren<Renderer>();
	}

	// Update is called once per frame
	void Update () 
	{
		m_Renderer.transform.RotateAround(Vector3.forward, m_RotationSpeed * Time.deltaTime);

		Vector3 previousPosition = transform.position;
		transform.position += Vector3.right * m_Direction * m_Speed * Time.deltaTime;

		RaycastHit hitInfo;

		if(Physics.Raycast(previousPosition, Vector3.right * m_Direction, out hitInfo, m_Speed * Time.deltaTime))
		{
			transform.position = hitInfo.point;

			OnTriggerEnter(hitInfo.collider);
		}
	}

	public void SetDirection(int direction)
	{
		m_Direction = direction;
	}
	
	void OnTriggerEnter(Collider otherCollider)
	{
		if(otherCollider.tag != m_TagToIgnore)
		{
			Health_System<int> health = otherCollider.GetComponent<Health_System<int>>();

			if(health != null)
			{
				health.TakeDamage(m_Damage);
			}
			
			Destroy(gameObject);
			
			Instantiate(m_SplatterParticlesPrefab, transform.position, Quaternion.identity);
		}
	}
}
