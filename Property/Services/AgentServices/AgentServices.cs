using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Property.Data;
using Property.DTOs.Agent;
using Property.DTOs.Category;
using Property.Models;

namespace Property.Services.AgentServices
{
	public class AgentServices : IAgentServices
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public AgentServices(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<List<GetAgentDTO>>> AddAgent(AddAgentDTO newAgent)
		{
			var serviceResponse = new ServiceResponse<List<GetAgentDTO>>();
			var agent = _mapper.Map<Agent>(newAgent);

			var agents = await _context.Agents
				.Include(x => x.ApplicationUser).ToListAsync();
			if (agents.Any())
			{
				bool flag = false;
				foreach (var agnt in agents)
				{
					if (agnt.ApplicationUser.Id == agent.ApplicationUser.Id)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					_context.Agents.Add(agent);
					await _context.SaveChangesAsync();
				}
			}
			else
			{
				_context.Agents.Add(agent);
				await _context.SaveChangesAsync();
			}

			serviceResponse.Data = await _context.Agents
				.Select(x => _mapper.Map<GetAgentDTO>(x)).ToListAsync();
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetAgentDTO>> GetAgentByUserId(string userId)
		{
			var agent = await _context.Agents
				.Include(x => x.ApplicationUser)
				.Include(x => x.ProductsRealEstate)
				.FirstOrDefaultAsync(x => x.ApplicationUser.Id == userId);

			var serviceResponse = new ServiceResponse<GetAgentDTO>()
			{
				Data = _mapper.Map<GetAgentDTO>(agent)
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetAgentDTO>> GetAgentById(int id)
		{
			var agent = await _context.Agents
				.Include(x => x.ApplicationUser)
				.Include(x => x.ProductsRealEstate)
				.FirstOrDefaultAsync(x => x.Id == id);

			var serviceResponse = new ServiceResponse<GetAgentDTO>()
			{
				Data = _mapper.Map<GetAgentDTO>(agent)
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetAgentDTO>>> GetAllAgents()
		{
			var agents = await _context.Agents
				.Include(c => c.ApplicationUser)
				.Include(c => c.ProductsRealEstate)
				.ToListAsync();
			var serviceResponse = new ServiceResponse<List<GetAgentDTO>>()
			{
				Data = agents.Select(p => _mapper.Map<GetAgentDTO>(p)).ToList()
			};
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetAgentDTO>> UpdateAgent(UpdateAgentDTO updatedAgent)
		{
			var serviceResponse = new ServiceResponse<GetAgentDTO>();

			try
			{
				var agent = await _context.Agents
					.FirstOrDefaultAsync(x => x.Id == updatedAgent.Id);
				if (agent is null) { throw new Exception($"Agent with Id '{updatedAgent.Id}' not found"); }

				agent.Name = updatedAgent.Name;
				agent.Profession = updatedAgent.Profession;

				await _context.SaveChangesAsync();

				serviceResponse.Data = _mapper.Map<GetAgentDTO>(agent);
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetAgentDTO>>> DeleteAgent(int id)
		{
			var serviceResponse = new ServiceResponse<List<GetAgentDTO>>();

			try
			{
				var agent = await _context.Agents.FirstOrDefaultAsync(x => x.Id == id);
				if (agent is null) { throw new Exception($"Agent with Id '{id}' not found"); }

				_context.Agents.Remove(agent);

				await _context.SaveChangesAsync();

				serviceResponse.Data = await _context.Agents
					.Select(c => _mapper.Map<GetAgentDTO>(c)).ToListAsync();
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}
			return serviceResponse;
		}
	}
}
