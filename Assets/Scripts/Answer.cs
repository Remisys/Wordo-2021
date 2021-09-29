using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StatesNameSpace;
public class Answer : MonoBehaviour
{
public Color green;
public Color red;
public Color transparent;
public float timer = 1;
float timer_current = 0;
bool green_state = true; 

InputField that;
ColorBlock cb;
Level lvl;
Text placeholder;
    // Start is called before the first frame update
    void Start()
    {	
			that = transform.gameObject.GetComponent<InputField>(); 
			placeholder = that.placeholder.GetComponent<Text>();
    	cb = that.colors;	
			lvl = GameObject.Find("Level_Manager").GetComponent<Level>();
		}

    // Update is called once per frame
    void Update()
		{
			if(timer_current > 0){
				timer_current -= Time.deltaTime;
				if(green_state){
					cb.normalColor = Color.Lerp(transparent, green, timer_current / timer);
					cb.selectedColor  = cb.disabledColor = cb.pressedColor = cb.highlightedColor = cb.normalColor;
				}
				else{
					cb.normalColor = Color.Lerp(transparent, red, timer_current / timer);
			  	cb.selectedColor  = cb.disabledColor = cb.pressedColor = cb.highlightedColor = cb.normalColor;
				}
				that.colors = cb;
			}
			else{
				timer_current = 0;
			}
			if(!that.text.Equals("") &&  lvl.GetState() == States.WAIT){
				lvl.GoToPlayMode();
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Return)) {
				OnEnter();
			}
    }

		public void GoRed(){
			timer_current = timer;
			green_state = false;	
		}

		public void GoGreen(){
			timer_current = timer;
			green_state = true;
		}
		
		void OnEnter(){
			bool result = lvl.CheckAnswer(that.text);
			if(result){ 
				GoGreen();
				placeholder.text = "";
			}
			else{ 
				GoRed();
				placeholder.text = lvl.GetCurrentAnswer();
			}
			that.text = "";
			that.Select();
			that.ActivateInputField();
		}

		
}
