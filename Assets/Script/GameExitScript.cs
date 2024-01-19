using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitScript : MonoBehaviour {

    uint exitCountValue = 0;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            exitCountValue++;
            if (!IsInvoking("disable_DoubleClick"))
                Invoke("disable_DoubleClick", 1.0f);
        }
        if (exitCountValue == 2)
        {
            CancelInvoke("disable_DoubleClick");
            Application.Quit();
        }
		
	}
    void disable_DoubleClick()
    {
        exitCountValue = 0;
    }
}
