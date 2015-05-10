using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Rect m_Bounds;

	public GameObject m_MistressBoss;
	public GameObject m_HusbandBoss;

	public GameObject m_Target;
	GameObject m_CurrentTarget;
	int m_CurrentBossFight = 0;

	public float m_CameraSpeed;
	public float m_CameraAcceleration;

	public Vector3 m_CameraThreshold;

	public GameObject[] m_CamPositions = new GameObject[2];

	Vector3 m_CurrentSpeed;

	bool m_InBossFight = false;
	float m_OriginalZPosition;

	void Start()
	{
		m_OriginalZPosition = transform.position.z;
		m_CurrentTarget = m_Target;
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 newPosition = transform.position;
		newPosition += m_CurrentSpeed * Time.deltaTime;

		newPosition.x = Mathf.Clamp (newPosition.x, m_Bounds.xMin, m_Bounds.xMax);
		newPosition.y = Mathf.Clamp (newPosition.y, m_Bounds.yMin, m_Bounds.yMax);

		transform.position = newPosition;

		Vector3 targetOffset = m_CurrentTarget.transform.position - transform.position;

		Vector3 targetSpeed = Vector3.zero;

		if(Mathf.Abs (targetOffset.x) > m_CameraThreshold.x)
		{
			targetSpeed.x = Mathf.Sign(targetOffset.x) * m_CameraSpeed;
		}

		if(Mathf.Abs (targetOffset.y) > m_CameraThreshold.y)
		{
			targetSpeed.y = Mathf.Sign(targetOffset.y) * m_CameraSpeed;
		}

		if(m_InBossFight)
		{
			transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z + (m_CamPositions[m_CurrentBossFight].transform.position.z - transform.position.z) * Time.deltaTime);
		}
		else
		{
			transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z + (m_OriginalZPosition - transform.position.z) * Time.deltaTime);
		}

		m_CurrentSpeed = Vector3.Lerp(m_CurrentSpeed, targetSpeed, m_CameraAcceleration * Time.deltaTime);

	}

	public void BeginBossFight()
	{
		m_InBossFight = true;
		m_CurrentTarget = m_CamPositions [m_CurrentBossFight];

		if(m_CurrentBossFight <= 0)
		{
			m_MistressBoss.GetComponent<Mistress>().EngagePlayer = true;
		}
		else
		{
			m_HusbandBoss.GetComponent<Mistress>().EngagePlayer = true;
		}
	}

	public void EndBossFight()
	{
		if(m_CurrentBossFight <= 0)
		{
			m_MistressBoss.GetComponent<Mistress>().EngagePlayer = false;
		}
		else
		{
			m_HusbandBoss.GetComponent<Mistress>().EngagePlayer = false;
		}

		m_InBossFight = false;
		m_CurrentBossFight++;
	}
}
