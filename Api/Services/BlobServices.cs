using Azure.Storage.Blobs;

namespace Api.Services
{
    public interface IBlobServices
    {
        public Task<string> GetBlob(string imageName);
        public Task<string> CreateBlob(string nomeImagem, string imagemBase64, string nomeAutor);
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

        public async Task<string> CreateBlob(string nomeImagem, string imagemBase64, string nomeAutor)
        {
            var tituloImagem = BuildBlobImagem(nomeImagem, nomeAutor);
            BlobClient blobClient = containerClient.GetBlobClient(tituloImagem);
            var binaryData = Base64Service.ConvertFromBase64(imagemBase64);
            await blobClient.UploadAsync(binaryData, true);

            if (!await blobClient.ExistsAsync()) throw new Exception("Blob was not created");
            return tituloImagem;
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
            
        private string BuildBlobImagem(string nomeImagem, string autor)
        {
            var extensionName = Path.GetExtension(nomeImagem);
            var nomeAutor = autor.ToLower().Replace(" ", "-");
            return string.Format("{0}_{1}{2}", nomeAutor, Guid.NewGuid().ToString(), extensionName);
        }
    }
}
