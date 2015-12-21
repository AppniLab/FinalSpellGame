using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

	public Transform returnToParent = null;
	public bool isDraggable = true;

	GameObject placeHolderObj = null;
	CanvasGroup cg = null;
	Vector3 originalScale;


	public void OnBeginDrag(PointerEventData ed)
	{
//		if(!DragDropSettings.Instance.isDropZoneEmpty)
//			return;
		if(!isDraggable)
			return;

		DragDropSettings.Instance.isDragStarted  = true;

		cg = this.GetComponent<CanvasGroup>();
		cg.blocksRaycasts = false;

		returnToParent = this.transform.parent.transform;

		placeHolderObj = new GameObject("PlaceHolder");
		LayoutElement le = placeHolderObj.AddComponent<LayoutElement>();
		le.preferredWidth = 120;
		le.preferredHeight = 80;
		placeHolderObj.transform.SetParent(this.transform.parent.transform);
		placeHolderObj.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
		this.transform.SetParent( this.transform.parent.parent.transform);
		this.GetComponent<RectTransform>().sizeDelta = new Vector2(120,80);

		originalScale = this.transform.localScale;
		//Vector3 newScale = new Vector3(originalScale.x+0.5f,originalScale.y+0.5f,originalScale.z+0.5f);
		//this.transform.localScale = newScale;
	}

	public void OnDrag(PointerEventData ed)
	{
//		if(!DragDropSettings.Instance.isDropZoneEmpty)
//			return;

		if(!isDraggable)
			return;

		this.transform.localScale = originalScale;

		this.transform.position = ed.position;

	}

	public void OnEndDrag(PointerEventData ed)
	{
//		if(!DragDropSettings.Instance.isDropZoneEmpty)
//			return;
//
		if(!isDraggable)
			return;

		Debug.Log("return To patent: "+returnToParent.name);
		//this.transform.localScale = originalScale;
		DragDropSettings.Instance.isDragStarted  = false;
		this.transform.SetParent(returnToParent);
		this.transform.SetSiblingIndex(placeHolderObj.transform.GetSiblingIndex());
		if(returnToParent.name == "GridPanel") // Original Position (Grid)
		{
			isDraggable = true;
			Destroy(placeHolderObj);
		}
		else // Answer Panel
		{
			isDraggable = false;
			AnswerController.Instance.EnableNextDropZone();
			GridController.Instance.RemoveLetterForGridArray(this.transform.GetChild(0).GetComponent<Text>().text.ToString().ToLower());
		}

		this.transform.localPosition = new Vector3(0,0,0);
		cg.blocksRaycasts = true;
		if(returnToParent.name.Contains(DragDropSettings.Instance.dropZoneName))
		{ 
			this.transform.SetSiblingIndex(1); // if we drop over dropZone we want it to be over our text element
			//DragDropSettings.Instance.isDropZoneEmpty = false;
		}
	}

	
}
