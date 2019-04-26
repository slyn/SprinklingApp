namespace SprinklingApp.Common.FileOperator {

    public class FileWriteOptions {
        public bool OverwriteFileIfExists { get; set; }
        public long MaxFileSize { get; set; }
        public bool CreateDirectoryIfNotExists { get; set; }
    }

}