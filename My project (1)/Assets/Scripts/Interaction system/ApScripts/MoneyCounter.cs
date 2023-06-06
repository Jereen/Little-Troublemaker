using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using Mono.Cecil.Cil;
using UnityEngine.SceneManagement;

public class MoneyCounter : MonoBehaviour
{
    public TMP_Text mCounterText;
    public Inventory inventory;
    [SerializeField] RoundController roundController;
    public RanEventManager ranEventManager;
    public bool counterOn = false;
    public int Delay = 1;
    public int moneyPerSec;
    protected float timer;
    
    //Consumer State var
    [Header("Appliance State")]
    public bool stateWashingMash = false;
    public bool stateDryer = false;
    public bool stateBob = false;
    public bool stateRadio = false;
    public bool stateFridge = false;
    public bool stateTV = false;
    public bool stateLamp = false;
    #region
    public bool stateSink_Toilet = false;
    public bool stateGamingSystem = false;
    public bool stateKettle = false;
    public bool stateCounter_Sink_Kitchen = false;
    public bool stateCounter_Dishwasher_Kitchen = false;
    public bool stateStove = false;
    public bool stateMicrowave = false;
    public bool stateBlender = false;
    public bool stateIron = false;
    public bool stateVacuum = false;
    public bool stateShower = false;
    public bool stateSink_Bathroom = false;
    public bool stateRadiator_MasterBedroom = false;
    public bool stateIpad = false;
    public bool stateToyTrain = false;
    public bool stateRadiator_Kidsroom = false;
    public bool stateComputer = false;

    public bool stateLight_Entry = false;
    public bool stateLight_Toilet = false;
    public bool stateLight_Livingroom = false;
    public bool stateLight_Diningroom = false;
    public bool stateLight_Kitchen = false;
    public bool stateLight_Laundryroom = false;
    public bool stateLight_Bathroom = false;
    public bool stateLight_MasterBedroom = false;
    public bool stateLight_Kidsroom = false;
    #endregion
    //Consumer Num ON
    [Header("Appliance State calnum")]
    public int numWashingMash = 0;
    public int numDryer = 0;
    public int numBob = 0;
    public int numRadio = 0;
    public int numFridge = 0;
    public int numTV = 0;
    public int numLamp = 0;
    #region
    public int numSink_Toilet = 0;
    public int numGamingSystem = 0;
    public int numKettle = 0;
    public int numCounter_Sink_Kitchen = 0;
    public int numCounter_Dishwasher_Kitchen = 0;
    public int numStove = 0;
    public int numMicrowave = 0;
    public int numBlender = 0;
    public int numIron = 0;
    public int numVacuum = 0;
    public int numShower = 0;
    public int numSink_Bathroom = 0;
    public int numRadiator_MasterBedroom = 0;
    public int numIpad = 0;
    public int numToyTrain = 0;
    public int numRadiator_Kidsroom = 0;
    public int numComputer = 0;

    public int numLight_Entry = 0;
    public int numLight_Toilet = 0;
    public int numLight_Livingroom = 0;
    public int numLight_Diningroom = 0;
    public int numLight_Kitchen = 0;
    public int numLight_Laundryroom = 0;
    public int numLight_Bathroom = 0;
    public int numLight_MasterBedroom = 0;
    public int numLight_Kidsroom = 0;
    #endregion
    //Consumer Time on
    [Header("Appliance Ticks/Second On")]
    public int timeWashingMash = 0;
    public int timeDryer = 0;
    public int timeBob = 0;
    public int timeRadio = 0;
    public int timeFridge = 0;
    public int timeTV = 0;
    public int timeLamp = 0;
    #region
    public int timeSink_Toilet = 0;
    public int timeGamingSystem = 0;
    public int timeKettle = 0;
    public int timeCounter_Sink_Kitchen = 0;
    public int timeCounter_Dishwasher_Kitchen = 0;
    public int timeStove = 0;
    public int timeMicrowave = 0;
    public int timeBlender = 0;
    public int timeIron = 0;
    public int timeVacuum = 0;
    public int timeShower = 0;
    public int timeSink_Bathroom = 0;
    public int timeRadiator_MasterBedroom = 0;
    public int timeIpad = 0;
    public int timeToyTrain = 0;
    public int timeRadiator_Kidsroom = 0;
    public int timeComputer = 0;

    public int timeLight_Entry = 0;
    public int timeLight_Toilet = 0;
    public int timeLight_Livingroom = 0;
    public int timeLight_Diningroom = 0;
    public int timeLight_Kitchen = 0;
    public int timeLight_Laundryroom = 0;
    public int timeLight_Bathroom = 0;
    public int timeLight_MasterBedroom = 0;
    public int timeLight_Kidsroom = 0;
    #endregion
    //Consumer Cost per Unit
    [Header("Appliance Cost per Second")]
    public int costWashingMash = 20;
    public int costDryer = 45;
    public int costBob = 5;
    public int costRadio = 2;
    public int costFridge = 5;
    public int costTV = 2;
    public int costLamp = 2;
    #region
    public int costSink_Toilet = 2;
    public int costGamingSystem = 10;
    public int costKettle = 2;
    public int costCounter_Sink_Kitchen = 2;
    public int costCounter_Dishwasher_Kitchen = 40;
    public int costStove = 65;
    public int costMicrowave = 20;
    public int costBlender = 5;
    public int costIron = 2;
    public int costVacuum = 5;
    public int costShower = 5;
    public int costSink_Bathroom = 2;
    public int costRadiator_MasterBedroom = 50;
    public int costIpad = 2;
    public int costToyTrain = 2;
    public int costRadiator_Kidsroom = 50;
    public int costComputer = 10;

    public int costLight_Entry = 10;
    public int costLight_Toilet = 10;
    public int costLight_Livingroom = 10;
    public int costLight_Diningroom = 10;
    public int costLight_Kitchen = 10;
    public int costLight_Laundryroom = 10;
    public int costLight_Bathroom = 10;
    public int costLight_MasterBedroom = 10;
    public int costLight_Kidsroom = 10;
    #endregion

    //Initialize text in the UI element
    private void Start()
    {
        mCounterText.text = inventory.currentMoney.ToString();
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            counterOn = true;
        }
        else { counterOn = false; }
    }

    private void Update()
    {
        //Starts the counter that calculates and adds costs to the total amount.
        if (roundController.roundComplete)
        {
            stateWashingMash = false; 
            stateDryer = false; 
            stateBob = false; 
            stateRadio = false; 
            stateFridge = false; 
            stateTV = false; 
            stateLamp = false;
            stateSink_Toilet = false;
            stateGamingSystem = false;
            stateKettle = false;
            stateCounter_Sink_Kitchen = false;
            stateCounter_Dishwasher_Kitchen = false;
            stateStove = false;
            stateMicrowave = false;
            stateBlender = false;
            stateIron = false;
            stateVacuum = false;
            stateShower = false;
            stateSink_Bathroom = false;
            stateRadiator_MasterBedroom = false;
            stateIpad = false;
            stateToyTrain = false;
            stateRadiator_Kidsroom = false;
            stateComputer = false;
            stateLight_Entry = false;
            stateLight_Toilet = false;
            stateLight_Livingroom = false;
            stateLight_Diningroom = false;
            stateLight_Kitchen = false;
            stateLight_Laundryroom = false;
            stateLight_Bathroom = false;
            stateLight_MasterBedroom = false;
            stateLight_Kidsroom = false;
            counterOn = false;
        }

        //Counter/Timer
        if (counterOn == true)
        {
            timer += Time.deltaTime;

            if (timer >= Delay)
            {
                timer = 0f;
                MoneyCounterUP();
            }
        }
        mCounterText.text = inventory.currentMoney.ToString();
        SetMoney();

        //Debug.Log(MoneyPerSec(moneyPerSec));
    }


    //Money Calculation + for how long an Appliance is on.
    private void MoneyCounterUP()
    {
        //Count up money collectivly and display it.
        
        inventory.currentMoney = inventory.currentMoney - MoneyPerSec(moneyPerSec);

        //Count up for how long each appliance is on (Delay Ticks = seconds) used for end of day cost breakup screen.
        timeWashingMash += numWashingMash;
        timeDryer += numDryer;
        timeBob += numBob;
        timeRadio += numRadio;
        timeFridge += numFridge;
        timeTV += numTV;
        timeLamp += numLamp;
    }

    //Save and update the money the player has in case of crashes
    public void SetMoney()
    {
        PlayerPrefs.SetInt("PlayerMoney", inventory.currentMoney);
    }

    //Get a number that has to be subtracted from the player each second
    private int MoneyPerSec(double moneyPerSec)
    {
        moneyPerSec = ( costWashingMash * numWashingMash +
                        costDryer * numDryer +
                        costBob * numBob +
                        costRadio * numRadio +
                        costFridge * numFridge +
                        costTV * numTV +
                        costLamp * numLamp +
                        costSink_Toilet * numSink_Toilet +
                        costGamingSystem * numGamingSystem +
                        costKettle * numKettle +
                        costCounter_Sink_Kitchen * numCounter_Sink_Kitchen +
                        costCounter_Dishwasher_Kitchen * numCounter_Dishwasher_Kitchen +
                        costStove * numStove +
                        costMicrowave * numMicrowave +
                        costBlender * numBlender +
                        costIron * numIron +
                        costVacuum * numVacuum +
                        costShower * numShower +
                        costSink_Bathroom * numSink_Bathroom +
                        costRadiator_MasterBedroom * numRadiator_MasterBedroom +
                        costIpad * numIpad +
                        costToyTrain * numToyTrain +
                        costRadiator_Kidsroom * numRadiator_Kidsroom +
                        costComputer * numComputer +
                        costLight_Entry * numLight_Entry +
                        costLight_Toilet * numLight_Toilet +
                        costLight_Livingroom * numLight_Livingroom +
                        costLight_Diningroom * numLight_Diningroom +
                        costLight_Kitchen * numLight_Kitchen +
                        costLight_Laundryroom * numLight_Laundryroom +
                        costLight_Bathroom * numLight_Bathroom +
                        costLight_MasterBedroom * numLight_MasterBedroom +
                        costLight_Kidsroom * numLight_Kidsroom
                        );
        #region Upgrades and Events
        //Apply House Upgrades
        if (inventory.SolarPanels)
        {
            moneyPerSec *= 0.8;
        }

        if (inventory.EnergyTiles)
        {
            moneyPerSec /= 2;
        }

        if (inventory.ImprovedIsolation)
        {
            costStove = 48;
            costRadiator_Kidsroom = 38;
            costRadiator_MasterBedroom = 38;
        }

        if (inventory.LEDBulbs)
        {
            costLamp /= 2;
            costLight_Entry /= 2;
            costLight_Toilet /= 2;
            costLight_Livingroom /= 2;
            costLight_Diningroom /= 2;
            costLight_Kitchen /= 2;
            costLight_Laundryroom /= 2;
            costLight_Bathroom /= 2;
            costLight_MasterBedroom /= 2;
            costLight_Kidsroom /= 2;
        }

        //Events
        if (ranEventManager.EventInflation)
        {
            moneyPerSec *= 1.25;
        }

        if (ranEventManager.EventPowerOutage)
        {
            moneyPerSec *= 0;
        }
        else
        {
            moneyPerSec *= 1;
        }
        #endregion
        return (int)moneyPerSec;
    }
}
