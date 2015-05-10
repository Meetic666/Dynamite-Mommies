using UnityEngine;
using System.Collections;

public enum KoalaPartEnum
{
	e_PartA,
	e_PartB,
	e_PartC,
	e_PartD,
	e_PartE,
	e_Count
}

public class KoalaPart : MonoBehaviour 
{
	public KoalaPartEnum m_Part;

	ProgressManager m_ProgressManager;

	public float m_HoverAmplitude;
	public float m_HoverSpeed;
	float m_HoverCenter;

	float m_Time;

	void Start()
	{
		m_ProgressManager = FindObjectOfType<ProgressManager>();

		m_HoverCenter = transform.position.y;
	}

	void Update()
	{
		m_Time += Time.deltaTime * m_HoverSpeed;

		Vector3 newPosition = transform.position;
		newPosition.y = m_HoverCenter + m_HoverAmplitude * 0.5f * Mathf.Cos (m_Time);
		transform.position = newPosition;
	}

	void OnTriggerEnter(Collider otherCollider)
	{
		if(otherCollider.GetComponent<Movement>())
		{
			m_ProgressManager.CollectKoalaPart(m_Part);
		}
	}
}
