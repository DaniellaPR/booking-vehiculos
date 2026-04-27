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

public class AuditoriaDataService : IAuditoriaDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public AuditoriaDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AuditoriaDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.AuditoriaRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : AuditoriaDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<AuditoriaDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.AuditoriaRepository.ListarAsync(cancellationToken);
        return entities.Select(AuditoriaDataMapper.ToDataModel).ToList();
    }

    public async Task<DataPagedResult<AuditoriaDataModel>> BuscarAsync(AuditoriaFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.AuditoriaQueryRepository.BuscarAsync(filtro.PageNumber, filtro.PageSize, cancellationToken);
        return new DataPagedResult<AuditoriaDataModel>
        {
            Items = result.Items.Select(AuditoriaDataMapper.ToDataModel).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<AuditoriaDataModel> CrearAsync(AuditoriaDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = AuditoriaDataMapper.ToEntity(model);
        await _unitOfWork.AuditoriaRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return AuditoriaDataMapper.ToDataModel(entity);
    }
}
