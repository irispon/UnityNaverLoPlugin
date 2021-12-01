using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scripted by WonWoo
public class NaverLoginPlugin :MonoBehaviour
{

    public static Action<string> SuccessCallback;
    public static Action FailCallback;


    private static string OAUTH_CLIENT_ID = "Bu4Jauce_5YJJkL5UINw";//네이버 어플리케이션 등록시 발급받는 아이디
    private static string OAUTH_CLIENT_SECRET = "u2LUeWwS28";//네이버 어플리케이션 등록시 발급받는 코드(보안 주의)
    private static string OAUTH_CLIENT_NAME = "네이버 아이디로 로그인";//접속시 뜰 이름
    public static NaverLoginPlugin GET
    {
        get
        {
            if (plugin == null)
            {
                plugin = new GameObject().AddComponent<NaverLoginPlugin>();
                plugin.name = "NaverLoginPlugin";//게임 오브젝트 이름을 가지고 message를 받으므로 이름 절대 바꾸면 안됨!
            }

            return plugin;
        }
    }
    private static NaverLoginPlugin plugin = null;

    AndroidJavaObject javaObj;
    AndroidJavaClass unityPlayer;
    AndroidJavaObject activity;
    AndroidJavaObject app;

    /// <summary>
    /// 처음 시작시 설정해야하는 코드(반드시)
    /// </summary>
    /// <param name="OAUTH_CLIENT_ID">네이버 어플리케이션 등록시 발급받는 아이디</param>
    /// <param name="OAUTH_CLIENT_SECRET">네이버 어플리케이션 등록시 발급받는 코드(보안 주의)</param>
    /// <param name="OAUTH_CLIENT_NAME">접속시 뜰 이름</param>
    public void Awake()
    {
        if(plugin == null)
        {
            plugin = this;
        }
        else if (plugin == this)
        {

        }
        else
        {
            Destroy(gameObject);
            return;
        }
        name = "NaverLoginPlugin";
        if (Application.platform == RuntimePlatform.Android)
        {
            var pluginClass = new AndroidJavaClass("com.example.plugin.NaverLoginTest");

            unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            app = activity.Call<AndroidJavaObject>("getApplicationContext");
            javaObj = pluginClass.CallStatic<AndroidJavaObject>("GetInstance");
            pluginClass.CallStatic("SetInfo", OAUTH_CLIENT_ID, OAUTH_CLIENT_SECRET, OAUTH_CLIENT_NAME);
    
            javaObj.Call("initData", app);
        }

        
    }

    /// <summary>
    /// unity message를 통해 callback됨
    /// </summary>
    public void NaverLoginCallBack(string message)
    {

        CallBack callBack=null;
        Debug.Log("callBack!   "+message);
        try
        {
            callBack = JsonUtility.FromJson<CallBack>(message);

            switch (callBack.message)
            {
                case Message.FAIL:
                    Debug.LogError("로그인 실패!");
                    if (FailCallback != null)
                        FailCallback();
                    break;
                case Message.SUCCESS:
                    Debug.Log("로그인 성공!"+ callBack.token);
                    if(SuccessCallback!=null)
                         SuccessCallback(callBack.token);
                    break;
            }

        }
        catch (Exception e)
        {
            Debug.Log("파싱 에러"+ message + "     "+e);
        }



    }
    public void AccessLogin()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            javaObj.Call("Click", activity);
        }
    } 

    class CallBack
    {
        public Message message; //0 = 실패, 1 = 성공
        public string token;
    }
    enum Message
    {
        FAIL,SUCCESS
    }

}
