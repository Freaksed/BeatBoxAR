using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class BandsInTownAPI : MonoBehaviour
{
    private const string APIKey = "capitol201939ad4ebef3caf1ac2914b0eb8203c030";
    private const string  URI = "https://search.bandsintown.com/search";

    private void Start()
    {
        FormRequest();
    }

    private void FormRequest()
    {
        float latitude = 32, longitude = -117;

        var query = new JObject
        {
            ["app_id"] = APIKey,
            ["location"] = "region",
            ["region"] = new JObject {["latitude"] = latitude, ["longitude"] = longitude, ["radius"] = 50}
        };
        StartCoroutine(SendRequest(query));
    }

    private IEnumerator SendRequest(JObject query)
    {
        using (UnityWebRequest request = new UnityWebRequest($"URI?{query}"))
        {
            request.method = "GET";

            yield return request.SendWebRequest();
            string[] pages = URI.Split('/');
            int page = pages.Length - 1;

            if (request.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + request.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + request.downloadHandler.text);
            }
        }
        yield return null;
    }
}