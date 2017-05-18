using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SoLib.Common
{
    public abstract class NotifyItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetValue<T>(ref T value, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (Equals(value, newValue))
            {
                return false;
            }
            else
            {
                value = newValue;
                OnPropertyChanged(propertyName);
                return true;
            }
        }
    }
}
