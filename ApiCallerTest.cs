using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ApiCallerTest : MonoBehaviour
{
    public Action<string> onApiRequestSuccess;
    public Action<string> onApiRequestFailed;
    private void Start()
    {
        string url = "www.examplewebsite.com";

        string[,] fields = { { "email", "example@example.com" }, { "password", "password" } };

        string[,] headers = { { "exampleheader", "exampleHeaderValue" } };

        PostAPICommunicator.Instance.RequestApi(url, fields, headers, onApiRequestSuccess, onApiRequestFailed);

        PostAPICommunicator.Instance.RequestApi(url, fields, null, onApiRequestSuccess, onApiRequestFailed);

        PostAPICommunicator.Instance.RequestApi(url, null, headers, onApiRequestSuccess, onApiRequestFailed);

        PostAPICommunicator.Instance.RequestApi(url, null, null, onApiRequestSuccess, onApiRequestFailed);

        PostAPICommunicator.Instance.RequestApi(url, null, null, OnApiRequestSuccess);

        PostAPICommunicator.Instance.RequestApi(url, null, null, null, OnApiRequestFailed);

        PostAPICommunicator.Instance.RequestApi(url, fields, headers);

        PostAPICommunicator.Instance.RequestApi(url, fields);

        PostAPICommunicator.Instance.RequestApi(url);
    }
    private void OnEnable()
    {
        onApiRequestSuccess += OnApiRequestSuccess;
        onApiRequestFailed += OnApiRequestFailed;
    }
    private void OnDisable()
    {
        onApiRequestSuccess -= OnApiRequestSuccess;
        onApiRequestFailed -= OnApiRequestFailed;
    }
    private void OnApiRequestFailed(string response)
    {
        Debug.Log("Request failed because " + response);

    }
    private void OnApiRequestSuccess(string response)
    {
        Debug.Log("response is " + response);
        //For Example
        //JsonUtility.FromJsonOverwrite(response, className);
    }
}