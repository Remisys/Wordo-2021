using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Location : MonoBehaviour
{
	public GameObject ext;
	public GameObject filename;
	public GameObject heading;
	InputField _this;
	InputField _ext;
	InputField _filename;
	Text _heading;
	Level level;
	bool once = true;
	string projectpath;
    void Start()
    {
				level = GameObject.Find("Level_Manager").GetComponent<Level>();
				projectpath = Application.dataPath;
				_this = this.transform.gameObject.GetComponent<InputField>();
				_this.text = projectpath;
				_ext = ext.GetComponent<InputField>();
				_filename = filename.GetComponent<InputField>();
				_heading = heading.GetComponent<Text>();
				OnChange();
	}

    // Update is called once per frame
    void Update()
    {
			if(once){
    		_this.onValueChanged.AddListener(delegate {OnChange();});        
    		_filename.onValueChanged.AddListener(delegate {OnChange();});        
				_ext.onValueChanged.AddListener(delegate {OnChange();});        
				once = false;
			}

			
    }

	//React to every input change in those input field
	public void OnChange(){
			bool exists = Reader.DoExist(_filename.text, _ext.text, _this.text, false);
			_heading.text =  exists? _heading.text : "File Not Found";
			if(exists)
				level.StartLevel(Reader.Read(_filename.text, _ext.text, _this.text, false));
	}
}
