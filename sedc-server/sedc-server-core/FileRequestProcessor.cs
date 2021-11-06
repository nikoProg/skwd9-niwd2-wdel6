using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    public class FileRequestProcessor : IRequestProcessor
    {
        private string[] textExtensions = new[] { ".html", ".txt" };

        public string BasePath { get; private set; }

        public FileRequestProcessor(string basePath)
        {
            if (!Directory.Exists(basePath)) 
            {
                throw new ApplicationException($"Folder {basePath} does not exist. Please create it before starting the server");
            }
            BasePath = basePath;
        }
        public IResponse Process(Request request)
        {
            if (request.Address.Path.Count == 0)
            {
                return new TextResponse
                {
                    Status = Status.BadRequest
                };
            }
            //string filename = Path.Combine(BasePath, request.Address.Path[0]);
            //string filename = Path.Combine(BasePath, Path.Combine((string[])request.Address.Path));

            string filePath = Path.Combine(request.Address.Path.ToArray());
            string filename = Path.Combine(BasePath, filePath);
            if (!File.Exists(filename))
            {
                return new TextResponse
                {
                    Status = Status.NotFound
                };
            }

            var extension = Path.GetExtension(filename);
                 
            if (textExtensions.Contains(extension))
            {
                var output = File.ReadAllText(filename);
                return new TextResponse
                {
                    Message = output
                };
            } else
            {
                var output = File.ReadAllBytes(filename);
                return new BinaryResponse
                {
                    Message = output
                };
            }

        }
    }
}
