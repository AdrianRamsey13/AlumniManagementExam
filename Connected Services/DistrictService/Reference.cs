﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlumniManagement.DistrictService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DistrictDTO", Namespace="http://schemas.datacontract.org/2004/07/AlumniWCF.DTO")]
    [System.SerializableAttribute()]
    public partial class DistrictDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int DistrictIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DistrictNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StateIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int DistrictID {
            get {
                return this.DistrictIDField;
            }
            set {
                if ((this.DistrictIDField.Equals(value) != true)) {
                    this.DistrictIDField = value;
                    this.RaisePropertyChanged("DistrictID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DistrictName {
            get {
                return this.DistrictNameField;
            }
            set {
                if ((object.ReferenceEquals(this.DistrictNameField, value) != true)) {
                    this.DistrictNameField = value;
                    this.RaisePropertyChanged("DistrictName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StateID {
            get {
                return this.StateIDField;
            }
            set {
                if ((this.StateIDField.Equals(value) != true)) {
                    this.StateIDField = value;
                    this.RaisePropertyChanged("StateID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StateName {
            get {
                return this.StateNameField;
            }
            set {
                if ((object.ReferenceEquals(this.StateNameField, value) != true)) {
                    this.StateNameField = value;
                    this.RaisePropertyChanged("StateName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DistrictService.IDistrictService")]
    public interface IDistrictService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDistrictService/GetDistrictByStateID", ReplyAction="http://tempuri.org/IDistrictService/GetDistrictByStateIDResponse")]
        AlumniManagement.DistrictService.DistrictDTO[] GetDistrictByStateID(int stateID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDistrictService/GetDistrictByStateID", ReplyAction="http://tempuri.org/IDistrictService/GetDistrictByStateIDResponse")]
        System.Threading.Tasks.Task<AlumniManagement.DistrictService.DistrictDTO[]> GetDistrictByStateIDAsync(int stateID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDistrictServiceChannel : AlumniManagement.DistrictService.IDistrictService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DistrictServiceClient : System.ServiceModel.ClientBase<AlumniManagement.DistrictService.IDistrictService>, AlumniManagement.DistrictService.IDistrictService {
        
        public DistrictServiceClient() {
        }
        
        public DistrictServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DistrictServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DistrictServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DistrictServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AlumniManagement.DistrictService.DistrictDTO[] GetDistrictByStateID(int stateID) {
            return base.Channel.GetDistrictByStateID(stateID);
        }
        
        public System.Threading.Tasks.Task<AlumniManagement.DistrictService.DistrictDTO[]> GetDistrictByStateIDAsync(int stateID) {
            return base.Channel.GetDistrictByStateIDAsync(stateID);
        }
    }
}
