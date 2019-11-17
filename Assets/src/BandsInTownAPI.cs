using Newtonsoft.Json.Linq;
using UnityEngine;

public class BandsInTownAPI : MonoBehaviour
{
    private const string APIKey = "nTG4tbSXpIaniCHlJ62q06GzIpROk6qh56EiK7N1";
    private const string  URI = "https://search.bandsintown.com/search";

    private void SendRequest()
    {
        float latitude = 32, longitude = -117;

        var query = new JObject();
        query.Add("location", "region");
        query.Add("region", new JObject
        {
            new JObject("latitude", latitude),
            new JObject("longitude", longitude),
            new JObject("radius", 50)
        });
    }
}