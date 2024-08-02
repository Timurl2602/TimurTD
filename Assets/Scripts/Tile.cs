using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsFree;
    public PlayerInputManager PlayerInputManager;
    private Renderer tileRenderer;

    private Color originalColor;
    private Color freeColor = Color.green;
    private Color notFreeColor = Color.red;

    void Start()
    {
        IsFree = true;
        tileRenderer = GetComponent<Renderer>();
        PlayerInputManager = FindObjectOfType<PlayerInputManager>();
        originalColor = tileRenderer.material.color;
    }

    void OnMouseEnter()
    {
        if (PlayerInputManager.PlayerInputActions.BuildMode.enabled)
        {
            if (IsFree)
            {
                tileRenderer.material.color = freeColor;
            }
            else
            {
                tileRenderer.material.color = notFreeColor;
            }
        }
    }
    
    void OnMouseExit()
    {
        tileRenderer.material.color = originalColor;
    }
}