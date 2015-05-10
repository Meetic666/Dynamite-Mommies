using UnityEngine;
using System.Collections;

public class ProgressManager : MonoBehaviour 
{
	bool[] m_PartsPickedUp;

	int m_NumberOfPartsPickedUp;

	// Use this for initialization
	void Start () 
	{
		m_PartsPickedUp = new bool[(int)KoalaPartEnum.e_Count];
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void CollectKoalaPart(KoalaPartEnum part)
	{
		m_PartsPickedUp[(int)part] = true;

		m_NumberOfPartsPickedUp++;
	}
}
