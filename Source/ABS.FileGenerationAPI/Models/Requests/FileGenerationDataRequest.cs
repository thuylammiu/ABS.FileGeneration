namespace ABS.FileGenerationAPI.Models.Requests
{
    public class FileGenerationDataRequest
    {
        public CancellationToken Cancellation { get; set; } = default; // source data for excel file
        public string FileName { get; set; } = "datageneration.xls";

        public string SheetName { get; set; } = "sheet1";
    }
}
