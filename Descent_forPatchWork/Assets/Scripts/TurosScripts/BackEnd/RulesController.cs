using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class RulesController : MonoBehaviour
{
    public TMP_Text contentText, extraText;
    [SerializeField] int idnum = 2;
    [SerializeField] Animator rulesAnimator;
    [SerializeField] GameObject bgPanel;

    public void SearchFieldHasChanged(string idNumber)
    {
        idnum = int.Parse(idNumber);
    }

    public void DatabaseCall()
    {
        bgPanel.SetActive(true);
        //StartCoroutine(GetRequest("http://localhost/PatchDatabase/?unityget="));

        //Add here ServerAdres!
        StartCoroutine(GetRequest("http://www.turovaarti.fi/databaseManager.php?unityget="));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + idnum))
        //using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                    string rawResponse = webRequest.downloadHandler.text;
                    string[] content = rawResponse.Split("*");

                    for (int i = 0; i < content.Length; i++)
                    {
                        Debug.Log("CONTENT: " + content[i]);
                    }

                    contentText.text = content[0];
                    extraText.text = content[1];

                    FindObjectOfType<VideoController>().SetTutorialClip(idnum);

                    rulesAnimator.SetTrigger("RulesReveal");

                    break;
            }
        }
    }
    public void HideRulesPanel()
    {
        rulesAnimator.SetTrigger("RulesHide");
        bgPanel.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(5).GetChild(5).gameObject.SetActive(false);
    }
}
