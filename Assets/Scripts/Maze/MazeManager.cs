using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{

    public int length;
    public int width;
    private int roomPosition;
    private int randomDirection;
    private char[,] map;
    private int[] sidesX = { 1, 0, -1, 0 };
    private int[] sidesY = { 0, 1, 0, -1 };
    private System.Random rand = new System.Random();
    private List<Tile> allTileList;
    private List<Node> nodeList;
    private bool[,] visited;

    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject ceiling;
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject backWallRoom;
    [SerializeField] private GameObject frontWallRoom;
    [SerializeField] private GameObject leftWallRoom;
    [SerializeField] private GameObject rightWallRoom;
    [SerializeField] private GameObject backWallDoor;
    [SerializeField] private GameObject frontWallDoor;
    [SerializeField] private GameObject leftWallDoor;
    [SerializeField] private GameObject rightWallDoor;
    [SerializeField] private GameObject normalRoom1;
    [SerializeField] private GameObject normalRoom2;
    [SerializeField] private GameObject itemRoom1;
    [SerializeField] private GameObject itemRoom2;
    [SerializeField] private GameObject enemyRoom;
    [SerializeField] private GameObject bossRoom;
    [SerializeField] private GameObject spawnRoom;

    [SerializeField] private GameObject player;

    private List<GameObject> walls;
    private List<GameObject> wallsRoom;
    private List<GameObject> wallsRoomDoor;

    private void instantiateNormalRoom1(int i, int j)
    {
        int[] roomX = { 0, 1, 1, 0 };
        int[] roomY = { 0, 0, 1, 1 };
        Instantiate(normalRoom1, new Vector3(i * 6, 0f, j * 6), Quaternion.identity);
        for (int k = 0; k < 4; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] == 'D')
                {
                    Instantiate(wallsRoomDoor[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
                else if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'N' && map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'n')
                {
                    Instantiate(walls[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateNormalRoom2(int i, int j)
    {
        int[] roomX = { 0, 1, 1, 0 };
        int[] roomY = { 0, 0, 1, 1 };
        Instantiate(normalRoom2, new Vector3(i * 6, 0f, j * 6), Quaternion.identity);
        for (int k = 0; k < 4; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] == 'D')
                {
                    Instantiate(wallsRoomDoor[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
                else if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'M' && map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'm')
                {
                    Instantiate(walls[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateItemRoom1(int i, int j)
    {
        int[] roomX = { 0, 0, 0, 0, 3, 3, 3, 3, 1, 2, 1, 2 };
        int[] roomY = { 0, 1, 2, 3, 0, 1, 2, 3, 0, 0, 3, 3 };
        GameObject itemroom = Instantiate(itemRoom1, new Vector3(i * 6, 0f, j * 6), Quaternion.identity);
        itemRoom1.GetComponent<ItemMazeScript>().generateItem(itemroom.transform.position);
        for (int k = 0; k < 12; k++)
        {
            for (int l = 0; l < 4; l++)
            {

                if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] == 'D')
                {
                    Instantiate(wallsRoomDoor[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
                else if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'I' && map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'i')
                {
                    Instantiate(walls[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateItemRoom2(int i, int j)
    {
        int[] roomX = { 0, 0, 0, 0, 3, 3, 3, 3, 1, 2, 1, 2 };
        int[] roomY = { 0, 1, 2, 3, 0, 1, 2, 3, 0, 0, 3, 3 };
        GameObject itemroom = Instantiate(itemRoom2, new Vector3(i * 6, 0f, j * 6), Quaternion.identity);
        itemRoom1.GetComponent<ItemMazeScript>().generateItem(itemroom.transform.position);
        for (int k = 0; k < 12; k++)
        {
            for (int l = 0; l < 4; l++)
            {

                if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] == 'D')
                {
                    Instantiate(wallsRoomDoor[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
                else if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'C' && map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'c')
                {
                    Instantiate(walls[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateEnemyRoom(int i, int j)
    {
        int[] roomX = { 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 1, 2, 3, 1, 2, 3 };
        int[] roomY = { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 0, 0, 4, 4, 4 };
        Instantiate(enemyRoom, new Vector3(i * 6, 0f, j * 6), Quaternion.identity);
        for (int k = 0; k < 16; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] == 'D')
                {
                    Instantiate(wallsRoomDoor[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
                else if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'E' && map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'e')
                {
                    Instantiate(walls[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateBossRoom(int i, int j)
    {
        int[] roomX = { 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 1, 2, 3, 1, 2, 3 };
        int[] roomY = { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 0, 0, 4, 4, 4 };
        Instantiate(bossRoom, new Vector3(i * 6, 0f, j * 6), Quaternion.identity);
        for (int k = 0; k < 16; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] == 'D')
                {
                    Instantiate(wallsRoomDoor[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
                else if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'B' && map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'b')
                {
                    Instantiate(walls[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateSpawnRoom(int i, int j)
    {
        int[] roomX = { 0, 0, 0, 2, 2, 2, 1, 1 };
        int[] roomY = { 0, 1, 2, 0, 1, 2, 0, 2 };
        Instantiate(spawnRoom, new Vector3(i * 6, 0f, j * 6), Quaternion.identity);
        for (int k = 0; k < 8; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] == 'D')
                {
                    Instantiate(wallsRoomDoor[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
                else if (map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 'S' && map[i + roomX[k] + sidesX[l], j + roomY[k] + sidesY[l]] != 's')
                {
                    Instantiate(walls[l], new Vector3(i * 6 + roomX[k] * 6 + sidesX[l] * 6, 0f, j * 6 + roomY[k] * 6 + sidesY[l] * 6), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateMaze()
    {
        for (int i = 1; i < length - 1; i++)
        {
            for (int j = 1; j < width - 1; j++)
            {
                if (map[i, j] == ' ' || map[i, j] == 'D')
                {
                    Instantiate(floor, new Vector3(i * 6, 0f, j * 6), Quaternion.identity);
                    Instantiate(ceiling, new Vector3(i * 6, 0f, j * 6), Quaternion.identity);
                    for (int k = 0; k < 4; k++)
                    {
                        if (map[i + sidesX[k], j + sidesY[k]] == '#')
                        {
                            Instantiate(wallsRoom[k], new Vector3(i * 6, 0f, j * 6), Quaternion.identity);
                        }
                    }
                }
                if (map[i, j] == 'N')
                {
                    instantiateNormalRoom1(i, j);
                }
                else if (map[i, j] == 'M')
                {
                    instantiateNormalRoom2(i, j);
                }
                else if (map[i, j] == 'I')
                {
                    instantiateItemRoom1(i, j);
                }
                else if (map[i, j] == 'C')
                {
                    instantiateItemRoom2(i, j);
                }
                else if (map[i, j] == 'E')
                {
                    instantiateEnemyRoom(i, j);
                }
                else if (map[i, j] == 'B')
                {
                    instantiateBossRoom(i, j);
                }
                else if (map[i, j] == 'S')
                {
                    instantiateSpawnRoom(i, j);
                }
            }
        }
    }

    private void generateNormalRoom1()
    {
        int xRand = 0;
        int yRand = 0;
        int[] roomX = { 0, 1, 1, 0 };
        int[] roomY = { 0, 0, 1, 1 };
        bool detect;
        do
        {
            detect = false;
            xRand = rand.Next(length - 7) + 2;
            yRand = rand.Next(width - 7) + 2;
            for (int j = -2; j < 4; j++)
            {
                for (int k = -2; k < 4; k++)
                {
                    if (map[xRand + j, yRand + k] != '#')
                    {
                        detect = true;
                    }
                }
            }
        }
        while (detect);
        for (int j = 0; j < 2; j++)
        {
            for (int k = 0; k < 2; k++)
            {
                map[xRand + j, yRand + k] = 'n';
            }
        }
        map[xRand, yRand] = 'N';
        do
        {
            roomPosition = rand.Next(4);
            randomDirection = rand.Next(4);
            if (map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] == '#')
            {
                map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] = 'D';
                break;
            }
        } while (true);
    }

    private void generateNormalRoom2()
    {
        int xRand = 0;
        int yRand = 0;
        int[] roomX = { 0, 1, 1, 0 };
        int[] roomY = { 0, 0, 1, 1 };
        bool detect;
        do
        {
            detect = false;
            xRand = rand.Next(length - 7) + 2;
            yRand = rand.Next(width - 7) + 2;
            for (int j = -2; j < 4; j++)
            {
                for (int k = -2; k < 4; k++)
                {
                    if (map[xRand + j, yRand + k] != '#')
                    {
                        detect = true;
                    }
                }
            }
        }
        while (detect);
        for (int j = 0; j < 2; j++)
        {
            for (int k = 0; k < 2; k++)
            {
                map[xRand + j, yRand + k] = 'm';
            }
        }
        map[xRand, yRand] = 'M';
        for (int i = 0; i < 2; i++)
        {
            do
            {
                roomPosition = rand.Next(4);
                randomDirection = rand.Next(4);
                if (map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] == '#')
                {
                    map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] = 'D';
                    break;
                }
            } while (true);
        }
    }

    private void generateSpawnRoom()
    {
        int xRand = 0;
        int yRand = 0;
        int[] roomX = { 0, 0, 0, 2, 2, 2, 1, 1 };
        int[] roomY = { 0, 1, 2, 0, 1, 2, 0, 2 };
        bool detect;
        do
        {
            detect = false;
            xRand = rand.Next(length - 8) + 2;
            yRand = rand.Next(width - 8) + 2;
            for (int j = -2; j < 5; j++)
            {
                for (int k = -2; k < 5; k++)
                {
                    if (map[xRand + j, yRand + k] != '#')
                    {
                        detect = true;
                    }
                }
            }
        }
        while (detect);
        for (int j = 0; j < 3; j++)
        {
            for (int k = 0; k < 3; k++)
            {
                map[xRand + j, yRand + k] = 's';
            }
        }
        map[xRand, yRand] = 'S';
        do
        {
            roomPosition = rand.Next(8);
            randomDirection = rand.Next(4);
            if (map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] == '#')
            {
                map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] = 'D';
                break;
            }
        } while (true);
    }

    private void generateItemRooms()
    {
        int xRand = 0;
        int yRand = 0;
        int[] roomX = { 0, 0, 0, 0, 3, 3, 3, 3, 1, 2, 1, 2 };
        int[] roomY = { 0, 1, 2, 3, 0, 1, 2, 3, 0, 0, 3, 3 };
        int roomPosition;
        int randomDirection;
        bool detect;
        for (int i = 0; i < 2; i++)
        {
            do
            {
                detect = false;
                xRand = rand.Next(length - 9) + 2;
                yRand = rand.Next(width - 9) + 2;

                for (int j = -2; j < 6; j++)
                {
                    for (int k = -2; k < 6; k++)
                    {
                        if (map[xRand + j, yRand + k] != '#')
                        {
                            detect = true;
                        }
                    }
                }
            } while (detect);
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    map[xRand + j, yRand + k] = 'i';
                }
            }
            map[xRand, yRand] = 'I';
            do
            {
                roomPosition = rand.Next(12);
                randomDirection = rand.Next(4);
                if (map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] == '#')
                {
                    map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] = 'D';
                    break;
                }
            } while (true);
        }
        for (int i = 0; i < 2; i++)
        {
            do
            {
                detect = false;
                xRand = rand.Next(length - 9) + 2;
                yRand = rand.Next(width - 9) + 2;
                for (int j = -2; j < 6; j++)
                {
                    for (int k = -2; k < 6; k++)
                    {
                        if (map[xRand + j, yRand + k] != '#')
                        {
                            detect = true;
                        }
                    }
                }
            } while (detect);
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    map[xRand + j, yRand + k] = 'c';
                }
            }
            map[xRand, yRand] = 'C';
            for (int j = 0; j < 2; j++)
            {
                do
                {
                    roomPosition = rand.Next(12);
                    randomDirection = rand.Next(4);
                    if (map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] == '#')
                    {
                        map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] = 'D';
                        break;
                    }
                } while (true);
            }
        }
    }

    private void generateEnemyRoom()
    {
        int xRand = 0;
        int yRand = 0;
        int[] roomX = { 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 1, 2, 3, 1, 2, 3 };
        int[] roomY = { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 0, 0, 4, 4, 4 };
        int roomPosition;
        int randomDirection;
        bool detect;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                detect = false;
                xRand = rand.Next(length - 10) + 2;
                yRand = rand.Next(width - 10) + 2;
                for (int j = -2; j < 7; j++)
                {
                    for (int k = -2; k < 7; k++)
                    {
                        if (map[xRand + j, yRand + k] != '#')
                        {
                            detect = true;
                        }
                    }
                }
            } while (detect);
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    map[xRand + j, yRand + k] = 'e';
                }
            }
            map[xRand, yRand] = 'E';
            do
            {
                roomPosition = rand.Next(16);
                randomDirection = rand.Next(4);
                if (map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] == '#')
                {
                    map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] = 'D';
                    break;
                }

            } while (true);
        }
    }

    private void generateBossRoom()
    {
        int xRand = 0;
        int yRand = 0;
        int[] roomX = { 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 1, 2, 3, 1, 2, 3 };
        int[] roomY = { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 0, 0, 4, 4, 4 };
        int roomPosition;
        int randomDirection;
        bool detect;
        do
        {
            detect = false;
            xRand = rand.Next(length - 10) + 2;
            yRand = rand.Next(width - 10) + 2;
            for (int j = -2; j < 7; j++)
            {
                for (int k = -2; k < 7; k++)
                {
                    if (map[xRand + j, yRand + k] != '#')
                    {
                        detect = true;
                    }
                }
            }
        } while (detect);
        for (int j = 0; j < 5; j++)
        {
            for (int k = 0; k < 5; k++)
            {
                map[xRand + j, yRand + k] = 'b';
            }
        }
        map[xRand, yRand] = 'B';
        do
        {
            roomPosition = rand.Next(16);
            randomDirection = rand.Next(4);
            if (map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] == '#')
            {
                map[xRand + roomX[roomPosition] + sidesX[randomDirection], yRand + roomY[roomPosition] + sidesY[randomDirection]] = 'D';
                break;
            }

        } while (true);
    }

    private void insert(Node node)
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].getPrice() > node.getPrice())
            {
                nodeList.Insert(i, node);
                return;
            }
        }
        nodeList.Insert(nodeList.Count, node);
    }

    private void generateMaze()
    {
        map = new char[length + 1, width + 1];
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                map[i, j] = '#';
            }
        }
        generateNormalRoom1();
        generateNormalRoom2();
        int r = rand.Next(2);
        if (r == 0) generateNormalRoom1();
        else generateNormalRoom2();
        generateItemRooms();
        generateEnemyRoom();
        generateBossRoom();
        generateSpawnRoom();

        List<Tile> tileList = new List<Tile>();
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (map[i, j] == 'D')
                {
                    tileList.Add(new Tile(i, j));
                }
            }
        }

        nodeList = new List<Node>();
        for (int i = 0; i < tileList.Count; i++)
        {
            for (int j = 1 + i; j < tileList.Count; j++)
            {
                if (tileList[i] != tileList[j])
                {
                    insert(new Node(tileList[i], tileList[j]));
                }

            }
        }

        List<Tile> connected = new List<Tile>();
        List<Node> graphRes = new List<Node>();
        bool first = true;

        foreach (Node node in nodeList)
        {
            if (first)
            {
                connected.Add(node.getDestination());
                connected.Add(node.getSource());
                graphRes.Add(node);
                first = false;
            }
            else if ((connected.Contains(node.getDestination()) && !connected.Contains(node.getSource())))
            {
                connected.Add(node.getSource());
                graphRes.Add(node);
            }
            else if ((!connected.Contains(node.getDestination()) && connected.Contains(node.getSource())))
            {
                connected.Add(node.getDestination());
                graphRes.Add(node);
            }
        }

        AStar astar = new AStar(length, width);

        foreach (Node node in graphRes)
        {
            map = astar.Trace(node.getSource(), node.getDestination(), map);
        }
    }

    void Start()
    {
        generateMaze();

        walls = new List<GameObject>() {backWall, rightWall, frontWall, leftWall};
        wallsRoom = new List<GameObject>() {frontWallRoom, leftWallRoom, backWallRoom, rightWallRoom};
        wallsRoomDoor = new List<GameObject>() {backWallDoor, rightWallDoor, frontWallDoor, leftWallDoor};

        instantiateMaze();
       
        GameObject spawnpoint = GameObject.Find("MazeSpawnPoint");
        player.transform.position = spawnpoint.transform.position + new Vector3(0f, 1.5f, 0f);
    }
}
