using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace BicycleWorldClientSession
{
    // WARNING: This code is only needed for test certificates such as those  
    // created by makecert. It is not recommended for production code. 
    public class PermissiveCertificatePolicy
    {
        string subjectName;
        static PermissiveCertificatePolicy currentPolicy;
        PermissiveCertificatePolicy(string subjectName)
        {
            this.subjectName = subjectName;
            ServicePointManager.ServerCertificateValidationCallback +=
                new System.Net.Security.RemoteCertificateValidationCallback
                (RemoteCertValidate);
        }

        public static void Enact(string subjectName)
        {
            currentPolicy = new PermissiveCertificatePolicy(subjectName);
        }

        bool RemoteCertValidate(object sender, X509Certificate cert,
                X509Chain chain, System.Net.Security.SslPolicyErrors error)
        {
            if (cert.Subject == subjectName)
            {
                return true;
            }

            return false;
        }
    }
}