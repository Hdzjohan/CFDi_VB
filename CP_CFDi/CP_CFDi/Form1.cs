using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using tagcode.xml;
using System.IO;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Diagnostics;
using System.Net.Mail;
using System.Collections;
using System.Threading;
using System.Globalization;
using System.Net;
using System.Xml.Xsl;
using System.Net.NetworkInformation;
using System.Deployment.Application;
using Quricol.Barcode;
using System.Drawing.Imaging;
 



namespace CP_CFDi
{
    public partial class CP_CFDi : Form
    {   //Datos recibidos de delphi
        string datosDelphi = string.Empty;
        //VARIABLES Y OBJETOS
        
        metodos BD = new metodos();        
        Utils u = new Utils();
        StringBuilder strB = new StringBuilder();
        SqlConnection conn;
        
        
        public CP_CFDi(string datosDelphi)
        {
            InitializeComponent();            
            this.datosDelphi = datosDelphi;     
                 
        }

        //Llenar arraylist en vez de las labels
        ArrayList label = new ArrayList();
        ArrayList conf = new ArrayList();
        string viajFactCP,addenda = string.Empty;
        bool bandera = true;
        bool buscarCP = true;
        bool timbrar = true;
        ToolTip yourToolTip = new ToolTip();                
               
        //Variables para agregar al receptor del CFDi
        string rfc=string.Empty;
        string nombre=string.Empty;
        string pais=string.Empty;
        string direcc=string.Empty;
        string numExt = string.Empty;
        string numInt = string.Empty;
        string ciudad=string.Empty;
        string estado=string.Empty;
        string cp=string.Empty;
        string colonia=string.Empty;
        string correo = string.Empty;
        string leyenda = string.Empty;
        string retenedor = string.Empty;
        

        //Guardamos la ruta absoluta de la carpeta Temp
        string temp = System.IO.Path.GetTempPath();
        string resultado, xmlTimbrado, resTimbre;//variables para agregar el timbrado al XML
        string keyFile, keyPass, CertFile,CerFileLocal,KeyPassLocal;//Variables para sellar el XML .cer .key y contraseña.
        string ipConexion,BdConexion;
        string directorioRaiz = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());//Directorio root como C:\,F:\ etc. segun donde este instalado
        

        //Variables para convertir a letra el importe
        string Num2Text = "";
        string res = "";
        string dec = "";
        Int64 entero;
        int decimales;
        double nro;

        //Variables para guardar licencias y rutas desde la base de datos
        string liccenciaBuildCFDI = string.Empty;//licencia tagcode
        string liccenciaViewXMLCFDI = string.Empty;//licencia tagcode
        string strUser = string.Empty;//Usuario masteredi
        string strPass = string.Empty;//contrasena masteredi
        string directorioGuardarCP = string.Empty;//directorio en el servidor para guardar las Notas de Traslado
        string directorioMasteredi = string.Empty;//directorio en el servidor para guardar las Notas de Traslado en masteredi
        string smtp = string.Empty;//smtp de salida enviar correo
        string usuarioCorreo = string.Empty;//usuario del correo
        string contrasenaCorreo = string.Empty;//contrasena del correo
        string de = string.Empty;//Quien envia el correo
        string asuntoCorreo = string.Empty;//asunto del correo
        int puertoCorreo = 25;//puerto de salida correo
        string cuerpoMensajeCorreo = string.Empty;//Texto para enviar dentro del correo.
        int timbres;//cantidad de timbres contratados
        string ultiFechAlert;//Ultima fecha en la que se mando la alerta
        int limiteTimbres;//cantidad minima de timbres para enviar el correo
        string alertaTimbresCorreo = string.Empty;//correo para enviar la alerta de los timbres
        string alertaTimbresCorreoCpp = string.Empty;//con copia al correo para enviar la alerta de los timbres
        string nomenclatura = string.Empty;//nomenclatura empresa
        string razonSocial = string.Empty;//razon social empresa
        string direccion = string.Empty;//direccion de la empresa
        string numeroExterior = string.Empty;//Numero exterior empresa
        string coloniaE = string.Empty;//Colonia empresa
        string municipioE = string.Empty;//Municipio empresa
        string ciudadE = string.Empty;//Ciudad empresa
        string estadoE = string.Empty;//Estado empresa
        string paisE = string.Empty;//Pais empresa
        string cpE = string.Empty;//Codigo Postal empresa
        string tel1 = string.Empty;//Telefono 1 empresa
        string tel2 = string.Empty;//Telefono 2 empresa
        string rfcE = string.Empty;//rfc empresa

        
        
        //variables para obtener atributos del timbrado
        string certificadoTCA = string.Empty;
        string version = string.Empty;
        string uuid = string.Empty;
        string fechaTim,textoQr = string.Empty;
        string selloCFD = string.Empty, noCerti = string.Empty, selloSAT = string.Empty;
       

        //Declaracion de variables a utilizar para usar el web service de masteredi
        string strPath = string.Empty;
        string strResultado = string.Empty;
        string strError = string.Empty;
        string strEstatusUsuario = string.Empty;
        ArrayList estatusUsuario = new ArrayList();

        //variable en texto plano para agragar al miFolio.text el timbre y despues guardarlo como XML.
        string estructuraXML;

        //1.-llenar los data grid al cargar el formulario
        private void CP_CFDi_Load(object sender, EventArgs e)
        {
            /*XslCompiledTransform transformador = new XslCompiledTransform();
            transformador.Load(@"C:\Users\Admin\Desktop\Factura CFDI Visual 2010\XSLT\cadenaoriginal_3_2.xslt");
            transformador.Transform(temp + label[0] + label[1] + ".xml", temp + label[0] + label[1] + "CO.txt");*/

            yourToolTip.ToolTipTitle = "Desplegar configuración";
            yourToolTip.ToolTipIcon = ToolTipIcon.Info;
            yourToolTip.IsBalloon = true;             
            yourToolTip.SetToolTip(ayuda_lbl,"Información de la configuración para Timbrar facturas.");
                                          
            
            //MessageBox.Show(DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));
            if (Conectar())
            {                
                try
                {
                   
                    licenciasRutasUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hay problemas para agregar las licencias, usuarios y rutas en el metodo: licenciasRutasUsuarios()\n" + ex, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                buscarCarta();
                leerConfXML();
            }else 
            {
                //MessageBox.Show("Probelmas para conectar con el servidor\n"+servidor_lbl.Text, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            //MessageBox.Show("Prueba");         
                   
      }

        public Boolean Conectar()
        {
            int counter = 0;
            string line, rutaArchiServidor,rutaArchiBd;
            Boolean estatusConect = false;
            
            rutaArchiServidor = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) + @"STC R2\servidor_de_datos.txt";
            rutaArchiBd = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) + @"STC R2\base_de_datos.txt";
            
            // Read the file and display it line by line.
            if (File.Exists(rutaArchiServidor))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(rutaArchiServidor);
                while ((line = file.ReadLine()) != null)
                {
                    if (line != "")
                    {   
                        ipConexion = line;
                        /*string[] remplazando = ipConexion.Split(',');
                        ipConexion = remplazando[0];*/
                        servidor_lbl.Text = ipConexion;
                    }
                    counter++;
                }
                file.Close();

               
              
            }
            else
            {
                MessageBox.Show("El archivo 'servidor_de_datos.txt' no existe: " + Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) + @"STC R2\", "Archivo de configuración inicial", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                Application.Exit();
            }

            if (File.Exists(rutaArchiBd))
            {
                System.IO.StreamReader file2 = new System.IO.StreamReader(rutaArchiBd);
                while ((line = file2.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        BdConexion = line;

                    }
                    counter++;
                }
                file2.Close();
            }
            else
            {
                MessageBox.Show("El archivo 'base_de_datos.txt' no existe: " + Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) + @"STC R2\", "Archivo de configuración inicial", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                Application.Exit();
            }         
                                  
            
            try
            {   
               this.conn = new SqlConnection(@"Data Source=" + ipConexion + ";Initial Catalog="+BdConexion+";User ID=STC;Password=333.;Trusted_Connection=False;"); 
               this.conn.Open();
                
            }
            catch (Exception e)
            {
                //this.conn.Dispose();
                MessageBox.Show("Servidor: "+ipConexion+"\nBase de datos: "+BdConexion+"\n"+e.Message);
                Application.Exit();
            }          
            
            if (this.conn.State == ConnectionState.Open)
            {
                this.conn.Close();
                estatusConect = true;    
                
            }

            return estatusConect;

        }
       
        //Generar addenda;
        private void GenerarAddenda() {
                                  
            SqlDataReader ResConsul;
            string consulta = "select Estru_ade from Addenda where Id_ade=" + gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells["Id_ade_cp"].Value.ToString();
            
            metodos BD = new metodos();
            ResConsul = BD.Buscar(consulta,this.conn);
            
            if(ResConsul.Read()){
                addenda= ResConsul.GetString(0).ToString();
                
            }else
            {
                MessageBox.Show("Revisar permisos en la tabla Addenda");
            }
                               
                                   
            for (int i = 0; i < gridCartPort.Columns.Count;i++)
            {             
                addenda = addenda.Replace(gridCartPort.Columns[i].Name.ToString(), gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[gridCartPort.Columns[i].Name].Value.ToString());
            }                   
                    
                  
        }

        private void leerConfXML() {

            //si no existe el directorio donde esta la configuracion inicial
            if (!(System.IO.File.Exists(@"\STC R2\conf.xml")))
            {
                
                MessageBox.Show("El archivo de configuracion conf.xml no existe en STC R2 \n Se va a generar", "Archivo de configuración inicial", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = ("    ");
                using (XmlWriter writer = XmlWriter.Create(@"\STC R2\conf.xml", settings))
                {
                    // Write XML data.
                    writer.WriteStartElement("configuracion");//nodo principal
                    writer.WriteElementString("usuarioCorreoSaliente", "usuario@tudominio.com.mx");//posicion 0
                    writer.WriteElementString("contrasenaCorreoSaliente", "****");//posicion 1
                    writer.WriteElementString("puertoSMPT", "puerto");//posicion 2
                    writer.WriteElementString("SMPT", "dominio");//posicion 3
                    writer.WriteElementString("timbrar", "****");//posicion 4
                    writer.WriteElementString("impresoraEtiquetas", "Godex");//posicion 5
                    writer.WriteEndElement();
                    writer.Flush();
                }

                Application.Exit();
            }

            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(@"\STC R2\conf.xml");

            while (reader.Read())
            {
                reader.MoveToContent();
                if (reader.NodeType == System.Xml.XmlNodeType.Text)
                {                  
                   conf.Add(reader.Value);
                }
            }       
                    
        }
        //3.-llenar grid General
        public void llenarDataGrid()
        {
            try
            {
                DataSet datosGridGeneral = new DataSet();
                datosGridGeneral = BD.ConsulTab("CartaPorteCFDii", " WHERE Impresion='N' and Fecha_cp>'02-12-2013'", "Numero", this.conn);
                gridCartPort.DataSource = datosGridGeneral.Tables["CartaPorteCFDii"];

                //MessageBox.Show(datosGridGeneral.Tables["CartaPorteCFDi"].Rows[0]["EstruAddenda"].ToString());
               
                if (gridCartPort.RowCount != 0)
                {
                    llenarLabel();
                }
                else
                {
                    ocultarComponentes();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Problemas al llenar grid general " + ex, "Importante");
                this.Close();
            }
            

        }

        //4.-Llenar conceptos 
        public void llenarConceptos()
        {
            try
            {
                DataSet datosGridConceptos = new DataSet();
                datosGridConceptos = BD.conceptos("ConceptoCartaPorte_vst", "Id_con_ccp", " WHERE Id_cp_ccp=" + label[43]/*lblIdCarata.Text*/, this.conn);
                gridConceptos.DataSource = datosGridConceptos.Tables["ConceptoCartaPorte_vst"];                              

            }
            catch (Exception ex)
            {
                MessageBox.Show("GRID CONCEPTOS \n" + ex);
            }
        }

        //5.-llenar Grid de Descripciones
        public void llenarGridDescripciones()
        {
            try
            {
                DataSet datosDescripciones = new DataSet();
                datosDescripciones = BD.descripciones("DatosFleteCartaPorte_vst", " WHERE Id_cp_dtfcp=" + label[43]/*lblIdCarata.Text*/, this.conn);
                gridDescripciones.DataSource = datosDescripciones.Tables["DatosFleteCartaPorte_vst"];

            }
            catch (Exception ex)
            {
                MessageBox.Show("GRID DESCRIPCIONES" + ex);
            }

        }


        //7.-funcion para llenar Label y caja de texto
        private void llenarLabel()
        {
            try
            {
                
                if (bandera == true)
                {
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[0].Value.ToString());// lblSerie.Text = 0
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[1].Value.ToString());// lblNumero.Text = 1
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[1].Value.ToString());// txtFolio.Text = 2
                    txtFolio.Text=gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[1].Value.ToString();

                   
                    //lleno algunos datos del remitente
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[23].Value.ToString());// lblIdClienteRemitente.Text = 3
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[24].Value.ToString());// lblPlazaRemitente.Text =  4
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[25].Value.ToString());// lblNombrePlazaRemitente.Text = 5
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[26].Value.ToString());// lblRFCRemitente.Text = 6
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[27].Value.ToString());// lblDireccionRemitente.Text = 7
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[28].Value.ToString());// lblColoniaRemitente.Text = 8
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[29].Value.ToString());// lblCiudadRemitente.Text = 9
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[30].Value.ToString());// lblEstadoRemitente.Text = 10
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[31].Value.ToString());// lblPaisRemitente.Text = 11
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[32].Value.ToString());// lblCPRemitente.Text = 12
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[33].Value.ToString());// lblTelefono1Remitente.Text = 13
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[34].Value.ToString());// lblRecoger.Text = 14


                    //lleno algunos datos del destinatario
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[35].Value.ToString());// lblIdClienteDestinatario.Text = 15
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[36].Value.ToString());// lblPlazaDestinatario.Text = 16
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[37].Value.ToString());// lblNombrePlazaDestinatario.Text = 17
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[38].Value.ToString());// lblRFCDestinatario.Text = 18
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[39].Value.ToString());// lblDiereccionDestinatario.Text = 19 
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[40].Value.ToString());// lblColoniaDestinatario.Text = 20
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[41].Value.ToString());// lblCiudadDestinatario.Text = 21
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[42].Value.ToString());// lblEstadoDestinatario.Text = 22
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[43].Value.ToString());// lblPaisDestinatario.Text = 23
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[44].Value.ToString());// lblCPDestinatario.Text = 24
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[45].Value.ToString());// lblTelefono1Destinatario.Text = 25
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[46].Value.ToString());// lblEntrega.Text = 26

                    //llenar otros datos
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[2].Value.ToString());// lblPago.Text = 27
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[3].Value.ToString());// lblTotal.Text = 28
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[3].Value.ToString());// lblTotalCartaPorte.Text = 29
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[4].Value.ToString());// lblOtrasLineas.Text = 30
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[5].Value.ToString());// lblTotalCobrar.Text = 31
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[16].Value.ToString());// lblDocumento.Text = 32
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[18].Value.ToString());// lblFecha.Text = 33
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[57].Value.ToString());// lblValor1.Text = 34
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[58].Value.ToString());// lblValor2.Text = 35
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[59].Value.ToString());// lblObservacines.Text = 36 
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[52].Value.ToString());// lblSubtotal.Text = 37
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[53].Value.ToString());// lblIvaActual.Text = 38
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[54].Value.ToString());// lblIva.Text = 39
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[55].Value.ToString());// lblPorcentajeR.Text = 40 
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[56].Value.ToString());// lblRetIva.Text =  41

                    if (gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[60].Value.ToString().Contains("CHIHUAHUA"))
                    {
                        label.Add("NVO. CASAS GRANDES CHI., " + gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[61].Value.ToString());// lblDireccion.Text = 42
                    }
                    else if (gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[60].Value.ToString().Contains("TLAQUE"))
                    {
                        label.Add("SUC. TLAQUEPAQUE, TLAQ., " + gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[61].Value.ToString());// lblDireccion.Text = 42
                    }
                    else if (gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[60].Value.ToString().Contains("ALAMO"))
                    {
                        label.Add("SUC. ALAMO GDL, " + gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[61].Value.ToString());// lblDireccion.Text = 42
                    }
                    else if (gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[60].Value.ToString().Contains("REYES"))
                    {
                        label.Add("SUC. REYES HEROLES GDL, " + gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[61].Value.ToString());// lblDireccion.Text = 42
                    }
                    else if (gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[60].Value.ToString().Contains("MEXICO"))
                    {
                        label.Add("TLALNEPANTLA, " + gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[61].Value.ToString());// lblDireccion.Text = 42
                    }
                    else
                    {
                        label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[60].Value.ToString() + ", " + gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[61].Value.ToString());// lblDireccion.Text = 42
                    }

                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[48].Value.ToString());// lblIdCarata.Text = 43 
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[49].Value.ToString());// lblLeyenda.Text = 44 
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[50].Value.ToString());// lblMetodoPago.Text = 45  
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[51].Value.ToString());// lblTerminacion.Text = 46
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[62].Value.ToString());// lblTelefono1.Text = 47
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[63].Value.ToString());// lblTelefono2.Text = 48

                    //Llenar label para crear la ruta donde se gurdara el xml
                    try
                    {
                        DateTime amo = Convert.ToDateTime(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[18].Value.ToString());
                        DateTime mes = Convert.ToDateTime(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[18].Value.ToString());

                        label.Add(amo.ToString("yyyy"));// lblamo.Text = 49
                        label.Add(mes.ToString("MM"));// lblMes.Text = 50                        
                    }catch (Exception e)
                    {
                        MessageBox.Show("Formato de fecha incorrecto \n"+e);
                    }                  
                    

                    //Paso el importe a letras y despues lo  asigno a la label
                    enletras(Convert.ToString(label[28]));
                    label.Add(res);// lblImportLetra.Text = 51 

                    //llenar el arraylist de la carta porte con los nuevos campos numExt numInt dele emisor y receptor
                 
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[65].Value.ToString());// lblDireccionRemitente.Text = 52 NumExt
                    if (label[52].ToString() != "" && label[52].ToString() != "N/A") { label[52] = " NumExt." + label[52].ToString(); } else { label[52] = ""; }
                                       
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[66].Value.ToString());// lblDireccionRemitente.Text = 53 NumInt
                    if (label[53].ToString() != "" && label[53].ToString() != "N/A") { label[53] = " NumInt." + label[53].ToString(); } else { label[53] = ""; }
                                                         
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[67].Value.ToString());// lblDiereccionDestinatario.Text = 54 NumExt 
                    if (label[54].ToString() != "" && label[54].ToString() != "N/A") { label[54] = " NumExt." + label[54].ToString(); } else { label[54] = ""; }
                                                            
                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[68].Value.ToString());// lblDiereccionDestinatario.Text = 55 NumInt 
                    if (label[55].ToString() != "" && label[55].ToString() != "N/A") { label[55] = " NumInt." + label[55].ToString(); } else { label[55] = ""; }
                                       
                    //Validar el retenedor
                    if (Convert.ToString(label[27])/*lblPago.Text*/ == "POR COBRAR")
                    {
                        //DESTINATARIO
                        retenedor = gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[37].Value.ToString();//lblRetenedor.Text
                        //label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[37].Value.ToString()); // 

                        nombre = Convert.ToString(label[17]);// lblNombrePlazaDestinatario.Text
                        rfc = Convert.ToString(label[18]);// lblRFCDestinatario.Text
                        direcc = Convert.ToString(label[19]);// lblDiereccionDestinatario.Text
                        numExt = gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[67].Value.ToString();
                        numInt = gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[68].Value.ToString();
                        colonia = Convert.ToString(label[20]);// lblColoniaDestinatario.Text
                        ciudad = Convert.ToString(label[21]);// lblCiudadDestinatario.Text
                        estado = Convert.ToString(label[22]);// lblEstadoDestinatario.Text
                        pais = Convert.ToString(label[23]);// lblPaisDestinatario.Text
                        cp = Convert.ToString(label[24]);// lblCPDestinatario.Text

                       
                        if (Convert.ToString(label[6])/*lblRFCRemitente.Text*/ == "XAXX010101000") { label[6]/*lblRFCRemitente.Text*/ = ""; }
                        //txtCorreo.Text 
                        correo = gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[47].Value.ToString();

                        if (correo == "ESCRIBE AQUI EL CORREO")
                        {
                            txtCorreo.Text = correo;
                        }
                        else
                        {
                            txtCorreo.Text = correo.ToLower();
                        }
                    }
                    else
                    {   //REMITENTE
                        retenedor = gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[25].Value.ToString();//lblRetenedor.Text
                        //label[52] = gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[25].Value.ToString();//  = 

                        nombre = Convert.ToString(label[5]);// lblNombrePlazaRemitente.Text
                        rfc = Convert.ToString(label[6]);// lblRFCRemitente.Text
                        direcc = Convert.ToString(label[7]);// lblDireccionRemitente.Text
                        numExt = gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[65].Value.ToString();
                        numInt = gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[66].Value.ToString();
                        colonia = Convert.ToString(label[8]);// lblColoniaRemitente.Text
                        ciudad = Convert.ToString(label[9]);// lblCiudadRemitente.Text
                        estado = Convert.ToString(label[10]);// lblEstadoRemitente.Text
                        pais = Convert.ToString(label[11]);// lblPaisRemitente.Text
                        cp = Convert.ToString(label[12]);// lblCPRemitente.Text
                        
                        
                        if (Convert.ToString(label[18])/*lblRFCDestinatario.Text*/ == "XAXX010101000") { label[18]/*lblRFCDestinatario.Text*/ = ""; }
                        correo = gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[64].Value.ToString();

                        if (correo == "ESCRIBE AQUI EL CORREO")
                        {
                            txtCorreo.Text = correo;
                        }
                        else
                        {
                            txtCorreo.Text = correo.ToLower();
                        }

                    }
                   

                    label.Add(gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells[57].Value.ToString());// lblValor1.Text = 34
                    //Validar la cuota por tonelada
                    if (Convert.ToString(label[34])/*lblValor1.Text*/ == "0.00" || Convert.ToString(label[34])/*lblValor1.Text*/ == "")
                    {
                        label[34] = "PRECIO CONVENIDO";//lblValor1.Text

                    }
                    
                    bandera = false;
                    llenarConceptos();
                    llenarGridDescripciones();
                    
                    if (timbrar == true && !datosDelphi.Equals("197035"))
                    {
                        validarRegistro();
                    }

                    //Si el valor de la addenda es diferente de vacio mandamos llenar con los datos respectivos para incluirla al XML
                    if (gridCartPort.Rows[gridCartPort.CurrentRow.Index].Cells["Id_ade_cp"].Value.ToString() != "")
                    {
                        GenerarAddenda();
                    }                           
                   
                }
            }catch (Exception e)
            {
                MessageBox.Show("Problemas para llenar las label." + e, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }
        
        //?.- Valido si ya esta registrada la carta porte en la tabla CFDi, si esta solo genero CB.png
        private void validarRegistro()
        {       
           
                SqlDataReader ResConsu;
                string consulta2 = "select idCFDi,codigoBD from CFDi where idCFDi=" + datosDelphi;

                metodos BD = new metodos();
                ResConsu = BD.Buscar(consulta2, this.conn);
               
                if (ResConsu.Read())
                {
                    try
                    {
                       
                            //MessageBox.Show(ResConsu.GetValue(ResConsu.GetOrdinal("codigoBD")).ToString());
                        

                        /*if (File.Exists(@directorioGuardarCP + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".xml"))
                        {*/
                            //LicenciasBuildCFDI lic1 = new LicenciasBuildCFDI();
                            //lic1.Licencia(liccenciaBuildCFDI);                           
                            //QRCode.GeneratePng(@temp + label[0] + label[1] + "Qc.jpg","asdfs",5,0, ErrorCorrectionLevel.StandardQuality);
                        pictureBox1.Image = QRCode.GetBitmap(ResConsu.GetValue(ResConsu.GetOrdinal("codigoBD")).ToString(),0,5, ErrorCorrectionLevel.HighQuality);
                        pictureBox1.Image.Save(@temp + label[0] + label[1] + ".jpg", ImageFormat.Jpeg);    
                        
                        
                        //QRCode.GenerateBitmap(@"\STC R2\Qc.jpeg","asdfs",5,0, ErrorCorrectionLevel.HighQuality);
                            //MessageBox.Show(temp);
                        //Crear y leer el codigo qr                            
                        //    u.CrearQR(@directorioGuardarCP + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".xml", temp + label[0] + label[1] + ".jpg");
                            
                            

                        if (Directory.Exists("C:\\WINDOWS\\Temp\\"))
                            {
                                try
                                {
                                    pictureBox1.Image.Save("C:\\WINDOWS\\Temp\\" + label[0] + label[1] + ".jpg", ImageFormat.Jpeg);
                                    //QRCode.GeneratePng("C:\\WINDOWS\\Temp\\" + label[0] + label[1] + ".jpg", ResConsu.GetValue(ResConsu.GetOrdinal("codigoBD")).ToString(),ErrorCorrectionLevel.StandardQuality);
                                    //u.CrearQR(@directorioGuardarCP + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".xml", "C:\\WINDOWS\\Temp\\" + label[0] + label[1] + ".jpg");
                                }
                                catch (Exception jpg)
                                {

                                    MessageBox.Show("No se puede guardar el jpg en C:\\WINDOWS\\Temp\\ ruta escrita manualmente\n" + jpg);
                                }

                            }
                            
                        /*}
                        else
                        {
                            MessageBox.Show("El xml no existe en la rutaaa\n" + @directorioGuardarCP + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".xml");
                        }*/
                    }
                    finally
                    {
                        Application.Exit();
                    }

                }              
               
        }

        //8.-Llamar a la funcion llenarDatosEmisorReceptor() para llenar los datos del emisor y receptor en el vento click en boton timbrar
        private void btnTimbrar_Click(object sender, EventArgs e)
        {
            if (btnTimbrar.Text == "CERRAR SIN TIMBRAR")
            {
                Application.Exit();
            }
            else
            {
                viajeFacturaCP();
                if (groupBox3.Visible == true)
                {
                    if (Convert.ToString(conf[4]) != "****")
                    {
                        if (Convert.ToString(conf[4]) == txtContrasena.Text || txtContrasena.Text == strPass)
                        {
                            //llamar al timbrado 
                            llamarTimbrado();                       
                        
                        }else
                        {
                            MessageBox.Show("!Contraseña no valida¡", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtContrasena.Focus();
                            txtContrasena.Text = "";
                        }

                    }else if (txtContrasena.Text == strPass)
                    {
                        //llamar al timbrado 
                        llamarTimbrado();
                    }else
                    {
                        MessageBox.Show("!Contraseña no valida¡", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtContrasena.Focus();
                        txtContrasena.Text = "";
                    }
                }else if (timbrar == true)
                {
                    //llamar timbre si no esta activa la busqueda de la carta porte
                    llamarTimbrado();

                }else 
                {
                    txtSerie.Focus();                
                    tamano = this.Size.Height;
                    this.Size = new Size(this.Width, 380);
                    groupBox3.Visible = true;
                    buscarCP = false;
                    MessageBox.Show("! Debe buscar una CP e introducir la contraseña para poder timbrar ¡", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }         
           
        }

        public void llamarTimbrado() {
                string[] remplazando = directorioGuardarCP.Split('\\');
            
                string rutaFTP = "";
                for (int contador = 0; contador <= remplazando.Length-1;contador++ )
                {
                    if (remplazando[contador] != "")
                    {
                      rutaFTP +="/"+remplazando[contador];
                    }
                
                }
                rutaFTP = "ftp:/" + rutaFTP + "/" + label[0] + "/" + label[49] + "/" + label[50]+"/";
                
               if (System.IO.File.Exists(@directorioGuardarCP + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".xml"))
               {
                   if (!rutaFTP.Contains("Prueba"))
                   {
                       MessageBox.Show("Esta carta porte " + label[0] + "-" + label[1] + " ya tiene un timbre \n" + "Descargarla en: \n" +  rutaFTP + label[0] + "/" + label[49] + "/" + label[50] + "/", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       this.Close();
                   }else
                   {
                       MessageBox.Show("No se abre el pdf porque la ruta es:\n" + rutaFTP);
                       this.Close();                       
                   }       
                   
               }else            
               {
                  if (txtFolio.Text == "4890") { MessageBox.Show("Esta carta " + label[0] + txtFolio.Text + " no debe ser timbrada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);  }
                  else
                  {                                     
                           /***********SQL PARA VALIDAR QUE NO EXISTE EL TIMBRE EN LA BASE DE DATOS**************/
                      SqlDataReader ResConsul;
                      string consulta = " select serie,folio,uuid from CFDi where idCFDi=" + Convert.ToInt32(label[43]);
                   
                      metodos BD = new metodos();
                      ResConsul = BD.Buscar(consulta, this.conn);
                    
                      if (ResConsul.Read())
                      {                   
                          MessageBox.Show("Esta carta porte " + ResConsul.GetString(0).ToString() + "-" + ResConsul.GetInt32(1).ToString() + " ya tiene un timbre" + "\nUUID=" + ResConsul.GetString(2).ToString()+"\nY el XML no existe", "TIMBRAR XML", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                      }else
                      {                        
                          if (chbXml.Checked == true || chbPdf.Checked == true)
                          {
                              if (!BD.validarEmail(txtCorreo.Text))
                              {
                                  MessageBox.Show("NO ES UN CORREO VALIDO \n " + txtCorreo.Text, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                  if (txtCorreo.Text == "ESCRIBE AQUI EL CORREO")
                                  {
                                      txtCorreo.Text = "";
                                  }
                                  txtCorreo.Focus();
                              }else
                              {
                                  btnTimbrar.Visible = false;
                                  limpiarVariablesTimbrado();
                                  llenarDatosEmisorReceptor();
                              }
                          }else
                          {
                              if (Convert.ToString(label[28])/*lblTotal.Text*/== "0.00")
                              {
                                  MessageBox.Show("El total y los conceptos deben ser mayor a 0\n" + strError, "Proceso de timbrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                  this.Close();
                              }else
                              {
                                  btnTimbrar.Visible = false;
                                  limpiarVariablesTimbrado();
                                  llenarDatosEmisorReceptor();
                              }
                          }                        
                      }  
                       
                  }//cerrar la validacion de datos delphi
                }//cerrar la validacion del fichero       
        
        }

       
        //9.-Funcion para llenar los datos utilizando las label posteriormente llamo a la funcion timbrar()
        private void llenarDatosEmisorReceptor()
        {
            try
            {
                //establecemos el numero de serie de la licencia del SDK para poder hacer uso de ella.
                //En caso de no asignara, el SDK funcionará con las restricciones de version DEMO,
                //las cuales permite hacer unicamente CFDI con el RFC Genérico XAXX010101000
                LicenciasBuildCFDI lic1 = new LicenciasBuildCFDI();
                DateTime FechTimbre=DateTime.Now;
                lic1.Licencia(liccenciaBuildCFDI);
                
                
                if (cbFechTimb.Checked == true && cbFechTimb.Visible == true)
                {   
                    //Timbro con la fecha actual no con la que se genero la carta porte
                    FechTimbre = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));
                    
                }else 
                {
                    //Timbrar con la fecha en la que se genera la carta porte el dato es tomado desde la base de datos
                    FechTimbre = Convert.ToDateTime(label[33]);                    
                }
                
                CFDI.Comprobante c = new CFDI.Comprobante(FechTimbre, Convert.ToString(label[44])/*lblMetodoPago.Text*/, CFDI.Comprobante.opTipoDeComprobante.ingreso, Convert.ToDouble(label[37]/*lblSubtotal.Text*/), Convert.ToDouble(label[28]/*lblTotal.Text*/), Convert.ToString(label[45])/*lblMetodoPago.Text*/, Convert.ToString(label[42])/*lblDireccion.Text*/ + "  Tels.:" + Convert.ToString(label[47])/*lblTelefono1.Text*/ + ", " + Convert.ToString(label[48])/*lblTelefono2.Text*/, Convert.ToString(label[1])/*lblNumero.Text*/, Convert.ToString(label[0])/*lblSerie.Text*/, "", default(DateTime),-1 /*lblTotal.Text*/, "PESOS MEXICANOS", "1", Convert.ToString(label[46])/*lblTerminacion.Text*/, Convert.ToString(label[44])/*lblLeyenda.Text*/, -1, "");    
                      
                //Establecemos datos del Emisor
                //esta variable con esta funcion nos dejará indicar el regimen fiscal de la empresa emisora
                CFDI.Emisor.RegimenFiscal reg = new CFDI.Emisor.RegimenFiscal();
                reg.AgregarRegimen(leyenda);

                //esta funcion nos permite indicar la información obligatoria para el emisor
                CFDI.Emisor em = new CFDI.Emisor(rfcE, reg, razonSocial);

                //con esta variable establecemos los datos del emisor
                CFDI.Emisor.DomicilioFiscal df = new CFDI.Emisor.DomicilioFiscal(direccion,municipioE , estadoE, paisE, cpE, numeroExterior, "N/A", coloniaE);

                //con esta funcion agregamos los datos fiscales del emisor
                em.EstablecerDomicilioFiscal(df);                

                //Establecemos datos del Receptor
                //esta funcion nos permite indicar la información obligatoria para el emisor
                CFDI.Receptor r = new CFDI.Receptor(rfc, nombre);

                //con esta variable establecemos los datos del receptor
                CFDI.Receptor.Domicilio d = new CFDI.Receptor.Domicilio(pais, direcc,numExt ,numInt, colonia, ciudad, "", null, estado, cp);
                r.EstablecerDomicilio(d);
                                
                //Establecemos Conceptos
                //Podemos crear un ciclo for ó cualquier otro tipo de ciclo para agregar la cantidad de conceptos necesarios.
                //Aquí solo aclaramos un poco la idea de como hay que hacerlo.
                CFDI.Conceptos co = new CFDI.Conceptos();         
                                               
                for (int fila = 0; fila <gridConceptos.RowCount-1; fila++)
                {
                    if (gridConceptos[1, fila].Value.ToString() != "0.00")
                        {                          
                            co.AgregarConcepto(1, "N/A", gridConceptos[0, fila].Value.ToString(), Convert.ToDouble(gridConceptos[1, fila].Value.ToString()), Convert.ToDouble(gridConceptos[1, fila].Value.ToString()));
                        }                   
                }               
                
                //Establecemos Impuestos
                //aquí declaramos un impuesto IVA trasladado con tasa de 16%
                CFDI.Impuestos im = new CFDI.Impuestos();
                
                if (Convert.ToDouble(label[39])/*lblRetIva.Text*/ >0)
                {
                    im.AgregarImpuestosTrasladados(CFDI.Impuestos.opTraslado.IVA, Convert.ToDouble(label[38]/*lblIvaActual.Text*/), Convert.ToDouble(label[39]/*lblIva.Text*/));
                }
                if (Convert.ToDouble(label[41])/*lblRetIva.Text*/>0)
                {
                    im.AgregarImpuestosRetenidos(CFDI.Impuestos.opRetencion.IVA, Convert.ToDouble(label[41]/*lblRetIva.Text*/));
                }
                /*******************************TOMAR LA RUTA DE .CER Y .KEY DE LA BASE DE DATOS**********************************/

                //ubicacion para guardar el XML en la aplicacion de masteredi y posteriormente timbrarla 
                SqlDataReader ResConsul;
                string consulta = "select * from Certifcado where Id_cer=(SELECT MAX(Id_cer) from Certifcado);";
                metodos BD = new metodos();
                ResConsul = BD.Buscar(consulta, this.conn);

                if (ResConsul.Read())
                {   
                    //Tomando la Ruta de los archivo .cer y .key y la clave privada desde la base de datos  
                    if (File.Exists(CerFileLocal))
                    {
                        CertFile = CerFileLocal;
                    }
                    else
                    {
                        CertFile = @ResConsul.GetString(1).ToString();
                    }

                    if (File.Exists(KeyPassLocal))
                    {
                        keyFile = KeyPassLocal;
                    }
                    else
                    {
                        keyFile = @ResConsul.GetString(2).ToString();
                    }

                    keyPass = ResConsul.GetString(3).ToString();
                }

                //Creamos el XML con todos los datos llenados especificando certificado, llave privada, contraseña y ruta de destino
                CFDI sellar = new CFDI();
                                     
                //Tambien lo guardo en un archivo TXT de manera local en la carperta temporal para despues timbrarlo. 
                sellar.CrearXML(CertFile, keyFile, keyPass, c, em, r, co, im, @temp + nomenclatura + c.serie + c.folio + ".txt");
                sellar.CrearXML(CertFile, keyFile, keyPass, c, em, r, co, im, @temp + nomenclatura + c.serie + c.folio + ".xml");

                //OBTENER CADENA ORIGINAL DEL CFD
                /*XslCompiledTransform transformador = new XslCompiledTransform();
                transformador.Load(@"C:\Users\Admin\Desktop\Factura CFDI Visual 2010\XSLT\cadenaoriginal_3_2.xslt");
                transformador.Transform(temp + label[0] + label[1] + ".xml", temp + label[0] + label[1] + "CO.txt");
                */
                   
                //Enviamos un mensaje al usuario para informar que se ha creado el xml
                //MessageBox.Show("¡Se ha creado el XML con éxito!");
                Timbrar();
                        
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error para sellar el xml\n" + ex);
            }


        }

        //10.-Funcion para pasarle el usuario, contraseña y el xml a timbrar por masteredi y guardo la respuesta en una variable para despues agregarla al xml
        private void Timbrar()
        {

            try
            {
                //Ruta por default Debug donde se encuentra el archivo XML version 3.0 a timbrar
                //strPath = Application.StartupPath;
                //Usuario asignado para ingresar al servicio web de pruebas
                //strUser = "MFW-7346";

                //Password asignado para ingresar al servicio web de pruebas
                //strPass = "ADMIN123*";
                //MessageBox.Show(strUser+strPass);
                
                //Se crea el objeto para cargar el archivo XML a procesar
                XmlDocument xmlCfd = new XmlDocument();                
               
                //Se carga el archivo xml a timbrar
                xmlCfd.Load(temp + nomenclatura + label[0] + label[1] + ".xml");

                //Se genera la instancia del servicio, que se agrego como referencia web desde el proyecto
                WSTimbrado.TimbradoCFDServiceExternal wsTimbre = new WSTimbrado.TimbradoCFDServiceExternal();              

                //Obtener el estatus del usuario,saldo,cantidad timbres,rfc,numero cliente
                strEstatusUsuario = wsTimbre.GetEstatuUser(strUser, strPass);

                string[] split = strEstatusUsuario.Split(new Char[] { '|', ' ', ',', '.', ':', '\t' });
                int contador = 0;

                foreach (string s in split)
                {
                    if (s.Trim() != "")
                    {
                        estatusUsuario.Add(s);
                    }
                    contador++;

                }
                
                //Se utiliza el método TimbradoCFDStrXML, en donde se le pasa el xml como cadena de string, junto con el usuario y password
                //asignado por MasterEdi
                //El resultado es el XML timbrado en formato de string
               
                strResultado = wsTimbre.TimbradoCFDStrXML(xmlCfd.InnerXml, strUser, strPass);
               
                //strResultado = wsTimbre.TimbradoCFDStrXML(xmlCfd.InnerXml, "MFW-7346", "ADMIN123*");
                /****************************GUARDAR EL TIMBRE EN UN ARCHIVO TXT*********************************************/
                XmlDocument xm = new XmlDocument();
                xm.LoadXml(string.Format(strResultado));

                //GUARDAR EL TIMBRE EN LA CARPETA TEMPORAL EN FORMATO .txt 
                xm.Save(temp + nomenclatura + label[0] + label[1] + "timbre.txt");        
                                               

                /*****************************FIN DE GUARDAR EL TIMBRE*****************************/


                /*****************************LEER EL TIMBRE CUANDO ES TIMBRADO POR SEGUNDA VEZ PARA AGREGARLO AL XML*****************************/
                StreamReader timbre = new StreamReader(temp + nomenclatura + label[0] + label[1] + "timbre.txt");
                while (timbre.Read() > 0)
                {
                    xmlTimbrado = timbre.ReadLine();
                    if (xmlTimbrado.Contains("tfd")) { resTimbre = xmlTimbrado;}
                } 
                timbre.Close();
                /*****************************FIN LEER EL TIMBRE PARA AGREGARLO AL XML*****************************/

                //Se busca si existo algun mensaje de error al generar el timbrado
                XmlNodeList xmlnode = xm.GetElementsByTagName("Mensaje", xm.DocumentElement.NamespaceURI);

                //Si existe entra a ver cual es el mensaje de error
                if (xmlnode.Count > 0)
                {                    
                    strError = xmlnode[0].InnerText;
                    if (resTimbre.Contains("<tfd"))
                    {              
                        MessageBox.Show("¡El folio " + nomenclatura + label[0] + label[1] + " ya tiene un timbre!", "Informe CFDI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try
                        {
                            if (timbres <= limiteTimbres && Convert.ToDateTime(ultiFechAlert) < Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")))
                            {                               
                                enviarAlertaTimbres();//enviar correo cuando la cantidad de timbres es menor o igual a 200
                            }
                        }catch (Exception error)
                        {
                            MessageBox.Show("No se puede enviar la alerta al correo "+alertaTimbresCorreo+" de timbres" + error, "Alerta de timbres", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        finally
                        {
                            string archivo=temp + nomenclatura + label[0] + label[1] + "timbre.txt";
                            if (!File.Exists(archivo))
                            { 
                                actualizarTimbres(); 
                            }//actualizar timbre si no existe uno en la carpeta temporal
                            
                            //Funcion para agregar el timbrado
                            agregarTimbre();
                        }                             
                                               
                    }else
                    {
                        MessageBox.Show("Existe un error al timbrar el XML:\n"+strError, "Proceso de timbrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }

                //Si la variable strError esta vacía es que se genero el timbre sin problemas y regresa el tag del timbre Fiscal
                if (strError == string.Empty)
                {                    
                    //Aqui concateno "<" la primera ves que es timbrado el XML
                    StreamReader timbre2 = new StreamReader(temp + nomenclatura + label[0] + label[1] + "timbre.txt");
                    while (timbre2.Read() > 0)
                    {
                        xmlTimbrado = timbre2.ReadLine();
                        if (xmlTimbrado.Contains("tfd"))
                        {
                            resTimbre = "<" + xmlTimbrado;
                        }

                    } 
                    timbre.Close();
                    //MessageBox.Show("¡El folio " + nomenclatura + label[0] + label[1] + " fue timbrado con exito!", "Informe CFDI", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    actualizarTimbres();
                    try
                    {
                        if (timbres <= limiteTimbres && Convert.ToDateTime(ultiFechAlert) < Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")))
                        {                           
                            enviarAlertaTimbres();//enviar correo cuando la cantidad de timbres es menor o igual a 200
                        }
                    }catch (Exception error)
                    {
                        MessageBox.Show("No se puede enviar la alerta al correo " + alertaTimbresCorreo + " de timbres" + error, "Alerta de timbres", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }finally 
                    {
                        //Funcion para agregar el timbrado
                        agregarTimbre();                    
                    }                          
                }

            }catch (Exception ex)
            {
                MessageBox.Show("Problemas para usar el WebService de MASTEREDI \n\n" + ex, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       }
        
        //Actualizar timbres en la base de datos
        private void actualizarTimbres() 
        {
            metodos update = new metodos();
            int res = -1;

            /***********SQL PARA ACUTUALIZAR LOS TIMBRES**************/
                    
            string actualizar = "exec Registrar_TimbresDisponibles_sp " + estatusUsuario[1].ToString();
            res = update.ABM(actualizar, this.conn);
            if (res == 1)
            {
                //MessageBox.Show("Se ha modificado un registro correctamente.", "Bajas", MessageBoxButtons.OK, MessageBoxIcon.Information);
             
            }
            else
            {
                MessageBox.Show("Problemas para actualizar la cantidad de timbres", "Cantidad de timbres", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        
        }

        //Enviar la alerta de limites al correo predertemido
        private void enviarAlertaTimbres() 
        {
            try
            {
                //Configuración del Mensaje
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(smtp);
                
                //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                mail.From = new MailAddress(usuarioCorreo, de, Encoding.UTF8);

                //Aquí ponemos el asunto del correo
                mail.Subject = "Cantidad de timbres disponibles "+ timbres;

                //Aquí ponemos el mensaje que incluirá el correo
                mail.Body = "Adquirir lo antes posible mas timbres con MASTEREDI TEL: (0155) 26 15 55 55  \r \nEste es un aviso desde la aplicacion CFDi en C#";

                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                mail.To.Add(alertaTimbresCorreo);
                mail.CC.Add(alertaTimbresCorreoCpp);

                //Configuracion del SMTP
                SmtpServer.Port = puertoCorreo; //Puerto que utiliza Gmail para sus servicios

                //Especificamos las credenciales con las que enviaremos el mail
                SmtpServer.Credentials = new System.Net.NetworkCredential(usuarioCorreo, contrasenaCorreo);
                //SmtpServer.UseDefaultCredentials = true;

                SmtpServer.EnableSsl = false;
                SmtpServer.Send(mail);
               
                metodos update = new metodos();
                int res = -1;

                /***********SQL PARA ACUTUALIZAR LA ULTIMA FECHA QUE SE ENVIO LA ALERTA**************/
               
                string actualizar = "update licenciasRutasUsuariosCFDi set ultiFechAler=GETDATE();";
                res = update.ABM(actualizar, this.conn);
                if (res == 1)
                {
                    //MessageBox.Show("Se ha modificado un registro correctamente.", "Bajas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Problemas para actualizar la cantidad de timbres", "Cantidad de timbres", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }catch (Exception ex)
            {
                MessageBox.Show("No se pudo enviar el correo \n" + ex);
            }


        }
      
        //11.-Funcion que agrega el timbre al archivo TXT"XML" y lo guardo como un xml.
        private void agregarTimbre()
        {     

            try
            {
                /****************************TRABAJAR CON ARCHIVO QUE NO TIENE EL TIMBRE TXT*************************************/
                StreamReader texto = new StreamReader(temp + nomenclatura + label[0] + label[1] + ".txt");
                                
                while (texto.Peek() > 0)
                {
                    resultado = texto.ReadLine();                                                                                                                                    /*"+adenda+"*/
                    if (resultado == "</cfdi:Comprobante>") 
                    { 
                        estructuraXML += "<cfdi:Complemento>" + resTimbre + "</cfdi:Complemento> " + string.Format(addenda) + " </cfdi:Comprobante>"; 
                    }else
                    {
                        estructuraXML += resultado;
                    }

                } 
                texto.Close();
                               
                //txtXml1.Text = estructuraXML;
                XmlDocument resXML = new XmlDocument();
                resXML.LoadXml(string.Format(estructuraXML));
                resXML.Save(temp + nomenclatura + label[0] + label[1] + ".xml");

                //Crear y leer el codigo qr
                u.CrearQR(temp + nomenclatura + label[0] + label[1] + ".xml", temp + nomenclatura + label[0] + label[1] + ".png");
                textoQr = u.LeerQR(temp + nomenclatura + label[0] + label[1] + ".png");
               
                //Validar y crear directorios en el servidor y en la computadora local donde se guardaran los XML y PDF
                try
                {
                    //si no existe el directorio en el servidor lo creamos 
                    if (!(Directory.Exists(@directorioGuardarCP + label[0] + "\\" + label[49] + "\\" + label[50])))
                    {
                        Directory.CreateDirectory(@directorioGuardarCP + label[0] + "\\" + label[49] + "\\" + label[50]);
                    }
                    
                    //si no existe el directorio en el computadora local lo creamos 
                    if (!(Directory.Exists(@"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50])))
                    {
                        Directory.CreateDirectory(@"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50]);
                    }
                }catch (Exception e)
                {
                    MessageBox.Show("Problemas para crear el directorio en el servidortqp o en la desktop local\n" + e, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                try
                {   //Si existe el directorio en el servidor guardamos el xml en el mismo y en la maquina local.
                    if (Directory.Exists(@directorioGuardarCP + label[0] + "\\" + label[49] + "\\" + label[50]))
                    {
                        resXML.Save(@directorioGuardarCP + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".xml");
                        resXML.Save(@"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".xml");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("No se puede guardar el XML en el directorio del servidortqp \n" + e, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                                                   
               

                //Si estructuraXML no esta vacia o nula leemos el timbrado para agregarlo a las labes
                string xml = string.Format(estructuraXML);

                if (!string.IsNullOrEmpty(xml))
                {

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(temp + nomenclatura + label[0] + label[1] + ".xml");

                    XmlNodeList resultado = xDoc.GetElementsByTagName("cfdi:Comprobante");
                    XmlNodeList lista = ((XmlElement)resultado[0]).GetElementsByTagName("cfdi:Complemento");
                    XmlNodeList certificado = ((XmlElement)resultado[0]).GetElementsByTagName("cfdi:Comprobante");

                    //XmlNodeList elemList = xDoc.GetElementsByTagName("cfdi:Comprobante");
                    for (int i = 0; i < resultado.Count; i++)
                    {
                        lblCertificado.Text = resultado[i].Attributes["noCertificado"].Value;

                    }

                    if (lista.Count >= 1)
                    {

                        foreach (XmlElement nodo in lista)
                        {

                            version = nodo["tfd:TimbreFiscalDigital"].GetAttribute("version");
                            uuid = nodo["tfd:TimbreFiscalDigital"].GetAttribute("UUID");
                            fechaTim = nodo["tfd:TimbreFiscalDigital"].GetAttribute("FechaTimbrado");
                            selloCFD = nodo["tfd:TimbreFiscalDigital"].GetAttribute("selloCFD");
                            noCerti = nodo["tfd:TimbreFiscalDigital"].GetAttribute("noCertificadoSAT");
                            selloSAT = nodo["tfd:TimbreFiscalDigital"].GetAttribute("selloSAT");
                            
                            lblCCS.Text = "||" + version + "|" + uuid + "|" + fechaTim + "|" + selloCFD + "|" + noCerti + "||";
                            lblSCFDI.Text = selloCFD;
                            lblSSAT.Text = selloSAT;
                            lblFF.Text = uuid;
                            lblCSAT.Text = noCerti;
                        }                        
                        guardarCFDi();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Porblemas al agregar el timbre al pdf y el timbre a las label \n" + e, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guardarCFDi()
        {
            metodos registrarCFDi = new metodos();
            int res = -1;

            /***********SQL PARA ACUTUALIZAR LOS TIMBRES**************/
            string registrar = "exec Registrar_CFDi " + Convert.ToInt32(label[43]) + ",'" + Convert.ToString(label[0]) + "'," + Convert.ToInt32(label[1]) + ",'" + fechaTim + "','" + uuid + "','" + noCerti +"','"+ textoQr+"','" + Convert.ToString(label[51]+"','" + selloSAT+"','" + selloCFD +"','"+ lblCCS.Text+"','"+leyenda+"'");
            
            res = registrarCFDi.ABM(registrar, this.conn);
            if (res == 1)
            {
                crearPDF();               

            }
                   
        }

        //12.-Funcion para crear el pdf y guardarlo en el servidor y en la maquina local.
        private void crearPDF()
        {
            try
            {
               
                string temp = System.IO.Path.GetTempPath();

                //For para crear la tabla de descripciones para luego agragarla al pdf
                string tabla = "<table  cellspacing='0' width='100%' border='0' style='font-family:Courier New, Courier, monospace;line-height:6pt;font-size:6px  !important;'>";
                tabla += "<tr><td width='5%'><p align='center'><strong>(1)NUM</strong></p></td><td width='5%'><p align='center'><strong>(2)CLASE</strong></p></td><td><p align='center' width='17%'><strong>(3)QUE SE DICE QUE CONTIENE</strong></p></td><td width='5%'><p align='center'><strong>(4)PESO</strong></p></td><td width='5%'><p align='center'><strong>MTS 3</strong></p></td><td width='10%'><p align='center'><strong>PESO ESTIMADO</strong></p></td></tr>";
                for (int i = 0; i < gridDescripciones.Rows.Count-1; i++)
                {
                    tabla += "<tr>";

                    for (int k = 0; k < gridDescripciones.Columns.Count; k++)
                    {
                       
                        if (gridDescripciones[k, i].Value != null)
                        {

                            if (k == 0)
                            {
                                tabla += "<td width='3%'><p align='center'>" + gridDescripciones[k, i].Value.ToString() + "</p></td>";
                            }
                            if (k == 2)
                            {
                                tabla += "<td width='20%'><p align='center'>" + gridDescripciones[k, i].Value.ToString() + "</p></td>";
                            }

                            if (k == 1)
                            {
                                tabla += "<td width='3%'><p align='center'>" + gridDescripciones[k, i].Value.ToString() + "</p></td>";

                            }
                            if (k == 3)
                            {
                                string peso = gridDescripciones[k, i].Value.ToString();
                                if (peso == "0.00") { peso = ""; }
                                tabla += "<td width='3%'><p align='center'>" + peso + "</p></td>";
                            }
                            if (k == 4)
                            {
                                string peso = gridDescripciones[k, i].Value.ToString();
                                if (peso == "0.00") { peso = ""; }
                                tabla += "<td width='3%'><p align='center'>" + peso + "</p></td>";
                            }
                            if (k == 5)
                            {
                                string peso = gridDescripciones[k, i].Value.ToString();
                                if (peso == "0.00") { peso = ""; }

                                tabla += "<td width='5%'><p align='center'>" + peso + "</p></td>";

                            }
                        }
                    }
                    tabla += "</tr>";

                } tabla += "</table>"+viajFactCP;

               
                //For para crear la tabla de conceptos para luego agragarla al pdf
                string tablaC = "<table  style='font-size:6px' border='0' cellspacing='0' cellpadding='0' align='left' width='100%'>";
                //tabla += "<tr><td width='5%'><p align='center'><strong>(1)NUM</strong></p></td><td width='5%'><p align='center'><strong>(2)CLASE</strong></p></td><td><p align='center' width='17%'><strong>(3)QUE SE DICE QUE CONTIENE</strong></p></td><td width='5%'><p align='center'><strong>(4)PESO</strong></p></td><td width='5%'><p align='center'><strong>MTS 3</strong></p></td><td width='10%'><p align='center'><strong>PESO ESTIMADO</strong></p></td></tr>";
                for (int i = 0; i < gridConceptos.Rows.Count - 1; i++)
                {
                    if (gridConceptos[1, i].Value.ToString() != "0.00")
                    {
                        tablaC += "<tr>";
                        decimal moneda = Convert.ToDecimal(gridConceptos[1, i].Value.ToString());
                        tablaC += "<td width='51%' valign='top'><p>" + gridConceptos[0, i].Value.ToString() + "</p></td>" + "<td width='49%'   valign='top'><p align='right'>" + moneda.ToString("C") + "</p></td>";
                        tablaC += "</tr>";
                    }
                    else
                    {

                        tablaC += "<tr>";
                        tablaC += "<td width='51%' valign='top'><p>" + "." + "</p></td>" + "<td width='49%'   valign='top'><p align='right'>" + "" + "</p></td>";
                        tablaC += "</tr>";
                    }
                } tablaC += "</table>";             
              

                StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
                styles.LoadTagStyle("table", "style", "font-family:Courier New, Courier, monospace;line-height:7pt;");
                
                string html = "<html xmlns='http://www.w3.org/1999/xhtml'><body><table id='contenedor' width='100%' border='0' height='100%' cellspacing='0'><tr><td width='6%' height='165'></td><td width='84%' valign='top'><table   width='130%' height='70' border='0' align='left' cellpadding='0' cellspacing='0' id='razonSocial' style='font-size:8px;'><tr><td width='46%' height='100%'   align='center' valign='top'><p align='center'><strong><h4>" + de + "</h4></strong></p></td></tr><tr><td width='46%' valign='top'><p   align='center'>" + direccion + numeroExterior + "</p></td></tr><tr><td width='46%' valign='top'><p align='center'>" + coloniaE + "</p></td></tr><tr><td width='46%' valign='top'><p align='center'>" + municipioE + estadoE + cpE + "</p></td></tr><tr><td width='46%' valign='top'><p   align='center'>" + tel1 + tel2 + "</p></td></tr><tr><td valign='top'><p align='center'>" + rfcE + "<br><br><br><a href=\"www.grupocalafia.com.mx\">www.grupocalafia.com.mx</a></p></td></tr><tr><td width='46%' valign='baseline'><br/><br/>EXPEDIDA  EN: " + label[42]/*lblDireccion.Text*/ + "<br/>  Tels.:" + label[47]/*lblTelefono1.Text*/ + ", " + label[48]/*lblTelefono2.Text*/ + "</td></tr><tr><td align='right' valign='bottom'></td></tr><tr></tr></table></td><td width='10%'><table id='factura' style='font-size:8px ¡important;'  border='1' cellspacing='0' cellpadding='2' align='right' width='75%' height='70%'><tr><td colspan='2' valign='top'><p align='center'><strong>CARTA   PORTE</strong></p></td></tr><tr><td width='45%' valign='top'><p align='center'><strong>SERIE</strong><br/>" + label[0] + "</p></td><td width='55%' valign='middle'><p   align='center'><strong>NUMERO</strong><br/>" + label[1] + "</p></td></tr><tr><td colspan='2' valign='top'><p align='center'><strong>NO.   CERTIFICADO</strong><br/>" + lblCertificado.Text + "</p></td></tr><tr><td colspan='2' align='center' valign='top'><strong>FECHA Y HORA DE CERTIFICACIÓN</strong><br/>" + fechaTim + "</td></tr><tr><td colspan='2' valign='top'><p   align='center'><strong>CERTIFICADO SAT</strong><br />" + lblCSAT.Text + "</p></td></tr><tr><td   colspan='2' valign='top'><p align='center'><strong>FOLIO FISCAL</strong><br />" + lblFF.Text + "</p></td></tr><tr><td colspan='2' valign='top'><p><strong>TIPO COMPROBANTE:</strong>" + lblComprobante.Text + "</p></td></tr><tr><td colspan='2'   valign='top'><p align='center'><strong>FECHA EXPEDICIÓN</strong><br />" + label[33]/*lblFecha.Text*/ + "</p></td></tr></table></td></tr><tr><td height='10' colspan='4' valign='top' style='font-size:8px ¡important'></td></tr><tr><td height='199' colspan='3' valign='middle'><table width='100%' height='598' border='1'   align='left' cellpadding='0' cellspacing='0' style='font-size:6px'><tr><td height='596' colspan='2' valign='top'><table style='font-size:6px' border='1'   cellspacing='0' cellpadding='0' align='left' width='100%'><tr><td height='444' colspan='4' valign='top'><table id='remitente' style='font-size:8px   ¡important;' border='1' cellspacing='0' cellpadding='0' align='left' width='100%'><tr><td height='8' valign='top'><p   align='center'><strong>REMITENTE</strong></p></td><td align='center' valign='top'><strong>DESTINATARIO</strong></td></tr><tr><td  height='25' valign='top'><table  id='remitente' style='font-size:7px   ¡important;' border='0' cellspacing='0' cellpadding='2' align='left' width='100%'><tr><td colspan='2' valign='top'><p>Número " + label[3]/*lblIdClienteRemitente.Text*/ + " - " + label[4]/*lblPlazaRemitente.Text*/ + "</p></td></tr><tr><td colspan='2' valign='top'><p><strong>" + label[5]/*lblNombrePlazaRemitente.Text*/ + "</strong></p></td></tr><tr><td   colspan='2' valign='top'><p>Calle:" + label[7] + label[52] + label[53]/*lblDireccionRemitente.Text*/ + " Col." + label[8]/*lblColoniaRemitente.Text*/ + "</p></td></tr><tr><td colspan='2'   valign='top'><p>Ciudad:" + label[9]/*lblCiudadRemitente.Text*/ + "  Estado:" + label[10]/*lblEstadoRemitente.Text*/ + "  Pais:" + label[11]/*lblPaisRemitente.Text*/ + " C.P:" + label[12]/*lblCPRemitente.Text*/+ "</p></td></tr><tr><td width='64%'   valign='top'><p>Tels.:" + label[13]/*lblTelefono1Remitente.Text*/ + "</p></td><td width='36%' valign='top'><p>R.F.C.:" + label[6]/*lblRFCRemitente.Text*/ + "</p></td></tr><tr><td height='6' colspan='2' valign='top'><p>RECOGER EN:" + label[14]/*lblRecoger.Text*/ + "</p></td></tr></table></td><td valign='top'><table   id='destinatario' border='0' style='font-size:7px ¡important' cellspacing='0' cellpadding='2' align='left' width='100%'><tr><td colspan='2'   valign='top'><p>Número " + label[15]/*lblIdClienteDestinatario.Text*/ + " - " + label[16]/*lblPlazaDestinatario.Text*/ + "</p></td></tr><tr><td colspan='2'   valign='top'><p><strong>" + label[17]/*lblNombrePlazaDestinatario.Text*/ + "</strong></p></td></tr><tr><td colspan='2' valign='top'><p>Calle:" + label[19] + label[54] + label[55]/*lblDiereccionDestinatario.Text*/ + " Col." + label[20]/*lblColoniaDestinatario.Text*/ + "</p></td></tr><tr><td colspan='2' valign='top'><p>Ciudad:" + label[21]/*lblCiudadDestinatario.Text*/ + " Estado:" + label[22]/*lblEstadoDestinatario.Text*/ + " Pais:" + label[23]/*lblPaisDestinatario.Text*/ + " C.P:" + label[24]/*lblCPDestinatario.Text*/+ "</p></td></tr><tr><td width='63%'   valign='top'><p>Tels.:" + label[25]/*lblTelefono1Destinatario.Text*/ + "</p></td><td width='37%' valign='top'><p>R.F.C.:" + label[18]/*lblRFCDestinatario.Text*/ + "</p></td></tr><tr><td colspan='2' valign='top'><p>ENTREGAR EN:" + label[26]/*lblEntrega.Text*/ + "</p></td></tr></table></td></tr><tr><td   height='20' colspan='3' valign='top'><table style='font-size:6px' border='1' cellspacing='0' cellpadding='0' align='left' width='100%'><tr><td width='20%'   valign='top'><p align='center'><strong>(6) FRACCIÓN NUM</strong></p></td><td width='29%' valign='top'><p align='center'><strong>(7)   CLASE</strong></p></td><td width='27%' valign='top'><p align='center'><strong>(8) CUOTA POR TONELADA</strong><br />" + label[34]/*lblValor1.Text*/ + "</p></td><td width='22%' valign='top'><p   align='center'><strong>VALOR DECLARADO</strong><br />" + label[35]/*lblValor2.Text*/ + "</p></td></tr><tr><td align='center' valign='middle'><strong>BULTOS</strong></td><td valign='top'>&nbsp;</td><td align='center'   valign='middle'><strong>(5) VOLUMEN</strong></td><td align='center' valign='top'><table style='font-size:6px' border='1' cellspacing='0' cellpadding='0' align='right' width='100%'><tr><td width='8%' rowspan='2' valign='top'><p   align='center'><strong>&nbsp;</strong><strong>CONCEPTO</strong></p></td><td width='13%' valign='top'><p align='center'><strong>(9) MOD.   PAGO</strong></p></td></tr><tr><td width='13%' height='8' valign='top'><p align='center'>" + label[27]/*lblPago.Text*/ + "</p></td></tr></table></td></tr><tr></tr></table></td></tr><tr><td height='23'   colspan='3' valign='top'><div align='left'><table width='100%' border='0' align='left' cellspacing='0'><tr><td width='80%' height='50'>" + tabla + "</td><td width='20%'>" + tablaC + "</td></tr></table></div></td></tr><tr><td height='61'   colspan='3' valign='top'><table width='100%' border='0' cellspacing='0'><tr><td width='77%' height='64'><table style='font-size:6px' border='1'   cellspacing='0' cellpadding='0' align='left' width='100%' height='100%'><tr><td width='64%'  valign='top'><p><strong>IMPORTE CON LETRA:</strong>" + label[51]/*lblImportLetra.Text*/ + "</p><br /><p><strong>RETENEDOR: </strong>" + retenedor/*lblRetenedor.Text*/ + "</p></td></tr></table></td><td width='23%'><table border='0' style='font-size:6px'   cellspacing='0' cellpadding='0' align='left' width='100%'><tr><td width='21%' valign='top'><p><strong>SUB-TOTAL</strong></p></td><td width='13%'   valign='top'><p align='right'>$ " + label[37]/*lblSubtotal.Text*/ + "</p></td></tr><tr><td width='21%' valign='top'><p><strong>%I.V.A " + label[38]/*lblIvaActual.Text*/ + "</strong></p></td><td width='13%'   valign='top'><p align='right'>$ " + label[39]/*lblIva.Text*/ + "</p></td></tr><tr><td width='21%' valign='top'><p><strong>RET I.V.A . " + label[40]/*lblPorcentajeR.Text*/ + "</strong></p></td><td   width='13%' valign='top'><p align='right'>$ " + label[41]/*lblRetIva.Text*/ + "</p></td></tr><tr><td width='21%' valign='top'><p><strong>TOTAL</strong></p></td><td width='13%'   valign='top'><p align='right'>$ " + label[28]/*lblTotal.Text*/ + "</p></td></tr></table></td></tr></table></td></tr><tr><td height='10' colspan='3' valign='top'><table style='font-  size:6px' width='100%' border='0' cellspacing='0'><tr><td width='50%'><div align='right'><strong>OBSERVACIONES</strong></div></td><td width='50%'><div   align='right'>Impuesto retenido con la ley del Impuesto al Valor Agregado</div></td></tr></table></td></tr><tr><td height='10' colspan='3'   valign='top'><table style='font-size:6px' cellpadding='2' width='100%' border='1' cellspacing='0'><tr><td width='28%'><table style='font-size:6px; monospace;line-height:1pt !important;' border='0' cellspacing='0'   cellpadding='2' align='left' width='100%'><tr><td width='17%' valign='top'><p><strong>TOTAL CARTA PORTE</strong></p></td><td width='9%' valign='top'><p>$   " + label[29]/*lblTotalCartaPorte.Text*/ + "</p></td></tr><tr><td width='17%' valign='top'><p><strong>OTRAS LINEAS</strong></p></td><td width='9%' valign='top'><p>$   " + label[30]/*lblOtrasLineas.Text*/ + "</p></td></tr><tr><td width='17%' valign='top'><p><strong>TOTAL A COBRAR</strong></p></td><td width='9%' valign='top'><p>$   " + label[31]/*lblTotalCobrar.Text*/ + "</p></td></tr><tr><td width='27%' colspan='2' valign='top'><p><strong>DOCUMENTÓ: </strong>" + label[32] /*lblDocumento.Text*/ + "</p></td></tr></table></td><td width='72%' valign='top'>" + label[36]/*lblObservacines.Text*/ + "</td></tr></table></td></tr><tr><td height='36' colspan='3'   valign='top'><table id='cadenas' style='font-size:7px ¡important; line-height:5pt !important' border='1' cellspacing='0' cellpadding='2' align='left' width='100%'><tr><td height='37' align='center'   valign='top'><strong>SELLO DEL SAT</strong><p style='top:1px' align='left'>" + lblSSAT.Text + "</p></td></tr><tr><td width='100%' align='center' valign='top'><strong>CADENA ORIGINAL DEL COMPLEMENTO DE CERTIFICACIÓN  DIGITAL DEL SAT</strong><p align='left'>" + lblCCS.Text + "</p></td></tr><tr><td width='100%'   valign='top' align='center'><strong>SELLO DIGITAL DEL CFDi</strong><p align='left'>" + lblSCFDI.Text + "</p></td></tr><tr><td width='100%' height='2' valign='top' nowrap></td></tr></table></td></tr><tr><td height='10' colspan='3' valign='top'><table style='font-size:6px; line-height:5pt !important' border='0' cellspacing='0' cellpadding='0' align='left' width='100%'><tr><td width='82%'   valign='top'><p><strong>CONDICIONES DEL CONTRATO DE TRANSPORTE QUE AMPARA ESTA CARTA DE PORTE</strong></p></td></tr><tr><td width='82%' valign='top'   nowrap><p align='justify'>PRIMERA.- Para los efectos del presente contrato de transporte se denomina \'Porteador\' al transportista y \'Remitente\' al   usuario que contrate el servicio.</p></td></tr><tr><td width='82%' valign='top' nowrap><p align='justify'>SEGUNDA.- El \'Remitente\' es responsable de que la   información proporcionada al \'Porteador\' sea veraz y que la documentación que entregue para efectos del transporte sea la correcta.</p></td></tr><tr><td   width='82%' valign='top' nowrap><p align='justify'>TERCERA.- El \'remitente\' debe declarar al “Porteador” el tipo de mercancía o efectos de que se trate,   peso, medidas y/o numero de la carga que entrega para su transporte y, en su caso, el valor de la misma. La carga que se entregue a granel será pesada por el   “Porteador\' en el primer punto donde haya báscula apropiada o, en su defecto, aforada en metros cúbicos con la conformidad del   “Remitente”.</p></td></tr><tr><td height='6' style='margin-right:40px' valign='top' nowrap><p align='justify'>CUARTA.- Para efectos del transporte, el “remitente” deberá entregar al   “Porteador” los documentos que las leyes y reglamentos exijan para llevar a cabo el servicio, en caso de no cumplirse con estos requisitos el “Porteador\'   está obligado a rehusar el transporte de las mercancías.</p> </td></tr><tr><td height='8' valign='top' nowrap> <p align='justify'>QUINTA.- Si por sospecha de   falsedad en la declaración del contenido de un bulto el “Porteador” deseare proceder a su reconocimiento, podrá hacerlo ante testigos y con asistencia del   “Remitente” o del consignatario, si este último no concurriere, se solicitará la presencia de un inspector de la Secretaría de Comunicaciones y Transportes,   y se levantará el acta correspondiente. El “Porteador” tendrá en todo caso la obligación de dejar los bultos en el estado en que se encontraban antes del   reconocimiento.</p></td></tr><tr><td height='8' valign='top' nowrap><p align='justify'>SEXTA.- El “Porteador” deberá recoger y entregar la carga precisamente   en los domicilios que señale el “Remitente”, ajustándose a los términos y condiciones convenidos. El “Porteador” sólo está obligado a llevar la carga la   domicilio del consignatario para su entrega una sola vez. Si está no fuera recibida, se dejará aviso de que la mercancía queda a disposición del interesado   en las bodegas que indique el “Porteador”.</p></td></tr><tr><td width='82%' height='6' valign='top' nowrap><p align='justify'>SEPTIMA.- si la carga no fuera   retirada dentro de los 30 días siguientes a aquel en que hubiese sido puesta a disposición del consignatario, el ”Porteador” podrá solicitar la venta en   pública subasta con arreglo a lo que dispone el Código de Comercio.</p></td></tr><tr><td height='6' valign='top' nowrap><p align='justify'>OCTAVA.- El   “Porteador” y el “Remitente” negociarán libremente el precio del servicio, tomando en cuenta su tipo, característica de los embarques, volumen, regularidad,   clase de carga y sistema de pago. </p></td></tr><tr><td height='9' valign='top' nowrap><p align='justify'>NOVENA.- Si el “Remitente” desea que el “Porteador”   asuma la responsabilidad por el valor de la mercancía o efectos que él declare o cubra toda clase de riesgos, inclusive los derivados en caso fortuito o de   fuerza mayor, las partes deberán convenir un cargo adicional, equivalente al valor de la prima del seguro que se contrate, el cual se deberá expresar en la   carta de porte electrónica o factura electrónica. </p></td></tr><tr><td height='8' valign='top' nowrap><p align='justify'>DECIMA.- cuando el importe del   flete no incluya el cargo adicional, la responsabilidad del “Porteador” queda expresamente limitada a la cantidad equivalente a 15 días del salario mínímo   vigente en el distrito Federal por tonelada o cuando se trate de embarques cuyo peso sea mayor a 200 Kg pero menor a 1000 Kg; y a 4 días de salario mínimo   por remesa cuando se trate de embarques con peso hasta de 200 Kg. </p></td></tr><tr><td height='9' valign='top' nowrap><p align='justify'>DECIMA PRIMERA.- El   precio del transporte deberá pagarse en origen, salvo convenio entre las partes de pago en destino. Cuando el transporte se hubiere concertado “Flete por   Cobrar”, la entrega de las mercancías o efectos se hará contra el pago del flete y el “Porteador” tendrá derecho a retenerlos mientras no se cubra el precio   convenido. </p></td></tr><tr><td height='9' valign='top' nowrap><p align='justify'>DECIMA SEGUNDA.- si al momento de la entrega resultare algún faltante o   avería, el consignatario deberá hacerla constar en ese acto en la carta de porte electrónica o factura electrónica y formular su reclamación por escrito al   “Porteador”, dentro de las 24 horas siguientes. </p></td></tr><tr><td height='8' valign='top' nowrap><p align='justify'>DECIMA TERCERA.- El “Porteador” queda   eximido de la obligación de recibir mercancías o efectos para su transporte, en los siguientes casos a) Cuando se trate de carga que por su naturaleza, peso,   volumen, embalaje defectuoso o cualquier otra circunstancia no pueda transportarse sin destruirse o sin causar daño a las demás artículos o al material   rodante, salvo que la empresa de que se trate tenga el equipo adecuado. b)Las mercancías cuyo transporte haya sido prohibido por disposiciones legales o   reglamentarias. Cuando tales disposiciones no prohíban precisamente el transporte de determinadas mercancías, pero si ordenen la presentación de ciertos   documentos para que puedan ser transportadas, el “Remitente estará obligado a entregar al “Porteador” los documentos correspondientes.</p> </td></tr><tr><td   height='11' valign='top' nowrap><p align='justify'>DECIMA CUARTA.- Los casos no previstos en las presentes condiciones y las quejas derivadas de su   aplicación se someterán por la vía administrativa a la Secretaría de comunicaciones y transporte.</p></td></tr><tr><td height='14' valign='top'   nowrap>.</td></tr><tr><td height='24' valign='top' nowrap><p align='justify'>POR ESTE PAGARE RECONOZCO(EMOS) DEBER Y ME(NOS) OBLIGO(AMOS) A PAGAR   INCONDICIONALMENTE A LA ORDEN DE TRANSPORTES CALAFIA S.A. DE C.V., EN SUS OFICINAS EN LA CIUDAD DE  GUADALAJARA, JAL., O EN EL LUGAR QUE ESTE ME(NOS) SEÑALE   EL DIA DE	    DE	LA CANTIDAD DE  $	VALOR RECIBIDO EN SERVICIOS A MI(NUESTRA) ENTERA SATISFACCIÓN. EN CASO DE MORA EL SALDO INSOLITO CAUSARA   INTERESES A RAZÓN DE % MENSUAL HASTA SU TOTAL LIQUIDACIÓN HACIENDO EXIGIBLE ESTA CANTIDAD EN EL MOMENTO DE HACER EL PAGO PRINCIPAL A DE __________ FIRMA DE   CONFORMIDAD </p></td></tr></table></td></tr><tr><td height='95' colspan='3' valign='middle' style='border:0   ¡important'><table style='font-size:7px ¡important;' cellspacing='0' cellpadding='2' hspace='0' vspace='0' align='center'><tr><td valign='top'   align='left'><p align='center'>" + label[44]/*lblLeyenda.Text*/ + "<br>METODO DE PAGO: " + label[45]/*lblMetodoPago.Text*/ + ", TERMINACIÓN: " + label[46]/*lblTerminacion.Text*/ + "<br>" + "RÉGIMEN FISCAL: " + leyenda + "<br><strong>Este  Documento es una representación impresa de un CFDi</strong></p></td> </tr>   </table></td></tr></table></td></tr></table></td></tr></table></td></tr><tr><td colspan='3'></td></tr></table></body></html>";
                
                Document pdf = new Document(PageSize.LETTER, 9, 8, 5, 5);

                PdfWriter.GetInstance(pdf, new FileStream(temp + nomenclatura + label[0] + label[1] + ".pdf", FileMode.OpenOrCreate));
                try
                {
                    PdfWriter.GetInstance(pdf, new FileStream(@directorioGuardarCP + "\\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".pdf", FileMode.Create));
                }
                catch (Exception e)
                {
                    MessageBox.Show("No se puede guardar el PDF en el servidortqp \n" + e, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                PdfWriter.GetInstance(pdf, new FileStream(@"\STC R2\CFDi\CartasPorte\" + "\\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".pdf", FileMode.Create));
                pdf.Open();

                //Get the assembly.
                System.Reflection.Assembly CurrAssembly = System.Reflection.Assembly.LoadFrom(System.Windows.Forms.Application.ExecutablePath);
                //MyShortcut.IconLocation = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("YourNamespace.IconFilename.ico");

                //System.IO.Stream stream = CurrAssembly.GetManifestResourceStream("logocalafia.JPG");
                if (File.Exists(@"\STC R2\CFDi VB\logocalafia.JPG"))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"\STC R2\CFDi VB\logocalafia.JPG");
                    logo.ScalePercent(25f);
                    logo.Alignment = iTextSharp.text.Image.UNDERLYING;
                    logo.SetAbsolutePosition(10, pdf.PageSize.Height - 77f);
                    pdf.Add(logo);
                }
                else
                {
                    MessageBox.Show("logocalafia.jpg No existe en "+ directorioRaiz+@"STC R2\CFDi VB\" , "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
                
                iTextSharp.text.Image cbb = iTextSharp.text.Image.GetInstance(@temp + nomenclatura + label[0] + label[1] + ".png");
                cbb.ScalePercent(42f);
                cbb.Alignment = iTextSharp.text.Image.UNDERLYING;
                cbb.SetAbsolutePosition(pdf.PageSize.Width - 601, pdf.PageSize.Height - 158f);
                pdf.Add(cbb);
                
                foreach (IElement E in HTMLWorker.ParseToList(new StringReader(html), styles))
                pdf.Add(E); 
                
                pdf.Close();

                //Valida si quieren enviar el xml o el pdf
                if (chbPdf.Checked || chbXml.Checked)
                {
                    enviarCorreo();
                }

            }
            catch (Exception)
            {

                MessageBox.Show("El PDF " + nomenclatura + label[0] + label[1] + " esta siendo utilizado por otro proceso", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            try
            {
                //Process.Start(temp + nomenclatura + label[0] + label[1] + ".pdf");
                Process.Start( @"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] +".pdf");

                if (System.IO.File.Exists(@temp + nomenclatura + label[0] + label[1] + ".pdf"))
                {
                    
                    try
                    {
                        System.IO.File.Delete(@temp + nomenclatura + label[0] + label[1] + ".pdf");
                        System.IO.File.Delete(@temp + nomenclatura + label[0] + label[1] + ".txt");
                           
                        System.IO.File.Delete(@temp + nomenclatura + label[0] + label[1] + ".png");
                        System.IO.File.Delete(@temp + nomenclatura + label[0] + label[1] + ".xml");
                        //System.IO.File.Delete(@temp + nomenclatura + label[0] + label[1] + "timbre.xml");

                    }
                    catch (System.IO.IOException e)
                    {
                        MessageBox.Show("excepcion "+e);
                    }
                }
               
            }
            catch (Exception e)
            {
                MessageBox.Show("El PDF no se puede mostrar \n" + e, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {                
                 this.Close();
                 Dispose();
            }

        }

        //13.-Funcion para enviar el XML y PDF por correo
        private void enviarCorreo()
        {

            try
            {
                if (Convert.ToString(conf[0]) != "usuario@tudominio.com.mx" && Convert.ToString(conf[1]) != "****" && Convert.ToString(conf[2]) != "puerto" && Convert.ToString(conf[3]) != "dominio")
                {
                    usuarioCorreo = Convert.ToString(conf[0]);
                    contrasenaCorreo = Convert.ToString(conf[1]);
                    puertoCorreo = Convert.ToInt32(conf[2]);
                    smtp = Convert.ToString(conf[3]);
                    
                    //Configuración del Mensaje
                    MailMessage maill = new MailMessage();
                    SmtpClient SmtpServerr = new SmtpClient(smtp,puertoCorreo);                    

                    //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                    maill.From = new MailAddress(usuarioCorreo, de, Encoding.UTF8);

                    //Aquí ponemos el asunto del correo
                    maill.Subject = asuntoCorreo + label[0] + label[1];
                    maill.CC.Add(usuarioCorreo);       
                    
                    if (chbXml.Checked && !chbPdf.Checked)
                    {
                        //Si queremos enviar XML  adjunto tenemos que especificar la ruta en donde se encuentran
                        maill.Attachments.Add(new Attachment(@"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".xml"));

                    }
                    if (chbPdf.Checked && !chbXml.Checked)
                    {
                        //Si queremos enviar PDF  adjunto tenemos que especificar la ruta en donde se encuentran
                        maill.Attachments.Add(new Attachment(@"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".pdf"));

                    }
                    if (chbPdf.Checked && chbXml.Checked)
                    {
                        //Si queremos enviar PDF y XML  adjunto tenemos que especificar la ruta en donde se encuentran
                        maill.Attachments.Add(new Attachment(@"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".pdf"));
                        maill.Attachments.Add(new Attachment(@"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".xml"));


                    }
                    //Aquí ponemos el mensaje que incluirá el correo
                    
                    maill.IsBodyHtml = true;
                    maill.Body=cuerpoMensajeCorreo;
                    
                    //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                    maill.To.Add(txtCorreo.Text);
                                        
                    //Configuracion del SMTP
                    //Especificamos las credenciales con las que enviaremos el mail
                    SmtpServerr.Credentials = new System.Net.NetworkCredential(usuarioCorreo, contrasenaCorreo);
                    //SmtpServer.UseDefaultCredentials = true;

                    SmtpServerr.EnableSsl = false;
                    SmtpServerr.Send(maill);
                    SmtpServerr.Dispose();
                }
                else
                {
                    //Configuración del Mensaje
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(smtp);


                    //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                    mail.From = new MailAddress(usuarioCorreo, de, Encoding.UTF8);

                    //Aquí ponemos el asunto del correo
                    mail.Subject = asuntoCorreo + label[0] + label[1];

                    if (chbXml.Checked && !chbPdf.Checked)
                    {
                        //Si queremos enviar XML  adjunto tenemos que especificar la ruta en donde se encuentran
                        mail.Attachments.Add(new Attachment(@"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".xml"));

                    }
                    if (chbPdf.Checked && !chbXml.Checked)
                    {
                        //Si queremos enviar PDF  adjunto tenemos que especificar la ruta en donde se encuentran
                        mail.Attachments.Add(new Attachment(@"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".pdf"));

                    }
                    if (chbPdf.Checked && chbXml.Checked)
                    {
                        //Si queremos enviar PDF y XML  adjunto tenemos que especificar la ruta en donde se encuentran
                        mail.Attachments.Add(new Attachment(@"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".pdf"));
                        mail.Attachments.Add(new Attachment(@"\STC R2\CFDi\CartasPorte\" + label[0] + "\\" + label[49] + "\\" + label[50] + "\\" + nomenclatura + label[0] + label[1] + ".xml"));


                    }
                    //Aquí ponemos el mensaje que incluirá el correo
                    mail.IsBodyHtml = true;
                    mail.Body = cuerpoMensajeCorreo;

                    //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                    mail.To.Add(txtCorreo.Text);

                    //Configuracion del SMTP
                    SmtpServer.Port = puertoCorreo; //Puerto que utiliza Gmail para sus servicios

                    //Especificamos las credenciales con las que enviaremos el mail
                    SmtpServer.Credentials = new System.Net.NetworkCredential(usuarioCorreo, contrasenaCorreo);
                    //SmtpServer.UseDefaultCredentials = true;

                    SmtpServer.EnableSsl = false;
                    SmtpServer.Send(mail);
                    SmtpServer.Dispose();
                }
            }

            catch (Exception ex)
            {
                Dispose();
                MessageBox.Show("NO SE PUDO ENVIAR EL CORREO \n" + ex);
                
            }

            //NOTA: Es importante que conozca las características correspondientes a la cuenta POP3 y SMTP de donde saldrá el correo electrónico.
            //Tambien es importante señalar que, en el campo "Mensaje", usted puede colocar codigo HTML.
            //Sobre las direcciones de correo CC y CCO puede agregarlas haciendo una separacion con un pipes "|"
            //Ejemplo: jimenez.alf@empresa.com|mariana.neg@empresa.com|sifuentes.mon@empresa.com   
        }

        private void ocultarComponentes()
        {
            txtFolio.Visible = false;
            btnTimbrar.Visible = false;
            gridCartPort.Enabled = false;
            lblCarta.Text = "";
            
            txtCorreo.Visible = false;
            chbPdf.Visible = false;
            chbXml.Visible = false;
            lblPara.Visible = false;

        }


        private void mostrarComponentes()
        {
            txtFolio.Visible = true;
            btnTimbrar.Visible = true;
            gridCartPort.Enabled = true;
            lblCarta.Text = "Numero CP:";
            
            txtCorreo.Visible = true;
            chbPdf.Visible = true;
            chbXml.Visible = true;
            lblPara.Visible = true;
            lblCarta.Visible = true;
            btnTimbrar.Visible = true;

            if (txtSerie.Text == "") 
            {
                txtSerie.Focus(); 
            } else if (txtSerie.Text != "" && txtFactura.Text != "")
            { 
                txtContrasena.Focus();
            }

            if (chbPdf.Checked==false || chbXml.Checked==false)
            {
                txtCorreo.Visible = false;
                lblPara.Visible = false;
            }

        }
        string dirDropBox;
        private void licenciasRutasUsuarios()
        {
            
            //string[] discos = Directory.GetLogicalDrives();
            //MessageBox.Show(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()));            
            //Environment.Exit(0);
            //MessageBox.Show(conn.ConnectionString);
            

           

            SqlDataReader ResConsul;
            string consulta1 = "select * from licenciasRutasUsuariosCFDi";
            
            metodos BD = new metodos();
            ResConsul = BD.Buscar(consulta1, this.conn);         
                              
            if (ResConsul.Read())
            {
                //Ping pingPc=new Ping();
                System.Version myVer;
                
                string Ver=Application.ProductVersion;                
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    myVer = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    Ver = string.Concat(myVer);                    
                }
                
                
                System.OperatingSystem osInfo = System.Environment.OSVersion;
                //MessageBox.Show(osInfo.Version.Major.ToString());
                if(osInfo.Version.Major<6)
                {
                    dirDropBox = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Mis Documentos\Dropbox\";  
                }else if(osInfo.Version.Major>=6)
                {
                    dirDropBox = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Dropbox\";
                    
                }
                //dirDropBox = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Documents\Dropbox\";
                //MessageBox.Show(dirDropBox);
                rutaEjecu_lbl.Text = Application.ExecutablePath;
                label1.Text = "Version:" + Ver +" /  Servidor conec...";
                
                //dirDropBox = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Dropbox\";
                CerFileLocal = @directorioRaiz + @"STC R2\CSD\00001000000202528447.cer";
                KeyPassLocal = @directorioRaiz + @"STC R2\CSD\tca890904p18_1212061131s.key";

                //Lleno las variables
                liccenciaBuildCFDI = ResConsul.GetString(0).ToString();
                liccenciaViewXMLCFDI = ResConsul.GetString(1).ToString();
                strUser = ResConsul.GetString(2).ToString();
                strPass = ResConsul.GetString(3).ToString();
                directorioGuardarCP = ResConsul.GetString(5).ToString();
               
                string[] remplazando = directorioGuardarCP.Split('\\');
                string ipDeRed =remplazando[2];

                string[] ipConexionA = ipConexion.Split(',');
                /*ipConexion = remplazandoo[0];*/
                
                
                //pingPc.Send(ipDeRed, 10).Status == IPStatus.Success
                if (ipDeRed==ipConexionA[0] && !Directory.Exists(dirDropBox))
                {
                    vpn_lbl.Text = "[Conectado y funcionando IP: "+ipDeRed+"\nRed Interna de Transportes Calafia 'VPN']";
                    yourToolTip.ToolTipTitle = "Información";
                    yourToolTip.AutoPopDelay = 1500000;
                    yourToolTip.SetToolTip(vpn_lbl, "Los XML y PDF se guardan en la 'Red Interna de Transportes Calafia automaticamente.'\nImportante estar autentificado por IP y por nombre del servidor [SERVIDORTQP]\nel cual debe estar registrado en el archivo hosts");
                          
                }
                else if (ipDeRed != ipConexionA[0] && Directory.Exists(dirDropBox))
                {
                    Process[] localAll = Process.GetProcesses();
                    Boolean dropBoxIniciado = false;

                    foreach (Process s in localAll)
                    {
                        if (s.ProcessName.Trim() == "Dropbox")
                        {
                            //MessageBox.Show(s.ProcessName);
                            dropBoxIniciado = true;
                        }

                    }
                    dirDropBox_chb.Checked = true;
                    yourToolTip.ToolTipTitle = "Información";
                    yourToolTip.AutoPopDelay = 10000;
                    yourToolTip.SetToolTip(dirDropBox_chb, "Exito el directorio existe");                      
                    
                    if (Directory.Exists(dirDropBox) && File.Exists(CerFileLocal) && File.Exists(KeyPassLocal) && dropBoxIniciado==true)
                    {
                        directorioGuardarCP = @dirDropBox;                       
                        vpn_lbl.Text = "[Conectado por la IP  " + ipConexion + "]\n'DEBE ESTAR CONECADO POR LA IP 200.76.182.56'";                        
                        yourToolTip.SetToolTip(vpn_lbl, "Los XML y PDF se guardan en DropBox automaticamente.");

                    }else if (!File.Exists(CerFileLocal) || !File.Exists(KeyPassLocal))
                    {
                        vpn_lbl.Text = "Conectado sin VPN y sin funcionar.\n\nCertificado o Llave digital local, no existe:\n"+CerFileLocal+"\n"+KeyPassLocal;                        
                        yourToolTip.SetToolTip(vpn_lbl, "Sin estos archivos, no puede ser generado el XML.");                        
                        btnTimbrar.Text = "CERRAR SIN TIMBRAR";
                    }else if(dropBoxIniciado==false)
                    {
                        MessageBox.Show("DropBox no esta iniciado. Por favor inicia sesion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }

                }
                else if (ipDeRed != ipConexionA[0] && !Directory.Exists(dirDropBox))
                {
                    yourToolTip.ToolTipTitle = "Información";
                    btnTimbrar.Text = "CERRAR SIN TIMBRAR";
                    yourToolTip.SetToolTip(dirDropBox_chb, "Si existe el directorio, aparcera palomiado automaticamente");
                    vpn_lbl.Text = "Conectado sin VPN y sin funcionar,\nfalta el directorio predeterminado de Dropbox";
                    yourToolTip.SetToolTip(vpn_lbl, "Los XML y PDF, no pueden ser guardados, sin este directorio.");
                }
                else if (ipDeRed == ipConexionA[0] && Directory.Exists(dirDropBox))
                {
                    MessageBox.Show("Debes desinstalar DropBox o Renombrar el directorio " + dirDropBox, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
 
                }

                if (!File.Exists(@directorioRaiz + @"STC R2\Ruta_CFDi.txt"))
                {                     
                    File.WriteAllText(@directorioRaiz + @"STC R2\Ruta_CFDi.txt",@rutaEjecu_lbl.Text); 
                }
                else
                {                  
                   StreamReader sr = new StreamReader(@directorioRaiz + @"STC R2\Ruta_CFDi.txt");                                  
                   string line = sr.ReadLine();
                   sr.Close();                                                       
                    
                   if(line!=@rutaEjecu_lbl.Text) 
                    {
                        StreamWriter escribirRuta = new StreamWriter(@directorioRaiz + @"STC R2\Ruta_CFDi.txt");
                        escribirRuta.WriteLine(@rutaEjecu_lbl.Text);
                        escribirRuta.Close();
                    }           
                }
                                             
                smtp = ResConsul.GetString(6).ToString();
                puertoCorreo = ResConsul.GetInt32(7);
                usuarioCorreo = ResConsul.GetString(8).ToString();
                contrasenaCorreo = ResConsul.GetString(9).ToString();
                de = ResConsul.GetString(10).ToString();
                asuntoCorreo = ResConsul.GetString(11).ToString();
                cuerpoMensajeCorreo = ResConsul.GetString(12).ToString();
                directorioMasteredi = ResConsul.GetString(13).ToString();
                timbres = ResConsul.GetInt32(14);
                limiteTimbres = ResConsul.GetInt32(15);
                alertaTimbresCorreo = ResConsul.GetString(16).ToString();
                leyenda = ResConsul.GetString(17).ToString();
                alertaTimbresCorreoCpp = ResConsul.GetString(18).ToString();
                nomenclatura = ResConsul.GetString(19).ToString();
                razonSocial = ResConsul.GetString(20).ToString();
                direccion = ResConsul.GetString(21).ToString();
                numeroExterior = ResConsul.GetString(22).ToString();
                coloniaE = ResConsul.GetString(23).ToString();
                municipioE = ResConsul.GetString(24).ToString();
                ciudadE = ResConsul.GetString(25).ToString();
                estadoE = ResConsul.GetString(26).ToString();
                paisE = ResConsul.GetString(27).ToString();
                cpE = ResConsul.GetString(28).ToString();
                tel1  = ResConsul.GetString(29).ToString();
                tel2  = ResConsul.GetString(30).ToString();
                rfcE = ResConsul.GetString(31).ToString();
                ultiFechAlert = ResConsul.GetDateTime(32).ToString("dd/MM/yyyy");
                //MessageBox.Show(ultiFechAlert);   
             

                
               
           }
            else
            {
                MessageBox.Show("La tabla licenciasRutasUsuariosCFDi no tiene datos");
            }

            
            

        }

        private string ReadLine(FileStream fileStream)
        {
            throw new NotImplementedException();
        }

        private object ReadLine()
        {
            throw new NotImplementedException();
        }


        //Metodo para limpiar las variables.
        private void limpiarVariablesTimbrado()
        {

            xmlTimbrado = "";
            resTimbre = "";
            strResultado = "";
            resultado = "";
            estructuraXML = "";
            strError = "";
            //txtXml1.Text = "";
            Num2Text = "";
            dec = "";
            res = "";
            entero = 0;
            decimales = 0;
            nro = 0;
            version= string.Empty;
            uuid= string.Empty;
            fechaTim= string.Empty;
            selloCFD= string.Empty;
            noCerti= string.Empty;
            selloSAT = string.Empty;
            
            
             
           

        }

        //Funicones para convertir los numeros a texto.
        public string enletras(string num)
        {
                        
            try
            {

                nro = Convert.ToDouble(num);

            }

            catch
            {

                return "";

            }

            entero = Convert.ToInt64(Math.Truncate(nro));

            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            if (decimales > 0)
            {

                dec = " PESOS " + decimales.ToString() + "/100 M.N";

            }
            if (decimales == 0) { dec = " PESOS " + decimales.ToString() + "/100 M.N"; }

            res = toText(Convert.ToDouble(entero)) + dec;

            return res;


        }
        private string toText(double value)
        {



            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";

            else if (value == 1) Num2Text = "UNO";

            else if (value == 2) Num2Text = "DOS";

            else if (value == 3) Num2Text = "TRES";

            else if (value == 4) Num2Text = "CUATRO";

            else if (value == 5) Num2Text = "CINCO";

            else if (value == 6) Num2Text = "SEIS";

            else if (value == 7) Num2Text = "SIETE";

            else if (value == 8) Num2Text = "OCHO";

            else if (value == 9) Num2Text = "NUEVE";

            else if (value == 10) Num2Text = "DIEZ";

            else if (value == 11) Num2Text = "ONCE";

            else if (value == 12) Num2Text = "DOCE";

            else if (value == 13) Num2Text = "TRECE";

            else if (value == 14) Num2Text = "CATORCE";

            else if (value == 15) Num2Text = "QUINCE";

            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);

            else if (value == 20) Num2Text = "VEINTE";

            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);

            else if (value == 30) Num2Text = "TREINTA";

            else if (value == 40) Num2Text = "CUARENTA";

            else if (value == 50) Num2Text = "CINCUENTA";

            else if (value == 60) Num2Text = "SESENTA";

            else if (value == 70) Num2Text = "SETENTA";

            else if (value == 80) Num2Text = "OCHENTA";

            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);

            else if (value == 100) Num2Text = "CIEN";

            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);

            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";

            else if (value == 700) Num2Text = "SETECIENTOS";

            else if (value == 900) Num2Text = "NOVECIENTOS";

            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);

            else if (value == 1000) Num2Text = "MIL";

            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);

            else if (value < 1000000)
            {

                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";

                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);

            }

            else if (value == 1000000) Num2Text = "UN MILLON";

            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);

            else if (value < 1000000000000)
            {

                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";

                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);

            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";

            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {

                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";

                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            }

            return Num2Text;


        }

        //14.-evento click para busacar carta porte
        private void btnConsultar_Click_1(object sender, EventArgs e)
        {
           
            bandera = true;
            datosDelphi = "";
            label.Clear();
            buscarCarta();
            txtContrasena.Focus();           
            
        }

        //15.-funcion para busacar carta porte
        private void buscarCarta() 
        {
           
           
            string estatusImp = "";
            if (rdbN.Checked) { estatusImp = "N"; }
            if (rdbS.Checked) { estatusImp = "S"; }

            if(datosDelphi!="")
            {
                                   
                    DataSet datosGridGeneral = new DataSet();
                    datosGridGeneral = BD.ConsulTab("CartaPorteCFDii", " WHERE Id_cp=" + datosDelphi, "Serie, Numero", this.conn);
                    gridCartPort.DataSource = datosGridGeneral.Tables["CartaPorteCFDii"];
                   
                    if (gridCartPort.RowCount == 0) { ocultarComponentes(); } else { llenarLabel(); btnTimbrar.Visible = true; mostrarComponentes(); }
                                   
            }

           
            if (txtFactura.Text != "" && txtSerie.Text != "")
            {

                DataSet datosGridGeneral = new DataSet();
                datosGridGeneral = BD.ConsulTab("CartaPorteCFDii", " WHERE Numero=" + txtFactura.Text + " AND Serie='" + txtSerie.Text + "' AND Impresion='" + estatusImp + "'", "Serie, Numero", this.conn);
                gridCartPort.DataSource = datosGridGeneral.Tables["CartaPorteCFDii"];
                if (gridCartPort.RowCount == 0) { ocultarComponentes(); } else { llenarLabel(); btnTimbrar.Visible = true; mostrarComponentes(); }

            }
        
        }

        private void viajeFacturaCP()
        {
           SqlDataReader ResConsulta;
           //MessageBox.Show(label[43].ToString());
           string sql = " select * from viajFactCP(" + label[43].ToString() + ");";
           ResConsulta = BD.Buscar(sql, this.conn);

                   if (ResConsulta.Read())
                   {
                       if (@ResConsulta.GetString(0).ToString() != "")
                       {
                           viajFactCP = "Facturas: <br>" + "Número Viaje: " + @ResConsulta.GetString(0).ToString() + ": " + @ResConsulta.GetString(1).ToString();

                           viajFactCP = "<table cellspacing='0' width='100%' border='0' style='font-family:Courier New, Courier, monospace;line-height:6pt;font-size:6px  !important;'><tr><td>" + viajFactCP + "</td></tr> </table>";
                       }
                       else {
                           viajFactCP = "";
                       }
                      // MessageBox.Show(viajFactCP);
                   }
        
        }
        //Cambiar a mayusculas las letras introducidas.
        private void txtSerie_TextChanged_1(object sender, EventArgs e)
        {
            txtSerie.CharacterCasing = CharacterCasing.Upper;
        }

        private void lblBuscar_Click(object sender, EventArgs e)
        {
            buscar("abrir");
        }

        private void buscar(string opcion)
        {
            if (opcion == "abrir")
            {
                this.ClientSize = new System.Drawing.Size(1005, 255);
            }
            else { this.ClientSize = new System.Drawing.Size(793, 255); }
        }

        private void txtCorreo_Click_1(object sender, EventArgs e)
        {
            if (txtCorreo.Text == "ESCRIBE AQUI EL CORREO")
            {
                txtCorreo.Text = "";
            }
        }

        private void chbXml_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chbPdf.Checked == false && chbXml.Checked == false) { lblPara.Visible = false; txtCorreo.Visible = false; } else { lblPara.Visible = true; txtCorreo.Visible = true; }
        }

        private void chbPdf_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chbPdf.Checked == false && chbXml.Checked == false) { lblPara.Visible = false; txtCorreo.Visible = false; } else { lblPara.Visible = true; txtCorreo.Visible = true; }
        }

        private void txtFactura_LostFocus_1(object sender, KeyPressEventArgs e)
        {
            bandera = true;
            datosDelphi = "";
            label.Clear();
            txtContrasena.Focus();
            buscarCarta();
        }
        private void txtFactura_LostFocus_1(object sender, EventArgs e)
        {
            if (txtSerie.Text != "" && txtFactura.Text != "")
            {
                bandera = true;
                datosDelphi = "";
                label.Clear();
                txtContrasena.Focus();
                buscarCarta();
            }
        }
        private void txtFactura_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46)
            {
                e.Handled = false;
            }else if(e.KeyChar==13)
            {
              
                bandera = true;
                datosDelphi = "";
                label.Clear();
                txtContrasena.Focus();
                buscarCarta();
            }

            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;

            }
        }
        int tamano;
        private void lblBuscar_Click_1(object sender, EventArgs e)
        {
            timbrar = false;
            
            if (buscarCP == true)
            {
                txtSerie.Focus();
                tamano = this.Size.Height;
                this.Size = new Size(this.Width, 380);
                groupBox3.Visible = true;
                buscarCP = false;
            }
            else {

                this.Size = new Size(this.Width,tamano);
                buscarCP = true;
                groupBox3.Visible = false;
                txtContrasena.Text = "";
            }
            
            
            
            //buscar("abrir");
        }

        private void CP_CFDi_MouseMove(object sender, MouseEventArgs e)
        {
            TopMost = true;
        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                //MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
  
        private void txtCorreo_KeyUp(object sender, KeyEventArgs e)
        {

           
            //MessageBox.Show(e.KeyValue.ToString());
            //if (e.KeyValue == 32) { }
            if (txtCorreo.Text != "" && txtCorreo.Text != "ESCRIBE AQUI EL CORREO" && e.KeyValue != 37 && e.KeyValue != 39 && e.KeyValue != 8 && e.KeyValue != 46 && e.KeyValue != 20 && e.KeyValue != 32 && e.KeyValue >= 65 && e.KeyValue <= 90)
            {
                //MessageBox.Show("no deberia");
                txtCorreo.CharacterCasing = CharacterCasing.Lower;
                txtCorreo.Select(txtCorreo.Text.Length, 0);                                         

            }
            

            

        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(e.KeyChar.ToString());
            if ((e.KeyChar == 32) || (e.KeyChar == '<') || (e.KeyChar == '>') || (e.KeyChar == '+') || (e.KeyChar == '"' && txtCorreo.Text.Contains("\"")) || (e.KeyChar == '\'' && txtCorreo.Text.Contains("'")) || (txtCorreo.Text=="" && e.KeyChar == '.' ))
            {
                e.Handled = true;
            }
            else if ((e.KeyChar == 64 && txtCorreo.Text.Contains("@")))
            {
                e.Handled = true;
            }
        }

        private void lblFechTimb_Click(object sender, EventArgs e)
        {
            if (cbFechTimb.Visible == false)
            {
                cbFechTimb.Visible = true;
            }
            else {
                cbFechTimb.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("smtp "+alertaTimbresCorreo+"\nUsuario "+usuarioCorreo+"\nde "+de+"\nAsunto "+asuntoCorreo+"\nPuerto "+puertoCorreo);
            //Configuración del Mensaje
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServe = new SmtpClient();
            SmtpServe.UseDefaultCredentials = true;
              
            
            //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
            mail.From = new MailAddress(usuarioCorreo, de, Encoding.UTF8);

            //Aquí ponemos el asunto del correo
            mail.Subject = "El correo se envio satisfactoriamente desde C#";
                      
            //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
            mail.To.Add(alertaTimbresCorreo);

            //Configuracion del SMTP
            SmtpServe.Port = puertoCorreo; //Puerto que utiliza Gmail para sus servicios
            SmtpServe.Host = smtp;
            //Especificamos las credenciales con las que enviaremos el mail
            SmtpServe.Credentials = new NetworkCredential(usuarioCorreo,contrasenaCorreo);
            SmtpServe.EnableSsl = false;
            try
            {
                SmtpServe.Send(mail);
            }
            catch (Exception e1) {
                MessageBox.Show("Problemas para enviar el correo \n" + e1, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SmtpServe.Dispose();
        }

        private void ayuda_lbl_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1060, this.Height);
        }

     

      

     

      

      

        
       

       
       

       

      

       





       
    }
}
