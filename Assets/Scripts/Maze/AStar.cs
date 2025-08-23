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
            if(tileList[i].getHeur() > t.getHeur())
            {
                tileList.Insert(i, tile);
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

    public char[,] trace(Tile s, Tile e, char[,] map){
        int[] dirX = { 0, 1, 0, -1 };
        int[] dirY = { 1, 0, -1, 0 };
        char[,] dMap = dupeMap(map);
        tileList = new List<Tile>();

        Insert(s);
        Tile curr = null;

        while(tileList.Count > 0){
            curr = tileList[0];
            tileList.RemoveAt(0);
            dMap[curr.getX(), curr.getY()] = 'X';

            if (curr.getX() == end.getX() && curr.getY() == end.getY()) return traceback(curr, map);

            for(int i = 0; i < 4; i++){
                if (curr.getX() + dirX[i] <= 0 || curr.getY() + dirY[i] <= 0 || curr.getX() + dirX[i] >= l || curr.getY() + dirY[i] >= w) continue;
                if (dMap[curr.getX() + dirX[i], curr.getY() + dirY[i]] == '#' || dMap[curr.getX() + dirX[i], curr.getY() + dirY[i]] == ' ' || dMap[curr.getX() + dirX[i], curr.getY() + dirY[i]] == 'D'){
                    Tile newTile = new Tile(curr.getX() + dirX[i], curr.getY() + dirY[i]);
                    newTile.SetHeur(end.getX(), end.getY());
                    newTile.setPrev(curr);
                    Insert(newTile);
                }
            }
        }
        return traceback(curr, map);
    }

    public char[,] traceback(Tile t, char[,] map){
        Tile curr = t;
        do{
            if (curr == null) break;
            if(map[curr.getX(), curr.getY()] != 'D') map[curr.getX(), curr.getY()] = ' ';
            curr = curr.getPrev();
        }while(true);
        return map;
    }
}
