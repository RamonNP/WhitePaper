using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToURL : MonoBehaviour
{
    //public string URL;//https://t.me/cardanowarriors

    public void SendToURLCustom(string url) {
        string newUrl = "window.open(\""+url+"\",\"_blank\")";
         //Application.ExternalEval("window.open(\"https://www.twitter.com/GilesDMiddleton\",\"_blank\")");
         Application.ExternalEval(newUrl);
    }
}
