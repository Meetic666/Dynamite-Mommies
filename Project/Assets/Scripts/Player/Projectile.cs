using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public int m_Damage;
	public float m_Speed;

	int m_Direction;
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += Vector3.right * m_Direction * m_Speed;
	}

	public void SetDirection(int direction)
	{
		m_Direction = direction;
	}
}
