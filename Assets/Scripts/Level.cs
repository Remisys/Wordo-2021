using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StatesNameSpace;
public class Level : MonoBehaviour
{
		List<Card> cards;
		public States state = States.IDLE;
		
		public GameObject wrong;
		public GameObject right;
		public GameObject done;
		public GameObject time;
		public GameObject count;
 
		float counter = 0f;
		int richtig = 0;
		int falsch = 0;
		int cardsdone = 0; 	
		int totalcards = 0; 
		
		Card current;
		
		public GameObject maintext;	
		Text _text;
    // Start is called before the first frame update
    void Start()
    {	
       _text = maintext.GetComponent<Text>();
    }
		
    // Update is called once per frame
    void Update()
    {
    	if(state == States.IDLE){
				
			}
			else if(state == States.WAIT){
				_text.text = current.GetText();
			}
			else if(state == States.PLAY){
				counter += Time.deltaTime;
				SetInfoText(time, Mathf.Floor(counter), "s");
			}
			else if(state == States.DONE){
				_text.text = "Well done!";
			}    
    }
		
		public States GetState(){
			return state;
		}
		
		void SetInfoText(GameObject g, object s){
			SetInfoText(g,s,"");
		}

		void SetInfoText(GameObject g, object s, string extra){	
			g.GetComponent<Text>().text = Convert.ToString(s) + extra;
		}
		public string GetCurrentAnswer(){
			if(current == null) return null;
			return current.GetCurrentAnswer();		
		}
		
		public void SetState(States s){
			state = s;
		}			
		public void StartLevel(List<Card> cs){
			Reset();
			if(cs.Count > 0){
				cards = cs;
				totalcards = cards.Count;
				SetInfoText(count, totalcards);
				state = States.WAIT;
				current = NextCard();
			}
			else{
				state = States.IDLE;
			}
		}
		
		public void GoToPlayMode(){
			state = States.PLAY;
		}

		//Pops up the next card on the list
		public Card NextCard(){
			if(Empty()){
				state = States.DONE;
				return null;
			}
			Card result = cards[0];
			cards.RemoveAt(0);
			return result;
		}

		//Checking the answer while also updating the stats
		public bool CheckAnswer(string s){
			if(state == States.PLAY){
				bool result = current.Answer(s);
				if(current.CardFinished()){
					current = NextCard();
					cardsdone++;

					SetInfoText(done, Mathf.Floor((float) cardsdone*100/totalcards ), "%");
				}
				if(result){
					richtig++;
					SetInfoText(right, richtig, "x");
				}
				else{ 
					falsch++;
					SetInfoText(wrong, falsch, "x");
				}
				if(current != null) _text.text = current.GetText();
				return result;
			}
			return false;
		}	

		//Are there any cards left remaining
		public bool Empty(){
			return cards.Count ==  0;
		}

		//Reset the stats for a new fresh round
		public void Reset(){
			SetInfoText(wrong, "0x");
			SetInfoText(right, "0x");
			SetInfoText(done, "0%");
			SetInfoText(time, "0s");
			SetInfoText(count, "0");
			totalcards = cardsdone = richtig = falsch = 0;
			counter = 0; 
		}
}

namespace StatesNameSpace{
		public enum States{IDLE, WAIT, PLAY, DONE};
}
