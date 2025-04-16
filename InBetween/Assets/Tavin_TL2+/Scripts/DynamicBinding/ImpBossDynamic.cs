using UnityEngine;

public class BossDynamicDemo
{
	public virtual void move()
	//public void move()
	{
		Debug.Log("Boss movement pattern");
	}
}

public class ImpDynamicDemo : BossDynamicDemo
{
	public override void move()
	//public void move()
	{
		Debug.Log("Imp movement pattern");
	}
}

public class DynamicDemo
{
	public void start()
	{
		BossDynamicDemo imp = new ImpDynamicDemo();
		
		Debug.Log("Static type: Boss\nDynamic type: Imp\nMovement pattern:");
		imp.move();
	}
}
