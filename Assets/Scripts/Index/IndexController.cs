using UnityEngine;
using System.Collections;

public class IndexController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevelWithGridSize(int gSize)
	{
		PlayerPrefs.SetInt("grid_size",gSize);
		Application.LoadLevel(1);
	}
}
