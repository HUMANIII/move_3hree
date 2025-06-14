using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class RankingSystem : MonoBehaviour
{
    private const string RankingURL = "https://script.google.com/macros/s/AKfycbwN6lxTK5JUeS0sQ6FI6Or1J-OdAo7QCnBAG0jSOdAP9z8YM94oSsvpOYzaurpWDfriVw/exec?requestType=RankData";
    private const int MaxRanking = 20; //이거 스크립트에서도 수정해줘야 최대 랭킹 수가 제대로 바뀜
    private const string nickname = "nickname";
    private const string score = "score";
    
    private int curRank = 0;
    
    [SerializeField] private GameObject RankingUI;
    [SerializeField] private GameObject LoadingUI;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private ScrollRectOption scrollRectOption;
    private RankSlot[] allSlots = new RankSlot[MaxRanking];

    private void Awake()
    {
        for (int i = 0; i < MaxRanking; i++)
        {
            var go = Instantiate(slotPrefab, slotParent);
            allSlots[i] = go.GetComponent<RankSlot>();
        }
        curRank = MaxRanking;
        RankingUI.SetActive(false);   
    }

    private void OnEnable()
    {
        StartCoroutine(LoadRanking());
        DOTween.TweensByTarget(this, true);
    }

    private IEnumerator LoadRanking()
    {
        LoadingUI.SetActive(true);
        scrollRectOption.gameObject.SetActive(false);
        var wr = UnityWebRequest.Get(RankingURL);
        yield return wr.SendWebRequest();
        var text = wr.downloadHandler.text;
#if UNITY_EDITOR
        Debug.Log(text);
#endif
        JArray ja = JArray.Parse(text);
        for (int i = 0; i < ja.Count; i++)
        {
            if(i >= MaxRanking)
                break;
            if(curRank <= i)
                allSlots[i] = Instantiate(slotPrefab, slotParent).GetComponent<RankSlot>();
            string nn = ja[i][nickname]?.ToString();
            int sc = ja[i][score]?.Value<int>() ?? 0;
            allSlots[i].SetData(i+1, nn, sc);
            allSlots[i].gameObject.SetActive(true);
        }
        LoadingUI.SetActive(false);
        scrollRectOption.gameObject.SetActive(true);

        for (int i = ja.Count; i < MaxRanking; i++)
        {
            curRank--;
            Destroy(allSlots[i].gameObject);
        }

        StartCoroutine(scrollRectOption.SetLayoutGroupOption());
    }
}
