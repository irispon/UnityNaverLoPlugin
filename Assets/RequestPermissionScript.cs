using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class RequestPermissionScript : MonoBehaviour
{
    void Start()
    {
        if (Permission.HasUserAuthorizedPermission("android.permission.READ_PHONE_STATE"))
        {
            // The user authorized use of the microphone.
        }
        else
        {
            // We do not have permission to use the microphone.
            // Ask for permission or proceed without the functionality enabled.
            Permission.RequestUserPermission("android.permission.READ_PHONE_STATE");
        }
    }
}