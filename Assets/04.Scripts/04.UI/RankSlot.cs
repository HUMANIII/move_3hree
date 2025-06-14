using TMPro;
using UnityEngine;

public class RankSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetData(int rank, string nickname, int score)
    {
        rankText.text = rank.ToString();
        if (rank is 1 or 2 or 3)
        {
            rankText.color = Color.yellow;
        }
        else
        {
            rankText.color = Color.white;
        }
        nameText.text = nickname;
        scoreText.text = score.ToString();
    }
}
