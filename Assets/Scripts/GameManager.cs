using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int phaseGameOver = 0;
    public bool isPlayerControllable = true;
    public TransitionSettings transition;

    private bool isResetComplete = false;
    private Quaternion endStateRotation;

    [SerializeField]
    private Image DamageScreen;
    [SerializeField]
    private Image GameoverScreen;
    [SerializeField]
    private Canvas GameoverCanvas;
    [SerializeField]
    private Canvas UICanvas;
    [SerializeField]
    private Image RetryBubble;
    [SerializeField]
    private TextMeshProUGUI gameoverScore;
    [SerializeField]
    private Slider _hpSlider;
    [SerializeField]
    private TextMeshProUGUI _pointTXT;
    [SerializeField]
    private AudioSource _audioSource;
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

        if (_point > 25 && _currentStage == 1)
        {
            _currentStage++;
            StageSetting(_currentStage);
        }
        else if (_point > 125 && _currentStage == 2)
        {
            _currentStage++;
            StageSetting(_currentStage);
        }
        else if (_point > 605 && _currentStage == 3)
        {
            _currentStage++;
            StageSetting(_currentStage);
        }

        //��������� ���ӿ��� �г� �ѱ�
        if (_hpSlider.value == 0 && phaseGameOver == 0)
        {
            endStateRotation = _eagle.transform.localRotation;
            OnGameOver();
            _audioSource.Pause();
            _eagle.GetComponent<AudioSource>().Pause();
        }

        if (phaseGameOver == 1 && _eagle.transform.localPosition.y < 15)
        {
            UICanvas.gameObject.SetActive(false);

            _eagle.transform.localPosition +=
                (new Vector3(_eagle.transform.localPosition.x, 20, 0) - _eagle.transform.position).normalized * 20.0f *
                Time.unscaledDeltaTime;
            if (_eagle.transform.localPosition.y > 12.5)
            {
                phaseGameOver = 2;
            }
        }

        if (phaseGameOver == 2 && _eagle.transform.localPosition.y > -5)
        {
            gameoverScore.text = $"{(int)_point} KM";

            DamageScreen.gameObject.SetActive(false);
            GameoverCanvas.gameObject.SetActive(true);
            GameoverScreen.color = new Color(0.5f,0.5f,0.5f,0.5f);

            _eagle.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
            _eagle.GetComponent<Animator>().SetBool("isDead", true);
            _eagle.transform.rotation = Quaternion.identity;
            _eagle.transform.localPosition += Vector3.down * 20.0f * Time.unscaledDeltaTime;

            if (_eagle.transform.localPosition.y < -4.9)
            {
                isPlayerControllable = true;
                _eagle.GetComponent<Bird>().OnGameOver();
                phaseGameOver = 3;
            }
        }

        if (isResetComplete)
        {
            RestartGame();
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
            Debug.Log("ũ��Ƽ��!!");
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
    
    private IEnumerator GameoverAlphaAnimation()
    {
        Color color = GameoverScreen.color;
        color.a = 0.0f;
        GameoverScreen.color = color;

        while (color.a <= 0.7f)
        {
            color.a += Time.unscaledDeltaTime / 2;
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

    private void OnGameOver()
    {
        isResetComplete = false;
        isPlayerControllable = false;
        phaseGameOver = 1;
        Time.timeScale = 0.0f;

        GameObject[] projectileList = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (var projectile in projectileList)
        {
            Destroy(projectile);
        }
        
        StopCoroutine("GameoverAlphaAnimation");
        StartCoroutine("GameoverAlphaAnimation");
    }

    public void ResetGame()
    {
        isPlayerControllable = false;
        phaseGameOver = 0;
        RetryBubble.gameObject.SetActive(false);
        
        _eagle.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        _eagle.GetComponent<Animator>().SetBool("isDead", false);

        _point = 0.0f;
        _hpSlider.value = 100;

        // _eagle.transform.localRotation = endStateRotation;

        StartCoroutine("WaitForMotion");
        
        Debug.Log("게임 리셋 완료.");
    }

    private void RestartGame()
    {
        //DamageScreen.gameObject.SetActive(true);
        //GameoverCanvas.gameObject.SetActive(false);

        isResetComplete = false;

        Time.timeScale = 1.0f;
        
        Debug.Log("게임 재시작.");

        StartCoroutine("LoadingScene");
    }
    
    private IEnumerator WaitForMotion()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        isResetComplete = true;
    }
    
    IEnumerator LoadingScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Bird");

        while (!loading.isDone)
        {
            yield return null;
        }
    }

    public void OnClickMainMenuButton()
    {
        Time.timeScale = 1.0f;
        TransitionManager.Instance().Transition("Title", transition, 0);
    }
}
