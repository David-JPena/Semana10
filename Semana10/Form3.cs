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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ListarClientes();
            ListarPedidos();
        }
        private void ListarClientes()
        {
            try
            {
                using (NegociosEntities DB = new NegociosEntities())
                {
                    var query = from cliente in DB.CLIENTE
                                select new
                                {
                                    cliente.IdCliente,
                                    cliente.NombreCia,
                                    Pais = cliente.PAIS.NombrePais, // Acceder al nombre del país a través de la navegación de propiedades
                                    cliente.Direccion,
                                    cliente.Telefono
                                };

                    dataGridView1.DataSource = query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message);
            }
        }


        private void BuscarCliente(string nombreCliente)
        {
            try
            {
                using (NegociosEntities DB = new NegociosEntities())
                {
                    var query = from cliente in DB.CLIENTE
                                where cliente.NombreCia.Contains(nombreCliente)
                                select new
                                {
                                    cliente.IdCliente,
                                    cliente.NombreCia,
                                    Pais = cliente.PAIS.NombrePais,
                                    cliente.Direccion,
                                    cliente.Telefono
                                };

                    dataGridView1.DataSource = query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar cliente: " + ex.Message);
            }
        }


        private void ListarPedidos()
        {
            try
            {
                using (NegociosEntities DB = new NegociosEntities())
                {
                    var query = from pedido in DB.PEDIDO
                                select new
                                {
                                    pedido.IdPedido,
                                    pedido.FechaPedido,
                                    Cliente = pedido.CLIENTE.NombreCia,
                                    Empleado = pedido.EMPLEADO.NomEmpleado,
                                    pedido.CiudadDestinatario
                                };

                    dataGridView2.DataSource = query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los pedidos: " + ex.Message);
            }
        }

        private void FiltrarPedidosPorClienteYFecha(string nombreCliente, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (NegociosEntities DB = new NegociosEntities())
                {
                    var query = from pedido in DB.PEDIDO
                                where pedido.CLIENTE.NombreCia.Contains(nombreCliente)
                                    && pedido.FechaPedido >= fechaInicio
                                    && pedido.FechaPedido <= fechaFin
                                select new
                                {
                                    pedido.IdPedido,
                                    pedido.FechaPedido,
                                    Cliente = pedido.CLIENTE.NombreCia,
                                    Empleado = pedido.EMPLEADO.NomEmpleado,
                                    pedido.CiudadDestinatario
                                };

                    dataGridView2.DataSource = query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar los pedidos por cliente y fecha: " + ex.Message);
            }
        }



        private void textBoxCliente_TextChanged(object sender, EventArgs e)
        {
            string nombreCliente = textBoxCliente.Text.Trim();
            BuscarCliente(nombreCliente);
        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaInicio = dateTimePickerStartDate.Value.Date;
            DateTime fechaFin = dateTimePickerEndDate.Value.Date;
            string nombreCliente = textBoxCliente.Text.Trim();

            FiltrarPedidosPorClienteYFecha(nombreCliente, fechaInicio, fechaFin);
        }


        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaInicio = dateTimePickerStartDate.Value.Date;
            DateTime fechaFin = dateTimePickerEndDate.Value.Date;
            string nombreCliente = textBoxCliente.Text.Trim();

            FiltrarPedidosPorClienteYFecha(nombreCliente, fechaInicio, fechaFin);
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            ListarClientes();
            ListarPedidos();

            // Limpiar el campo de texto del filtro de cliente
            textBoxCliente.Text = "";

            // Restablecer las fechas de los DateTimePickers
            dateTimePickerStartDate.Value = DateTime.Today;
            dateTimePickerEndDate.Value = DateTime.Today;

            // Actualizar los DataGridViews
            dataGridView1.Refresh();
            dataGridView2.Refresh();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

