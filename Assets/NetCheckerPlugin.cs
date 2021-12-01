using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WNetChecker
{
    public enum NetState
    {
         None,G2,G3,LTE,G5,WIFI
    }
    public class NetCheckerPlugin:MonoBehaviour
    {
        public AndroidJavaObject instance;
        public static NetCheckerPlugin Get;
        public void Awake()
        {
            Get = this;

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject app = activity.Call<AndroidJavaObject>("getApplicationContext");

            var pluginClass = new AndroidJavaClass("com.example.netcheckerplugin.WNetChecker");
          //  Debug.Log("plugin is null" + (pluginClass==null));
            instance = pluginClass.CallStatic<AndroidJavaObject>("Instance",app);
         //   Debug.Log("instance is null"+ (instance==null));
        }


        public NetState GetNetWorkState()
        {
           // Debug.Log("get net? instance"+ (instance==null));
            if (instance == null)
            {
                return NetState.None;
            }
            else
            {
            //    Debug.Log(("넷 체크 "+instance.Call<int>("GetNetWorkState")));
                return (NetState)(instance.Call<int>("GetNetWorkState"));
            }

        }
        public float GetNetWorkStrength()
        {
            if (instance == null)
            {
                return 0;
            }
            else
            {
             //   Debug.Log("강도 체크 "+ instance.Call<int>("getRSSI"));
                return instance.Call<int>("getRSSI");
            }
        
        }
    }   
}

