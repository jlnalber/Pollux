using System;

namespace Pollux.Graph
{
    public partial class Graph
    {
        public class GraphExceptions
        {
            public class NameAlreadyExistsException : Exception
            {
                public new string Message = "This name already exists!";
            }
        }
    }
}
