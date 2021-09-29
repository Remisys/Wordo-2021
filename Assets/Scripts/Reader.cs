using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Reader {

char seperator = Path.DirectorySeparatorChar;
static string projectpath = Application.dataPath;
enum States{IDLE, TEXT, ANSWER, DONE}; 

public static List<Card> Read(string name, string ext, string filepath, bool useprojectpath){
	if(!DoExist(name, ext, filepath, useprojectpath)) return null; 
	States state = States.IDLE;
	List<Card> result = new List<Card>();
	List<string> answers = new List<string>();
	
	string line;
	StringBuilder sb = new StringBuilder();
	StreamReader sr = new StreamReader(FullPath(name, ext, filepath, useprojectpath));
	
	while((line = sr.ReadLine()) != null){
		if(line.Equals("CARD") && state == States.IDLE){ state = States.TEXT; continue;}
		else if(line.Equals("<A>") && state == States.TEXT){ state = States.ANSWER; continue;}
		else if(line.Equals("</A>") && state == States.ANSWER)state = States.DONE;
		
		if(state == States.TEXT) sb.Append(line + "\n");
		else if(state == States.ANSWER) answers.AddRange(line.Split(','));
		else if(state == States.DONE){
			state = States.IDLE;
			string s = sb.ToString();
			if(s.Length > 0 && answers.Count > 0){			
				Card c = new Card(s, answers);
				result.Add(c);
				answers.Clear();
			}
			sb.Clear();
		}
	}
	sr.Close();
	return result;
}

public static bool DoExist(string name, string ext, string filepath, bool useprojectpath){
	return File.Exists(FullPath(name, ext, filepath, useprojectpath));
}

public static string CombineFile(string name, string ext){
	return name + ext;
}

public static string FullPath(string name, string ext, string filepath, bool useprojectpath){         
	return useprojectpath? Path.Combine(projectpath, filepath, CombineFile(name, ext)) : Path.Combine(filepath, CombineFile(name,ext));
}
}
