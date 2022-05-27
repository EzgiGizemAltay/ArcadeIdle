using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Oncol : MonoBehaviour
{
    [SerializeField] private GameObject stuff, money;
    [SerializeField] private GameObject stuffMaker, stuffSellerPos;
    [SerializeField] private GameObject stuffParent, moneyParent;
    [SerializeField] private float ydiff = 0.15f, ydiffmon= 0.15f;
    private readonly GameObject[] stuffNew = new GameObject[20];
    private readonly GameObject[] moneyNew = new GameObject[20];
    [SerializeField] private int numberOfMoneyAndStuff = 20;
    [SerializeField] private GameObject car, carBuyingPlace;

    private void Start()
    {
        money.SetActive(false);
        stuff.SetActive(false);
        car.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StuffMaker"))
        {
            StartCoroutine(WaitandCreate(0.1f));
        }

        if (other.gameObject.CompareTag("StuffSeller"))
        {
            StartCoroutine(WaitandTake(0.1f));
            StartCoroutine(WaitandSell(0.1f));
        }

        if (other.gameObject.CompareTag("CarBuyer"))
        {
            StartCoroutine(WaitandBuy(0.1f));
        }
        IEnumerator WaitandCreate(float wait)
        {
            for (int i = 0; i < numberOfMoneyAndStuff; i++)
            {
                stuff.SetActive(true);
                stuffNew[i] = Instantiate(stuff, stuffMaker.transform.position, Quaternion.identity);
                stuffNew[i].transform.parent = stuffParent.transform;
                yield return new WaitForSeconds(wait);
                stuffNew[i].transform.DOLocalJump(new Vector3(stuff.transform.localPosition.x, stuff.transform.localPosition.y +ydiff,stuff.transform.localPosition.z), 2f, 0, 0.5f);
                ydiff += 0.15f;
            }
            ydiff = 0.15f;
        }

        IEnumerator WaitandTake(float wait1)
        {
            for (int i = 0; i < numberOfMoneyAndStuff; i++)
            {
                stuffNew[19-i].transform.DOJump(stuffSellerPos.transform.position, 2, 0, 0.1f);
                yield return new WaitForSeconds(wait1);
                stuffNew[19-i].SetActive(false);
            }
            stuff.SetActive(false);
        }

        IEnumerator WaitandSell(float wait)
        {
            for (int i = 0; i < numberOfMoneyAndStuff; i++)
            {
                money.SetActive(true);
                moneyNew[i] = Instantiate(money, stuffSellerPos.transform.position, Quaternion.identity);
                moneyNew[i].transform.parent = moneyParent.transform;
                yield return new WaitForSeconds(wait);
                moneyNew[i].transform.DOLocalJump(new Vector3(money.transform.localPosition.x, money.transform.localPosition.y +ydiffmon,money.transform.localPosition.z), 2f, 0, 0.5f);
                ydiffmon += 0.15f;
            }
            ydiffmon = 0.15f;
        }
        
        IEnumerator WaitandBuy(float wait1)
        {
            for (int i = 0; i < numberOfMoneyAndStuff; i++)
            {
                moneyNew[19-i].transform.DOJump(carBuyingPlace.transform.position, 2, 0, 0.1f);
                yield return new WaitForSeconds(wait1);
                moneyNew[19-i].SetActive(false);
            }
            money.SetActive(false);
            carBuyingPlace.SetActive(false);
            car.SetActive(true);
        }
    }
    
}
