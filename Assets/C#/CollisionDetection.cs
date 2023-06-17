using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public int gridSize = 100; // Grid boyutu
    public int objectCount = 100; // Nesne say�s�

    public Transform enemiesParent;
    public List<GameObject> objects = new List<GameObject>(); // Nesnelerin listesi
    private CollisionGrid collisionGrid; // �arp��ma gridi

    private void Start()
    {
        for (int i = 0; i < enemiesParent.childCount; i++)

        {
            objects.Add(enemiesParent.GetChild(i).gameObject);
        }
        objectCount = objects.Count;
        // �arp��ma gridini olu�tur
        collisionGrid = new CollisionGrid(gridSize, gridSize);

        // Nesneleri olu�tur ve gridde yerle�tir
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(0, gridSize), 0f, Random.Range(0, gridSize));
            GameObject obj = objects[i];
            obj.transform.position = position;

            // Griddeki h�creye ekle
            collisionGrid.AddAtom((int)(position.x / gridSize), (int)(position.z / gridSize), (uint)i);
        }
    }

    private void FixedUpdate()
    {
        // Nesnelerin �arp��ma kontrol�n� yap
        for (int i = 0; i < objects.Count; i++)
        {
            GameObject obj = objects[i];
            Vector3 position = obj.transform.position;

            // Eski grid h�cresinden ��kar
            int oldX = (int)(position.x / gridSize);
            int oldY = (int)(position.z / gridSize);
            collisionGrid.RemoveAtom(oldX, oldY, (uint)i);

            // Yeni grid h�cresine ekle
            int newX = (int)(position.x / gridSize);
            int newY = (int)(position.z / gridSize);
            collisionGrid.AddAtom(newX, newY, (uint)i);

            // Griddeki �evresini kontrol et
            List<GameObject> potentialCollisions = collisionGrid.GetPotentialCollisions(position, objects);

            // Potansiyel �arp��malar� kontrol et
            for (int j = 0; j < potentialCollisions.Count; j++)
            {
                GameObject potentialCollision = potentialCollisions[j];

                // �ki nesne aras�nda �arp��ma kontrol� yap
                if (obj != potentialCollision && CheckCollision(obj, potentialCollision))
                {
                    Debug.Log(obj.name + " ve " + potentialCollision.name + " �arp��t�!");
                }
            }
        }
    }

    private bool CheckCollision(GameObject obj1, GameObject obj2)
    {
        // �ki nesnenin �arp��ma kontrol�n� burada ger�ekle�tirin
        // �rne�in, nesnelerin collider'lar�na eri�erek �arp��ma kontrol� yapabilirsiniz

        // Collider'lar�n�z� burada kullanarak �arp��ma kontrol�n� ger�ekle�tirin
        // �rne�in, obj1.GetComponent<Collider>().bounds.Intersects(obj2.GetComponent<Collider>().bounds) gibi bir kontrol yapabilirsiniz

        // Bu �rnekte fizik motorunu kullanmad���m�z i�in basit bir mesafe kontrol� yapaca��m
        float distance = Vector3.Distance(obj1.transform.position, obj2.transform.position);
        return distance < 1f; // Mesafe 1 birimden k���kse �arp��ma oldu�unu kabul edelim
    }
}

public class CollisionCell
{
    public const int CellCapacity = 4;
    public const int MaxCellIndex = CellCapacity - 1;

    public uint ObjectsCount { get; private set; }
    public uint[] Objects { get; private set; }

    public CollisionCell()
    {
        ObjectsCount = 0;
        Objects = new uint[CellCapacity];
    }

    public void AddAtom(uint id)
    {
        if (ObjectsCount < MaxCellIndex)
        {
            Objects[ObjectsCount] = id;
            ObjectsCount++;
        }
    }

    public void RemoveAtom(uint id)
    {
        for (int i = 0; i < ObjectsCount; i++)
        {
            if (Objects[i] == id)
            {
                Objects[i] = Objects[ObjectsCount - 1];
                ObjectsCount--;
                return;
            }
        }
    }

    public void Clear()
    {
        ObjectsCount = 0;
    }
}

public class CollisionGrid
{
    private CollisionCell[,] grid;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public CollisionGrid(int width, int height)
    {
        Width = width;
        Height = height;
        grid = new CollisionCell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new CollisionCell();
            }
        }
    }

    public bool AddAtom(int x, int y, uint atom)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            grid[x, y].AddAtom(atom);
            return true;
        }
        return false;
    }

    public void RemoveAtom(int x, int y, uint atom)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            grid[x, y].RemoveAtom(atom);
        }
    }

    public void Clear()
    {
        foreach (CollisionCell cell in grid)
        {
            cell.Clear();
        }
    }

    public List<GameObject> GetPotentialCollisions(Vector3 position, List<GameObject> objects)
    {
        List<GameObject> potentialCollisions = new List<GameObject>();

        // Griddeki �evresini kontrol et
        int x = (int)(position.x / Width);
        int y = (int)(position.z / Height);

        // Gerekli grid h�crelerini kontrol et
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                int checkX = x + dx;
                int checkY = y + dy;

                // Ge�ersiz h�creleri atla
                if (checkX < 0 || checkX >= Width || checkY < 0 || checkY >= Height)
                    continue;

                // H�credeki nesneleri potansiyel �arp��malara ekle
                CollisionCell cell = grid[checkX, checkY];
                for (int i = 0; i < cell.ObjectsCount; i++)
                {
                    GameObject collisionObj = objects[(int)cell.Objects[i]];
                    if (!potentialCollisions.Contains(collisionObj))
                    {
                        potentialCollisions.Add(collisionObj);
                    }
                }
            }
        }

        return potentialCollisions;
    }
}
