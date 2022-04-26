using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class UICtrlDash : MonoBehaviour
{
    public Text dashFullVarTxt;
    public Text dashCountVarTxt;
    public PlayerController pc;
    int prevDashCount = -1;
	// Update is called once per frame
	private void Start()
	{
		dashFullVarTxt.text = "/ " + pc.DashFull;
		dashCountVarTxt.text = pc.DashCount.ToString();
	}
	void Update()
    {
        if(prevDashCount != pc.DashCount)
		{
			dashCountVarTxt.text = pc.DashCount.ToString();
			prevDashCount = pc.DashCount;
		}
    }
}
