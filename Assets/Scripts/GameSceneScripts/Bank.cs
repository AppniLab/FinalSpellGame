using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class Bank{

	private static string filePath = "";
	private static ArrayList myDictionaryList = new ArrayList();

	public static void CreateDictionary()
	{
		string dictPath = Application.dataPath +"/Resources/wordsdict.txt";
		try 
		{
			
			using (StreamReader sr = new StreamReader(dictPath)) 
			{
				string word;
				while ((word = sr.ReadLine()) != null) 
				{
					//Debug.Log("word :"+word);
					myDictionaryList.Add(word);
				}
			}
		}
		catch (Exception e) 
		{
			Debug.Log("The file could not be read");
		}
		
	}

	public static bool isThisWordExist(string str)
	{
		bool isExist = false;
		if(myDictionaryList.Contains(str))
			isExist = true;
		else
			isExist = false;

		return isExist;
	}

	public static string GetRandomWord()
	{
		int strIndex = UnityEngine.Random.Range(0,myDictionaryList.Count);

		return (string)myDictionaryList[strIndex];

	}

}
