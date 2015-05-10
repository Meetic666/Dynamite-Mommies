using UnityEngine;
using System.Collections;

public class Intro_Camera : MonoBehaviour 
{
	public float m_CreditsSpeed;

	public Rect m_InitialListPosition;
	public Rect m_BackgroundSize;

	public Texture2D[] m_DevNames = new Texture2D[5];
	public Texture2D m_Background;

	void Start()
	{
		m_BackgroundSize = Camera.main.pixelRect;
	}

	void Update()
	{
		m_InitialListPosition.y -= Time.deltaTime * m_CreditsSpeed;

		if(m_InitialListPosition.y + m_InitialListPosition.height * m_DevNames.Length <= -m_InitialListPosition.height)
		{
			Application.LoadLevel ("Main");
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void OnGUI()
	{
		GUI.DrawTexture (m_BackgroundSize, m_Background);

		for(int i = 0; i < m_DevNames.Length; i++)
		{
			GUI.DrawTexture(new Rect(m_InitialListPosition.x, m_InitialListPosition.y + (m_InitialListPosition.height * i), m_InitialListPosition.width, m_InitialListPosition.height), m_DevNames[i]);
		}
	}
}
