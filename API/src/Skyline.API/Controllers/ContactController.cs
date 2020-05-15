using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skyline.API.Dtos;
using Skyline.ApplicationCore.Entities.ContactAggregate;
using Skyline.ApplicationCore.Interfaces;
using Skyline.ApplicationCore.Specifications;

namespace Skyline.API.Controllers
{
    /// <summary>
    /// 联系人接口
    /// </summary>
    //[Route("[controller]")]
    [Route("contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private const int MaxPageSize = 30;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactController(IMapper mapper, IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取所有联系人
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 1, int pageSize = 10)
        {
            if (pageSize > MaxPageSize)
            {
                return UnprocessableEntity($"The page size must be less than or equal to {MaxPageSize}");
            }
            var filterPaginatedSpecification = new ContactFilterPaginatedSpecification((pageIndex - 1) * pageSize, pageSize);
            var entities = await _contactRepository.ListAsync(filterPaginatedSpecification);
            if (!entities.Any())
            {
                return NotFound();
            }
            var dto = _mapper.Map<IReadOnlyList<ContactQueryDto>>(entities);
            return Ok(dto);
        }

        /// <summary>
        /// 获取某一个联系人
        /// </summary>
        /// <param name="id">联系人ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _contactRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<ContactQueryDto>(entity);
            return Ok(dto);
        }

        /// <summary>
        /// 新增一个联系人
        /// </summary>
        /// <param name="dto">待新增联系人数据对象</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ContactCreateDto dto)
        {
            var entity = _mapper.Map<Contact>(dto);
            entity.Status = ContactStatus.Submitted;
            entity.OwnerId = default(Guid).ToString();
            var createdContact = await _contactRepository.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = createdContact.Id }, createdContact);
        }

        /// <summary>
        /// 更新联系人
        /// </summary>
        /// <param name="dto">待更新联系人数据对象</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ContactUpdateDto dto)
        {
            var entity = await _contactRepository.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.MobileNumber = dto.MobileNumber;
            entity.Name = dto.Name;
            entity.Province = dto.Province;
            entity.City = dto.City;
            entity.Email = dto.Email;
            entity.Address = dto.Address;
            entity.Zip = dto.Zip;

            await _contactRepository.UpdateAsync(entity);
            return Ok();
        }

        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="id">联系人ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _contactRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await _contactRepository.DeleteAsync(entity);
            return NoContent();
        }
    }
}