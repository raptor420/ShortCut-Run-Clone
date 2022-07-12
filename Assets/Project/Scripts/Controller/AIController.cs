using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class AIController : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 5;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] AnimatorController animator;
    bool startRun = false;
    [SerializeField] Transform characterBody;
    [Header("FINISHER")]
    bool finished;
   [SerializeField] NavMeshAgent agent;


    private void Update()
    {
     
        StackerAnimChecker();
    }
    private void StackerAnimChecker()
    {
        if (!startRun) return;
        if (GetComponent<Stacker>().getStackAmount() > 0)
        {
            characterBody.localRotation = Quaternion.identity;

            GetComponent<AnimatorController>().StateCarry();
        }
        else
        {
            characterBody.localRotation = Quaternion.identity;

            GetComponent<AnimatorController>().StateRun();

        }
    }
    public void StartAI()
    {
        animator.StateRun();
        agent.SetDestination(GameManager.instance.finisher.transform.position);
        startRun = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {

            StartCoroutine(FinishGame());

        }
    }
    private IEnumerator FinishGame()
    {
        finished = true;

        if (GetComponent<Stacker>().getStackAmount() > 0) { animator.StateThrow(); yield return (StartCoroutine(GetComponent<Stacker>().ThrowStackCoroutine())); }




        animator.StateRun();
        if (finished)
        {
            Vector2 pos = Random.insideUnitCircle * 15;
            agent.SetDestination(GameManager.instance.finisher.transform.position +new Vector3(pos.x,0,pos.y) );
        }
        yield return new WaitForSeconds(1);
        startRun = false;
        transform.DOLocalRotate(new Vector3(0, 180, 0), .1f, RotateMode.LocalAxisAdd);

        animator.StateVictory();

    }
}
