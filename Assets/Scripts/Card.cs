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
	public bool Answer(string t){
		if(!CardFinished()){
			string s = answers[0];
			if(s.Equals(t)){
				answers.RemoveAt(0);
				text = replace(text, keyword, s);
				return true;
			}
			return false;
		}
		else 
			return false;
	}

	public string GetCurrentAnswer(){
		if(CardFinished()) return null;
		return answers[0];
	}

	public static string replace(string full, string key, string replacement){
		if(full.IndexOf(key) == -1) return full;
		string left = full.Substring(0, full.IndexOf(key)); 
		string right = full.Substring(full.IndexOf(key) + key.Length);
		return left + replacement + right;
	}	
	public string GetText(){
		return text;
	}
	public bool CardFinished(){
		return answers.Count == 0;
	}
	
}
