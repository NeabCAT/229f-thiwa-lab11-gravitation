using UnityEngine;
using System.Collections.Generic;

public class Gravitation : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f; //Gravitational Constant 6.674

    //create a List of objects in the galaxy to attract
    public static List<Gravitation> otherObjectsList;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjectsList == null) 
        { 
            otherObjectsList = new List<Gravitation>();
        }

        //add object (with gravity script) to attract to the list
        otherObjectsList.Add(this);

        //orbitting
        if (!planet)
        { rb.AddForce(Vector3.left * orbitSpeed); }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Gravitation obj in otherObjectsList) 
        {
            if (obj != this) 
            {
                Attract(obj);
            }
        }
    }
    void Attract(Gravitation other)
    {
        Rigidbody otherRb = other.rb; //ดึงค่ามวล m
        Vector3 direction = rb.position - otherRb.position; //ทิศทางจากวัตถุมวล M ไป m

        float distance = direction.magnitude; //หาระยะห่าง r

        if (distance == 0f) return; //ป้องกันไม่ให้มันมีแรงดึงดูด เมื่อวัตถุทั้งสองอยู่ตำแหน่งเดียวกัน

        // F = G(m1 * m2) / r^2
        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationForce = forceMagnitude * direction.normalized; //นำแรงที่ได้มาใส่ทิศทาง
        otherRb.AddForce(gravitationForce); //ใส่แรงดึงดูดพร้อมทิศทางให้กับวัตถุ
    }

}
