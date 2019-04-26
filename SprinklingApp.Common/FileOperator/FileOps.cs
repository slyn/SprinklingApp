using System;
using System.ComponentModel;
using System.IO;

namespace SprinklingApp.Common.FileOperator {

    public static class FileOps {
        #region ReadText

        public static string ReadText(string fullPath) {
            return File.ReadAllText(fullPath);
        }

        #endregion

        #region WriteFile

        public static void WriteFile(string path, string data, FileWriteOptions fileWriteOptions) {
            fileWriteOptions = fileWriteOptions ?? new FileWriteOptions();

            if (string.IsNullOrEmpty(path)) {
                throw new NullReferenceException("The Path cannot be empty!");
            }

            if (!path.Contains(".")) {
                throw new InvalidEnumArgumentException("Invalid file extention! The file extension is required.");
            }

            string fullFileName = Path.GetFileName(path);
            string fileName = fullFileName.Split('.')[0];
            string fileExtension = fullFileName.Split('.')[1];
            string directoryName = Path.GetDirectoryName(path);

            if (directoryName == null) {
                throw new NullReferenceException("Path of directory is null!");
            }

            if (!Directory.Exists(directoryName)) {
                if (fileWriteOptions.CreateDirectoryIfNotExists) {
                    Directory.CreateDirectory(directoryName);
                } else {
                    throw new DirectoryNotFoundException($"Could not find a part of the path '{directoryName}'.");
                }
            }

            if (IsExistingFile(path)) {
                bool isFileSizeOutOfMaxSizeRange = false;
                if (fileWriteOptions.MaxFileSize != default(long)) {
                    long fileSize = new FileInfo(path).Length;
                    if (fileSize > fileWriteOptions.MaxFileSize) {
                        isFileSizeOutOfMaxSizeRange = true;
                    }
                }

                if (!fileWriteOptions.OverwriteFileIfExists || isFileSizeOutOfMaxSizeRange) {
                    string tempPath = path;
                    int count = 1;
                    while (true) {
                        if (!File.Exists(tempPath)) {
                            path = tempPath;
                            break;
                        }

                        tempPath = Path.Combine(directoryName, string.Format("{0}({1}).{2}", fileName, count, fileExtension));
                        count++;
                    }
                }
            }

            StreamWriter streamWriter = new StreamWriter(path, !fileWriteOptions.OverwriteFileIfExists);
            streamWriter.WriteLine(data);
            streamWriter.Close();
        }

        #endregion

        #region DeleteFile

        public static void DeleteFile(string path) {
            bool isExisting = IsExistingFile(path);
            if (!isExisting) {
                throw new Exception("Given path not found : " + path);
            }

            File.Delete(path);
        }

        #endregion

        #region IsExistingFile

        public static bool IsExistingFile(string fullPath) {
            return File.Exists(fullPath);
        }

        #endregion
    }

}