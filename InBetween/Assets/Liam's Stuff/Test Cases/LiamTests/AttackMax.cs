using UnityEngine;

public class SimulateAttackButtonPress : MonoBehaviour
{
    private float testDuration = 10f;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        StartCoroutine(SimulateAttack());
    }

    System.Collections.IEnumerator SimulateAttack()
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Simulating attack button press");
        }
    }
}
