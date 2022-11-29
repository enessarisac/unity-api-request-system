using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using Newtonsoft.Json;
using UnityEngine.Events;

public class PostAPICommunicator : MonoBehaviour
{
    public static PostAPICommunicator Instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    public void RequestApi(string url, string[,] addFieldData = null, string[,] headers = null, Action<string> onWebRequestSuccess = null, Action<string> onWebRequestFailed = null)
    {
        StartCoroutine(IEPostApiRequest(url, addFieldData, headers, onWebRequestSuccess, onWebRequestFailed));
    }
    IEnumerator IEPostApiRequest(string url, string[,] addFieldData = null, string[,] headers = null, Action<string> onWebRequestSuccess = null, Action<string> onWebRequestFailed = null)
    {
        WWWForm form = new WWWForm();
        if (addFieldData != null)
        {
            for (int i = 0; i < addFieldData.GetLength(0); i++)
            {
                form.AddField(addFieldData[i, 0], addFieldData[i, 1]);
            }
        }

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            if (headers != null)
            {
                for (int i = 0; i < headers.GetLength(0); i++)
                {
                    webRequest.SetRequestHeader(headers[i, 0], headers[i, 1]);
                }
            }
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                onWebRequestSuccess?.Invoke(webRequest.downloadHandler.text);
            }
            else
            {
                onWebRequestFailed?.Invoke(webRequest.error);
            }
        }
    }
}
