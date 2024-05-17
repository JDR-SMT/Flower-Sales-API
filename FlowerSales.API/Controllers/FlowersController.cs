using Asp.Versioning;
using FlowerSales.API.Models;
using FlowerSales.API.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace FlowerSales.API.Controllers
{
	#region v1
	[ApiVersion("1.0")]
	[ApiController]
	[Route("flowers")]
	public class FlowersControllerV1(FlowersService flowersService) : ControllerBase
	{
		private readonly FlowersService _flowersService = flowersService;

		#region Flowers
		// sort and filter all flowers
		[HttpGet]
		public async Task<ActionResult> GetFlowers([FromQuery] FlowerQueryParameters queryParameters)
		{
			var flowers = await _flowersService.GetAsync();
			var queryable = flowers.AsQueryable();

			// sort by name
			if (!string.IsNullOrEmpty(queryParameters.SortBy))
			{
				if (typeof(Flower).GetProperty(queryParameters.SortBy) != null)
				{
					queryable = queryable.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
				}
			}

			// filter by category
			if (!string.IsNullOrEmpty(queryParameters.Category))
			{
				queryable = queryable.Where(f => f.Category.Contains(queryParameters.Category, StringComparison.CurrentCultureIgnoreCase));
			}

			// filter by name
			if (!string.IsNullOrEmpty(queryParameters.Name))
			{
				queryable = queryable.Where(f => f.Name.Contains(queryParameters.Name, StringComparison.CurrentCultureIgnoreCase));
			}

			// filter by store location
			if (!string.IsNullOrEmpty(queryParameters.StoreLocation))
			{
				queryable = queryable.Where(f => f.StoreLocation.Contains(queryParameters.StoreLocation, StringComparison.CurrentCultureIgnoreCase));
			}

			// filter by post code
			if (queryParameters.PostCode != null)
			{
				queryable = queryable.Where(f => f.PostCode == queryParameters.PostCode);
			}

			// filter minimum price
			if (queryParameters.MinPrice != null)
			{
				queryable = queryable.Where(f => f.Price >= queryParameters.MinPrice.Value);
			}

			// filter maximum price
			if (queryParameters.MaxPrice != null)
			{
				queryable = queryable.Where(f => f.Price <= queryParameters.MaxPrice.Value);
			}

			// pagination
			queryable = queryable
				.Skip(queryParameters.Size * (queryParameters.Page - 1))
				.Take(queryParameters.Size);

			return Ok(queryable);
		}
		#endregion

		#region Flower
		// return flower by id
		[HttpGet("{id:length(24)}")]
		public async Task<ActionResult> GetFlower(string id)
		{
			var flower = await _flowersService.GetAsync(id);

			if (flower is null)
			{
				return NotFound();
			}

			return Ok(flower);
		}
		#endregion

		#region Add
		// add a new flower
		[HttpPost]
		public async Task<IActionResult> AddFlower(Flower newFlower)
		{
			await _flowersService.AddAsync(newFlower);

			return CreatedAtAction(nameof(GetFlower), new { id = newFlower.Id }, newFlower);
		}
		#endregion

		#region Update
		// update an existing flower by id
		[HttpPut("{id:length(24)}")]
		public async Task<IActionResult> UpdateFlower(string id, Flower updatedFlower)
		{
			var flower = await _flowersService.GetAsync(id);

			if (flower is null)
			{
				return NotFound();
			}

			// update product
			updatedFlower.Id = flower.Id;

			await _flowersService.UpdateAsync(id, updatedFlower);

			return NoContent();
		}
		#endregion

		#region Delete
		// delete an existing flower by id
		[HttpDelete("{id:length(24)}")]
		public async Task<IActionResult> DeleteFlower(string id)
		{
			var flower = await _flowersService.GetAsync(id);

			if (flower is null)
			{
				return NotFound();
			}

			await _flowersService.DeleteAsync(id);
			return NoContent();
		}
		#endregion
	}
	#endregion

	#region v2
	[ApiVersion("2.0")]
	[ApiController]
	[Route("flowers")]
	public class FlowersControllerV2(FlowersService flowersService) : ControllerBase
	{
		private readonly FlowersService _flowersService = flowersService;

		#region Flowers
		// sort and filter available flowers
		[HttpGet]
		public async Task<ActionResult> GetFlowers([FromQuery] FlowerQueryParameters queryParameters)
		{
			var flowers = await _flowersService.GetAvailableAsync();
			var queryable = flowers.AsQueryable();

			// sort by name
			if (!string.IsNullOrEmpty(queryParameters.SortBy))
			{
				if (typeof(Flower).GetProperty(queryParameters.SortBy) != null)
				{
					queryable = queryable.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
				}
			}

			// filter by category
			if (!string.IsNullOrEmpty(queryParameters.Category))
			{
				queryable = queryable.Where(f => f.Category.Contains(queryParameters.Category, StringComparison.CurrentCultureIgnoreCase));
			}

			// filter by name
			if (!string.IsNullOrEmpty(queryParameters.Name))
			{
				queryable = queryable.Where(f => f.Name.Contains(queryParameters.Name, StringComparison.CurrentCultureIgnoreCase));
			}

			// filter by store location
			if (!string.IsNullOrEmpty(queryParameters.StoreLocation))
			{
				queryable = queryable.Where(f => f.StoreLocation.Contains(queryParameters.StoreLocation, StringComparison.CurrentCultureIgnoreCase));
			}

			// filter by post code
			if (queryParameters.PostCode != null)
			{
				queryable = queryable.Where(f => f.PostCode == queryParameters.PostCode);
			}

			// filter minimum price
			if (queryParameters.MinPrice != null)
			{
				queryable = queryable.Where(f => f.Price >= queryParameters.MinPrice.Value);
			}

			// filter maximum price
			if (queryParameters.MaxPrice != null)
			{
				queryable = queryable.Where(f => f.Price <= queryParameters.MaxPrice.Value);
			}

			// pagination
			queryable = queryable
				.Skip(queryParameters.Size * (queryParameters.Page - 1))
				.Take(queryParameters.Size);

			return Ok(queryable);
		}
		#endregion

		#region Flower
		// return flower by id
		[HttpGet("{id:length(24)}")]
		public async Task<ActionResult> GetFlower(string id)
		{
			var flower = await _flowersService.GetAsync(id);

			if (flower is null)
			{
				return NotFound();
			}

			return Ok(flower);
		}
		#endregion

		#region Add
		// add a new flower
		[HttpPost]
		public async Task<IActionResult> AddFlower(Flower newFlower)
		{
			await _flowersService.AddAsync(newFlower);

			return CreatedAtAction(nameof(GetFlower), new { id = newFlower.Id }, newFlower);
		}
		#endregion

		#region Update
		// update an existing flower by id
		[HttpPut("{id:length(24)}")]
		public async Task<IActionResult> UpdateFlower(string id, Flower updatedFlower)
		{
			var flower = await _flowersService.GetAsync(id);

			if (flower is null)
			{
				return BadRequest();
			}

			// update product
			updatedFlower.Id = flower.Id;

			await _flowersService.UpdateAsync(id, updatedFlower);

			return NoContent();
		}
		#endregion

		#region Delete
		// delete an existing flower by id
		[HttpDelete("{id:length(24)}")]
		public async Task<IActionResult> DeleteFlower(string id)
		{
			var flower = await _flowersService.GetAsync(id);

			if (flower is null)
			{
				return NotFound();
			}

			await _flowersService.DeleteAsync(id);
			return NoContent();
		}
		#endregion
	}
	#endregion
}
