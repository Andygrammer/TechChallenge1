namespace Api.Services
{
    public class Base64Service
    {
        public static string ConvertToBase64(MemoryStream blobStream) => Convert.ToBase64String(blobStream.ToArray());

        public static BinaryData ConvertFromBase64(string blobBase64)
        {
            var bytes = Convert.FromBase64String(blobBase64);
            var contents = new StreamContent(new MemoryStream(bytes));
            BinaryReader reader = new BinaryReader(contents.ReadAsStream());
            byte[] buffer = new byte[contents.ReadAsStream().Length];
            reader.Read(buffer, 0, buffer.Length);
            contents.ReadAsStream().Close();
            return new BinaryData(buffer);
        }
    }
}
