using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour 
{
	public Transform m_StartPosition;
	public Transform m_EndPosition;

	float m_Time;
	public float m_Speed;
	
	// Update is called once per frame
	void Update () 
	{
		m_Time += Time.deltaTime * m_Speed;

		transform.position = Vector3.Lerp (m_StartPosition.position, m_EndPosition.position, (Mathf.Cos (m_Time) + 1.0f) * 0.5f);
	}
}
