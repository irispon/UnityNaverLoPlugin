using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetCheckTestCode : MonoBehaviour
{
    // Start is called before the first frame update
    public Text test;
    void Start()
    {
      if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("tes");
        }
    }

    // Update is called once per frame
    public void Update()
    {
        try
        {
            test.text = "    ¼¼±â " + WNetChecker.NetCheckerPlugin.Get.GetNetWorkStrength()+ WNetChecker.NetCheckerPlugin.Get.GetNetWorkState().ToString();
        }
        catch(Exception e)
        {
            test.text = e.ToString();
        }
      
    }
}
