using UnityEngine;
using System.Collections;
using System.Text;
using System;
using System.Linq;

public class MyHelperMethods  
{
	/*public static string[] RandomizeArray(string[] arr)
	{
		for (int i = arr.Length - 1; i > 0; i--) {
			int r = UnityEngine.Random.Range(0,i);
			var tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}
		return arr;
	}*/

	public static ArrayList RandomizeArray(ArrayList arr)
	{
		for (int i = arr.Count - 1; i > 0; i--) {
			int r = UnityEngine.Random.Range(0,i);
			var tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}
		return arr;
	}

	public static string[] ArrayListToStringArray(string[] letterArray,ArrayList letterList)
	{
		//string[] strArray = new string[letterList.Count];
		Debug.Log("Letter List count :"+letterList.Count.ToString());
		for(int i= 0 ;i< letterList.Count;i++)
		{
			letterArray[i] = letterList[i].ToString();
			Debug.Log("@@@@@ Letter :@@"+letterList[i].ToString());
		}
		return letterArray;
	}

	public static char GetLetter()
	{
		string chars = "abcdefghijklmnopqrstuvwxyz";

		int num = UnityEngine.Random.Range(0, chars.Length -1);
		return chars[num];
		
	}
}
