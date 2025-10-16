using System.Text.Json;

namespace BatchValidatorMockTest.Helpers
{
    public static class MockHelper
    {
        /// <summary>
        /// if logs are in json file, use this
        /// </summary>
        /// <param name="filePath">path of mock response file</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static BatchResponse LoadMockResponse(string filePath)
        {
            if (!File.Exists(filePath))
            {

                throw new FileNotFoundException($"Mock file not found: {filePath}");
            }


            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            switch (extension)
            {
                case ".txt":
                    // Read the entire log file as a single string
                    string logs = File.ReadAllText(filePath);
                    return new BatchResponse
                    {
                        Output = logs
                    };

                case ".json":
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<BatchResponse>(json);

                default:
                    throw new NotSupportedException($"File type '{extension}' is not supported.");
            }
        }
    }
}
