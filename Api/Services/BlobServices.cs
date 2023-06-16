using Azure.Storage.Blobs;
using PosgramAPI.Models.Dto;

namespace Api.Services
{
    public interface IBlobServices
    {
        public Task<string> GetBlob(string imageName);
        public Task<string> CreateBlob(PostagemDto newPost);
        public Task DeleteBlob(string imageName);
    }

    public class BlobServices : IBlobServices
    {
        private BlobContainerClient containerClient;
        private readonly IConfiguration _configuration;

        public BlobServices(IConfiguration configuration)
        {
            _configuration = configuration;
            var azStorageConn = configuration.GetValue<string>("AzureConnectionStrings:StorageAccountConnection");
            var blobServiceClient = new BlobServiceClient(azStorageConn);
            containerClient = blobServiceClient.GetBlobContainerClient("sc-postechchallenge1");
        }

        public async Task<string> CreateBlob(PostagemDto newPost)
        {
            var nomeImagem = BuildBlobImagem(newPost);
            BlobClient blobClient = containerClient.GetBlobClient(nomeImagem);
            var binaryData = Base64Service.ConvertFromBase64(newPost.Imagem);
            await blobClient.UploadAsync(binaryData, true);

            if (!await blobClient.ExistsAsync()) throw new Exception("Blob was not created");
            return nomeImagem;
        }

        public async Task DeleteBlob(string imageName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(imageName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> GetBlob(string imageName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(imageName);
            var memoStream = new MemoryStream();
            await blobClient.DownloadToAsync(memoStream);
            var base64String = Base64Service.ConvertToBase64(memoStream);
            memoStream.Close();
            return base64String;
        }
            
        private string BuildBlobImagem(PostagemDto newPost)
        {
            var extensionName = Path.GetExtension(newPost.NomeImagem);
            var nomeAutor = newPost.Autor.ToLower().Replace(" ", "-");
            return string.Format("{0}_{1}{2}", nomeAutor, Guid.NewGuid().ToString(), extensionName);
        }
    }
}
