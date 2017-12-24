﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoLib.Algorithm.DataStructure
{
    public class Graph
    {
        private Boolean[] _discovered;
        private Boolean[] _processed;
        private Int32[] _parent;

        public Edge[] Edges { get; set; }
        public Int32[] Degrees { get; set; }
        public Int32 VerticesCount { get; set; }
        public Int32 EdgesCount { get; set; }
        public Boolean Directed { get; set; }

        public Graph(Int32 verticesCount, Boolean directed)
        {
            VerticesCount = verticesCount;
            EdgesCount = 0;
            Directed = directed;
            Edges = new Edge[VerticesCount];
            Degrees = new Int32[VerticesCount];

            _discovered = new Boolean[verticesCount];
            _processed = new Boolean[verticesCount];
            _parent = new Int32[verticesCount];
        }

        public void InsertEdge(Int32 x, Int32 y, Boolean directed)
        {
            Edge edge = new Edge();
            edge.Weight = 0;
            edge.Y = y;
            edge.Next = this.Edges[x];
            this.Edges[x] = edge;
            this.Degrees[x]++;
            if (!directed)
            {
                InsertEdge(y, x, true);
            }
            else
            {
                this.EdgesCount++;
            }
        }

        public void Read(Int32[,] edges)
        {
            for (Int32 i = 0; i < edges.GetLength(0); i++)
            {
                InsertEdge(edges[i, 0], edges[i, 1], Directed);
            }
        }

        public void Print()
        {
            for (Int32 i = 0; i < this.VerticesCount; i++)
            {
                Debug.Write($"vertex {i}:");
                Edge edge = this.Edges[i];
                while (edge != null)
                {
                    Debug.Write($" {edge.Y}");
                    edge = edge.Next;
                }
                Debug.WriteLine("");
            }
        }

        #region Breadth-first Search

        private void InitializeSearch()
        {
            for (Int32 i = 0; i < VerticesCount; i++)
            {
                _discovered[i] = _processed[i] = false;
                _parent[i] = -1;
            }
        }

        private void ProcessEdge(Int32 start, Int32 end)
        {

        }

        private void BFS(Int32 start)
        {
            InitializeSearch();
            _discovered[start] = true;
            Queue<Int32> queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Int32 vertex = queue.Dequeue();
                Edge edge = this.Edges[vertex];
                while (edge != null)
                {
                    if (!_discovered[edge.Y])
                    {
                        _discovered[edge.Y] = true;
                        _parent[edge.Y] = vertex;
                        queue.Enqueue(edge.Y);
                    }
                    else if (!_processed[edge.Y])
                    {
                        ProcessEdge(vertex, edge.Y);
                    }

                    edge = edge.Next;
                }
                _processed[vertex] = true;
            }
        }

        private void Find(Int32 start, Int32 end)
        {
            if (start == end || end == -1)
            {
                Debug.Write($"{start}");
            }
            else
            {
                Find(start, _parent[end]);
                Debug.Write($" {end}");
            }
        }

        public void FindPath(Int32 start, Int32 end)
        {
            BFS(start);
            Debug.WriteLine("");
            Find(start, end);
            Debug.WriteLine("");
        }

        #endregion
    }

    public class Edge
    {
        public Int32 Y { get; set; }
        public Int32 Weight { get; set; }
        public Edge Next { get; set; }
    }
}