using System;

namespace Pollux
{
    public partial class GraphDarstellung
    {
        public new class GraphExceptions
        {
            public class NameAlreadyExistsException : Exception
            {
                public new string Message = "This name already exists!";
            }

            public class UnsupportedUIElementException : Exception
            {
                public new string Message = "This UIElement isn't allowed here, please choose another one.";
            }
        }
    }
}
