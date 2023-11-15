using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace GameOfLifeKata.API;

public class Healthchecks : IHealthCheck {
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default) {
        var isHealthy = true;

        // ...
        string directorypath = Program.GetPath();
        if (!Directory.Exists(directorypath)) {
            return Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, "There is no directory"));
        }

        if (OperatingSystem.IsWindows()) {
            var user = WindowsIdentity.GetCurrent().User.ToString();
            var directoryInfo = new DirectoryInfo(directorypath);

            // Obtener los permisos
            var accessControl = directoryInfo.GetAccessControl();

            // Mostrar los permisos
            var accessRules = accessControl.GetAccessRules(true, true, typeof(NTAccount));

            foreach (AuthorizationRule rule in accessRules)
            {
                FileSystemAccessRule accessRule = rule as FileSystemAccessRule;
                if (rule.IdentityReference.ToString().Equals(user) && (accessRule.FileSystemRights & FileSystemRights.Read) != FileSystemRights.ReadData &&
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
        else {
            try {
                var filename = Path.Combine(directorypath,"dummy.txt");
                var fileStream = File.Create(filename);
                if (!(fileStream.CanRead && fileStream.CanWrite)) {
                    throw new IOException("there is no permissions", 78);
                }
                fileStream.Dispose();
                File.Delete(filename);
                return Task.FromResult(
                    HealthCheckResult.Healthy("A healthy result."));

            }
            catch(IOException ioex) when(ioex.HResult == 78)
            {
                return Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus,ioex.Message));
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus, "Unable to determine the permissions"));
            }
        }
    }
}

