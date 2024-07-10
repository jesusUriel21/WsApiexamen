namespace WsApiexamen.Models.Config
{
    public class Connection : IConnection
    {
      
        public string cnSQL { get; set; }   

    }

    public interface IConnection
    {
        public string cnSQL { set; get; }
    }

}
