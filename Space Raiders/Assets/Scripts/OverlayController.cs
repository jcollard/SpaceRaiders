using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(GameController))]
public class OverlayController : MonoBehaviour
{
    [field: SerializeField]
    private TextMeshProUGUI Score { get; set; }

    public void UpdateScore(int score)
    {
        Score.text = score.ToString().PadLeft(8, '0');
    }
}
