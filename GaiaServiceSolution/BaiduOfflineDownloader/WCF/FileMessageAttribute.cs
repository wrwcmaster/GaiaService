using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.WCF
{
    class FileMessageAttribute : Attribute, IOperationBehavior
    {
        string _fileBlockName;
        public FileMessageAttribute(string fileBlockName)
        {
            _fileBlockName = fileBlockName;
        }

        public void AddBindingParameters(OperationDescription operationDescription, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.ClientOperation clientOperation)
        {
            clientOperation.Formatter = new FileMessageFormatter(clientOperation.Formatter, _fileBlockName);
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {

        }

        public void Validate(OperationDescription operationDescription)
        {

        }
    }
}
