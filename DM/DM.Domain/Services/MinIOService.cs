using AutoMapper;
using DM.Common.Helpers;
using DM.DAL.Entities;
using DM.DAL.Interfaces;
using DM.Domain.DTO;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel;
using offline_module.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace offline_module.Domain.Services
{
    public class MinIOService : IMinIOService
    {
        public readonly string endPoint;
        public readonly string accessKey;
        public readonly string secretKey;
        public MinioClient minio;
        public readonly string bucketName;
        public readonly int port;
        public readonly bool IsMinIoEndPointSecure;

        private IUnitOfWork Context { get; set; }
        private readonly IMapper _mapper;

        public MinIOService(IConfiguration configuration, IUnitOfWork Context, IMapper mapper)
        {
            var minioConfig = configuration.GetSection("MinIoConfig");

            this.endPoint = minioConfig["EndPoint"];
            this.accessKey = minioConfig["AccessKey"];
            this.secretKey = minioConfig["SecretKey"];
            this.bucketName = minioConfig["BucketName"];
            this.IsMinIoEndPointSecure = Convert.ToBoolean(minioConfig["IsSecure"]);
            this.port = Convert.ToInt32(minioConfig["port"]);
            _mapper = mapper;
            this.Context = Context;

            Initialize();
        }

        private void Initialize()
        {
            try
            {
                this.minio = new MinioClient()
                   .WithEndpoint(endPoint, port)
                   .WithCredentials(accessKey, secretKey)
                   .WithRegion("us-east-1") // doesn't work without it
                   .WithSSL(false)
                   .Build();
            }
            catch 
            {
                throw;
            }
        }

        public async Task<int> UploadItems(int userId, IEnumerable<int> itemIds)
        {
            foreach (var id in itemIds)
            {
                var itemFromDb = Context.Items.GetById(id);

                var mappedItem = _mapper.Map<ItemDto>(itemFromDb);
                var isComplete = await UploadItem(mappedItem);
            }

            return userId;
        }

        public async Task<int> DownloadItems(int userId, IEnumerable<int> itemIds)
        {
            foreach (var id in itemIds)
            {
                var itemFromDb = Context.Items.GetById(id);

                var mappedItem = _mapper.Map<ItemDto>(itemFromDb);
                var isComplete = await DownloadItem(mappedItem);
            }

            return userId;
        }

        private async Task<bool> DownloadItem(ItemDto item)
        {
            using var fileStream = new MemoryStream();

            var project = Context.Projects.GetById(item.ProjectId);
            var objectName = Path.GetFileName(item.Name);

            var getObjectArgs = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(fileStream));

            try
            {
                await minio.GetObjectAsync(getObjectArgs).ConfigureAwait(false);

                // Вернуть указатель позиции в начало потока
                fileStream.Position = 0;

                var localFilePath = GetFolderPath(item, project);
                File.WriteAllBytes(localFilePath, fileStream.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<bool> UploadItem(ItemDto item)
        {


            /*var location = "us-east-1";*/
            var project = Context.Projects.GetById(item.ProjectId);


            var objectName = Path.GetFileName(item.Name);
            var filePath = GetFolderPath(item, project);
            //TODO: Content type
            var contentType = GetContentType(item.Name);

            try
            {
                // Make a bucket on the server, if not already present.
                // PS: bucket name cannot have uppercase in name.
                // if bucket has cyrillic symbols, minio throws error. So do something

                var putObjectArgs = new PutObjectArgs()
                   .WithBucket(bucketName)
                   .WithObject(objectName)
                   .WithFileName(filePath)
                   .WithContentType(contentType);

                await minio.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                Console.WriteLine("Successfully uploaded " + objectName);
                return true;
            }
            catch
            {
                throw new Exception("Exception");
            }
        }

        private string GetFolderPath(ItemDto item, Project project)
        {
            var userProfileDirectory = FilesystemUtility.Database;

            var res = userProfileDirectory +
                "\\" +
                project.Title +
                "\\" +
                item.RelativePath;

            return res;
        }

        private string GetContentType(string fileName)
        {
            var ext = Path.GetExtension(fileName);

            var res = FileHelper.GetFileTypes(ext);

            return res;
        }

        // TODO
        static string ConvertToLatin(string input)
        {
            Encoding srcEncoding = Encoding.GetEncoding("windows-1251");
            Encoding destEncoding = Encoding.GetEncoding("iso-8859-1");
            byte[] srcBytes = srcEncoding.GetBytes(input);
            byte[] destBytes = Encoding.Convert(srcEncoding, destEncoding, srcBytes);
            char[] destChars = new char[destEncoding.GetCharCount(destBytes, 0, destBytes.Length)];
            destEncoding.GetChars(destBytes, 0, destBytes.Length, destChars, 0);
            return new string(destChars);
        }

        private async Task<string> CreateBucket(string bucketName)
        {
            var beArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);

            bool found = await minio.BucketExistsAsync(beArgs).ConfigureAwait(false);
            if (!found)
            {
                var mbArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await minio.MakeBucketAsync(mbArgs).ConfigureAwait(false);
            }
            return bucketName;
        }
    }
}
