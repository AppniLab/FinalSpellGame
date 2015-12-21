using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridController : MonoBehaviour {

	// Public Variables
	public static GridController Instance = null;
	public int gridSize;
	public GameObject gridElementPrefab;
	public GameObject gridParent = null;


	private ArrayList gridElementList = new ArrayList();
	private string gridString = "";
	private ArrayList gridCharList = new ArrayList();
	private string[] strArray;
	void Awake()
	{
		if(Instance == null)
			Instance = this;
	}

	void Start () 
	{
		Bank.CreateDictionary();
		if(PlayerPrefs.HasKey("grid_size"))
			gridSize = PlayerPrefs.GetInt("grid_size");

		StartCoroutine(CreateMyGrid());
//		bool isExist = GridValidator.CheckGridValidation(new []{"f","a","s","t","e"});
//		Debug.Log("isExist :"+isExist.ToString());
	}

	void Update () {
	
	}


	public IEnumerator CheckGridStatus()
	{
		yield return null;
		/*ArrayList newLetterList = new ArrayList();
		for(int i = 0;i<gridParent.transform.childCount;i++)
		{
			GameObject obj = gridParent.transform.GetChild(i).gameObject;
			if(obj.tag == "letter")
			{
				newLetterList.Add(obj.transform.GetChild(0).GetComponent<Text>().text.ToLower());
			}
		}*/
		strArray = null;
		strArray = new string[gridCharList.Count];

		strArray = MyHelperMethods.ArrayListToStringArray(strArray,gridCharList);
		//Debug.Log("Now is hthis grid Valid :"+ GridValidator.CheckGridValidation(strArray));
		//Debug.Log("~~~~~~~~~~~Str Array Count: "+strArray.Length.ToString());
		if(!GridValidator.CheckGridValidation(strArray))
		{
			StartCoroutine(CreateMyGrid());
		}
	}

	public void RemoveLetterForGridArray(string s)
	{
		//Debug.Log("##### Remove this Letter :"+s);
		gridCharList.Remove(s);
	}

	#region GridCreationMethods
	IEnumerator SetGridString()
	{
		gridString = Bank.GetRandomWord();
		//Debug.Log("-----Random Word------ :"+gridString);
		gridCharList.Clear();
		if(gridString.Length < (gridSize*gridSize))
		{
			for(int i = 0; i< (gridSize*gridSize)-(gridString.Length);i++)
			{
				string randomStr = MyHelperMethods.GetLetter().ToString();
				//Debug.Log("@@@@ random Str @@@@: "+ randomStr);
				gridCharList.Add(randomStr);

			}
		}
		char[] ch = gridString.ToCharArray();
		foreach(char c in ch)
		{
			gridCharList.Add(c.ToString());
		}
		gridCharList = MyHelperMethods.RandomizeArray(gridCharList);
		strArray = new string[gridCharList.Count];
		for(int i= 0 ;i< gridCharList.Count;i++)
		{
			strArray[i] = gridCharList[i].ToString();
			//Debug.Log(">>>>>>.string Value>>>>>>>>>>>>> :"+strArray[i].ToString());
		}
		yield return null;
		//bool isExist = GridValidator.CheckGridValidation(strArray);
		//Debug.Log("isExist :"+isExist.ToString());
		//Debug.Log(GridValidator.CheckGridValidation(strArray));
	}

	void SetElementSize()
	{
		float parentHeight = gridParent.GetComponent<RectTransform>().rect.height;

		int elementSize =(int) parentHeight/gridSize;

		gridParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(elementSize,elementSize);

	}

	public IEnumerator CreateMyGrid()
	{
		ClearGridPanel();
		SetElementSize();
		StartCoroutine(SetGridString());
		for(int i = 0; i<(gridSize*gridSize);i++)
		{
			GameObject Obj = Instantiate(gridElementPrefab,Vector3.zero,Quaternion.identity) as GameObject;
			Obj.transform.SetParent(gridParent.transform);
			gridElementList.Add(Obj);
			Obj.transform.GetChild(0).GetComponent<Text>().text = gridCharList[i].ToString().ToUpper();
			Obj.AddComponent<Draggable>();
			yield return null;
		}

	}


	void ClearGridPanel()
	{
		if(gridParent.transform.childCount ==0)
		{
			return;
		}

		for(int i = 0 ;i<gridParent.transform.childCount;i++)
		{
			Destroy(gridParent.transform.GetChild(i).gameObject);
			//yield return null;
		}
	}
	#endregion
}
