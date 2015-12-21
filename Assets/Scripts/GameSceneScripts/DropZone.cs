using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler,IPointerExitHandler 
{
	RectTransform parentRect;
	GameObject dragObj;
	void Start()
	{
		DragDropSettings.Instance.dropZoneName = this.gameObject.name;
		parentRect = this.transform.GetComponent<RectTransform>();
	}

	public void OnPointerEnter(PointerEventData ed)
	{
		if(!DragDropSettings.Instance.isDragStarted)
			return;
		Color changeColor = new Color(200f/256f,220f/256f,140f/256f,256f/256f);
		Color newColor	 = this.GetComponent<Image>().color;
		newColor = changeColor;
		this.GetComponent<Image>().color = newColor;

	}

	public void OnDrop(PointerEventData ed)
	{
		dragObj = ed.pointerDrag;
		RectTransform objRect = dragObj.transform.GetComponent<RectTransform>();
		objRect.anchorMin = new Vector2(0.5f,0.5f);
		objRect.anchorMax = new Vector2(0.5f,0.5f);
		objRect.sizeDelta = parentRect.sizeDelta; //new Vector2(200,140); //new Vector2(140,100);
		dragObj.GetComponent<Draggable>().returnToParent = this.transform;
		this.GetComponent<Image>().color = new Color(256f/256f,256f/256f,256f/256f,256f/256f);
		//dragObj.GetComponent<Draggable>().isDraggable = false;
		//DDOptionController.Instance.CheckForAnswer(dragObj);
	}

	public void OnPointerExit(PointerEventData ed)
	{
		if(!DragDropSettings.Instance.isDragStarted)
			return;
		if(dragObj)
		{
			dragObj.GetComponent<CanvasGroup>().interactable = false;
			//dragObj.GetComponent<Draggable>().enabled  = false;
		}
		this.GetComponent<Image>().color = new Color(256f/256f,256f/256f,256f/256f,256f/256f);
	}

}
