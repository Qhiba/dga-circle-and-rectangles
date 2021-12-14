﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour
{
    public bool IsDestructable { get; private set; }

    public void SetDestructable(bool isDestructable)
    {
        IsDestructable = isDestructable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && IsDestructable)
        {
            UIController.Instance.IncrementScore();
            gameObject.SetActive(false);
        }
    }
}