using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainGenerator : MonoBehaviour
{
    #region Variables
    public Vector3 spawnPos;

    [Header("Car Settings")]
    [Space]
    public int denialDungeonLength = 1;
    public int isolationDungeonLength = 1;
    public int angerDungeonLength = 1;
    public int sorrowDungeonLength = 1;
    public int acceptanceDungeonLength = 1;

    [Header("Required References")]
    [Space]
    public Transform characterLists;
    public Transform carContainer;
    public TrainManager manager;
    public GameObject gameManager;
    public GameObject camRig;
    public GameObject audioManager;
    #endregion


    private void Start()
    {
        StartTrainGeneration();
    }

    //This starts the train generation process
    void StartTrainGeneration()
    {
        StartCoroutine("GenerateTrain");
    }

    //This generates the train :)
    IEnumerator GenerateTrain()
    {
        // I use an IEnum because I need time between each car generation to avoid overlapping issues
        //There might be a better way but this was more or less clean

        #region Car Checklist
        //Did I mention clean?
        //These bools track what I still need to generate
        bool denialGenerated = false;
        bool isolationGenerated = false;
        bool angerGenerated = false;
        bool sorrowGenerated = false;
        bool mainCarsGenerated = false;
        #endregion

        #region Car Category Array
        //This tracks the car categories that are available
        string[] mainCars;
        mainCars = new string[4];
        mainCars[0] = "Denial";
        mainCars[1] = "Isolation";
        mainCars[2] = "Anger";
        mainCars[3] = "Sorrow";
        #endregion

        //This generates the first starting car and the first inbetween car before the real game starts
        GenerateCar("Start", manager.currentCharacter);
        yield return new WaitForSeconds(0.3f);
        GenerateCar("InBetween", manager.currentCharacter);
        yield return new WaitForSeconds(0.3f);


        //This handles the main cars that can be changed
        while (mainCarsGenerated == false)
        {
            string randCar = mainCars[Random.Range(0, mainCars.Length)];
            int dungeonLength = 0;
            bool canGen = false;


            //This will setup the settings used to generate each dungeon
            switch (randCar)
            {
                #region Denial Dungeon Settings
                case "Denial":
                    if (denialGenerated)
                    {
                        break;
                    }
                    else
                    {
                        dungeonLength = denialDungeonLength;
                        denialGenerated = true;
                        canGen = true;
                    }
                    break;
                #endregion

                #region Isolation Dungeon Settings
                case "Isolation":
                    if (isolationGenerated)
                    {
                        break;
                    }
                    else
                    {
                        dungeonLength = isolationDungeonLength;
                        isolationGenerated = true;
                        canGen = true;
                    }
                    break;
                #endregion

                #region Anger Dungeon Settings
                case "Anger":
                    if (angerGenerated)
                    {
                        break;
                    }
                    else
                    {
                        dungeonLength = angerDungeonLength;
                        angerGenerated = true;
                        canGen = true;
                    }
                    break;
                #endregion

                #region Sorrow Dungeon Settings
                case "Sorrow":
                    if (sorrowGenerated)
                    {
                        break;
                    }
                    else
                    {
                        dungeonLength = sorrowDungeonLength;
                        sorrowGenerated = true;
                        canGen = true;
                    }
                    break;
                    #endregion
            }

            #region Train Car Generation
            //This actually generates the train
            if (canGen)
            {
                //For every car I need for the length of the dungeon
                for (int i = 0; i < dungeonLength; i++)
                {
                    if (i == 0)
                    {
                        GenerateCar("Rest", manager.currentCharacter);
                        yield return new WaitForSeconds(0.3f);

                        //Generate an inbetween to bridge the cars
                        GenerateCar("InBetween", manager.currentCharacter);
                        yield return new WaitForSeconds(0.3f);
                    }

                    //If the current car is just another car
                    else if (i != dungeonLength)
                    {
                        //Generate one of the Denial cars
                        GenerateCar(randCar, manager.currentCharacter);
                        yield return new WaitForSeconds(0.3f);

                        //Generate an inbetween to bridge the cars
                        GenerateCar("InBetween", manager.currentCharacter);
                        yield return new WaitForSeconds(0.3f);

                    }

                    //If the current car is the final car aka the Boss car
                    if (i == dungeonLength - 1)
                    {
                        //Boss fight!
                        //Generate an inbetween to bridge the cars
                        GenerateCar("InBetween", manager.currentCharacter);
                        yield return new WaitForSeconds(0.3f);
                    }
                }
            }
            #endregion


            #region Break Condition
            if (denialGenerated && isolationGenerated && angerGenerated && sorrowGenerated)
            {
                mainCarsGenerated = true;
                break;
            }
            #endregion
        }

        #region Final Car Generation
        GenerateCar("InBetween", manager.currentCharacter);
        yield return new WaitForSeconds(0.3f);

        GenerateCar("InBetween", manager.currentCharacter);
        yield return new WaitForSeconds(0.3f);

        GenerateCar("Acceptance", manager.currentCharacter);
        yield return new WaitForSeconds(0.3f);
        #endregion
    }


    //This handles selecting the car to generate and placing it
    void GenerateCar(string carType, string charaList)
    {
        //This is the list of cars that can generate with the character being played
        CarGenerateList carList = characterLists.Find(charaList).GetComponent<CarGenerateList>();


        GameObject newCar = carList.GetRandomCar(carType); //This is the random car that's grabbed
        GameObject carObj = Instantiate(newCar); //This is the instanced object version of the random car we grabbed
        
        //This sets the position and parenting of the car plus setting it up with required references
        carObj.transform.SetParent(carContainer);
        carObj.transform.position = spawnPos;
        carObj.GetComponent<CarChunkInfo>().Setup(gameManager, camRig, audioManager);

        //This sets the spawn position of the next car to the hitch point of this car
        //The hitch point is where the two cars should connect
        spawnPos.x = newCar.transform.Find("HitchPoint").transform.position.x + carObj.transform.position.x;


        #region Starting Car Extra Function
        //Starting cars have some special functions they need to follow
        if (carType == "Start")
        {
            GameObject positionPoint = newCar.transform.Find("PlayerSpawnPositions").GetComponent<ObjectSelector>().SelectRandomObject();
            Vector3 spawnPos = positionPoint.transform.position;
            manager.player.position = spawnPos;
        }
        #endregion
    }
}
