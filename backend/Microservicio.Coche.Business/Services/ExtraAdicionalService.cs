using Microservicios.Coche.Business.DTOs.ExtraAdicional;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services
{
    public class ExtraAdicionalService : IExtraAdicionalService
    {
        private readonly IExtraAdicionalDataService _dataService;

        public ExtraAdicionalService(IExtraAdicionalDataService dataService)
        {
            _dataService = dataService;
        }

        // ✅ FIX: Antes devolvía new List<>() vacía hardcodeada
        public async Task<IReadOnlyList<ExtraAdicionalResponse>> ListarAsync(CancellationToken cancellationToken = default)
        {
            var extras = await _dataService.ListarAsync(cancellationToken);
            return extras.Select(e => e.ToDto()).ToList();
        }

        // ✅ FIX: Antes devolvía new ExtraAdicionalResponse() vacío hardcodeado
        public async Task<ExtraAdicionalResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var extra = await _dataService.ObtenerPorIdAsync(id, cancellationToken);

            if (extra is null)
                throw new NotFoundException("No se encontró el extra adicional solicitado.");

            return extra.ToDto();
        }

        // ✅ FIX: Antes devolvía new ExtraAdicionalResponse() vacío sin guardar nada
        public async Task<ExtraAdicionalResponse> CrearAsync(CrearExtraAdicionalRequest request, CancellationToken cancellationToken = default)
        {
            var dataModel = request.ToDataModel();
            var creado = await _dataService.CrearAsync(dataModel, cancellationToken);
            return creado.ToDto();
        }

        // ✅ FIX: Antes devolvía new ExtraAdicionalResponse() vacío sin guardar nada
        public async Task<ExtraAdicionalResponse> ActualizarAsync(ActualizarExtraAdicionalRequest request, CancellationToken cancellationToken = default)
        {
            var existente = await _dataService.ObtenerPorIdAsync(request.EXT_id, cancellationToken);

            if (existente is null)
                throw new NotFoundException("No se encontró el extra adicional a actualizar.");

            var dataModel = request.ToDataModel(existente);
            var actualizado = await _dataService.ActualizarAsync(dataModel, cancellationToken);

            if (actualizado is null)
                throw new NotFoundException("No se pudo actualizar el extra adicional porque no existe.");

            return actualizado.ToDto();
        }

        public async Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken = default)
        {
            var existente = await _dataService.ObtenerPorIdAsync(id, cancellationToken);

            if (existente is null)
                throw new NotFoundException("No se encontró el extra adicional a eliminar.");

            await _dataService.EliminarAsync(id, cancellationToken);
        }
    }
}