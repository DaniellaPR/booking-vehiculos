using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Interfaces;
using Microservicios.Coche.DataManagement.Mappers;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Services;

public class LicenciaConducirDataService : ILicenciaConducirDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public LicenciaConducirDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<LicenciaConducirDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.LicenciaConducirRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : LicenciaConducirDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<LicenciaConducirDataModel>> ListarPorClienteAsync(Guid clienteId, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.LicenciaConducirRepository.ListarAsync(cancellationToken);

        // Si la colección contiene elementos typed como object (p. ej. IEnumerable<object>),
        // usamos dynamic en tiempo de ejecución para leer CLI_id y luego mapear.
        var filtrados = entities
            .Cast<object>()
            .Where(e =>
            {
                try
                {
                    return (Guid)((dynamic)e).CLI_id == clienteId;
                }
                catch
                {
                    return false;
                }
            })
            .ToList();

        return filtrados
            .Select(e => LicenciaConducirDataMapper.ToDataModel((dynamic)e))
            .Cast<LicenciaConducirDataModel>()
            .ToList();
    }

    public async Task<DataPagedResult<LicenciaConducirDataModel>> BuscarAsync(LicenciaConducirFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.LicenciaConducirQueryRepository.BuscarAsync(filtro.PageNumber, filtro.PageSize, cancellationToken);
        return new DataPagedResult<LicenciaConducirDataModel>
        {
            Items = result.Items.Select(LicenciaConducirDataMapper.ToDataModel).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<LicenciaConducirDataModel> CrearAsync(LicenciaConducirDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = LicenciaConducirDataMapper.ToEntity(model);
        await _unitOfWork.LicenciaConducirRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return LicenciaConducirDataMapper.ToDataModel(entity);
    }

    public async Task<LicenciaConducirDataModel?> ActualizarAsync(LicenciaConducirDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.LicenciaConducirRepository.ObtenerParaActualizarAsync(model.LIC_id, cancellationToken);
        if (entity is null) return null;

        entity.LIC_numero = model.LIC_numero;
        entity.LIC_categoria = model.LIC_categoria;
        entity.LIC_vigencia = model.LIC_vigencia;
        entity.LIC_fechaModificacion = model.LIC_fechaModificacion ?? DateTime.UtcNow;
        entity.LIC_usuarioModificacion = model.LIC_usuarioModificacion;

        _unitOfWork.LicenciaConducirRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return LicenciaConducirDataMapper.ToDataModel(entity);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.LicenciaConducirRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (entity is null) return false;

        _unitOfWork.LicenciaConducirRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IEnumerable<LicenciaConducirDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.LicenciaConducirRepository.ListarAsync(cancellationToken);
        return entities
            .OfType<Microservicios.Coche.DataAccess.Entities.LicenciaConducirEntity>()
            .Select(e => LicenciaConducirDataMapper.ToDataModel(e))
            .ToList();
    }
}