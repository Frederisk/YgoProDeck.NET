using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using YgoProDeck.Lib.Response;

namespace YgoProDeck.Telegram;

public static class InfoProgress {

    public enum ImageType {
        Full,
        Small,
        Cropped,
    }

    private static readonly DirectoryInfo ImageBaseDirectory;

    private static readonly Dictionary<ImageType, DirectoryInfo> ImageDirectoryMap;

    static InfoProgress() {
        // Set the base directory for images
        var userImagePath = Environment.GetEnvironmentVariable("TGO_IMAGE_PATH");
        if (String.IsNullOrEmpty(userImagePath)) {
            var defaultImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            ImageBaseDirectory = new(defaultImagePath);
        } else {
            ImageBaseDirectory = new(userImagePath);
        }
        // Set the image directory map
        ImageDirectoryMap = [];
        foreach (ImageType type in Enum.GetValues<ImageType>()) {
            var directory = new DirectoryInfo(Path.Combine(ImageBaseDirectory.FullName, type.ToString()));
            ImageDirectoryMap.Add(type, directory);
        }
        // Create the image directories
        foreach (var item in ImageDirectoryMap.Values) {
            if (!item.Exists) {
                item.Create();
            }
        }
    }

    private static readonly Object locker = new();
    private static readonly Dictionary<(UInt64, ImageType), Task> downloading = [];

    public static async Task<FileStream> GetImage(CardImage imageInfo, ImageType type) {
        var directory = ImageDirectoryMap[type];
        var file = new FileInfo(Path.Combine(directory.FullName, $"{imageInfo.Id}.jpg"));
        if (!file.Exists) {
            Task? downloadTask;

            lock (locker) {
                // Check if the image is already downloading, if yes return the task
                if (!downloading.TryGetValue((imageInfo.Id, type), out downloadTask)) {
                    // If not, start the download and add it to the dictionary
                    downloadTask = DownloadImage(imageInfo, type, file);
                    downloading.Add((imageInfo.Id, type), downloadTask);
                }
            }

            await downloadTask;
        }
        return file.OpenRead();
    }

    private static async Task DownloadImage(CardImage imageInfo, ImageType type, FileInfo file) {
        var uri = type switch {
            ImageType.Full => imageInfo.ImageUrl,
            ImageType.Small => imageInfo.ImageUrlSmall,
            ImageType.Cropped => imageInfo.ImageUrlCropped,
            _ => throw new NotImplementedException(),
        };

        var client = new HttpClient();
        var response = await client.GetAsync(uri);
        var stream = await response.Content.ReadAsStreamAsync();
        using var writeFile = file.OpenWrite();
        await stream.CopyToAsync(writeFile);

        lock (locker) {
            // Download complete
            // Remove the download task from the dictionary
            downloading.Remove((imageInfo.Id, type));
        }

        return;
    }
}
