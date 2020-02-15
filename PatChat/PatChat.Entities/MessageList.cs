using PatChat.Entities.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatChat.Entities
{
    public class MessageList:IEnumerable
    {

        public class MessageNode
        {
            internal MessageNode next;
            internal Message data;
        }
        MessageNode First;
        MessageNode Last;
        public MessageList() => First = Last = null;
        public void Push(Message message)
        {
            MessageNode node = new MessageNode();
            node.data = message;

            if(First is null)
            {
                First =Last= node;
                node.next = null;
            }
            else
            {
                Last.next = node;
                node.next = null;
                Last = node;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        private LinkedListEnumerator GetEnumerator()
        {
            return new LinkedListEnumerator(First);
        }
        public class LinkedListEnumerator : IEnumerator
         {
            MessageNode head;
            MessageNode Move;
            public LinkedListEnumerator(MessageNode first)
            {
                head = Move = first;
            }
            public object Current => Move.data;

            public bool MoveNext()
            {
                Move = Move.next;
                return Move != null;
            }

            public void Reset()
            {
                Move = head;
            }
        }
    }
}
