using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour{
public GameObject Location;
	
	void OnMouseDown(){
		Location.GetComponent<Location>().OnChange();		
	}

}
