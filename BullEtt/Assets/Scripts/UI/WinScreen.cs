using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
	public void LoadMainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
