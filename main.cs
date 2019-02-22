/*
    Copyright 2019 © Ramón Romero @ramonromeroqro
    A01700318 for ITESM

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    Thanks also to @eduardolarios ,  and the $sudo comunity 
*/
using System;
using System.Collections.Generic;

public class Board
{
    // size of board
    public int totalsize, width, height;
    //Arr to coordinate nodes
    public Node[] arrNodes;
    public HashSet<Node> goalNorth;
    public HashSet<Node> goalSouth;
    public HashSet<Node> goalEast;
    public HashSet<Node> goalWest;

    public Board(int w, int h)
    {
        this.totalsize = w * h;
        this.width = w;
        this.height = h;
        // Nodes creation
        this.arrNodes = new Node[this.totalsize];

        this.goalNorth = new HashSet<Node>();
        this.goalSouth = new HashSet<Node>();
        this.goalWest = new HashSet<Node>();
        this.goalEast = new HashSet<Node>();

        for (int i = 0; i < this.totalsize; i++)
        {
            arrNodes[i] = new Node(i, this);
            if (i >= 0 && i < this.width)
            {
                this.goalNorth.Add(arrNodes[i]);
            }
            if (i >= this.totalsize - this.width && i < this.totalsize)
            {
                this.goalSouth.Add(arrNodes[i]);
            }
            if (i % this.width == 0)
            {
                this.goalWest.Add(arrNodes[i]);
            }
            if ((i % this.width) == (this.width - 1))
            {
                this.goalEast.Add(arrNodes[i]);

            }

        }
        this.setPathsForAll();

    }

    //Get Node By Coordinates
    public Node getNodeXY(int x, int y)
    {
        if (x >= this.width || x < 0 || y >= this.height || y < 0)
        {
            return null;
        }
        else
        {
            return this.arrNodes[(y * this.width) + (x)];
        }
    }


    // Set 6 paths per Node
    public void setPathsForAll()
    {

        for (int i = 0; i < this.totalsize; i++)
        {
            //Middle
            arrNodes[i].setpaths(this);

        }


    }

    public HashSet<Node> reachable(Node n)
    {
        HashSet<Node> visited = new HashSet<Node>();
        Queue<Node> q = new Queue<Node>();
        q.Enqueue(n);
        visited.Add(n);
        while (q.Count != 0)
        {
            Node current = q.Dequeue();
            HashSet <Node> toVisit = new HashSet <Node> (current.validPaths);
            toVisit.ExceptWith(visited);
            
            foreach (var i in toVisit)
            {
                
                    visited.Add(i);
                    q.Enqueue(i);
                
            }
        }
        return visited;
    }
    /*
    
    
    
def find_all_reachable_nodes(vertex):
    """Return set containing all vertices reachable from vertex."""
    visited = set()
    q = Queue()
    q.enqueue(vertex)
    visited.add(vertex)
    while not q.is_empty():
        current = q.dequeue()
        for dest in current.get_neighbours():
            if dest not in visited:
                visited.add(dest)
                q.enqueue(dest)
    return visited */



    public string specsBoard()
    {
        return "Total Size: " + this.totalsize + "\nW: " + this.width + "\nH: " + this.height;
    }

    public void conectionsAll()
    {
        for (int i = 0; i < this.totalsize; i++)
        {
            //Middle
            arrNodes[i].printConections();

        }
    }


    public string printBoard()
    {
        string s = "|";
        for (int i = 0; i < this.totalsize; i++)
        {
            Node current = arrNodes[i];
            if (i % this.width == 0)
            {
                string init = new string(' ', 2 * current.y);
                s = s + init + current.player + "   ";
            }
            else
            {
                if ((i % this.width == this.width - 1))
                {
                    string end = new string(' ', 2 * ((this.height - 1) - current.y));

                    s = s + current.player + end + "|\n";
                    if (i != this.totalsize - 1)
                    {
                        s = s + "|";
                    }
                }
                else
                {
                    s = s + current.player + "   ";
                }
            }
        }
        return s;
    }


}

public class Node
{
    //The player using the node 0 must expose an empty node
    public int player;
    //Compound and simple position
    public int x, y, i;
    // The six possible paths

    //Dictionary<string, Node> paths;
    public HashSet<Node> paths;
    public HashSet<Node> validPaths;


    public void setpaths(Board b)
    {
        paths = new HashSet<Node>();
        validPaths = new HashSet<Node>();
        /*
    public Node leftNode = null;
    public Node rightNode = null;
    public Node upperRightNode = null;
    public Node upperLeftNode = null;
    public Node lowerLeftNode = null;
    public Node lowerRightNode = null;
    */
        /* 
           for (int i = 0; i < this.totalsize; i++)
           {
               //Middle
               Node current = arrNodes[i];
               current.leftNode = getNodeXY(current.x-1, current.y);
               current.rightNode = getNodeXY(current.x+1, current.y);
               //Upper
               current.upperLeftNode = getNodeXY(current.x, current.y-1);
               current.upperRightNode = getNodeXY(current.x+1, current.y-1);
               //Lower
               current.lowerLeftNode = getNodeXY(current.x-1, current.y+1);
               current.lowerRightNode = getNodeXY(current.x, current.y+1);
           } */

        if (this.x >= 0 && this.x < b.width - 1)
        {
            paths.Add(b.getNodeXY(this.x + 1, this.y));
        }
        if (this.x > 0 && this.x < b.width)
        {
            paths.Add(b.getNodeXY(this.x - 1, this.y));
        }
        if (this.y >= 0 && this.y < b.height - 1)
        {
            if (this.x > 0 && this.x < b.width)
            {
                paths.Add(b.getNodeXY(this.x - 1, this.y + 1));
            }
            paths.Add(b.getNodeXY(this.x, this.y + 1));
        }

        if (this.y > 0 && this.y < b.height)
        {
            if (this.x >= 0 && this.x < b.width - 1)
            {
                paths.Add(b.getNodeXY(this.x + 1, this.y - 1));

            }
            paths.Add(b.getNodeXY(this.x, this.y - 1));
        }



    }

    public Node(int p, Board b)
    {
        this.i = p;
        this.x = p % b.width;
        this.y = p / b.width;
        this.player = 0;
    }

    public string printNode()
    {
        return "i: " + this.i + "x: " + this.x + "y: " + this.y;
    }

    public void printConections()
    {
        foreach (var i in this.paths)
        {
            //Console.WriteLine($"node{this.i}: (x={this.x}, y={this.y}) :: {pair.Key} -> (x={pair.Value.x}, y={pair.Value.y})");
            Console.WriteLine($"node{this.i}: (x={this.x}, y={this.y}) ::  -> (x={i.x}, y={i.y})");
        }
    }

}




class Hex
{
    static void Main()
    {
        Console.WriteLine("Boards's height  ->  ");
        int w = 0;
        int.TryParse(Console.ReadLine(), out w);
        Console.WriteLine("Boards's width   ->  ");
        int h = 0;
        int.TryParse(Console.ReadLine(), out h);
        // Console.WriteLine(w);
        // Console.WriteLine(h);
        // Start game
        Board nb = new Board(w, h);
        Console.WriteLine("P1 East-West");
        Console.WriteLine("P2 North-South");

        Console.Write(nb.printBoard());
        // Start set players
        //int i=0;


        HashSet<Node> playedByP1 = new HashSet<Node>();
        HashSet<Node> playedByP2 = new HashSet<Node>();
        bool finished = false;
        int currentPlayer = 1;
        bool valid = true;
        while (finished != true)
        {
            Console.WriteLine("\nX    ->  ");
            int x = 0;
            int.TryParse(Console.ReadLine(), out x);
            Console.WriteLine("Y    ->  \n");
            //Console.WriteLine("----------------------");
            int y = 0;
            int.TryParse(Console.ReadLine(), out y);
            Node n = nb.getNodeXY(x, y);
            if (playedByP1.Contains(n) || playedByP1.Contains(n) || n == null)
            {
                Console.WriteLine("invalid move");
                valid = false;

            }
            if (valid == true)
            {
                n.player = currentPlayer;
                if (currentPlayer == 1)
                {
                    //Console.WriteLine(n.paths.Overlaps(playedByP1) );
                    if (n.paths.Overlaps(playedByP1) == true)
                    {
                        // Add intersection to HashSet Possible
                        HashSet<Node> h1 = new HashSet<Node>(n.paths);
                        h1.IntersectWith(playedByP1);
                        n.validPaths.UnionWith(h1);
                        //nb.deepthSearch(n);
                        HashSet<Node> p = nb.reachable(n);
                        //Console.WriteLine(p.Overlaps(nb.goalEast) && p.Overlaps(nb.goalWest));
                        if (p.Overlaps(nb.goalEast) && p.Overlaps(nb.goalWest))
                        {
                            finished = true;
                            break;
                        }


                        //depth
                    }
                    playedByP1.Add(n);

                    currentPlayer = 2;

                }
                else
                {
                    if (n.paths.Overlaps(playedByP2) == true)
                    {

                        HashSet<Node> h2 = new HashSet<Node>(n.paths);
                        h2.IntersectWith(playedByP2);
                        n.validPaths.UnionWith(h2);

                        HashSet<Node> p = nb.reachable(n);
                        //Console.WriteLine(p.Overlaps(nb.goalSouth) && p.Overlaps(nb.goalNorth));

                        if (p.Overlaps(nb.goalSouth) && p.Overlaps(nb.goalNorth))
                        {
                            finished = true;
                            break;
                        }

                        //depth
                    }
                    playedByP2.Add(n);
                    currentPlayer = 1;


                }
                //if (n.paths.IntersectWith)
            }

            else
            {
                Console.WriteLine("No valid, try again");

            }
            Console.Write(nb.printBoard());




        }
        Console.Write(nb.printBoard());
        Console.WriteLine($"\nFinished, Player {currentPlayer} wins\n");

        //  n = getnode(x,y)

        // if node in SetPlaye1 or in setPlayer2:
        //     no viable
        // else:

        //     n.changeplayer
        //     if n.paths & SetCurrentPlayer(interseccion):
        //         search()
        //         recorrido a profundidad -> if any( i in Horizontal1 and Horizontal2): finish: 
        //     else:
        //         Set.add(n)



        /* Debug paths, goalStates
        
        nb.conectionsAll();

        foreach (var i in nb.goalEast)
        {
            //Console.WriteLine($"node{this.i}: (x={this.x}, y={this.y}) :: {pair.Key} -> (x={pair.Value.x}, y={pair.Value.y})");
            Console.WriteLine($"gE -> (i={i.i}, x={i.x}, y={i.y})");
        }

        foreach (var i in nb.goalWest)
        {
            //Console.WriteLine($"node{this.i}: (x={this.x}, y={this.y}) :: {pair.Key} -> (x={pair.Value.x}, y={pair.Value.y})");
            Console.WriteLine($"gW -> (i={i.i}, x={i.x}, y={i.y})");
        }

        foreach (var i in nb.goalNorth)
        {
            //Console.WriteLine($"node{this.i}: (x={this.x}, y={this.y}) :: {pair.Key} -> (x={pair.Value.x}, y={pair.Value.y})");
            Console.WriteLine($"gN -> (i={i.i}, x={i.x}, y={i.y})");
        }

        foreach (var i in nb.goalSouth)
        {
            //Console.WriteLine($"node{this.i}: (x={this.x}, y={this.y}) :: {pair.Key} -> (x={pair.Value.x}, y={pair.Value.y})");
            Console.WriteLine($"gS -> (i={i.i}, x={i.x}, y={i.y})");
        }
        
         */


    }
}