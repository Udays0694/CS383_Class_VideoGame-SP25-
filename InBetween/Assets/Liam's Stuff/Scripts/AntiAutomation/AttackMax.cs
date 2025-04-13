using UnityEngine;
using System.Collections;

public class SimulateAttackButtonPress : MonoBehaviour
{
    private float testDuration = 10f;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        StartCoroutine(SimulateAttack());
    }

    IEnumerator SimulateAttack()
    {
        while (Time.time - startTime < testDuration)
        {
            SimulateAttackButtonPressAction();
            yield return null; 
        }

        Debug.Log("Attack simulation finished.");
    }

    void SimulateAttackButtonPressAction()
    {
        Debug.Log("Simulating attack button press");
    }
}
