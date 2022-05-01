using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class UICtrlDash : MonoBehaviour
{
    public Text dashFullVarTxt;
    public Text dashCountVarTxt;
    int prevDashCount = -1;
	// Update is called once per frame
	private void Start()
	{
		dashFullVarTxt.text = "/ " + PlayerController.Instance.DashFull;
		dashCountVarTxt.text = PlayerController.Instance.DashCount.ToString();
	}
	void Update()
    {
        if(prevDashCount != PlayerController.Instance.DashCount)
		{
			dashCountVarTxt.text = PlayerController.Instance.DashCount.ToString();
			prevDashCount = PlayerController.Instance.DashCount;
		}
    }
}
