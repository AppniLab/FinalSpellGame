using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerController : MonoBehaviour {

	public static AnswerController Instance = null;

	public GameObject AnswerZone = null;
	private int currentDropZoneIndex = 0;
	private int letterCount = 0;
	private int answerChildCount;
	private string finalLetter = "";
	private int maxWordlength = 6;
	void Awake()
	{
		if(Instance == null)
			Instance = this;
	}
	// Use this for initialization
	void Start () 
	{
		answerChildCount = AnswerZone.transform.childCount;

	}

	#region Answer_Helper_methods
	void CheckStatusOfAnswerZone()
	{
		for(int i =0 ;i<answerChildCount;i++)
		{
			if(AnswerZone.transform.GetChild(i).transform.childCount >0)
			{
				letterCount ++;
			}
		}
	}

	private string ReadLetter()
	{
		for(int i =0 ;i<answerChildCount;i++)
		{
			GameObject childObj = AnswerZone.transform.GetChild(i).gameObject;
			if(childObj.transform.childCount >0)
			{
				finalLetter +=	 childObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text;
			}
		}

		return finalLetter;
	}

	public void EnableNextDropZone()
	{
		AnswerZone.transform.GetChild(currentDropZoneIndex).GetComponent<DropZone>().enabled = false;
		currentDropZoneIndex ++;
		if(currentDropZoneIndex <maxWordlength)
		{
			AnswerZone.transform.GetChild(currentDropZoneIndex).GetComponent<DropZone>().enabled = true;
		}

	}

	IEnumerator ClearAnswerZone()
	{
		for(int i = 0;i < AnswerZone.transform.childCount ;i++)
		{
			GameObject chilObj = AnswerZone.transform.GetChild(i).gameObject;
			if(chilObj.transform.childCount >0)
				Destroy (chilObj.transform.GetChild(0).gameObject);
			yield return null;
		}
		AnswerZone.transform.GetChild(0).gameObject.GetComponent<DropZone>().enabled =true;
		currentDropZoneIndex =0;
		StartCoroutine(GridController.Instance.CheckGridStatus());
	}

	#endregion

	#region UI_Handler

	IEnumerator CheckAnswerHandler()
	{
		yield return null;
		//CheckStatusOfAnswerZone();
		ReadLetter();
		finalLetter = "";
		if(finalLetter.Length == 0)
			Debug.Log("No Letter added");
		else
		{
			Debug.Log("final Letter :"+ finalLetter);
			Debug.Log(Bank.isThisWordExist(finalLetter.ToLower()));
		}
		StartCoroutine(ClearAnswerZone());

	}

	public void CheckAnswer()
	{
		StartCoroutine(CheckAnswerHandler());
	}
	#endregion




}
