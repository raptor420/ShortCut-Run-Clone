using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public bool GameStart = false;
    public GameObject finisher;
    [SerializeField]AIController[] AIs;
    [SerializeField]PlayerController player;
  [SerializeField]  List<GameObject> racers = new List<GameObject>();
    [SerializeField] GameObject txtSwpieToMove;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        racers.Add(player.gameObject);
        foreach (var ai in AIs)
        {
            racers.Add(ai.gameObject);
        }
        SwiptTextInit();
    }

    private void SwiptTextInit()
    {
        txtSwpieToMove.SetActive(true);
        txtSwpieToMove.transform.DOPunchScale(Vector3.one*.1f, 1).SetDelay(.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (!GameStart)
        {
            InitStartAndInitAI();
        }


    // Debug.Log(GetPlayerPlace());
    }

    private void InitStartAndInitAI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameStart = true;
            txtSwpieToMove.SetActive(false);
            AudioManager.instance.PlayAudio(AudioManager.instance.startRace);

            foreach (var ai in AIs)
            {
                ai.StartAI();
            }
        }
    }

    public float   GetDistance(GameObject t)
    {
     return   Vector3.Distance(new Vector3(t.transform.position.x, finisher.transform.position.y, t.transform.position.z), finisher.transform.position);

    }
    public int GetPlayerPlace()
    {

        racers = racers.OrderByDescending(ch => GetDistance(ch)).ToList();
        racers.Reverse();
        for (int i = racers.Count-1; i > -1; i--)
        {
            if (racers[i].CompareTag("Player"))
            {
                return i +1;
            }
        }
        return 0;
    }
    public void LoadScene()
    {
        StartCoroutine(LoadSceneCo());
    }
    public IEnumerator LoadSceneCo()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
