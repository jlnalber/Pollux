using System;

namespace Thestias
{
    public partial class Graph
    {
        public class GraphExceptions
        {
            public class NameAlreadyExistsException : Exception
            {
                public new string Message = "This name already exists!";
            }

            public class GraphIsNotEditableException : Exception
            {
                public new string Message = "This graph is not editable!";
            }
        }
    }
}
