using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class APIMethods
{
    private const string defaultContentType = "application/json";
    public async Task<Response> HttpGet(string url)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);

        webRequest.SendWebRequest();

        Debug.Log("Before Done");
        while (!webRequest.isDone)
        {
            await Task.Yield();
        }
        Debug.Log("After Done");

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                Debug.Log("Data: " + data);
                return new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                    Data = data
                };
            default:
                Debug.LogError(string.Format("Something went wrong: {0}", webRequest.error));
                return new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                };
        }
    }

    public async Task<Response> HttpPost(string url, string body, IEnumerable<RequestHeader> headers = null)
    {
        using UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, body);

        if (headers != null)
        {
            foreach (RequestHeader header in headers)
            {
                webRequest.SetRequestHeader(header.Key, header.Value);
            }
        }

        webRequest.uploadHandler.contentType = defaultContentType;
        webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));

        webRequest.SendWebRequest();

        while (!webRequest.isDone)
        {
            await Task.Yield();
        }

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                Debug.Log("Data: " + data);
                return new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                    Data = data
                };
            default:
                Debug.LogError(string.Format("Something went wrong: ${0}", webRequest.error));
                return new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                };
        }
    }
}
