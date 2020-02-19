using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceGame : MonoBehaviour
{
    public string inputValue = "1";

    public Text outputText;
    public InputField inputField;
    public Button button;

    int minDiceNum = 1;
    int maxDiceNum = 6;

    int throwNormalDice()
    {
        Debug.Log("Throwing Dice ...");
        // Plus 1 to max because max on range is exclusive (includes everything up to max)
        int diceResult = Random.Range(minDiceNum, maxDiceNum+1);
        Debug.Log($"Result: {diceResult}");
        return diceResult;
    }

    int ThrowLoadedDice()
    {
        Debug.Log("Throwing Dice ...");
        int randomProbability = Random.Range(1, 101);
        int diceResult = 0;
        if (randomProbability < 36)
        {
            diceResult = 6;
        }
        else
        {
            diceResult = Random.Range(1, 5);
        }
        Debug.Log($"Result: {diceResult}");
        return diceResult;
    }

    public void ProcessGame()
    {
        inputValue = inputField.text;
        try
        {
            int inputInteger = int.Parse(inputValue);
            int totalSix = 0;
            for (int i = 0; i < 10; i++)
            {
                int diceResult = throwNormalDice();
                if (diceResult == 6)
                {
                    totalSix++;
                }
                if (diceResult == inputInteger)
                {
                    outputText.text = $"Dice Result: {diceResult.ToString()}\r\nYOU WIN!";
                }
                else
                {
                    outputText.text = $"Dice Result: {diceResult.ToString()}\r\nYou Lose!";
                }
            }
            Debug.Log($"Total of siz: {totalSix.ToString()}");
        }
        catch
        {
            outputText.text = "Input is not a number";
            Debug.LogError("Input is not a number");
        }
    }
}
