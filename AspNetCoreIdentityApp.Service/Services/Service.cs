using AspNetCoreIdentityApp.Core.Repositories;
using AspNetCoreIdentityApp.Core.Services;
using AspNetCoreIdentityApp.Core.UnitOfWork;
using AspNetCoreIdentityApp.Service.DtoMappers;
using AspNetCoreIdentityApp.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreIdentityApp.Service.Services
{
    public class Service<TEntity, TDto> : IService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityRepository<TEntity> _repository;
        public Service(IUnitOfWork unitOfWork, IEntityRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            return Response<TDto>.Success(newDto, 200);
        }

        /// <summary>
        /// Cektiğiniz verileri RAM e alır sonrasında üzerinde işlem yapar.
        /// </summary>
        /// <returns></returns>
        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var entities = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await _repository.GetAllAsync());
            return Response<IEnumerable<TDto>>.Success(entities, 200);
        }

        /// <summary>
        /// ToListAsync cagırılana kadar sorguyu olusturur ToListAsync deyince DB ye sorguyu atar ve bu sorgu sonucunu direkt dondurur.
        /// </summary>
        /// <returns></returns>
        public async Task<Response<IEnumerable<TDto>>> GetAllWithAsync()
        {
            var filteredEntities = _repository.GetAll().Take(20);
            var entities = ObjectMapper.Mapper.Map<IQueryable<TDto>>(await filteredEntities.ToListAsync());
            return Response<IEnumerable<TDto>>.Success(entities, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<TDto>.Fail("Id Not Found.", 404, true);
            }
            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(entity), 200);
        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<NoDataDto>.Fail("Id Not Found.", 404, true);
            }

            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> Update(TDto entity, int id)
        {
            var currentEntity = await _repository.GetByIdAsync(id);
            if (currentEntity == null)
            {
                return Response<NoDataDto>.Fail("Id Not Found.", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _repository.Update(updateEntity);

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204); // 204 durum kodu => 'NoContent' => Response body'sinde hiçbirsey sey olmayacak.
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _repository.Where(predicate);
            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }

        public async Task<Response<IEnumerable<TDto>>> WhereWith(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _repository.Where(predicate).Take(10);
            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }
    }
}
