using System;

namespace Thestias
{
    public partial class Graph
    {
        public delegate void ChangedEvent(object sender, ChangedEventArgs e);
        public ChangedEvent Changed = (object sender, ChangedEventArgs e) => { };

        public class ChangedEventArgs : EventArgs
        {
            public enum ChangedElements
            {
                Graph, Vertex, Edge
            }

            public enum ChangedTypes
            {
                Addition, Removal, Renaming
            }

            public object ChangedObject;
            public ChangedElements ChangedElement;
            public ChangedTypes ChangedType;

            public ChangedEventArgs(object changedObject, ChangedElements changedElement, ChangedTypes changedType)
            {
                this.ChangedObject = changedObject;
                this.ChangedElement = changedElement;
                this.ChangedType = changedType;
            }
        }
    }
}
