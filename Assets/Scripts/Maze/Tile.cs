using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int X { get; set; }
    public int Y { get; set; }
    public Tile Prev { get; set; }
    public float Heur { get; private set; }

    public Tile(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetHeur(int tarPosX, int tarPosY)
    {
        int dx = X - tarPosX;
        int dy = Y - tarPosY;
        Heur = Mathf.Sqrt(dx * dx + dy * dy);
    }
}