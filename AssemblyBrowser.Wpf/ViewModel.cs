using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using AssemblyBrowser.Lib.TreeComponent;
using Microsoft.Win32;

namespace AssemblyBrowser.Wpf
{
    public class ViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand OpenFileCommand => new OpenFileCommand(OpenAssembly);
        public string OpenedFile { get; set; } = "File not opened";
        public List<INode> Namespaces { get; set; }
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private void OpenAssembly()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Assemblies|*.dll;*.exe", 
                Title = "Select assembly", 
                Multiselect = false
            };
            
            var isOpen = openFileDialog.ShowDialog() ;
                
            if (isOpen != null && isOpen.Value)
            {
                OpenedFile = openFileDialog.FileName;
                OnPropertyChanged(nameof(OpenedFile));
                
                Namespaces = AssemblyBrowser.Lib.AssemblyBrowser.GetAssemblyInfo(OpenedFile);
                OnPropertyChanged(nameof(Namespaces));
            }
        }

    }
}