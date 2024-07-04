using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AgilionPdtScanner.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        private string title = string.Empty;
        private bool isLoading;
        private bool isNotLoading;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
        public bool IsLoading { get => isLoading; set => SetProperty(ref isLoading, value); }
        public bool IsNotLoading { get => isNotLoading; set => SetProperty(ref isNotLoading, value); }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action? onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
