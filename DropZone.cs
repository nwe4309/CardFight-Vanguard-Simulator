using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour 
{

    public List<GameObject> dropZone;

    public GameObject confirmButton;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        HoverDropZone();

        PurgeNullCards();
	}

    public void PurgeNullCards()
    {
        // Loop through all cards in the drop zone
        for (int i = 0; i < dropZone.Count; i++)
        {
            // If any of the cards are a blank null card
            if(dropZone[i].GetComponent<Card>().cardName == "null")
            {
                // Remove and destroy the null card
                Destroy(dropZone[i]);
                dropZone.RemoveAt(i);
            }
        }
    }

    public void HoverDropZone()
    {
        // Stores the position of the mouse in world space
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Casts a ray from the camera to the mouse and stores what was hit in a RaycastHit object
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

        // If something was hit
        if (hit)
        {
            if(dropZone.Count > 0 && dropZone[dropZone.Count-1].transform.position == hit.transform.position)
            {
                confirmButton.SetActive(true);
            }
            else
            {
                confirmButton.SetActive(false);
            }

        }
    }
}
