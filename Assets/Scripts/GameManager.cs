using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private Image DamageScreen;
    [SerializeField]
    private Slider _hpSlider;
    [SerializeField]
    private TextMeshProUGUI _pointTXT;
    [SerializeField]
    private GameObject _eagle;
    [SerializeField]
    private GameObject _effect;
    [SerializeField]
    private GameObject _pop;
    [SerializeField]
    private GameObject _gameover;

    [SerializeField]
    private float _point;

    [System.Serializable]
    private class Stageinfo
    {
        public float Eagle_speed;
        public float Eagle_angle;
        public float Earth_speed;
        public float Hp_speed;
        public float Point_speed;
        public int Bug_SpawnRate;
    }

    [Header("Stages")]
    [SerializeField] 
    private Stageinfo[] Stageinfoes = null;

    [Header("CurrentStageinfo")]
    [SerializeField]
    private int _currentStage;
    public float Eagle_speed;
    public float Eagle_angle;
    public float Earth_speed;
    public float Hp_speed;
    public float Point_speed;
    public int Bug_SpawnRate;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _currentStage = 1;
        StageSetting(_currentStage);
    }

    // Update is called once per frame
    void Update()
    {
        _hpSlider.value -= Hp_speed * Time.deltaTime;
        _point += Point_speed * Time.deltaTime;

        _pointTXT.text = $"{(int)_point} KM";

        if(_point > 25 && _currentStage == 1)
        {
            _currentStage++;
            StageSetting(_currentStage);
        }
        else if(_point > 125 && _currentStage == 2)
        {
            _currentStage++;
            StageSetting(_currentStage);
        }
        else if (_point > 605 && _currentStage == 3)
        {
            _currentStage++;
            StageSetting(_currentStage);
        }

        //게임종료시 게임오버 패널 켜기
        if(_hpSlider.value == 0)
        {
            _gameover.SetActive(true);
        }
    }

    public void GetDamage(int dmg)
    {
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        _hpSlider.value -= dmg;

        if (_hpSlider.value < 0)
        {
            _hpSlider.value = 0;
        }
    }

    public void GetHealth(int hp)
    {
        if (_eagle.GetComponent<Bird>().Iscrictic)
        {
            _hpSlider.value += 2* hp;
            Debug.Log("크리티컬!!");
            _eagle.GetComponent<Bird>().Iscrictic = false;
        }
        else
        {
            _hpSlider.value += hp;
        }
        

        if(_hpSlider.value > 100)
        {
            _hpSlider.value = 100;
        }
    }

    public void Effect(Vector3 position)
    {
        _effect.transform.position = position;
        _effect.SetActive(true);
    }

    public void Pop(Vector3 position)
    {
        _pop.transform.position = position;
        _pop.SetActive(true);
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = DamageScreen.color;
        color.a = 0.4f;
        DamageScreen.color = color;

        while (color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            DamageScreen.color = color;

            yield return null;
        }
    }

    private void StageSetting(int stage)
    {
        Eagle_speed = Stageinfoes[stage -1].Eagle_speed;
        Eagle_angle = Stageinfoes[stage - 1].Eagle_angle;
        Earth_speed = Stageinfoes[stage - 1].Earth_speed;
        Hp_speed = Stageinfoes[stage - 1].Hp_speed;
        Point_speed = Stageinfoes[stage - 1].Point_speed;
        Bug_SpawnRate = Stageinfoes[stage - 1].Bug_SpawnRate;

        _eagle.GetComponent<Bird>().Reposition(Eagle_angle);
        
    }
}
