//------------------------------------------------------------------------------
// <自动生成>
//     此代码由工具生成。
//     //
//     对此文件的更改可能导致不正确的行为，并在以下条件下丢失:
//     代码重新生成。
// </自动生成>
//------------------------------------------------------------------------------

namespace MachineTranslationService
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MachineTranslationParameter", Namespace="http://schemas.datacontract.org/2004/07/TranslationWcfService")]
    public partial class MachineTranslationParameter : object
    {
        
        private MachineTranslationService.LanguageOptions LanguageOptionsField;
        
        private string OriginalTextField;
        
        private MachineTranslationService.TranslationClass TranslationClassField;
        
        private string UserIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MachineTranslationService.LanguageOptions LanguageOptions
        {
            get
            {
                return this.LanguageOptionsField;
            }
            set
            {
                this.LanguageOptionsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OriginalText
        {
            get
            {
                return this.OriginalTextField;
            }
            set
            {
                this.OriginalTextField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MachineTranslationService.TranslationClass TranslationClass
        {
            get
            {
                return this.TranslationClassField;
            }
            set
            {
                this.TranslationClassField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserId
        {
            get
            {
                return this.UserIdField;
            }
            set
            {
                this.UserIdField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LanguageOptions", Namespace="http://schemas.datacontract.org/2004/07/TranslationWcfService")]
    public partial class LanguageOptions : object
    {
        
        private MachineTranslationService.LanguageOption SourceField;
        
        private MachineTranslationService.LanguageOption TargetField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MachineTranslationService.LanguageOption Source
        {
            get
            {
                return this.SourceField;
            }
            set
            {
                this.SourceField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MachineTranslationService.LanguageOption Target
        {
            get
            {
                return this.TargetField;
            }
            set
            {
                this.TargetField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TranslationClass", Namespace="http://schemas.datacontract.org/2004/07/TranslationWcfService")]
    public enum TranslationClass : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        A = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        B = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        C = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        D = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        E = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        F = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        G = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        H = 7,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LanguageOption", Namespace="http://schemas.datacontract.org/2004/07/TranslationWcfService")]
    public enum LanguageOption : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        en = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        zh = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        jp = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ko = 3,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MachineTranslationResult", Namespace="http://schemas.datacontract.org/2004/07/TranslationWcfService")]
    public partial class MachineTranslationResult : object
    {
        
        private string OrderedResultField;
        
        private string OriginalTextField;
        
        private string UnorderedResultField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrderedResult
        {
            get
            {
                return this.OrderedResultField;
            }
            set
            {
                this.OrderedResultField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OriginalText
        {
            get
            {
                return this.OriginalTextField;
            }
            set
            {
                this.OriginalTextField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UnorderedResult
        {
            get
            {
                return this.UnorderedResultField;
            }
            set
            {
                this.UnorderedResultField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MachineTranslationService.IMachineTranslationService")]
    public interface IMachineTranslationService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMachineTranslationService/Translate", ReplyAction="http://tempuri.org/IMachineTranslationService/TranslateResponse")]
        System.Threading.Tasks.Task<MachineTranslationService.MachineTranslationResult> TranslateAsync(MachineTranslationService.MachineTranslationParameter translationParameter);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface IMachineTranslationServiceChannel : MachineTranslationService.IMachineTranslationService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class MachineTranslationServiceClient : System.ServiceModel.ClientBase<MachineTranslationService.IMachineTranslationService>, MachineTranslationService.IMachineTranslationService
    {
        
    /// <summary>
    /// 实现此分部方法，配置服务终结点。
    /// </summary>
    /// <param name="serviceEndpoint">要配置的终结点</param>
    /// <param name="clientCredentials">客户端凭据</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public MachineTranslationServiceClient() : 
                base(MachineTranslationServiceClient.GetDefaultBinding(), MachineTranslationServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IMachineTranslationService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MachineTranslationServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(MachineTranslationServiceClient.GetBindingForEndpoint(endpointConfiguration), MachineTranslationServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MachineTranslationServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(MachineTranslationServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MachineTranslationServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(MachineTranslationServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MachineTranslationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<MachineTranslationService.MachineTranslationResult> TranslateAsync(MachineTranslationService.MachineTranslationParameter translationParameter)
        {
            return base.Channel.TranslateAsync(translationParameter);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IMachineTranslationService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IMachineTranslationService))
            {
                return new System.ServiceModel.EndpointAddress("http://10.15.11.114:8082/MachineTranslationService/services/MachineTranslationSer" +
                        "vice/");
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return MachineTranslationServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IMachineTranslationService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return MachineTranslationServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IMachineTranslationService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IMachineTranslationService,
        }
    }
}
