using System;
using System.Text;

public class Node
{
    public Node prev = null;
    public Node next = null;
    public char ch;

    public Node(char chh)
    {
        ch = chh;
    }
}

public class Program
{
    public static Node head = null;
    public static Node current = null;

    public static void Main()
    {
        int t = int.Parse(Console.ReadLine());

        for (int j = 0; j < t; ++j)
        {
            string str = Console.ReadLine();
            head = null;
            current = null;
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] != '<' && str[i] != '>' && str[i] != '-')
                {
                    if (current == null)
                    {
                        current = new Node(str[i]);
                        current.next = head;
                        if (head != null)
                            head.prev = current;
                        head = current;
                    }
                    else
                    {
                        Node newNode = new Node(str[i]);
                        newNode.next = current.next;
                        newNode.prev = current;
                        if (current.next != null)
                        {
                            current.next.prev = newNode;
                        }
                        current.next = newNode;
                        current = newNode;
                    }
                }
                else if (str[i] == '<')
                {
                    if (current == null)
                        continue;
                    current = current.prev;
                }
                else if (str[i] == '>')
                {
                    if (current == null)
                    {
                        current = head;
                    }
                    else
                    {
                        if (current.next != null)
                            current = current.next;
                    }
                }
                else
                {
                    if (current == null)
                    {
                        continue;
                    }
                    if (current.prev == null)
                    {
                        head = current.next;
                        current = null;
                        if (head != null)
                            head.prev = null;
                    }
                    else
                    {
                        current.prev.next = current.next;
                        if (current.next != null)
                            current.next.prev = current.prev;
                        current = current.prev;
                    }
                }
            }

            while (head != null)
            {
                result.Append(head.ch);
                head = head.next;
            }
            Console.WriteLine(result);
        }
    }
}

