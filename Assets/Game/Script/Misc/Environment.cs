using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Environment : MonoBehaviour
{
    Vector2 screenSize;

    static BoxCollider2D[] colliders = new BoxCollider2D[4];
    private void Awake()
    {
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(Vector3.zero), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0f))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(Vector3.zero), Camera.main.ScreenToWorldPoint(new Vector2(0f, Screen.height))) * 0.5f;

        float size = 0.5f;
        BoxCollider2D collider;
        //bottom collider
        collider = transform.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2(0, -screenSize.y - size / 2 + 1f);
        collider.size = new Vector2(screenSize.x * 2, size);

        // top collider
        collider = transform.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2(0, screenSize.y + size / 2);
        collider.size = new Vector2(screenSize.x * 2, size);

        // left collider
        collider = transform.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2(-screenSize.x - size / 2, 0);
        collider.size = new Vector2(size, screenSize.y * 2);

        // //right collider
        collider = transform.AddComponent<BoxCollider2D>();
        collider.offset = new Vector2(+screenSize.x + size / 2, 0);
        collider.size = new Vector2(size, screenSize.y * 2);
    }
}
