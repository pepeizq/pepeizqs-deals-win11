using Microsoft.UI.Xaml;

namespace Principal
{

    public partial class App : Application
    {

        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        public Window m_window;
    }
}
