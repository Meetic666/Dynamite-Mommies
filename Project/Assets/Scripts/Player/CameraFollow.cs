using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Rect m_Bounds;

	public GameObject m_Target;

	public float m_CameraSpeed;
	public float m_CameraAcceleration;

	public Vector2 m_CameraThreshold;

	Vector3 m_CurrentSpeed;
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 newPosition = transform.position;
		newPosition += m_CurrentSpeed * Time.deltaTime;

		newPosition.x = Mathf.Clamp (newPosition.x, m_Bounds.xMin, m_Bounds.xMax);
		newPosition.y = Mathf.Clamp (newPosition.y, m_Bounds.yMin, m_Bounds.yMax);

		transform.position = newPosition;

		Vector3 targetOffset = m_Target.transform.position - transform.position;

		Vector3 targetSpeed = Vector3.zero;

		if(Mathf.Abs (targetOffset.x) > m_CameraThreshold.x)
		{
			targetSpeed.x = Mathf.Sign(targetOffset.x) * m_CameraSpeed;
		}

		if(Mathf.Abs (targetOffset.y) > m_CameraThreshold.y)
		{
			targetSpeed.y = Mathf.Sign(targetOffset.y) * m_CameraSpeed;
		}

		m_CurrentSpeed = Vector3.Lerp(m_CurrentSpeed, targetSpeed, m_CameraAcceleration * Time.deltaTime);


	}
}
