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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listaPedidos();
        }

        void listaPedidos()
        {
            try
            {
                using (NegociosEntities DB = new NegociosEntities())
                {
                    // Obtener la fecha seleccionada en el DateTimePicker
                    DateTime fechaSeleccionada = dateTimePickerStartDate.Value.Date;

                    // Obtener el texto ingresado en el TextBox para filtrar por nombre de cliente
                    string textoCliente = textBuscar.Text.Trim();

                    var query = from pedido in DB.PEDIDO
                                where pedido.FechaPedido > fechaSeleccionada // Filtrar por fecha
                                      && (string.IsNullOrEmpty(textoCliente) || pedido.CLIENTE.NombreCia.Contains(textoCliente)) // Filtrar por nombre de cliente
                                orderby pedido.FechaPedido ascending
                                select new
                                {
                                    pedido.IdPedido,
                                    pedido.FechaPedido,
                                    pedido.CLIENTE.NombreCia
                                };

                    dataGridView1.DataSource = query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBuscar_TextChanged(object sender, EventArgs e)
        {
            listaPedidos();
        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            listaPedidos();
        }
    }
}
