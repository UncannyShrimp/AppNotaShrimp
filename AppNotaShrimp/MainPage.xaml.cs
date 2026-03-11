using AppNotaShrimp.Data;
using AppNotaShrimp.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace AppNotaShrimp
{
    public partial class MainPage : ContentPage
    {
        private readonly DataContext _dataContext;

        public ObservableCollection<Models.Notas> listNotas { get; set; }

        public MainPage(DataContext dataContext)
        {
            InitializeComponent();
            BindingContext = this;  //MVVM: El BindingContext se establece en la propia página para que los elementos de la interfaz puedan enlazar a las propiedades y comandos definidos en esta clase.
            _dataContext = dataContext; //Inyección de dependencias para acceder al contexto de datos
            listNotas = new ObservableCollection<Notas>(); //Carga las notas desde la base de datos y las asigna a la colección observable para que la interfaz se actualice automáticamente cuando cambien los datos.
            CargarNotas();

        }

        private async void CargarNotas()
        {
            try 
            { 
                await _dataContext.Database.EnsureCreatedAsync(); //Asegura que la base de datos esté creada antes de intentar cargar las notas.
                var notas = await _dataContext.Notas.ToListAsync(); //Carga las notas desde la base de datos de forma asíncrona.
                foreach (var nota in notas)
                {
                    listNotas?.Add(nota); 
                }
            }
            catch 
            { 

            }
        }
    }

}
