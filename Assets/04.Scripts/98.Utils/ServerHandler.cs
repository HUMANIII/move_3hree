using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public static class ServerHandler// : MonoBehaviour
{
    private const string URL =
        "https://script.google.com/macros/s/AKfycbwN6lxTK5JUeS0sQ6FI6Or1J-OdAo7QCnBAG0jSOdAP9z8YM94oSsvpOYzaurpWDfriVw/exec";
    private const string FuncName = "simpleScoreUpdate";
    private const string Action = "action";
    private const string Nickname = "nickname";
    private const string Score = "score";

    /// <summary>
    /// 간단한 점수 제공용
    /// </summary>
    /// <param name="nickname">닉네임</param>
    /// <param name="score">점수</param>
    public static IEnumerator SendScore(string nickname, int score)
    {
        Dictionary<string, string> body = new();
        body.Add(Nickname, nickname);
        body.Add(Score, score.ToString());
        body.Add(Action, FuncName);
        
        string jsonBody = JsonConvert.SerializeObject(body, Formatting.None);
        var rawbody = Encoding.UTF8.GetBytes(jsonBody);
        
        UnityWebRequest wr = new UnityWebRequest(URL, "POST");
        wr.uploadHandler = new UploadHandlerRaw(rawbody);
        wr.downloadHandler = new DownloadHandlerBuffer();
        wr.SetRequestHeader("Content-Type", "application/json");
        wr.SendWebRequest();
        
        yield return new WaitUntil(() => wr.isDone);
        
        Debug.Log(wr.downloadHandler.text);
    }
}
