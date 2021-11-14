using System.Collections;
using TMPro;
using UnityEngine;
public class Planet : MonoBehaviour
{
    public int Population;
    public float ReproductionSpeed;
    public int MaxPopulation;
    public BaseFaction BaseFaction;
    public BaseType BaseType;
    
    public Unit UnitPrefab;

    [SerializeField] private TextMeshPro PopulationText;
    private float tickCount;
    private float needTimerTick;
    private Vector3 cachedPosition;
    private SpriteRenderer spriteRenderer;
    
    private void OnEnable()
    {
        Timer.Tick += OnTimerTick;
    }

    private void OnDisable()
    {
        Timer.Tick -= OnTimerTick;
    }
    
    private void Start()
    {
        cachedPosition = transform.position;
        SetStartStats(BaseType);
        needTimerTick = ReproductionSpeed / 0.10f;
        PopulationText.text = Population.ToString();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //StartCoroutine(SendUnits());
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Collision(other);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
       Collision(other);
    }

    private void Collision(Collider2D other)
    {
        if (BaseFaction == BaseFaction.None)
        {
            if (other.gameObject.CompareTag("Unit") || other.gameObject.CompareTag("EnemyUnit"))
            {
                if (Population == 0)
                {
                    Unit unit = other.gameObject.GetComponent<Unit>();
                    BaseFaction = unit.UnitFaction;
                    spriteRenderer.color = unit.gameObject.gameObject.GetComponent<SpriteRenderer>().color;
                    gameObject.tag = unit.PlanetTag;
                    UnitPrefab = unit.BaseUnitPrefab;
                    return;
                }
                Population--;
                PopulationText.text = Population.ToString();
            }
            Destroy(other.gameObject);
        }
        else if (BaseFaction == BaseFaction.AIRed)
        {
            UnitCollisionCheck(other.gameObject,"EnemyUnit");
        }
        else if (BaseFaction == BaseFaction.Player)
        {
            UnitCollisionCheck(other.gameObject,"Unit");
        }
    }
    
    private void UnitCollisionCheck(GameObject other,string unitTag)
    {
        if (!other.gameObject.CompareTag(unitTag))
        {
           
            if (Population == 0)
            {
                Unit unit = other.gameObject.GetComponent<Unit>();
                BaseFaction = unit.UnitFaction;
                spriteRenderer.color = unit.gameObject.gameObject.GetComponent<SpriteRenderer>().color;
                gameObject.tag = unit.PlanetTag;
                UnitPrefab = unit.BaseUnitPrefab;
                return;
            } 
            PopulationText.text = Population.ToString();
            Population--;
        }
        else
        {
            Population++;
            PopulationText.text = Population.ToString();
        }
        Destroy(other.gameObject);
    }
    
    private void OnTimerTick()
    {
        if (BaseFaction == BaseFaction.None)
            return;

        if (MaxPopulation <= Population)
        {
            return;
        }

        tickCount++;

        if (tickCount.Equals(needTimerTick))
        {
            Population++;
            PopulationText.text = Population.ToString();
            tickCount = 0;
        }
    }
    
    public void SendHalfUnits(Transform target)
    {
        StartCoroutine(SendUnits(target));
    }

    private IEnumerator SendUnits(Transform target)
    {
        int tempPopulation = Population;
        for (int i = 0; i <  tempPopulation / 2; i++)
        {
            Vector3 test = target.position - cachedPosition;
            var unit = Instantiate(UnitPrefab, test.normalized + cachedPosition, Quaternion.identity);
            unit.Target = target;
            unit.UnitFaction = BaseFaction;
            unit.PlanetTag = gameObject.tag;
            unit.BaseUnitPrefab = UnitPrefab;
            Population--;
            PopulationText.text = Population.ToString();
            yield return new WaitForSeconds(0.3f);
        }
  
    } 
    
    private void SetStartStats(BaseType baseType)
    {
        switch (baseType)
        {
            case BaseType.Large:
                Population = 50;
                ReproductionSpeed = 1.6f;
                MaxPopulation = 200;
                break;
            case BaseType.Big:
                Population = 40;
                ReproductionSpeed = 1.6f;
                MaxPopulation = 150;
                break;
            case BaseType.Medium:
                Population = 20;
                ReproductionSpeed = 1.6f;
                MaxPopulation = 100;
                break;
            case BaseType.Low:
                Population = 10;
                ReproductionSpeed = 1.6f;
                MaxPopulation = 50;
                break;
        }
    }

    private void LookAtCenter()
    {
        float angle = 0;

        Vector3 relative = transform.InverseTransformPoint(Vector3.zero);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }
}

public enum BaseFaction
{
    None = 0,
    Player = 1,
    AIRed = 2,
    AIGreen = 3
}

public enum BaseType
{
    Large = 0,
    Big = 1,
    Medium = 2,
    Low = 3
}