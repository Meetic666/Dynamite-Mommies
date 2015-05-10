using UnityEngine;
using System.Collections;

public class ProgressManager : MonoBehaviour 
{
	public GameObject[] m_PartDisplay;
	int m_NumberOfPartsPickedUp;
	
	public GameObject m_Camera;
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void EndBossFight()
	{
		foreach(BoxCollider collider in GetComponents<BoxCollider>())
		{
			collider.isTrigger = true;
		}

		GetComponent<BoxCollider>().enabled = false;

		m_Camera.GetComponent<CameraFollow> ().EndBossFight ();
	}

	public void CollectKoalaPart(KoalaPartEnum part)
	{
		m_PartDisplay[(int)part].SetActive (true);

		m_NumberOfPartsPickedUp++;
	}

	void OnTriggerExit(Collider other)
	{
		if(m_NumberOfPartsPickedUp >= m_PartDisplay.Length && other.tag == "Player" && other.GetComponent<Movement>() && other.GetComponent<Movement>().HorizontalSpeed > 0.0f)
		{
			foreach(BoxCollider collider in GetComponents<BoxCollider>())
			{
				collider.isTrigger = false;
			}

			m_Camera.GetComponent<CameraFollow>().BeginBossFight();
		}
	}
}
