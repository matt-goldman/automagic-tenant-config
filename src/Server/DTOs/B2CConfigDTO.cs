namespace SSW.Med_Man.MVC.DTOs
{
    public class B2CConfigDTO : ConfigDTO
    {
        public string ClientId { get; set; }
        public string Domain { get; set; }
        public string TenantName { get; set; }
        public string Instance { get; set; }
        public string SignUpSignInPolicyId { get; set; }
    }
}