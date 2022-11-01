using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gunObject,gunObjectUI;
    public List<GameObject> PoliceCars;
   [HideInInspector] int PoliceNumber;
    [HideInInspector] public int randomSpawnInteger;
    float SpawnTimer;
    public float maxSpawnTimer;
  [HideInInspector]  public List<GameObject>  spawnPoints;
    public GameObject spawnPointsParent;
    public List<GameObject> balloons;
    [HideInInspector] public int balloonCount;
    [HideInInspector] public int explodedBalloons;
    public TextMeshProUGUI ballonText;
    public GameObject treeHit, rockHit;
    [HideInInspector] public bool winBool,failBool;
    public GameObject Panel,winPanel,failPanel;
    [HideInInspector] public int oilLevel, oilCost;  
    public TextMeshProUGUI oilLevelText, oilCostText;
    [HideInInspector] public int gunLevel, gunCost;
    public TextMeshProUGUI gunLevelText, gunCostText;
    [HideInInspector] public int healthLevel, healthCost;
    public TextMeshProUGUI healthLevelText, healthCostText;
    public Image oilImage;
    public GameObject tapToStartButton;
    public CinemachineVirtualCamera cam,camStart;
    public GameObject carUI, carUImesh;
    public GameObject oilMain;
    public Material[] car1armorMats,car2armorMats,car3armorMats;
    public GameObject car1armors,car1armorsUI,car2armors,car2armorsUI,car3armors,car3armorsUI;
    public Mesh[] carModels;
    public GameObject gunModels,gunModelsUI;
    public GameObject oilGoldIcon, gunGoldIcon, armorGoldIcon;
    public ParticleSystem particleCar, particleGun;
    int firstLogin3;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("LevelIndex"))
        {
            PlayerPrefs.SetInt("LevelIndex", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(PlayerPrefs.GetInt("LevelIndex"));
        }
        if (PlayerPrefs.GetInt("FirstLogin3")==0)
        {
            PlayerPrefs.DeleteAll();
            
            firstLogin3 = 1;
            PlayerPrefs.SetInt("FirstLogin3", firstLogin3);
        }
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Balloon"))
        {

            balloons.Add(fooObj);
            
        }
        balloonCount = balloons.Count;
        for (int i = 0; i < spawnPointsParent.transform.childCount; i++)
        {
            spawnPoints.Add(spawnPointsParent.transform.GetChild(i).gameObject);
        }
        ballonText.text = explodedBalloons + "/" + balloonCount;
        #region FirstPlay

        if (PlayerPrefs.GetInt("OilLevel") == 0)
        {
            oilLevel = 1;
            PlayerPrefs.SetInt("OilLevel", oilLevel);
        }
        if (PlayerPrefs.GetInt("OilCost") == 0)
        {
            oilCost = 100;
            PlayerPrefs.SetInt("OilCost", oilCost);
        }
        if (PlayerPrefs.GetFloat("Oil") == 0)
        {
            ArcadeVehicleController.instance.maxOil = 20;
            PlayerPrefs.SetFloat("Oil", ArcadeVehicleController.instance.maxOil);
        }

        if (PlayerPrefs.GetInt("GunLevel") == 0)
        {
            gunLevel = 1;
            PlayerPrefs.SetInt("GunLevel", gunLevel);
        }
        if (PlayerPrefs.GetInt("GunCost") == 0)
        {
            gunCost = 100;
            PlayerPrefs.SetInt("GunCost", gunCost);
        }
        if (PlayerPrefs.GetFloat("GunDamage") == 0)
        {
            Gun.instance.bulletDamage = 5;
            PlayerPrefs.SetFloat("GunDamage", Gun.instance.bulletDamage);
        }

        if (PlayerPrefs.GetInt("HealthLevel") == 0)
        {
            healthLevel = 1;
            PlayerPrefs.SetInt("HealthLevel", healthLevel);
        }
        if (PlayerPrefs.GetInt("HealthCost") == 0)
        {
            healthCost = 100;
            PlayerPrefs.SetInt("HealthCost", healthCost);
        }
        if (PlayerPrefs.GetFloat("Health") == 0)
        {
            ArcadeVehicleController.instance.MaxHealth = 100;        
            PlayerPrefs.SetFloat("Health", ArcadeVehicleController.instance.MaxHealth);
        }

        #endregion

        #region UpgradePlayerPrefs
        oilLevel = PlayerPrefs.GetInt("OilLevel");
        oilLevelText.text = "LEVEL " + oilLevel.ToString();
        oilCost = PlayerPrefs.GetInt("OilCost");
        oilCostText.text = oilCost.ToString();    
        ArcadeVehicleController.instance.maxOil = PlayerPrefs.GetFloat("Oil");
        ArcadeVehicleController.instance.oil = ArcadeVehicleController.instance.maxOil;
        if (oilLevel == 14)
        {
            oilLevelText.text = "MAX";
            oilGoldIcon.SetActive(false);
            oilCostText.enabled = false;
        }

        gunLevel = PlayerPrefs.GetInt("GunLevel");
        gunLevelText.text = "LEVEL " + gunLevel.ToString();
        gunCost = PlayerPrefs.GetInt("GunCost");
        gunCostText.text = gunCost.ToString();
        Gun.instance.bulletDamage = PlayerPrefs.GetFloat("GunDamage");
        if (gunLevel == 14)
        {
            gunLevelText.text = "MAX";
            gunGoldIcon.SetActive(false);
            gunCostText.enabled = false;
        }

        healthLevel = PlayerPrefs.GetInt("HealthLevel");
        healthLevelText.text = "LEVEL " + healthLevel.ToString();
        healthCost = PlayerPrefs.GetInt("HealthCost");
        healthCostText.text = healthCost.ToString();
        ArcadeVehicleController.instance.MaxHealth = PlayerPrefs.GetFloat("Health");
        ArcadeVehicleController.instance.Health = ArcadeVehicleController.instance.MaxHealth;


        #endregion

        #region CarModels&Armors
        if (healthLevel < 5)
        {
            gunObject.transform.localPosition = new Vector3(0,0.279f, -0.859f);
            gunObjectUI.transform.localPosition = new Vector3(0,0.279f, -0.859f);

            car1armors.SetActive(true);
            car1armorsUI.SetActive(true);
            if (healthLevel == 2)
            {
                car1armors.transform.GetChild(0).gameObject.SetActive(true); car1armorsUI.transform.GetChild(0).gameObject.SetActive(true);
            }
            if (healthLevel == 3)
            {
                car1armors.transform.GetChild(0).gameObject.SetActive(true); car1armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                car1armors.transform.GetChild(1).gameObject.SetActive(true); car1armorsUI.transform.GetChild(1).gameObject.SetActive(true);
            }
            if (healthLevel == 4)
            {
                car1armors.transform.GetChild(0).gameObject.SetActive(true); car1armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                car1armors.transform.GetChild(1).gameObject.SetActive(true); car1armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                car1armors.transform.GetChild(2).gameObject.SetActive(true); car1armorsUI.transform.GetChild(2).gameObject.SetActive(true);
            }

            ArcadeVehicleController.instance.body.GetComponent<MeshFilter>().mesh = carModels[0];
            carUImesh.GetComponent<MeshFilter>().mesh = carModels[0];

            ArcadeVehicleController.instance.body.GetComponent<MeshRenderer>().material = car1armorMats[healthLevel - 1];
            carUImesh.GetComponent<MeshRenderer>().material = car1armorMats[healthLevel - 1];
        }
        if(healthLevel>=5 && healthLevel < 10)
        {
            gunObject.transform.localPosition = new Vector3(0, 0.414f, -0.323f);
            gunObjectUI.transform.localPosition = new Vector3(0, 0.414f, -0.323f);

            car1armors.SetActive(false);
            car1armorsUI.SetActive(false);
            car2armors.SetActive(true);
            car2armorsUI.SetActive(true);
            if (healthLevel == 6)
            {
                car2armors.transform.GetChild(0).gameObject.SetActive(true); car2armorsUI.transform.GetChild(0).gameObject.SetActive(true);
            }
            if (healthLevel == 7)
            {
                car2armors.transform.GetChild(0).gameObject.SetActive(true); car2armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                car2armors.transform.GetChild(1).gameObject.SetActive(true); car2armorsUI.transform.GetChild(1).gameObject.SetActive(true);
            }
            if (healthLevel == 8)
            {
                car2armors.transform.GetChild(0).gameObject.SetActive(true); car2armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                car2armors.transform.GetChild(1).gameObject.SetActive(true); car2armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                car2armors.transform.GetChild(2).gameObject.SetActive(true); car2armorsUI.transform.GetChild(2).gameObject.SetActive(true);
            }
            if (healthLevel == 9)
            {
                car2armors.transform.GetChild(0).gameObject.SetActive(true); car2armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                car2armors.transform.GetChild(1).gameObject.SetActive(true); car2armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                car2armors.transform.GetChild(2).gameObject.SetActive(true); car2armorsUI.transform.GetChild(2).gameObject.SetActive(true);
                car2armors.transform.GetChild(3).gameObject.SetActive(true); car2armorsUI.transform.GetChild(3).gameObject.SetActive(true);
            }

            ArcadeVehicleController.instance.body.GetComponent<MeshFilter>().mesh = carModels[1];
            carUImesh.GetComponent<MeshFilter>().mesh = carModels[1];

            ArcadeVehicleController.instance.body.GetComponent<MeshRenderer>().material = car2armorMats[healthLevel - 5];
            carUImesh.GetComponent<MeshRenderer>().material = car2armorMats[healthLevel - 5];
        }
        if (healthLevel >= 10 && healthLevel < 15)
        {
            gunObject.transform.localPosition = new Vector3(0, 0.414f, -0.17f);
            gunObjectUI.transform.localPosition = new Vector3(0, 0.414f, -0.17f);

            car2armors.SetActive(false);
            car2armorsUI.SetActive(false);
            car3armors.SetActive(true);
            car3armorsUI.SetActive(true);
            if (healthLevel == 11)
            {
                car3armors.transform.GetChild(0).gameObject.SetActive(true); car3armorsUI.transform.GetChild(0).gameObject.SetActive(true);
            }
            if (healthLevel == 12)
            {
                car3armors.transform.GetChild(0).gameObject.SetActive(true); car3armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                car3armors.transform.GetChild(1).gameObject.SetActive(true); car3armorsUI.transform.GetChild(1).gameObject.SetActive(true);
            }
            if (healthLevel == 13)
            {
                car3armors.transform.GetChild(0).gameObject.SetActive(true); car3armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                car3armors.transform.GetChild(1).gameObject.SetActive(true); car3armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                car3armors.transform.GetChild(2).gameObject.SetActive(true); car3armorsUI.transform.GetChild(2).gameObject.SetActive(true);
            }
            if (healthLevel == 14)
            {
                car3armors.transform.GetChild(0).gameObject.SetActive(true); car3armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                car3armors.transform.GetChild(1).gameObject.SetActive(true); car3armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                car3armors.transform.GetChild(2).gameObject.SetActive(true); car3armorsUI.transform.GetChild(2).gameObject.SetActive(true);
                car3armors.transform.GetChild(3).gameObject.SetActive(true); car3armorsUI.transform.GetChild(3).gameObject.SetActive(true);

                healthLevelText.text = "MAX ";
                healthCostText.enabled = false;
                armorGoldIcon.SetActive(false);
            }

            ArcadeVehicleController.instance.body.GetComponent<MeshFilter>().mesh = carModels[2];
            carUImesh.GetComponent<MeshFilter>().mesh = carModels[2];

            ArcadeVehicleController.instance.body.GetComponent<MeshRenderer>().material = car2armorMats[healthLevel - 10];
            carUImesh.GetComponent<MeshRenderer>().material = car2armorMats[healthLevel - 10];
        }

        for (int i = 0; i < gunLevel; i++)
        {
            if (i == gunLevel - 1)
            {
                gunModels.transform.GetChild(i).gameObject.SetActive(true);
                gunModelsUI.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                gunModels.transform.GetChild(i).gameObject.SetActive(false);
                gunModelsUI.transform.GetChild(i).gameObject.SetActive(false);

            }
        }
        #endregion

      
       


        Time.timeScale = 0;
    }
    IEnumerator PanelC()
    {
       

        yield return new WaitForSeconds(2);
        ArcadeVehicleController.instance.gameObject.SetActive(false);
        cam.Priority = 20;
        Panel.SetActive(true);
        carUI.SetActive(true);
        oilMain.SetActive(false);
        if (winBool)
        {
            winPanel.SetActive(true);
            carUI.transform.position = ArcadeVehicleController.instance.transform.position+new Vector3(0,0.5f,0);
        }
        if (failBool)
        {
            failPanel.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        

        if (!winBool && !failBool)
        {
            ArcadeVehicleController.instance.oil -= Time.deltaTime;
            oilImage.fillAmount = (ArcadeVehicleController.instance.oil / ArcadeVehicleController.instance.maxOil);
        }
        if ((ArcadeVehicleController.instance.oil <= 0 || ArcadeVehicleController.instance.Health<=0) && !failBool && !winBool)
        {
            ArcadeVehicleController.instance.oil = 0;
            failBool = true;
            ArcadeVehicleController.instance.MaxSpeed = 0.1f;
           
            foreach (GameObject polices in GameObject.FindGameObjectsWithTag("Police"))
            {
                // Destroy(polices);
                polices.GetComponent<ArcadeAiVehicleController>().health = 0;
               polices.GetComponent<ArcadeAiVehicleController>().Destoyed();

            }
            PlayerPrefs.SetFloat("Money", ArcadeVehicleController.instance.moneyCount);
            StartCoroutine(PanelC());
        }
        if (balloonCount == explodedBalloons && !winBool && !failBool)
        {
            winBool = true;
           
            ArcadeVehicleController.instance.MaxSpeed = 0.1f;
            foreach (GameObject police in GameObject.FindGameObjectsWithTag("Police"))
            {

                // Destroy(police);
                police.GetComponent<ArcadeAiVehicleController>().health = 0;
                police.GetComponent<ArcadeAiVehicleController>().Destoyed();

            }
            PlayerPrefs.SetFloat("Money", ArcadeVehicleController.instance.moneyCount);
            StartCoroutine(PanelC());
        }
        
        SpawnTimer += Time.deltaTime;
        if (SpawnTimer > maxSpawnTimer && !winBool && !failBool)
        {
          
            randomSpawnInteger = Random.Range(0, spawnPoints.Count);
            PoliceNumber = Random.Range(0, PoliceCars.Count);
          var policeCar =  Instantiate(PoliceCars[PoliceNumber], spawnPoints[randomSpawnInteger].transform.position, Quaternion.identity);
            policeCar.transform.LookAt(ArcadeVehicleController.instance.transform);
            SpawnTimer = 0;
        }
    }
    public void Buttons(int ButtonNo)
    {
        if (ButtonNo == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (ButtonNo == 2)
        {
            if (ArcadeVehicleController.instance.moneyCount >= oilCost && oilLevel<14)
            {
                ArcadeVehicleController.instance.moneyCount -= oilCost;
                PlayerPrefs.SetFloat("Money", ArcadeVehicleController.instance.moneyCount);
                ArcadeVehicleController.instance.moneyText.text = ArcadeVehicleController.instance.moneyCount.ToString();

                oilLevel += 1;
                PlayerPrefs.SetInt("OilLevel", oilLevel);
                oilLevelText.text ="LEVEL "+ oilLevel.ToString();

                oilCost += 50;
                PlayerPrefs.SetInt("OilCost", oilCost);
                oilCostText.text = oilCost.ToString();

                ArcadeVehicleController.instance.maxOil += 5;
                PlayerPrefs.SetFloat("Oil", ArcadeVehicleController.instance.maxOil);

                if (oilLevel == 14)
                {
                    oilLevelText.text = "MAX";
                    oilGoldIcon.SetActive(false);
                    oilCostText.enabled = false;
                }
            }
           

        }
        if (ButtonNo == 3)
        {
            if (ArcadeVehicleController.instance.moneyCount >= gunCost && gunLevel<14)
            {
                ArcadeVehicleController.instance.moneyCount -= gunCost;
                PlayerPrefs.SetFloat("Money", ArcadeVehicleController.instance.moneyCount);
                ArcadeVehicleController.instance.moneyText.text = ArcadeVehicleController.instance.moneyCount.ToString();

                gunLevel += 1;
                PlayerPrefs.SetInt("GunLevel", gunLevel);
                gunLevelText.text = "LEVEL " + gunLevel.ToString();

                for (int i = 0; i < gunLevel; i++)
                {
                    if (i == gunLevel - 1)
                    {
                        gunModels.transform.GetChild(i).gameObject.SetActive(true);
                        gunModelsUI.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        gunModels.transform.GetChild(i).gameObject.SetActive(false);
                        gunModelsUI.transform.GetChild(i).gameObject.SetActive(false);

                    }
                }
              

                gunCost += 50;
                PlayerPrefs.SetInt("GunCost", gunCost);
                gunCostText.text = gunCost.ToString();

                Gun.instance.bulletDamage += 2;
                PlayerPrefs.SetFloat("GunDamage", Gun.instance.bulletDamage);

                if (gunLevel == 14)
                {
                    gunLevelText.text = "MAX";
                    gunCostText.enabled = false;
                    gunGoldIcon.SetActive(false);
                }
                particleGun.Play();
            }


        }
        if (ButtonNo == 4)
        {
            if (ArcadeVehicleController.instance.moneyCount >= healthCost && healthLevel<14)
            {
                ArcadeVehicleController.instance.moneyCount -= healthCost;
                PlayerPrefs.SetFloat("Money", ArcadeVehicleController.instance.moneyCount);
                ArcadeVehicleController.instance.moneyText.text = ArcadeVehicleController.instance.moneyCount.ToString();

                healthLevel += 1;
                PlayerPrefs.SetInt("HealthLevel", healthLevel);
                healthLevelText.text = "LEVEL " + healthLevel.ToString();

                healthCost += 50;
                PlayerPrefs.SetInt("HealthCost", healthCost);
                healthCostText.text = healthCost.ToString();

                ArcadeVehicleController.instance.MaxHealth += 20;
                PlayerPrefs.SetFloat("Health", ArcadeVehicleController.instance.MaxHealth);

                if (healthLevel < 5)
                {
                    gunObject.transform.localPosition = new Vector3(0, 0.279f, -0.859f);
                    gunObjectUI.transform.localPosition = new Vector3(0, 0.279f, -0.859f);

                    car1armors.SetActive(true);
                    car1armorsUI.SetActive(true);
                    if (healthLevel == 2)
                    {
                        car1armors.transform.GetChild(0).gameObject.SetActive(true); car1armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    if (healthLevel == 3)
                    {
                        car1armors.transform.GetChild(0).gameObject.SetActive(true); car1armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                        car1armors.transform.GetChild(1).gameObject.SetActive(true); car1armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                    }
                    if (healthLevel == 4)
                    {
                        car1armors.transform.GetChild(0).gameObject.SetActive(true); car1armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                        car1armors.transform.GetChild(1).gameObject.SetActive(true); car1armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                        car1armors.transform.GetChild(2).gameObject.SetActive(true); car1armorsUI.transform.GetChild(2).gameObject.SetActive(true);
                    }

                    ArcadeVehicleController.instance.body.GetComponent<MeshFilter>().mesh = carModels[0];
                    carUImesh.GetComponent<MeshFilter>().mesh = carModels[0];

                    ArcadeVehicleController.instance.body.GetComponent<MeshRenderer>().material = car1armorMats[healthLevel - 1];
                    carUImesh.GetComponent<MeshRenderer>().material = car1armorMats[healthLevel - 1];
                }
                if (healthLevel >= 5 && healthLevel < 10)
                {
                    gunObject.transform.localPosition = new Vector3(0, 0.414f, -0.323f);
                    gunObjectUI.transform.localPosition = new Vector3(0, 0.414f, -0.323f);

                    car1armors.SetActive(false);
                    car1armorsUI.SetActive(false);
                    car2armors.SetActive(true);
                    car2armorsUI.SetActive(true);
                    if (healthLevel == 6)
                    {
                        car2armors.transform.GetChild(0).gameObject.SetActive(true); car2armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    if (healthLevel == 7)
                    {
                        car2armors.transform.GetChild(0).gameObject.SetActive(true); car2armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                        car2armors.transform.GetChild(1).gameObject.SetActive(true); car2armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                    }
                    if (healthLevel == 8)
                    {
                        car2armors.transform.GetChild(0).gameObject.SetActive(true); car2armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                        car2armors.transform.GetChild(1).gameObject.SetActive(true); car2armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                        car2armors.transform.GetChild(2).gameObject.SetActive(true); car2armorsUI.transform.GetChild(2).gameObject.SetActive(true);
                    }
                    if (healthLevel == 9)
                    {
                        car2armors.transform.GetChild(0).gameObject.SetActive(true); car2armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                        car2armors.transform.GetChild(1).gameObject.SetActive(true); car2armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                        car2armors.transform.GetChild(2).gameObject.SetActive(true); car2armorsUI.transform.GetChild(2).gameObject.SetActive(true);
                        car2armors.transform.GetChild(3).gameObject.SetActive(true); car2armorsUI.transform.GetChild(3).gameObject.SetActive(true);
                    }

                    ArcadeVehicleController.instance.body.GetComponent<MeshFilter>().mesh = carModels[1];
                    carUImesh.GetComponent<MeshFilter>().mesh = carModels[1];

                    ArcadeVehicleController.instance.body.GetComponent<MeshRenderer>().material = car2armorMats[healthLevel - 5];
                    carUImesh.GetComponent<MeshRenderer>().material = car2armorMats[healthLevel - 5];
                }
                if (healthLevel >= 10 && healthLevel < 15)
                {
                    gunObject.transform.localPosition = new Vector3(0, 0.414f, -0.17f);
                    gunObjectUI.transform.localPosition = new Vector3(0, 0.414f, -0.17f);

                    car2armors.SetActive(false);
                    car2armorsUI.SetActive(false);
                    car3armors.SetActive(true);
                    car3armorsUI.SetActive(true);
                    if (healthLevel == 11)
                    {
                        car3armors.transform.GetChild(0).gameObject.SetActive(true); car3armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    if (healthLevel == 12)
                    {
                        car3armors.transform.GetChild(0).gameObject.SetActive(true); car3armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                        car3armors.transform.GetChild(1).gameObject.SetActive(true); car3armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                    }
                    if (healthLevel == 13)
                    {
                        car3armors.transform.GetChild(0).gameObject.SetActive(true); car3armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                        car3armors.transform.GetChild(1).gameObject.SetActive(true); car3armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                        car3armors.transform.GetChild(2).gameObject.SetActive(true); car3armorsUI.transform.GetChild(2).gameObject.SetActive(true);
                    }
                    if (healthLevel == 14)
                    {
                        car3armors.transform.GetChild(0).gameObject.SetActive(true); car3armorsUI.transform.GetChild(0).gameObject.SetActive(true);
                        car3armors.transform.GetChild(1).gameObject.SetActive(true); car3armorsUI.transform.GetChild(1).gameObject.SetActive(true);
                        car3armors.transform.GetChild(2).gameObject.SetActive(true); car3armorsUI.transform.GetChild(2).gameObject.SetActive(true);
                        car3armors.transform.GetChild(3).gameObject.SetActive(true); car3armorsUI.transform.GetChild(3).gameObject.SetActive(true);

                        healthLevelText.text = "MAX ";
                        healthCostText.enabled = false;
                        armorGoldIcon.SetActive(false);
                    }

                    ArcadeVehicleController.instance.body.GetComponent<MeshFilter>().mesh = carModels[2];
                    carUImesh.GetComponent<MeshFilter>().mesh = carModels[2];

                    ArcadeVehicleController.instance.body.GetComponent<MeshRenderer>().material = car2armorMats[healthLevel - 10];
                    carUImesh.GetComponent<MeshRenderer>().material = car2armorMats[healthLevel - 10];
                }

                particleCar.Play();
            }


        }
        if (ButtonNo == 5)
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }
        if (ButtonNo == 6)
        {
            Time.timeScale = 1;
            tapToStartButton.SetActive(false);
            camStart.Priority = 0;
        }
        if (ButtonNo == 7)
        {
            if (SceneManager.GetActiveScene().buildIndex == 10)
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
           
        }
    }
}
