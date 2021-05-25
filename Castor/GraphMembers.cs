using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Windows;
using Thestias;

namespace Castor
{
    public partial class VisualGraph
    {
        public static CultureInfo Cul;
        public static ResourceManager Resman;
        private bool showProperties = false;
        public bool ShowProperties
        {
            get
            {
                return showProperties;
            }
            set
            {
                showProperties = value;
                if (showProperties)
                {
                    this.Eigenschaften.Visibility = Visibility.Visible;
                    this.Eigenschaften_Column.Width = new(525);
                }
                else
                {
                    this.Eigenschaften.Visibility = Visibility.Collapsed;
                    this.Eigenschaften_Column.Width = new(0);
                }
            }
        }
        public bool CanMoveVertices = true;
        public Graph Graph;
        public List<VisualVertex> Vertices;
        public List<VisualEdge> Edges;
    }
}
