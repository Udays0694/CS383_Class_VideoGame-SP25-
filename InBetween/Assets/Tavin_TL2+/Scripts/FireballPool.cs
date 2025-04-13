using UnityEngine;
using System.Collections.Generic;

public class FireballPool : MonoBehaviour
{
	[SerializeField] private GameObject fireball;
    private List<GameObject> fireballs = new List<GameObject>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

	public GameObject getInstance()
	{
		for(int i = 0; i < fireballs.Count; i++)
		{
			if(!fireballs[i].activeSelf)
			{
				return fireballs[i];
			}
		}
		
		fireballs.Add(Instantiate(fireball));
		return fireballs[fireballs.Count - 1];
	}
}
