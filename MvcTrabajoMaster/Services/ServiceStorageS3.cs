﻿using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTrabajoMaster.Services
{
    public class ServiceStorageS3
    {

        private string bucketName;
        private IAmazonS3 awsClient;

        public ServiceStorageS3(IAmazonS3 client, IConfiguration configuration)
        {

            this.awsClient = client;
            this.bucketName = configuration.GetValue<string>("AWS:bucket");
        }

        //Es importante este metodo ya que, aunque insertemos la url en el método InsertarDoctor debemos subirlo al bucket

        public async Task<bool> UploadFileAsync(Stream stream, string fileName)
        {

            PutObjectRequest request = new PutObjectRequest
            {
                InputStream = stream,
                Key = fileName,
                BucketName = this.bucketName
            };

            PutObjectResponse response = await this.awsClient.PutObjectAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //En el momento de modificar, eliminamos la imagen que tuviese anteriormente 

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            DeleteObjectResponse response = await this.awsClient.DeleteObjectAsync(this.bucketName, fileName);


            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Para buscar el archivo
        public async Task<Stream> GetFileAsync(string fileName)
        {
            GetObjectResponse response = await this.awsClient.GetObjectAsync(this.bucketName, fileName);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.ResponseStream;
            }
            else
            {
                return null;
            }
        }
    }
}
