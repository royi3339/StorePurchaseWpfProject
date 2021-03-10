using System.ComponentModel;

namespace PL.VM
{
    public class viewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Updating the value with the "PropertyChanged".
        /// </summary>
        /// <param name="propName"> The name of the property that we change. </param>
        public void update(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
