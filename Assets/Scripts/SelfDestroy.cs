using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {

    public float m_timeToDestroy;

	// Use this for initialization
	void Start () {
        StartCoroutine(SelfDestuct());
    }
	
	IEnumerator SelfDestuct()
    {
        yield return new WaitForSeconds(m_timeToDestroy);
        Destroy(this.gameObject);
    }
}
