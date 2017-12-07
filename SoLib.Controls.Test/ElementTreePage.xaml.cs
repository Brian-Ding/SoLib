using SoLib.Controls.ElementTree;
using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SoLib.Controls.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ElementTreePage : Page
    {
        public ElementTreePage()
        {
            this.InitializeComponent();
            tree.RootElement = new Element(Guid.NewGuid(), new Rectangle() { Fill = new SolidColorBrush(Colors.Red), Height = 50, Width = 50 }, new Button(), new List<Element>()
            {
                new Element(Guid.NewGuid(), new Rectangle() { Fill = new SolidColorBrush(Colors.Orange), Height = 50, Width = 50 }, new Button(), new List<Element>
                {
                    new Element(Guid.NewGuid(), new Rectangle() { Fill = new SolidColorBrush(Colors.Purple), Height = 50, Width = 50 }, new Button(), null),
                    new Element(Guid.NewGuid(), new Rectangle() { Fill = new SolidColorBrush(Colors.Black), Height = 50, Width = 50 }, new Button(), new List<Element>
                    {
                        new Element(Guid.NewGuid(), new Rectangle() { Fill = new SolidColorBrush(Colors.Brown), Height = 50, Width = 50 }, new Button(), null),
                        new Element(Guid.NewGuid(), new Rectangle() { Fill = new SolidColorBrush(Colors.Pink), Height = 50, Width = 50 }, new Button(), null),
                    })
                }),
                new Element(Guid.NewGuid(), new Rectangle() { Fill = new SolidColorBrush(Colors.Yellow), Height = 50, Width = 50 }, new Button(), new List<Element>()
                {
                    new Element(Guid.NewGuid(), new Rectangle() { Fill = new SolidColorBrush(Colors.Green), Height = 50, Width = 50 }, new Button(), null),
                    new Element(Guid.NewGuid(), new Rectangle() { Fill = new SolidColorBrush(Colors.Blue), Height = 50, Width = 50 }, new Button(), null),
                    new Element(Guid.NewGuid(), new Rectangle() { Fill = new SolidColorBrush(Colors.Indigo), Height = 50, Width = 50 }, new Button(), null)
                })
            });
        }
    }
}
