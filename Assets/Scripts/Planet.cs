using System.Collections;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public int Population;
    public float ReproductionSpeed;
    public int MaxPopulation;
    public float UnitMoveSpeedScale;
    public BaseFaction BaseFaction;
    public BaseType BaseType;
    public Unit UnitPrefab;

    public Transform SpawnPoint;
    public Data Data;
    
    [SerializeField] private TextMeshPro PopulationText;
    private float tickCount;
    private float needTimerTick;

    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        Timer.Tick += OnTimerTick;
    }

    private void OnDisable()
    {
        Timer.Tick -= OnTimerTick;
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
        if (tickCount >= needTimerTick)
        {
            Population++;
            PopulationText.text = Population.ToString();
            tickCount = 0;
        }
    }

    private void Start()
    {
        
        SetStartStats(BaseType);
        needTimerTick = ReproductionSpeed / 0.02f;
        PopulationText.text = Population.ToString();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (!other.gameObject.name.Equals($"To {gameObject.name}"))
        {
            return;
        }
        
        if (BaseFaction == BaseFaction.None)
        {
            if (Population == 0)
            {
                Unit unit = other.gameObject.GetComponent<Unit>();
                BaseFaction = unit.UnitFaction;
                spriteRenderer.color = unit.gameObject.gameObject.GetComponent<SpriteRenderer>().color;
                gameObject.tag = unit.PlanetTag;
                UnitPrefab = unit.BaseUnitPrefab;
                UnitMoveSpeedScale = unit.MoveSpeedScale;
                return;
            }

            Population--;
            PopulationText.text = Population.ToString();

            Destroy(other.gameObject);
        }
        else
        {
            UnitCollisionCheck(other.gameObject);
        }
    }

    private void UnitCollisionCheck(GameObject other)
    {
        if (!other.gameObject.name.Equals($"To {gameObject.name}"))
        {
            return;
        }
        
        if (!other.gameObject.CompareTag(gameObject.tag))
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
    
    public void SendHalfUnits(Transform target)
    {
        StartCoroutine(SendUnits(target));
    }

    private IEnumerator SendUnits(Transform target)
    {
        int tempPopulation = Population;
        Population = 0;
        PopulationText.text = Population.ToString();
        
        SetSpawnPointPosition(target);
        
        //Тихий ужас
        
        Vector2 offset1 = SpawnPoint.TransformPoint(new Vector2(0.1f,0));
        Vector2 offset2 = SpawnPoint.TransformPoint(new Vector2(0.3f,0));
        Vector2 offset3 = SpawnPoint.TransformPoint(new Vector2(-0.1f,0));
        Vector2 offset4 = SpawnPoint.TransformPoint(new Vector2(-0.3f,0));

        int cyclesCount = tempPopulation / 4;
        int remains = tempPopulation - cyclesCount * 4;
        
        for (int i = 0; i < cyclesCount; i += 1)
        {
            SpawnUnit(offset1, target);
            SpawnUnit(offset2, target);
            SpawnUnit(offset3, target);
            SpawnUnit(offset4, target);
          
            yield return new WaitForSeconds(0.1f);
        }
        
        if (remains > 0 && remains < 4)
        {
            switch (remains)
            {
                case 1:
                    SpawnUnit(offset4, target);
                    break;
                case 2 :
                    SpawnUnit(offset4, target);
                    SpawnUnit(offset3, target);
                    break;
                case 3:
                    SpawnUnit(offset4, target);
                    SpawnUnit(offset3, target);
                    SpawnUnit(offset1, target);
                    break;
            }
        }
        
        SpawnPoint.position = transform.position;
    }

    private void SpawnUnit(Vector3 spawnPos, Transform unitTarget)
    {
        var unit = Instantiate(UnitPrefab, spawnPos, Quaternion.identity);
        unit.Target = unitTarget;
        unit.UnitFaction = BaseFaction;
        unit.PlanetTag = gameObject.tag;
        unit.BaseUnitPrefab = UnitPrefab;
        unit.gameObject.name = $"To {unitTarget.gameObject.name}";
        unit.MoveSpeedScale = UnitMoveSpeedScale;
    }

    private void SetSpawnPointPosition(Transform target)
    {
        LookAt(SpawnPoint, target.transform);
        Vector2 relative = SpawnPoint.TransformDirection(Vector2.up);
        SpawnPoint.position += (Vector3)relative / 1.5f;
    }

    private void LookAt(Transform self, Transform target)
    {
        float angle = 0;

        Vector3 relative = self.InverseTransformPoint(target.position);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        self.Rotate(0, 0, -angle);
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
            
        if (BaseFaction == BaseFaction.None)
        {
            UnitMoveSpeedScale = 1;
        }
        else if (BaseFaction == BaseFaction.Player)
        {
            UnitMoveSpeedScale = Data.MoveSpeedScale;
            ReproductionSpeed = Data.ReproductionTime;
        }
        else
        {
            UnitMoveSpeedScale = Data.EnemyMoveSpeedScale;
            ReproductionSpeed = Data.EnemyReproductionTime;
        }
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