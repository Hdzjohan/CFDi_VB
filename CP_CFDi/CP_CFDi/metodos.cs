using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace CP_CFDi
{
    class metodos
    {

        //SqlConnection conn; 
      
        //SqlConnection conn = new SqlConnection(@"Data Source=PC05-PC\PC05;Initial Catalog=stc_db;User ID=sa;Password=Calaf1a;Trusted_Connection=False;");
      
       
        /*public SqlConnection conexion(string ipConexion)
        {
           this.conn = new SqlConnection(@"Data Source="+ipConexion+";Initial Catalog=stc_db;User ID=sa;Password=Calaf1a;Trusted_Connection=False;");
           
           return this.conn; 
        }*/

        /*METODO PARA LLENAR DATA GRID GENERAL*/
        public DataSet ConsulTab(string tabla, string condicion, string campo,SqlConnection conn)
        {
            
            DataSet datSet = new DataSet();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            // esta es la anterior consulta antes de los numeros exterior e interior String consulta = "select distinct Serie_bfol,Numero_cp as Numero,Nombre_fpg as FormaDePago,TotalCP_cp as Total, OtrasLineas_cp as Lineas,TotalCobrar_cp as TotalCobrar, Nombre_tenv as TipoEnvio, isnull(FechaHoraEntrega,'') as FechaHoraEntrega, isnull(Recibio_cp,'') as Recibio, isnull(SustituidaPor_cp,'') as SustituidaPor,  isnull(SustituyeA_cp,'') as SustituyeA, isnull(Numero_rec,'') as NumRecoleccion,  isnull(Estatus_cp,'') as Estatus,EstatusImpresion_cp as Impresion, isnull(Nombre_est,'') as Avance, Numero_Inf as NumInforme,Documento_cp as Documento, FechaActualizacion_cp as FechaActualizacion, Fecha_cp as Fecha, Id_rut_cp as NumRuta,  Nombre_rut as NombreRuta, CiudadOrigen, CiudadDestino, Remitente_cp as NumRemitente,RemitePlazaPlz_cp as NumPlaza, RemitenteNombre, RemitenteRFC, RemitentePlazaDireccion,RemitentePlazaColonia,RemitentePlazaCiudad,RemitentePlazaEstado, RemitentePlazaPais,RemitentePlazaCP,RemitentePlazaTelefono1,RecogerEn_cp,Destinatario_cp as NumDestinatario, DestinatarioPlazaPlz as NumPlaza, DestinatarioNombre, DestinatarioRFC,DestinatarioPlazaDireccion,  DestinatarioPlazaColonia,DestinatarioPlazaCiudad,DestinatarioPlazaEstado,DestinatarioPlazaPais,DestinatarioPlazaCP, DestinatarioPlazaTelefono1,EntregarEn_cp,correoDestinatario as CorreoDestinatario,Id_cp,  Leyenda_cp,Nombre_concxc,NumCtaPago_cp,Subtotal_cp,PorcentajeIVA_cp,IVA_cp,PorcentajeRETIVA_cp,RETETIVA_cp, CuotaTonelada_cp,ValorDeclarado_cp,observaciones_cp,NombreSuc,EstadoSucursal,Tel1,Tel2,correoRemitente as CorreoRemitente FROM " + tabla + condicion + " order by " + campo + " DESC";
            String consulta = "select * FROM " + tabla + condicion + " order by " + campo + " DESC";
            //MessageBox.Show(consulta+"\n"+conn);
            SqlCommand comando = new SqlCommand(consulta, conn);
            adaptador.SelectCommand = comando;
            if (conn.State == ConnectionState.Open) { conn.Close(); }
            conn.Open();
           
            
            adaptador.Fill(datSet, tabla);

            conn.Close();
            return datSet;

        }

        /*LLENAR GRID DE DESCRIPCIONES*/
        public DataSet descripciones(string tabla, string condicion, SqlConnection conn)
        {

            DataSet datSet = new DataSet();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            String consulta = "SELECT Numero_dtfcp as Numero, Nombre_cbul as Clase, Contienen_dtfcp as Se_dice_Contener,Peso_dtfcp as Peso,MtsCubicos_dtfcp as MTS, PesoEstimado_dtfcp as Peso_Estimado FROM " + tabla + condicion;
            //MessageBox.Show(consulta);
            SqlCommand comando = new SqlCommand(consulta, conn);
            adaptador.SelectCommand = comando;
            if (conn.State == ConnectionState.Open) { conn.Close(); }
            conn.Open();
            adaptador.Fill(datSet, tabla);
            conn.Close();
            return datSet;

        }


        /*LLENAR GRID DE CONCEPTOS*/
        public DataSet conceptos(string tabla, string campo, string condicion, SqlConnection conn)
        {

            DataSet datSet = new DataSet();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            String consulta = "SELECT Nombre_con as Nombre_concepto, Importe_ccp as Cobrar FROM " + tabla + condicion + " order by " + campo + " ASC";
            //MessageBox.Show(consulta);
            SqlCommand comando = new SqlCommand(consulta, conn);
            adaptador.SelectCommand = comando;
           
            if (conn.State == ConnectionState.Open) { conn.Close(); }
            conn.Open();
            adaptador.Fill(datSet, tabla);

            conn.Close();
            return datSet;

        }

        /*METODO QUE RECIBE UNA SENTENCIA SQL Y REGRESA EL VALOR LEIDO*/
        public SqlDataReader Buscar(string codConsulta, SqlConnection conn)
        {
            
            SqlCommand comando = new SqlCommand(codConsulta, conn);
            if (conn.State==ConnectionState.Open) { conn.Close(); }
            conn.Open();

            SqlDataReader lector = comando.ExecuteReader();
            
            return lector;
            //conn.Close();
        }

        //validar la estructura de el correo
        public bool validarEmail(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                { return true; }
                else
                { return false; }
            }
            else
            { return false; }
        }

        public int ABM(string instruccion, SqlConnection conn)
        {

            int respuesta = 1;

            SqlCommand comando = new SqlCommand(instruccion, conn);
            if (conn.State == ConnectionState.Open) { conn.Close(); }
            conn.Open();

            respuesta = comando.ExecuteNonQuery();

            conn.Close();
            return respuesta;


        }
    }
}
