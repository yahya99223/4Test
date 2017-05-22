using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WebApp.Models
{
    public class UserAttachment
    {

    }

    public class UserAttachmentStore
    {
        private readonly CloudBlobClient client;
        private readonly Uri baseUri = new Uri(ConfigurationManager.AppSettings["StorageBaseUri"]);
        public UserAttachmentStore()
        {
            client = new CloudBlobClient(baseUri, new StorageCredentials(ConfigurationManager.AppSettings["StorageAccountName"], ConfigurationManager.AppSettings["StorageKey"]));
        }

        public async Task<string> SaveImage(Stream inputStream)
        {
            var container = client.GetContainerReference("images");
            var id = Guid.NewGuid().ToString("N");
            var blob = container.GetBlockBlobReference(id);
            await blob.UploadFromStreamAsync(inputStream);
            return id;
        }

        public Uri UriFor(string imageId)
        {
            var container = client.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(imageId);

            var sharedAccessSignaturePolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.Now.AddMinutes(-10),
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(3)
            };
            var sharedAccessSignatureToken = blob.GetSharedAccessSignature(sharedAccessSignaturePolicy);

            return new Uri(baseUri, $"/images/{imageId}{sharedAccessSignatureToken}");
        }
    }
}