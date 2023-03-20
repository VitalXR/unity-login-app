using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class APIResponseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void GetSignup()
    {
        Debug.Log("called get signup");

        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("https://api.publicapis.org/entries");
        string responseBody = await response.Content.ReadAsStringAsync();

        Debug.Log(responseBody);
    }

    public async void GetData()
    {
        Debug.Log("called get Data");

        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("https://api.publicapis.org/entries");
        string responseBody = await response.Content.ReadAsStringAsync();

        Debug.Log(responseBody);
    }
}
