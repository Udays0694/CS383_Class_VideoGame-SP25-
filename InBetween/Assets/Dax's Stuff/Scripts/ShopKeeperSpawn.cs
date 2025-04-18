using UnityEngine;

public class ShopKeeperSpawn : MonoBehaviour
{
    public GameObject shopKeeper;

    private bool isSpawned = false; 
    private void Update() //Temp way to spawn shopKeeper
    {
        if(Input.GetKeyDown(KeyCode.L) && !isSpawned)
        {
            SpawnShopKeeper();
        }

        if(Input.GetKeyDown(KeyCode.O) && isSpawned)
        {
            despawnShopKeeper();
        }    
    }

    void despawnShopKeeper()
    {
        isSpawned = false;

        shopKeeper.SetActive(false);

        Animator animator = shopKeeper.GetComponent<Animator>();

        Vector3 offset = new Vector3(0.26f, 0.075f, 0.0f); //Fix issue with sprite sheet being in slighlty differnt positions by moving the sprite right before it changes animations
        animator.gameObject.transform.position -= offset;
    }

    void SpawnShopKeeper()
    {
        isSpawned = true; 

        shopKeeper.SetActive(true); //Enable spawnkeeper 

        Animator animator = shopKeeper.GetComponent<Animator>();

        animator.Play("Spawn"); //Play spawn animaton
        StartCoroutine(SwitchIdleAnim(animator, 1.333f)); //Starts a timer after spawn animation to switch to idle animation
        
    }

    System.Collections.IEnumerator SwitchIdleAnim(Animator animator, float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 offset = new Vector3(0.26f, 0.075f, 0.0f); //Fix issue with sprite sheet being in slighlty differnt positions by moving the sprite right before it changes animations
        animator.gameObject.transform.position += offset;

        animator.Play("Idle"); //Play idle animation after spawn animation 


    }
}
