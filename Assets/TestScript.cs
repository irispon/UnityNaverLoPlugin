using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    NaverLoginPlugin plugin;
    // Start is called before the first frame update
    void Start()
    {

        plugin= NaverLoginPlugin.GET;
        //var pluginClass = new AndroidJavaClass("com.example.plugin.NaverLoginTest");

        //unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        //app = activity.Call<AndroidJavaObject>("getApplicationContext");
        //javaObj = pluginClass.CallStatic<AndroidJavaObject>("GetInstance");
        //javaObj.Call("initData",app);
    }

    // Update is called once per frame
    void Update()       
    {
        
    }

    public void Click()
    {
        plugin.AccessLogin();
    }
}
