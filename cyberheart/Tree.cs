using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace cyberheart
{
    class Leaf
    {
        Point position;
        List<Leaf> neighbours = new List<Leaf>();
        public Leaf(Point position)
        {
            this.position = position;
        }
        public Point getPoint()
        {
            return position;
        }
        public List<Point> FindNeighbors(Bitmap image, Color edgeColor, 
            Color markedEdgeColor)
        {
            List<Point> neighbours = new List<Point>();

            Color up = image.GetPixel(position.X, position.Y + 1);
            Color down = image.GetPixel(position.X, position.Y - 1);
            Color left = image.GetPixel(position.X - 1, position.Y);
            Color right = image.GetPixel(position.X + 1, position.Y);

            if (up == edgeColor)
                neighbours.Add(new Point(position.X, position.Y + 1));
            if (down == edgeColor)
                neighbours.Add(new Point(position.X, position.Y - 1));
            if (left == edgeColor)
                neighbours.Add(new Point(position.X - 1, position.Y));
            if (right == edgeColor)
                neighbours.Add(new Point(position.X + 1, position.Y));

            image.SetPixel(position.X, position.Y, markedEdgeColor);

            return neighbours;
        }
    }
    class Tree
    {
        Color edgeColor = Color.FromArgb(0,0,0);
        Color markedEdgeColor = Color.FromArgb(255,255,255);
        
        public List<Leaf> tree = new List<Leaf>();

        public void GenerateTree(Bitmap image, Point startPoint)
        {
            tree.Add(new Leaf(startPoint));
            int currentLear = 0;
            while (tree.Count > currentLear)
            {
                Leaf l = tree[currentLear];
                currentLear++;

                List<Point> myList = new List<Point>();
                myList = l.FindNeighbors(image, edgeColor, markedEdgeColor);

                foreach (Point p in myList)
                {
                    tree.Add(new Leaf(p));
                }
            }
            
        }


    }
}
