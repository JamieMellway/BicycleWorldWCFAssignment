using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BicycleWorldServiceModelClient.BicycleWorldService;
using BicycleWorldServiceModelClient;
using System.ServiceModel;

namespace BicycleWorldClientSession
{

    public sealed class UserSession
    {
        private static readonly UserSession current = new UserSession();
        static UserSession() { }
        private UserSession() { }
        public static UserSession Current { get { return current; } }

        private BicycleWorldSalesServiceClient client = null;
        public BicycleWorldSalesServiceClient Client
        {
            get
            {
                if (client != null && client.State == CommunicationState.Faulted) client.Abort();

                if (client == null || client.State == CommunicationState.Closed)
                {
                    client = new BicycleWorldSalesServiceClient();
                    client.ClientCredentials.UserName.UserName = LoginUser.Current.Username;
                    client.ClientCredentials.UserName.Password = LoginUser.Current.Password;
                }

                return client;
            }
        }

        public void CloseSession()
        {
            try
            {
                if (client != null)
                {
                    if (client.State == System.ServiceModel.CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    else if (client.State == System.ServiceModel.CommunicationState.Faulted)
                    {
                        client.Abort();
                    }
                }
            }
            catch (FaultException)
            {
                client.Abort();
            }
            catch (CommunicationException)
            {
                client.Abort();
            }
            catch (TimeoutException)
            {
                client.Abort();
            }
            catch { throw; }

        }

    }
}
