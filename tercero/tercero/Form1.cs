using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tercero
{
    public partial class Form1 : Form
    {

        private string apiURL;
        private HttpClient httpClient;
        public Form1()
        {
            InitializeComponent();
            apiURL = "https://localhost:7110";
            httpClient = new HttpClient();
        }



        private async void btnIniciar_Click(object sender, EventArgs e)
        {
            loadingGIF.Visible = true;
            await Esperar();
            var nombre = txtInput.Text;
            try
            {
                var saludo = await ObtenerSaludo(nombre);
                MessageBox.Show(saludo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
            loadingGIF.Visible = false;
        }



        private async Task Esperar()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        private async Task<string> ObtenerSaludo(string nombre)
        {
            using (var respuesta = await httpClient.GetAsync($"{apiURL}/saludos2/{nombre}"))
            {
                respuesta.EnsureSuccessStatusCode();
                var saludo = await respuesta.Content.ReadAsStringAsync();
                return saludo;
            }
        }


    }
}
