using UnityEngine;
using System.Collections;

public class DragDropSettings : MonoBehaviour {

	public static DragDropSettings Instance = null;
	public bool isDragStarted = false;
	public bool isDropZoneEmpty = true;
	public string dropZoneName = "";

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
			Destroy(this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
