using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreTest : MonoBehaviour
{
    public int factorScoreFlightDistance = 100;
    public int factorScoreEagleHealth = 10;
    public bool isGamePlaying = true;
    
    [SerializeField] private TextMeshProUGUI textScoreFlightDistance;
    [SerializeField] private TextMeshProUGUI textScoreEagleHealth;
    [SerializeField] private GameObject panelPause;
    
    private float scoreFlightDistance = 0.0f;
    private float scoreEagleHealth = 100.0f;
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (isGamePlaying)
        {
            scoreEagleHealth -= Time.fixedDeltaTime * factorScoreEagleHealth;
            scoreFlightDistance += Time.fixedDeltaTime * factorScoreFlightDistance;

            textScoreEagleHealth.text = ((int)scoreEagleHealth).ToString();
            textScoreFlightDistance.text = ((int) scoreFlightDistance).ToString();
        }
    }

    public void OnClickPauseButton()
    {
        isGamePlaying = !isGamePlaying;
        panelPause.SetActive(!panelPause.activeInHierarchy);
    }
}
