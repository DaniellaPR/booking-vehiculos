using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface ILicenciaConducirRepository
    {
        Task<LicenciaConducirEntity?> ObtenerPorIdAsync(Guid licId, CancellationToken cancellationToken = default);
        Task AgregarAsync(LicenciaConducirEntity licencia, CancellationToken cancellationToken = default);
        void Actualizar(LicenciaConducirEntity licencia);
        void Eliminar(LicenciaConducirEntity licencia);
        Task<LicenciaConducirEntity> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<object>> ListarAsync(CancellationToken cancellationToken);
    }
}