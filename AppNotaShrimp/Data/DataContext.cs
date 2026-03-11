using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppNotaShrimp.Models;

namespace AppNotaShrimp.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)  { }//Requerido para configurar la conexión a la base de datos
       
        public DbSet<Models.Notas> Notas { get; set; } //DBset para la tabla Notas
    }
}
