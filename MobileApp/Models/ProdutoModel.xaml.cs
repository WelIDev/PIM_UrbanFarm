using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MobileApp.Models
{
    public class ProdutoModel : INotifyPropertyChanged
    {
        private int _quantidade;
        private double _preco;
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string? Icone { get; set; }
        public double ValorTotal { get; set; }
        public int Quantidade
        {
            get => _quantidade;
            set
            {
                _quantidade = value;
                OnPropertyChanged();
            }
        }

        public double Preco
        {
            get => _preco;
            set
            {
                _preco = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value,
            [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
