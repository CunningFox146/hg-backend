using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace TheHungerGames.Util;

public static class KleiEncode
{
    public static async Task<string> KleiEncodingToJson(this string test)
    {
        var bytes = Convert.FromBase64String(test)[16..];
        var inflatedString = await Decompress(bytes);
        return Encoding.Default.GetString(inflatedString);
    }
    
    private static async Task<byte[]> Decompress(byte[] data)
    {
        var outputStream = new MemoryStream();
        using var compressedStream = new MemoryStream(data);
        await using var inputStream = new InflaterInputStream(compressedStream);
    
        await inputStream.CopyToAsync(outputStream);
        outputStream.Position = 0;
        return outputStream.ToArray();
    }
}