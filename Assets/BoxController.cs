﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoxController : MonoBehaviour {

	//Declare UDPReceiver
	private UDPReceiver udprcv;
	
	//Boxes
	public GameObject systemObj1;
	public GameObject systemObj2;
	public GameObject systemObj3;
	public GameObject systemObj4;

	//Boxes & Flicker instances
	public Image box1;
	public Image box2;
	public Image box3;
	public Image box4;
	private BoxFlicker box_10hz;
	private BoxFlicker box_12hz;
	private BoxFlicker box_15hz;
	private BoxFlicker box_20hz;

	//Indicator in pics
	public GameObject systemObj21;
	public GameObject systemObj22;
	public GameObject systemObj23;
	public GameObject systemObj24;
	//public GameObject systemObj25;

	//Frame counter (NOT debug)
	private int flagMan = 0;
	
	//(For debug) Each boxes counter
	public GameObject systemObj5;
	public GameObject systemObj6;
	public GameObject systemObj7;
	public GameObject systemObj8;
	public GameObject systemObj9;
	public GameObject systemObj10;
	public Text text1;
	public Text text2;
	public Text text10;
	public Text text12;
	public Text text15;
	public Text text20;
	public Text text_Indicator1;
	public Text text_Indicator2;
	public Text text_Indicator3;
	public Text text_Indicator4;
	//public Text text_Indicator5;

	//(For debug) Received PORT number
	public GameObject systemObj11, systemObj12, systemObj13, systemObj14, systemObj15;
	public Text text_PORT1, text_PORT2, text_PORT3, text_PORT4, text_PORT5;

	//(For debug) Frame Counter and Elapsed Time
	private float updateDuration = 0.0f;
	private int updateFrameCounter = 0;

	//Flicker pattern arrays
	public int[] pattern30 = new int[] {
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 1, 0, 1, 0, 1
	};

	public int[] pattern20 = new int[] {
		0, 0, 1, 0, 0, 1, 0, 0, 1, 0,
		0, 1, 0, 0, 1, 0, 0, 1, 0, 0,
		1, 0, 0, 1, 0, 0, 1, 0, 0, 1,
		0, 0, 1, 0, 0, 1, 0, 0, 1, 0,
		0, 1, 0, 0, 1, 0, 0, 1, 0, 0,
		1, 0, 0, 1, 0, 0, 1, 0, 0, 1,
	};

	public int[] pattern15 = new int[] {
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
	};

	public int[] pattern12 = new int[] {
		0, 0, 0, 1, 1, 0, 0, 0, 1, 1,
		0, 0, 0, 1, 1, 0, 0, 0, 1, 1,
		0, 0, 0, 1, 1, 0, 0, 0, 1, 1,
		0, 0, 0, 1, 1, 0, 0, 0, 1, 1,
		0, 0, 0, 1, 1, 0, 0, 0, 1, 1,
		0, 0, 0, 1, 1, 0, 0, 0, 1, 1
	};

	public int[] pattern10 = new int[] {
		0, 0, 0, 1, 1, 1, 0, 0, 0, 1,
		1, 1, 0, 0, 0, 1, 1, 1, 0, 0,
		0, 1, 1, 1, 0, 0, 0, 1, 1, 1,
		0, 0, 0, 1, 1, 1, 0, 0, 0, 1,
		1, 1, 0, 0, 0, 1, 1, 1, 0, 0,
		0, 1, 1, 1, 0, 0, 0, 1, 1, 1
	};

	// Use this for initialization
	void Start () {
		//Define frame rate
		Application.targetFrameRate = 60;

		//Set UDPReceiver instance & Port number set
		udprcv = GetComponent<UDPReceiver> ();
		udprcv.PORT_SET (20321, 20322, 20323, 20324, 20326);

		//Set GetComponents (must be put here, or Start() function)
		box1 = systemObj1.GetComponent<Image>(); 
		box2 = systemObj2.GetComponent<Image>();
		box3 = systemObj3.GetComponent<Image>();
		box4 = systemObj4.GetComponent<Image>();

		//Indicators in pic
		text_Indicator1 = systemObj21.GetComponent<Text> ();
		text_Indicator2 = systemObj22.GetComponent<Text> ();
		text_Indicator3 = systemObj23.GetComponent<Text> ();
		text_Indicator4 = systemObj24.GetComponent<Text> ();
		//text_Indicator5 = systemObj25.GetComponent<Text> ();

		//(For debug)
		text1 = systemObj5.GetComponent<Text> ();
		text2 = systemObj6.GetComponent<Text> ();
		text10 = systemObj7.GetComponent<Text> ();
		text12 = systemObj8.GetComponent<Text> ();
		text15 = systemObj9.GetComponent<Text> ();
		text20 = systemObj10.GetComponent<Text> ();
		text_PORT1 = systemObj11.GetComponent<Text> ();
		text_PORT2 = systemObj12.GetComponent<Text> ();
		text_PORT3 = systemObj13.GetComponent<Text> ();
		text_PORT4 = systemObj14.GetComponent<Text> ();
		text_PORT5 = systemObj15.GetComponent<Text> ();

		//Set BoxFlicker instance
		box_10hz = systemObj1.AddComponent<BoxFlicker>(); 
		box_12hz = systemObj2.AddComponent<BoxFlicker>(); 
		box_15hz = systemObj3.AddComponent<BoxFlicker>(); 
		box_20hz = systemObj4.AddComponent<BoxFlicker>(); 

		box_10hz.Setting (pattern10, box1);
		box_12hz.Setting (pattern12, box2);
		box_15hz.Setting (pattern15, box3);
		box_20hz.Setting (pattern20, box4);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("========= For Debug =========");
	
		//(For Debug) Call UDPReceiver 
		Debug.Log ("udprcv(20321):" + udprcv.PORT_GET_1 ());
		Debug.Log ("udprcv(20322):" + udprcv.PORT_GET_2 ());
		Debug.Log ("udprcv(20323):" + udprcv.PORT_GET_3 ());
		Debug.Log ("udprcv(20324):" + udprcv.PORT_GET_4 ());
		Debug.Log ("udprcv(20326):" + udprcv.PORT_GET_5 ());

		//Show their UDP signals
		text_PORT4.text = udprcv.PORT_GET_4 ();
		text_PORT5.text = udprcv.PORT_GET_5 ();

		//=== Convert UDP acquired signals
		int tmpInt_p1, tmpInt_p2, tmpInt_p3;
		string tmpString_p1, tmpString_p2, tmpString_p3;

		//Stimulus
		tmpString_p1 = udprcv.PORT_GET_1 ();
		tmpInt_p1 = System.Int32.Parse (tmpString_p1) - 33024;
		text_PORT1.text = tmpInt_p1.ToString ();

		//Target
		tmpString_p2 = udprcv.PORT_GET_2 ();
		tmpInt_p2 = System.Int32.Parse (tmpString_p2) - 33024;
		text_PORT2.text = tmpInt_p2.ToString ();

		//Result
		tmpString_p3 = udprcv.PORT_GET_3 ();
		tmpInt_p3 = System.Int32.Parse (tmpString_p3) - 33024;
		text_PORT3.text = tmpInt_p3.ToString ();
	

		//Depict stimulus position on pic
		// -- init
		if (text_PORT4.text.Contains ("32774")) {
			text_PORT2.text = "33024";
		}

		// -- depict1 ~ 4
		if (text_PORT2.text.Contains ("33024")) { 
			text_Indicator1.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator2.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator3.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator4.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_PORT3.text = "-";
		} else if (text_PORT2.text.Contains ("33025")) {
			text_Indicator1.color = new Color (1.00f, 1.00f, 0.00f, 1.00f);
			text_Indicator2.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator3.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator4.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
		} else if (text_PORT2.text.Contains ("33026")) {
			text_Indicator1.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator2.color = new Color (1.00f, 1.00f, 0.00f, 1.00f);
			text_Indicator3.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator4.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
		} else if (text_PORT2.text.Contains ("33027")) {
			text_Indicator1.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator2.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator3.color = new Color (1.00f, 1.00f, 0.00f, 1.00f);
			text_Indicator4.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
		} else if (text_PORT2.text.Contains ("33028")) {
			text_Indicator1.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator2.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator3.color = new Color (1.00f, 1.00f, 0.00f, 0.00f);
			text_Indicator4.color = new Color (1.00f, 1.00f, 0.00f, 1.00f);
		}

		//(For Debug) Frame Counter and Elapsed Time
		updateDuration += Time.deltaTime;
		++updateFrameCounter;
			
		if (updateFrameCounter % 60 == 0) {
			text1.text = updateDuration.ToString ();
			text2.text = updateFrameCounter.ToString ();
		}

		//Switch
		if (text_PORT5.text.Contains ("32773")) {
			++flagMan;
		} else {
		//} else if (text_PORT1.text.Contains ("OVTK_StimulationId_ExperimentStop")) {
			flagMan = 0;
		}

		if (flagMan == 60) 
			flagMan = 0;

		//Flash box
		box_10hz.Box (flagMan);
		box_12hz.Box (flagMan);
		box_15hz.Box (flagMan);
		box_20hz.Box (flagMan);

		//(For Debug) Counter to assure flashing frequencies for each boxes on production
		text10.text = box_10hz.GetCounter ();
		text12.text = box_12hz.GetCounter ();
		text15.text = box_15hz.GetCounter ();
		text20.text = box_20hz.GetCounter ();
	
	}
}
