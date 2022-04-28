using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopKontrol : MonoBehaviour
{
    Rigidbody rb;
    public float hýz = 50f;
    public Text zaman, can , durum;
    float zamanSayaci = 10;
    int canSayaci = 3;
    bool oyunDevam = true;
    bool oyunTamam = false;
    public Button buton;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

 
    void Update()
    {
        if (oyunDevam && !oyunTamam)
        {
            zamanSayaci -= Time.deltaTime;
            zaman.text = (int)zamanSayaci + "";
           
        }
      else  if (!oyunTamam)
        {
            durum.text = "Oyun Tamamlanamadý";
            buton.gameObject.SetActive(true);
        }
        if (zamanSayaci < 0)
        {
            oyunDevam = false;
        }
        
    }
    private void FixedUpdate()
    {
        if (oyunDevam && !oyunTamam)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
            rb.AddForce(kuvvet * hýz);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;  // döngüsel hareketi sýfýrladýk
        }



    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bitis")
        {
            Destroy(gameObject, 5f);
            oyunTamam = true;
            durum.text = "Oyun TAMAMLANDI.TEBRÝKLER";
            buton.gameObject.SetActive(true);
        }
        else if (collision.gameObject.name!="zemin" && collision.gameObject.name!="labirentzemin") 
        {
            canSayaci--;
            can.text = canSayaci + "";
            if (canSayaci == 0)
            {
                oyunDevam= false;
            }
        }

    }
}
