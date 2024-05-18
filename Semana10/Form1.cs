using Semana10.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semana10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            using (NegociosEntities DB = new NegociosEntities())
            {
                var categorias = DB.CATEGORIA.Select(c => c.NombreCategoria).ToList();
                cboCategoria.DataSource = categorias;
            }
            listaProductos();
        }
        void listaProductos()
        {
            try
            {
                // Obtener la categoría seleccionada en el ComboBox
                string categoriaSeleccionada = cboCategoria.SelectedItem?.ToString();

                using (NegociosEntities DB = new NegociosEntities())
                {
                    var query = from producto in DB.PRODUCTO
                                join categoria in DB.CATEGORIA on producto.IdCategoria equals categoria.IdCategoria
                                where (string.IsNullOrEmpty(categoriaSeleccionada) || categoria.NombreCategoria == categoriaSeleccionada)
                                      && producto.PrecioUnidad > 10
                                orderby producto.NombreProducto ascending
                                select new
                                {
                                    producto.NombreProducto,
                                    producto.PrecioUnidad,
                                    categoria.NombreCategoria
                                };

                    dataGridView1.DataSource = query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaProductos();
        }
    }
}
