using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Card 
{
	string keyword = "...";
	string text; 
	List<string> answers;
	public Card(string t, List<string> strs){
		SetText(t);
		AddAnswers(strs);
	}
	public void SetText(string t){
		text = t;
	}
	public void AddAnswers(List<string> strs){
		answers = new List<string>(strs);
	}

	//Try to answer the 'card'. Returns a bool based on the correctness of your answer
	public bool Answer(string t){
		if(!CardFinished()){
			string s = answers[0];
			if(s.ToLower().Equals(t.ToLower())){
				answers.RemoveAt(0);
				text = replace(text, keyword, s);
				return true;
			}
			return false;
		}
		else 
			return false;
	}

	//Get the answer to the first blank line if available
	public string GetCurrentAnswer(){
		if(CardFinished()) return null;
		return answers[0];
	}

	//It replaces the keyword 'key' in the string full with new keyword 'replacement'
	public static string replace(string full, string key, string replacement){
		if(full.IndexOf(key) == -1) return full;
		string left = full.Substring(0, full.IndexOf(key)); 
		string right = full.Substring(full.IndexOf(key) + key.Length);
		return left + replacement + right;
	}

	//Get the current text of the card. Subjects to change when answered
	public string GetText(){
		return text;
	}

	//Is the card finished?
	public bool CardFinished(){
		return answers.Count == 0;
	}
	
}
