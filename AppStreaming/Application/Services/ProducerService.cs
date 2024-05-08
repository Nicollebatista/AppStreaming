using Application.Repository;
using Application.ViewModels;
using DataBase.Models;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProducerService
    {

            private readonly ProducerRepository _producerRepository;
            public ProducerService(ApplicationContext dbContext)
            {
              _producerRepository = new(dbContext);
            }
            public async Task Update(ProducerViewModel vm)
            {
                Producer producer = new();
                producer.Id = vm.Id;
                producer.Name = vm.Name;

                await _producerRepository.UpdateAsync(producer);
            }
            public async Task Add(ProducerViewModel vm)
            {
                Producer producer = new();
                producer.Name = vm.Name;

            await _producerRepository.AddAsync(producer);

            }
            public async Task Delete(int id)
            {
                var producer = await _producerRepository.GetByIdAsync(id);
                await _producerRepository.DeleteAsync(producer);

            }
            public async Task<List<ProducerViewModel>> GetAllViewModel()
            {
                var ProducerList = await _producerRepository.GetAllAsync();
                return ProducerList.Select(producer => new ProducerViewModel
                {
                    Id = producer.Id,
                    Name = producer.Name,

                }).ToList();
            }
            public async Task<ProducerViewModel> GetByIdSaveViewModel(int id)
            {
                var producer = await _producerRepository.GetByIdAsync(id);
                ProducerViewModel vm = new();
                vm.Id = producer.Id;
                vm.Name = producer.Name;

                return vm;
            }
      
    }
    }
