using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class DoublyLinkedList : IEnumerable<double>
    {
        private Node? head;
        public int Count { get; private set; }

        public void AddFirst(double data)
        {
            Node newNode = new Node(data);
            if (head != null) { newNode.Next = head; head.Previous = newNode; }
            head = newNode;
            Count++;
        }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= Count || head == null) throw new IndexOutOfRangeException();
                Node curr = head;
                for (int i = 0; i < index; i++) curr = curr.Next!;
                return curr.Data;
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count || head == null) return;
            Node curr = head;
            for (int i = 0; i < index; i++) curr = curr.Next!;

            if (curr.Previous != null) curr.Previous.Next = curr.Next;
            else head = curr.Next;

            if (curr.Next != null) curr.Next.Previous = curr.Previous;
            Count--;
        }

        public double? GetFirstPositive()
        {
            foreach (var x in this) if (x > 0) return x;
            return null;
        }

        public double GetAverage()
        {
            if (Count == 0) return 0;
            double sum = 0;
            foreach (var x in this) sum += x;
            return sum / Count;
        }

        public int CountLessThanAverage()
        {
            double avg = GetAverage();
            int count = 0;
            foreach (var x in this) if (x < avg) count++;
            return count;
        }

        public DoublyLinkedList GetNewListGreaterThanAverage()
        {
            double avg = GetAverage();
            DoublyLinkedList newList = new DoublyLinkedList();
            foreach (var x in this) if (x > avg) newList.AddFirst(x);
            return newList;
        }

        public void RemoveBeforeMax()
        {
            if (head == null) return;
            Node maxNode = head;
            double maxVal = head.Data;
            int maxIdx = 0, i = 0;

            for (Node? c = head; c != null; c = c.Next, i++)
                if (c.Data > maxVal) { maxVal = c.Data; maxNode = c; maxIdx = i; }

            head = maxNode;
            head.Previous = null;
            Count -= maxIdx;
        }

        public IEnumerator<double> GetEnumerator()
        {
            for (Node? c = head; c != null; c = c.Next) yield return c.Data;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}