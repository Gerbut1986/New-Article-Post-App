namespace NewsPortal.PL._829528a_441d_484m_862i_22475963ffdn
{
    using System.Configuration;

    public class Credential
    {
        public static string ConnStr => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public System.DateTime RecordDate { get; set; }
    }
}