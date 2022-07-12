using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public bool GameStart = false;
    public GameObject finisher;
    [SerializeField]AIController[] AIs;
    [SerializeField]PlayerController player;
  [SerializeField]  List<GameObject> racers = new List<GameObject>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        racers.Add(player.gameObject);
        foreach(var ai in AIs)
        {
            racers.Add(ai.gameObject);
        }
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
}
