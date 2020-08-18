using System.ComponentModel;

namespace MathQuizXamarin.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public string Title { get { return "Welcome to the MathQuiz app"; } }

        public BaseViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
