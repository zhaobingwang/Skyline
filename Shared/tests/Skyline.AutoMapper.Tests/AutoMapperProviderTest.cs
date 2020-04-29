using AutoMapper;
using System;
using Xunit;

namespace Skyline.AutoMapper.Tests
{
    public class AutoMapperProviderTest
    {
        [Fact]
        public void MapTo_ReturnDto_WithEntity()
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entity, Dto>();
            });
            var entity = GetEntity();
            AutoMapperProvider mapperProvider = new AutoMapperProvider(config);

            // Act
            var dto = mapperProvider.MapTo<Dto>(entity);
            var dto2 = new Dto();
            dto2 = mapperProvider.MapTo(entity, dto2);

            // Assert
            Assert.True(dto.ValuesEqualTo(entity));
            Assert.True(entity.ValuesEqualTo(dto2));
        }

        private Entity GetEntity()
        {
            return new Entity
            {
                Id = 1,
                Name = "test",
                LockTime = new DateTime(2020, 4, 29, 12, 11, 12),
                IsLocked = true,
                Status = 2
            };
        }
    }

    public static class Extension
    {
        public static bool ValuesEqualTo(this Entity entity, Dto dto)
        {
            return ValuesEqual(entity, dto);
        }
        public static bool ValuesEqualTo(this Dto dto, Entity entity)
        {
            return ValuesEqual(entity, dto);
        }
        private static bool ValuesEqual(Entity entity, Dto dto)
        {
            return entity.Id == dto.Id && entity.Name == dto.Name
                && entity.LockTime == dto.LockTime && entity.IsLocked == dto.IsLocked
                && entity.Status == dto.Status;
        }
    }

    #region Test Model
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LockTime { get; set; }
        public bool IsLocked { get; set; }
        public short Status { get; set; }
    }
    public class Dto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LockTime { get; set; }
        public bool IsLocked { get; set; }
        public short Status { get; set; }
    }
    #endregion
}
