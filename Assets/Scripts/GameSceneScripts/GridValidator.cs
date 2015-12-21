using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
using System.Linq;
using System.Collections.Generic;

public class GridValidator  
{
	static string filePath = "";
	static ArrayList myDictionary = new ArrayList();
	static string[] gridStrArray;
	static bool isGridValid = true;



	public static bool  CheckGridValidation(string[] gridstr)
	{
		bool isThisGridValid = false;
		int wordLength = 3;
		while(wordLength <= gridstr.Length)
		{
			if(GridStatus(gridstr,wordLength))
			{
				isThisGridValid = true;
				Debug.Log("Grid is  Valid");
				break;
			}
			else
			{
				Debug.Log("Grid is not Valid");
				isThisGridValid = false;
				wordLength++;
			}
		}
		
		return isThisGridValid;
	}
	
	private static void CreateStringArray(string grid,string[] gridstrarray)
	{
		char[] strChar = grid.ToCharArray();
		for(int i = 0 ;i< strChar.Length;i++)
		{
			gridstrarray[i]= strChar[i].ToString();
		}
		
	}

	private static bool GridStatus(string[] grid,int wordLength)
	{
		//string str = "faste";
		isGridValid = false;
		gridStrArray = new string[grid.Length];
		//CreateStringArray(grid,gridStrArray);
		var letters =  grid; 
		//StreamWriter sw = new StreamWriter(filePath,true);
		foreach (var val in Sequence(letters, wordLength))
		{
			if( Bank.isThisWordExist(val))
			{
				Debug.Log("Value :"+val);
				//sw.WriteLine(val);
				isGridValid = true;
				break;
			}
			else
				isGridValid = false;
		}
		
		//sw.Close();
		//sw.Dispose();
		Debug.Log("is Grid Valild :"+isGridValid);
		return isGridValid;
	}
	
	private static IQueryable<string> Sequence(string[] alphabet, int size)
	{
		// create the first level
		var sequence = alphabet.AsQueryable();
		size = Mathf.Min(size,alphabet.Length);
		// add each subsequent level
		for (var i = 1; i < size; i++)
			sequence = AddLevel(sequence, alphabet);
		
		return from value in sequence
			orderby value
				select value;
	}
	
	private static IQueryable<string> AddLevel(IQueryable<string> current, string[] characters)
	{
		return from one in current
			from character in characters
				select one + character;
	}
}
