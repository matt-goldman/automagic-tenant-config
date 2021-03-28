namespace MedMan.Application.Config.Common
{
    public class B2CConfigDto : ConfigDto
    {
        public string ClientId { get; set; }
        public string Domain { get; set; }
        public string TenantName { get; set; }
        public string Instance { get; set; }
        public string SignUpSignInPolicyId { get; set; }
    }
}