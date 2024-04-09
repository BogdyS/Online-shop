using Amazon.S3;
using Amazon.S3.Model;
using Core.Options;
using Microsoft.Extensions.Options;
using Shop.Services.Interfaces;

namespace Shop.Services.AWS
{
    public class S3ItemsService : IS3Service
    {
        private readonly ItemsBucketOptions _options;
        private readonly IAmazonS3 _client;

        public S3ItemsService(
            IAmazonS3 client,
            IOptions<ItemsBucketOptions> options)
        {
            _client = client;
            _options = options.Value;
        }

        public async Task<string> GetFileUrl(string? key)
        {
            if(key == null)
            {
                return null;
            }

            var fileExistsRequst = new GetObjectMetadataRequest
            {
                BucketName = _options.BucketName,
                Key = key
            };

            var result = true;
            try
            {
                await _client.GetObjectMetadataAsync(fileExistsRequst);
            }
            catch
            {
                result = false;
            }

            if (result)
            {
                return $"{_options.BaseUrl}/{key}";
            }

            throw new ApplicationException("Object not found in storage");
        }

        public async Task UploadFile(Stream fileStream, string key)
        {
            var uploadFileRequest = new PutObjectRequest
            {
                AutoCloseStream = true,
                InputStream = fileStream,
                BucketName = _options.BucketName,
                Key = key
            };

            await _client.PutObjectAsync(uploadFileRequest);
        }
    }
}
