using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject nakedBear;
    public GameObject miniBear;
    public GameObject fatBear;

    public Text moneyText;
    public Text pointsText;
    public Text peopleText;
    public Text incomeLevelText;

    float price;
    public float money;
    float moneyRate = 5;
    float incomeLevel = 1;

    float currentPeople;
    float maximumPeople = 5;

    float timer = 0f;

    public void Update()
    {
        moneyText.text = "£" + money.ToString();
        peopleText.text = currentPeople.ToString() + "/" + maximumPeople;
        incomeLevelText.text = "Level: " + incomeLevel.ToString();

        int nakedBearAmount = GameObject.FindGameObjectsWithTag("NakedBear").Length;
        int miniBearAmount = GameObject.FindGameObjectsWithTag("MiniBear").Length;
        int FatBearAmount = GameObject.FindGameObjectsWithTag("FatBear").Length;

        currentPeople = nakedBearAmount + (miniBearAmount * 2) + (FatBearAmount * 5);



        timer += Time.deltaTime;
        if (timer >= moneyRate)
        {
            timer = 0f;
            money += 50;
        }

    }

    public void UpgradeIncome()
    {
        price = 500;
        if (money >= price && incomeLevel < 5)
        {
            moneyRate = moneyRate - 1.1f;
            money -= price;
            incomeLevel += 1;
        }
    }

    public void UpgradeMaximumPeople()
    {
        price = 250;
        if (maximumPeople < 75 && money >= price)
        {
            maximumPeople += 5;
            money -= price;
        }
    }

    public void Spawn_Naked_Bear()
    {
        price = 50;
        if (money >= price && currentPeople <= maximumPeople - 1)
        {
            Instantiate(nakedBear, nakedBear.transform.position, nakedBear.transform.rotation);
            money -= price;
            currentPeople += 1;
        }
    }

    public void Spawn_Mini_Bear()
    {
        price = 200;
        if (money >= price && currentPeople <= maximumPeople - 2)
        {
            Instantiate(miniBear, miniBear.transform.position, miniBear.transform.rotation);

            money -= price;
            currentPeople += 2;
        }
    }

    public void Spawn_Fat_Bear()
    {
        price = 900;
        if (money >= price && currentPeople <=  maximumPeople - 5)
        {
            Instantiate(fatBear, fatBear.transform.position, fatBear.transform.rotation);
            money -= price;
            currentPeople += 5;
        }
    }

    public void PowerUp()
    {
        price = 3000;
        if (money >= price && currentPeople <= maximumPeople - 32)
        {
            for (int i = 0; i < 7; i++)
            {
            Instantiate(nakedBear, nakedBear.transform.position, nakedBear.transform.rotation);
            }
            for (int i = 0; i < 5; i++)
            {
                Instantiate(miniBear, miniBear.transform.position, miniBear.transform.rotation);

            }
            for (int i = 0; i < 3; i++)
            {
                Instantiate(fatBear, fatBear.transform.position, fatBear.transform.rotation);
            }

            money -= price;
        }
    }
}
