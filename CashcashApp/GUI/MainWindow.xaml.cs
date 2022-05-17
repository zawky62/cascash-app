using System;
using System.Windows;
using System.Windows.Controls;

namespace CashcashApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PageAuthentification auth = new(this);
            frame.Content = auth;
        }

        private void frame_ContentRendered(object sender, EventArgs e) // Change le titre de la fenêtre au fil de la navigation 
        {
            Page? page = frame.Content as Page;
            if (page != null)
            {
                this.Title = page.Title;
            }

        }


    }
}
