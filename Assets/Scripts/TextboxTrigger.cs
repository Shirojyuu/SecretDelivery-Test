using UnityEngine;
using System.Collections;

public class TextboxTrigger : MonoBehaviour {

    [SerializeField] private string message;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            GameObject.FindObjectOfType<TextboxHandler>().SetTextToPrint(message);
            Destroy(gameObject);
        }
    }
}
