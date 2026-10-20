using UnityEngine;
using TMPro;
using System;

public class Calculator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;

    private double currentResult = 0;
    private string currentOperation = "";
    private bool isNewCalculation = true;
    private bool isOperationSet = false;

    public void NumberPressed(string number)
    {
        if (isNewCalculation || displayText.text == "0")
        {
            displayText.text = number;
            isNewCalculation = false;
        }
        else
        {
            displayText.text += number;
        }

        isOperationSet = false;
    }

    public void OperationPressed(string operation)
    {
        double numberOnDisplay = Convert.ToDouble(displayText.text);

        if (string.IsNullOrEmpty(currentOperation))
        {
            currentResult = numberOnDisplay;
        }
        else if (!isOperationSet)
        {
            CalculateRunningTotal(numberOnDisplay);
        }

        currentOperation = operation;
        isNewCalculation = true;
        isOperationSet = true;
    }

    private void CalculateRunningTotal(double secondOperand)
    {
        switch (currentOperation)
        {
            case "+":
                currentResult += secondOperand;
                break;
            case "-":
                currentResult -= secondOperand;
                break;
            case "*":
                currentResult *= secondOperand;
                break;
            case "/":
                if (secondOperand != 0)
                    currentResult /= secondOperand;
                else
                {
                    displayText.text = "Error";
                    Clear();
                    return;
                }
                break;
        }
        displayText.text = currentResult.ToString();
    }

    public void CalculateResult()
    {
        if (string.IsNullOrEmpty(currentOperation))
            return;

        double numberOnDisplay = Convert.ToDouble(displayText.text);
        CalculateRunningTotal(numberOnDisplay);

        currentOperation = "";
        isNewCalculation = true;
        isOperationSet = false;
    }

    public void Clear()
    {
        currentResult = 0;
        currentOperation = "";
        displayText.text = "0";
        isNewCalculation = true;
        isOperationSet = false;
    }
}