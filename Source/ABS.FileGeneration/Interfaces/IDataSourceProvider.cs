namespace ABS.FileGeneration.Interfaces
{
    /*
     This approach enhances the flexibility of the data source, 
    allowing the client to decide which data source to use. 
    Therefore, the injection of this interface will occur on the client side (in this case, the web API).
     */
    public interface IDataSourceProvider<T> where T : class
    {     
        IEnumerable<T> GetData();
    }
}
