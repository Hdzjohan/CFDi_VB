﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.1026
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.1026.
// 
#pragma warning disable 1591

namespace CP_CFDi.WSTimbrado {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="TimbradoCFDServiceExternalSoap", Namespace="http://tempuri.org/")]
    public partial class TimbradoCFDServiceExternal : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback TimbradoCFDStrOperationCompleted;
        
        private System.Threading.SendOrPostCallback TimbradoCFDStrXMLAcuseOperationCompleted;
        
        private System.Threading.SendOrPostCallback TimbradoCFDStrXMLOperationCompleted;
        
        private System.Threading.SendOrPostCallback TimbradoCFDXmlOperationCompleted;
        
        private System.Threading.SendOrPostCallback TimbradoCFDXmlAcuseOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetEstatuUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback TrackingBalanceOperationCompleted;
        
        private System.Threading.SendOrPostCallback EnviarXmlCancelacionCFDIUsrOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public TimbradoCFDServiceExternal() {
            this.Url = global::CP_CFDi.Properties.Settings.Default.CP_CFDi_WSTimbrado_TimbradoCFDServiceExternal;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event TimbradoCFDStrCompletedEventHandler TimbradoCFDStrCompleted;
        
        /// <remarks/>
        public event TimbradoCFDStrXMLAcuseCompletedEventHandler TimbradoCFDStrXMLAcuseCompleted;
        
        /// <remarks/>
        public event TimbradoCFDStrXMLCompletedEventHandler TimbradoCFDStrXMLCompleted;
        
        /// <remarks/>
        public event TimbradoCFDXmlCompletedEventHandler TimbradoCFDXmlCompleted;
        
        /// <remarks/>
        public event TimbradoCFDXmlAcuseCompletedEventHandler TimbradoCFDXmlAcuseCompleted;
        
        /// <remarks/>
        public event GetEstatuUserCompletedEventHandler GetEstatuUserCompleted;
        
        /// <remarks/>
        public event TrackingBalanceCompletedEventHandler TrackingBalanceCompleted;
        
        /// <remarks/>
        public event EnviarXmlCancelacionCFDIUsrCompletedEventHandler EnviarXmlCancelacionCFDIUsrCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/TimbradoCFDStr", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string TimbradoCFDStr(string strXML, string strUser, string strPass) {
            object[] results = this.Invoke("TimbradoCFDStr", new object[] {
                        strXML,
                        strUser,
                        strPass});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TimbradoCFDStrAsync(string strXML, string strUser, string strPass) {
            this.TimbradoCFDStrAsync(strXML, strUser, strPass, null);
        }
        
        /// <remarks/>
        public void TimbradoCFDStrAsync(string strXML, string strUser, string strPass, object userState) {
            if ((this.TimbradoCFDStrOperationCompleted == null)) {
                this.TimbradoCFDStrOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTimbradoCFDStrOperationCompleted);
            }
            this.InvokeAsync("TimbradoCFDStr", new object[] {
                        strXML,
                        strUser,
                        strPass}, this.TimbradoCFDStrOperationCompleted, userState);
        }
        
        private void OnTimbradoCFDStrOperationCompleted(object arg) {
            if ((this.TimbradoCFDStrCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TimbradoCFDStrCompleted(this, new TimbradoCFDStrCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/TimbradoCFDStrXMLAcuse", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public RespuestaTimbradoStr TimbradoCFDStrXMLAcuse(string strXML, string strUser, string strPass) {
            object[] results = this.Invoke("TimbradoCFDStrXMLAcuse", new object[] {
                        strXML,
                        strUser,
                        strPass});
            return ((RespuestaTimbradoStr)(results[0]));
        }
        
        /// <remarks/>
        public void TimbradoCFDStrXMLAcuseAsync(string strXML, string strUser, string strPass) {
            this.TimbradoCFDStrXMLAcuseAsync(strXML, strUser, strPass, null);
        }
        
        /// <remarks/>
        public void TimbradoCFDStrXMLAcuseAsync(string strXML, string strUser, string strPass, object userState) {
            if ((this.TimbradoCFDStrXMLAcuseOperationCompleted == null)) {
                this.TimbradoCFDStrXMLAcuseOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTimbradoCFDStrXMLAcuseOperationCompleted);
            }
            this.InvokeAsync("TimbradoCFDStrXMLAcuse", new object[] {
                        strXML,
                        strUser,
                        strPass}, this.TimbradoCFDStrXMLAcuseOperationCompleted, userState);
        }
        
        private void OnTimbradoCFDStrXMLAcuseOperationCompleted(object arg) {
            if ((this.TimbradoCFDStrXMLAcuseCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TimbradoCFDStrXMLAcuseCompleted(this, new TimbradoCFDStrXMLAcuseCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/TimbradoCFDStrXML", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string TimbradoCFDStrXML(string strXML, string strUser, string strPass) {
            object[] results = this.Invoke("TimbradoCFDStrXML", new object[] {
                        strXML,
                        strUser,
                        strPass});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TimbradoCFDStrXMLAsync(string strXML, string strUser, string strPass) {
            this.TimbradoCFDStrXMLAsync(strXML, strUser, strPass, null);
        }
        
        /// <remarks/>
        public void TimbradoCFDStrXMLAsync(string strXML, string strUser, string strPass, object userState) {
            if ((this.TimbradoCFDStrXMLOperationCompleted == null)) {
                this.TimbradoCFDStrXMLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTimbradoCFDStrXMLOperationCompleted);
            }
            this.InvokeAsync("TimbradoCFDStrXML", new object[] {
                        strXML,
                        strUser,
                        strPass}, this.TimbradoCFDStrXMLOperationCompleted, userState);
        }
        
        private void OnTimbradoCFDStrXMLOperationCompleted(object arg) {
            if ((this.TimbradoCFDStrXMLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TimbradoCFDStrXMLCompleted(this, new TimbradoCFDStrXMLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/TimbradoCFDXml", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode TimbradoCFDXml(System.Xml.XmlNode xmldTimbre, string strUser, string strPass) {
            object[] results = this.Invoke("TimbradoCFDXml", new object[] {
                        xmldTimbre,
                        strUser,
                        strPass});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void TimbradoCFDXmlAsync(System.Xml.XmlNode xmldTimbre, string strUser, string strPass) {
            this.TimbradoCFDXmlAsync(xmldTimbre, strUser, strPass, null);
        }
        
        /// <remarks/>
        public void TimbradoCFDXmlAsync(System.Xml.XmlNode xmldTimbre, string strUser, string strPass, object userState) {
            if ((this.TimbradoCFDXmlOperationCompleted == null)) {
                this.TimbradoCFDXmlOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTimbradoCFDXmlOperationCompleted);
            }
            this.InvokeAsync("TimbradoCFDXml", new object[] {
                        xmldTimbre,
                        strUser,
                        strPass}, this.TimbradoCFDXmlOperationCompleted, userState);
        }
        
        private void OnTimbradoCFDXmlOperationCompleted(object arg) {
            if ((this.TimbradoCFDXmlCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TimbradoCFDXmlCompleted(this, new TimbradoCFDXmlCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/TimbradoCFDXmlAcuse", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public RespuestaTimbrado TimbradoCFDXmlAcuse(System.Xml.XmlNode xmldTimbre, string strUser, string strPass) {
            object[] results = this.Invoke("TimbradoCFDXmlAcuse", new object[] {
                        xmldTimbre,
                        strUser,
                        strPass});
            return ((RespuestaTimbrado)(results[0]));
        }
        
        /// <remarks/>
        public void TimbradoCFDXmlAcuseAsync(System.Xml.XmlNode xmldTimbre, string strUser, string strPass) {
            this.TimbradoCFDXmlAcuseAsync(xmldTimbre, strUser, strPass, null);
        }
        
        /// <remarks/>
        public void TimbradoCFDXmlAcuseAsync(System.Xml.XmlNode xmldTimbre, string strUser, string strPass, object userState) {
            if ((this.TimbradoCFDXmlAcuseOperationCompleted == null)) {
                this.TimbradoCFDXmlAcuseOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTimbradoCFDXmlAcuseOperationCompleted);
            }
            this.InvokeAsync("TimbradoCFDXmlAcuse", new object[] {
                        xmldTimbre,
                        strUser,
                        strPass}, this.TimbradoCFDXmlAcuseOperationCompleted, userState);
        }
        
        private void OnTimbradoCFDXmlAcuseOperationCompleted(object arg) {
            if ((this.TimbradoCFDXmlAcuseCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TimbradoCFDXmlAcuseCompleted(this, new TimbradoCFDXmlAcuseCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetEstatuUser", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetEstatuUser(string strUser, string strPass) {
            object[] results = this.Invoke("GetEstatuUser", new object[] {
                        strUser,
                        strPass});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetEstatuUserAsync(string strUser, string strPass) {
            this.GetEstatuUserAsync(strUser, strPass, null);
        }
        
        /// <remarks/>
        public void GetEstatuUserAsync(string strUser, string strPass, object userState) {
            if ((this.GetEstatuUserOperationCompleted == null)) {
                this.GetEstatuUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetEstatuUserOperationCompleted);
            }
            this.InvokeAsync("GetEstatuUser", new object[] {
                        strUser,
                        strPass}, this.GetEstatuUserOperationCompleted, userState);
        }
        
        private void OnGetEstatuUserOperationCompleted(object arg) {
            if ((this.GetEstatuUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetEstatuUserCompleted(this, new GetEstatuUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/TrackingBalance", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string TrackingBalance(int idEmpresa) {
            object[] results = this.Invoke("TrackingBalance", new object[] {
                        idEmpresa});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TrackingBalanceAsync(int idEmpresa) {
            this.TrackingBalanceAsync(idEmpresa, null);
        }
        
        /// <remarks/>
        public void TrackingBalanceAsync(int idEmpresa, object userState) {
            if ((this.TrackingBalanceOperationCompleted == null)) {
                this.TrackingBalanceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTrackingBalanceOperationCompleted);
            }
            this.InvokeAsync("TrackingBalance", new object[] {
                        idEmpresa}, this.TrackingBalanceOperationCompleted, userState);
        }
        
        private void OnTrackingBalanceOperationCompleted(object arg) {
            if ((this.TrackingBalanceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TrackingBalanceCompleted(this, new TrackingBalanceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EnviarXmlCancelacionCFDIUsr", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string EnviarXmlCancelacionCFDIUsr([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] xmlCancelacion, string strUser, string strPwd) {
            object[] results = this.Invoke("EnviarXmlCancelacionCFDIUsr", new object[] {
                        xmlCancelacion,
                        strUser,
                        strPwd});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void EnviarXmlCancelacionCFDIUsrAsync(byte[] xmlCancelacion, string strUser, string strPwd) {
            this.EnviarXmlCancelacionCFDIUsrAsync(xmlCancelacion, strUser, strPwd, null);
        }
        
        /// <remarks/>
        public void EnviarXmlCancelacionCFDIUsrAsync(byte[] xmlCancelacion, string strUser, string strPwd, object userState) {
            if ((this.EnviarXmlCancelacionCFDIUsrOperationCompleted == null)) {
                this.EnviarXmlCancelacionCFDIUsrOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarXmlCancelacionCFDIUsrOperationCompleted);
            }
            this.InvokeAsync("EnviarXmlCancelacionCFDIUsr", new object[] {
                        xmlCancelacion,
                        strUser,
                        strPwd}, this.EnviarXmlCancelacionCFDIUsrOperationCompleted, userState);
        }
        
        private void OnEnviarXmlCancelacionCFDIUsrOperationCompleted(object arg) {
            if ((this.EnviarXmlCancelacionCFDIUsrCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarXmlCancelacionCFDIUsrCompleted(this, new EnviarXmlCancelacionCFDIUsrCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1026")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class RespuestaTimbradoStr {
        
        private string statusEnvioField;
        
        private string cfdiField;
        
        private string acuseEnvioField;
        
        /// <comentarios/>
        public string StatusEnvio {
            get {
                return this.statusEnvioField;
            }
            set {
                this.statusEnvioField = value;
            }
        }
        
        /// <comentarios/>
        public string Cfdi {
            get {
                return this.cfdiField;
            }
            set {
                this.cfdiField = value;
            }
        }
        
        /// <comentarios/>
        public string AcuseEnvio {
            get {
                return this.acuseEnvioField;
            }
            set {
                this.acuseEnvioField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1026")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class RespuestaTimbrado {
        
        private string statusEnvioField;
        
        private System.Xml.XmlNode cfdiField;
        
        private System.Xml.XmlNode acuseEnvioField;
        
        /// <comentarios/>
        public string StatusEnvio {
            get {
                return this.statusEnvioField;
            }
            set {
                this.statusEnvioField = value;
            }
        }
        
        /// <comentarios/>
        public System.Xml.XmlNode Cfdi {
            get {
                return this.cfdiField;
            }
            set {
                this.cfdiField = value;
            }
        }
        
        /// <comentarios/>
        public System.Xml.XmlNode AcuseEnvio {
            get {
                return this.acuseEnvioField;
            }
            set {
                this.acuseEnvioField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void TimbradoCFDStrCompletedEventHandler(object sender, TimbradoCFDStrCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TimbradoCFDStrCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TimbradoCFDStrCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void TimbradoCFDStrXMLAcuseCompletedEventHandler(object sender, TimbradoCFDStrXMLAcuseCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TimbradoCFDStrXMLAcuseCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TimbradoCFDStrXMLAcuseCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public RespuestaTimbradoStr Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((RespuestaTimbradoStr)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void TimbradoCFDStrXMLCompletedEventHandler(object sender, TimbradoCFDStrXMLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TimbradoCFDStrXMLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TimbradoCFDStrXMLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void TimbradoCFDXmlCompletedEventHandler(object sender, TimbradoCFDXmlCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TimbradoCFDXmlCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TimbradoCFDXmlCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void TimbradoCFDXmlAcuseCompletedEventHandler(object sender, TimbradoCFDXmlAcuseCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TimbradoCFDXmlAcuseCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TimbradoCFDXmlAcuseCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public RespuestaTimbrado Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((RespuestaTimbrado)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetEstatuUserCompletedEventHandler(object sender, GetEstatuUserCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetEstatuUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetEstatuUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void TrackingBalanceCompletedEventHandler(object sender, TrackingBalanceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TrackingBalanceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TrackingBalanceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void EnviarXmlCancelacionCFDIUsrCompletedEventHandler(object sender, EnviarXmlCancelacionCFDIUsrCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnviarXmlCancelacionCFDIUsrCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnviarXmlCancelacionCFDIUsrCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591