using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletCount : MonoBehaviour {
    private TextMeshProUGUI bullet_count;
    void Start() {
        bullet_count = GetComponent<TextMeshProUGUI>();
    }   

    void Update() {
        bullet_count.text = "Bullet: " + PlayerMovement.bulletcount;
    }
}
