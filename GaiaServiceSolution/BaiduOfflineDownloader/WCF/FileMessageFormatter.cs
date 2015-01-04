using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace BaiduOfflineDownloader.WCF
{
    class FileMessageFormatter : IClientMessageFormatter
    {
        private IClientMessageFormatter _defaultFormatter;
        private string _boundary;
        private string _fileBlockName;
        public FileMessageFormatter(IClientMessageFormatter originalFormatter, string fileBlockName)
        {
            this._defaultFormatter = originalFormatter;
            _boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            _fileBlockName = fileBlockName;
        }

        public object DeserializeReply(Message message, object[] parameters)
        {
            return this._defaultFormatter.DeserializeReply(message, parameters);
        }

        public Message SerializeRequest(MessageVersion messageVersion, object[] parameters)
        {
            //TODO: support multiple files
            if (parameters.Length > 0 && parameters[0] is FileStream)
            {
                using (FileStream fs = parameters[0] as FileStream)
                {
                    MemoryStream outStream = new MemoryStream();

                    StreamWriter writer = new StreamWriter(outStream);
                    writer.WriteLine("--" + _boundary);
                    writer.WriteLine("Content-Disposition: form-data; name=\"" + _fileBlockName + "\"; filename=\"" + fs.Name + "\"");
                    writer.WriteLine("Content-Type: application/octet-stream");
                    writer.WriteLine();
                    writer.Flush();
                    fs.CopyTo(outStream);
                    writer.WriteLine();
                    writer.WriteLine("--" + _boundary + "--");
                    writer.Flush();

                    outStream.Seek(0, SeekOrigin.Begin);
                    parameters[0] = outStream;

                    Message request = this._defaultFormatter.SerializeRequest(messageVersion, parameters);

                    if (!request.Properties.ContainsKey(HttpRequestMessageProperty.Name))
                    {
                        request.Properties[HttpRequestMessageProperty.Name] = new HttpRequestMessageProperty();
                    }
                    HttpRequestMessageProperty http = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                    http.Headers.Set(HttpRequestHeader.ContentType, "multipart/form-data; boundary=" + _boundary);
                    http.Headers.Remove(HttpRequestHeader.Expect);
                    http.Headers.Set(HttpRequestHeader.ContentLength, outStream.Length.ToString());
                    return request;

                }
            }
            else
            {
                return this._defaultFormatter.SerializeRequest(messageVersion, parameters);
            }
        }
    }
}
