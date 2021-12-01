using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scripted by WonWoo
public class NaverLoginPlugin :MonoBehaviour
{

    public static Action<string> SuccessCallback;
    public static Action FailCallback;


    private static string OAUTH_CLIENT_ID = "Bu4Jauce_5YJJkL5UINw";//���̹� ���ø����̼� ��Ͻ� �߱޹޴� ���̵�
    private static string OAUTH_CLIENT_SECRET = "u2LUeWwS28";//���̹� ���ø����̼� ��Ͻ� �߱޹޴� �ڵ�(���� ����)
    private static string OAUTH_CLIENT_NAME = "���̹� ���̵�� �α���";//���ӽ� �� �̸�
    public static NaverLoginPlugin GET
    {
        get
        {
            if (plugin == null)
            {
                plugin = new GameObject().AddComponent<NaverLoginPlugin>();
                plugin.name = "NaverLoginPlugin";//���� ������Ʈ �̸��� ������ message�� �����Ƿ� �̸� ���� �ٲٸ� �ȵ�!
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
    /// ó�� ���۽� �����ؾ��ϴ� �ڵ�(�ݵ��)
    /// </summary>
    /// <param name="OAUTH_CLIENT_ID">���̹� ���ø����̼� ��Ͻ� �߱޹޴� ���̵�</param>
    /// <param name="OAUTH_CLIENT_SECRET">���̹� ���ø����̼� ��Ͻ� �߱޹޴� �ڵ�(���� ����)</param>
    /// <param name="OAUTH_CLIENT_NAME">���ӽ� �� �̸�</param>
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
    /// unity message�� ���� callback��
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
                    Debug.LogError("�α��� ����!");
                    if (FailCallback != null)
                        FailCallback();
                    break;
                case Message.SUCCESS:
                    Debug.Log("�α��� ����!"+ callBack.token);
                    if(SuccessCallback!=null)
                         SuccessCallback(callBack.token);
                    break;
            }

        }
        catch (Exception e)
        {
            Debug.Log("�Ľ� ����"+ message + "     "+e);
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
        public Message message; //0 = ����, 1 = ����
        public string token;
    }
    enum Message
    {
        FAIL,SUCCESS
    }

}
