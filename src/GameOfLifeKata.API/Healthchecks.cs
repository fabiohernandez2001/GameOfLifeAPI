using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace GameOfLifeKata.API
{
    public class Healthchecks : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true;

            // ...
            string pathfile = @"C:\Users\fahernandez\source\repos\GameOfLifeAPI\GameOfLifePersistance\GamesJSON\";
            if (!Directory.Exists(pathfile)) {
                return Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus, "There is no directory"));
            }
            var user = WindowsIdentity.GetCurrent().User;
            // Crear un objeto DirectoryInfo
            var directoryInfo = new DirectoryInfo(pathfile);

            // Obtener los permisos
            var accessControl = directoryInfo.GetAccessControl();

            // Mostrar los permisos
            var accessRules = accessControl.GetAccessRules(true, true, typeof(NTAccount));

            foreach (AuthorizationRule rule in accessRules)
            {
                FileSystemAccessRule accessRule = rule as FileSystemAccessRule;
                if (rule.IdentityReference.Equals(user) && (accessRule.FileSystemRights & FileSystemRights.Read ) != FileSystemRights.ReadData &&
                    (accessRule.FileSystemRights & FileSystemRights.Write) != FileSystemRights.Write)
                {
                    isHealthy = false;
                }
            }
            if (isHealthy)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("A healthy result."));
            }

            return Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, "There is no permissions"));
        }
    }
}
