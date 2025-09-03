using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private int l;
    private int w;
    private List<Tile> tileList;

    public AStar(int l, int w)
    {
        this.l = l;
        this.w = w;
    }

    public void insertTile(Tile t){
        if (tileList.Contains(t)) return;
        for(int i = 0; i < tileList.Count; i++){
            if(tileList[i].Heur > t.Heur)
            {
                tileList.Insert(i, t);
                return;
            }
        }
        tileList.Insert(tileList.Count, t);
    }

    public char[,] dupeMap(char[,] map){
        char[,] dMap = new char[l, w];
        for(int i = 0; i < l; i++){
            for (int j = 0; j < w; j++){
                dMap[i, j] = map[i, j];
            }
        }
        return dMap;
    }

    public char[,] Trace(Tile s, Tile e, char[,] map){
        int[] dirX = { 0, 1, 0, -1 };
        int[] dirY = { 1, 0, -1, 0 };
        char[,] dMap = dupeMap(map);
        tileList = new List<Tile>();

        insertTile(s);
        Tile curr = null;

        while(tileList.Count > 0){
            curr = tileList[0];
            tileList.RemoveAt(0);
            dMap[curr.X, curr.Y] = 'X';

            if (curr.X == e.X && curr.Y == e.Y) return traceback(curr, map);

            for(int i = 0; i < 4; i++){
                if (curr.X + dirX[i] <= 0 || curr.Y + dirY[i] <= 0 || curr.X + dirX[i] >= l || curr.Y + dirY[i] >= w) continue;
                if (dMap[curr.X + dirX[i], curr.Y + dirY[i]] == '#' || dMap[curr.X + dirX[i], curr.Y + dirY[i]] == ' ' || dMap[curr.X + dirX[i], curr.Y + dirY[i]] == 'D'){
                    Tile newTile = new Tile(curr.X + dirX[i], curr.Y + dirY[i]);
                    newTile.SetHeur(e.X, e.Y);
                    newTile.Prev = curr;
                    insertTile(newTile);
                }
            }
        }
        return traceback(curr, map);
    }

    public char[,] traceback(Tile t, char[,] map){
        Tile curr = t;
        do{
            if (curr == null) break;
            if(map[curr.X, curr.Y] != 'D') map[curr.X, curr.Y] = ' ';
            curr = curr.Prev;
        }while(true);
        return map;
    }
}
