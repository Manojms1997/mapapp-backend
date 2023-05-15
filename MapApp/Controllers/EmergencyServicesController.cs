using GeoCoordinatePortable;
using MapApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MapApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmergencyServiceLocationController : ControllerBase
	{
		private readonly DataContext _context;

		public EmergencyServiceLocationController(DataContext context)
		{
			_context = context;
		}

		// GET: api/EmergencyServiceLocation
		[HttpGet]
		public async Task<ActionResult<IEnumerable<EmergencyServiceLocation>>> GetEmergencyServiceLocations()
		{
			return await _context.EmergencyServiceLocations.ToListAsync();
		}

		// GET: api/EmergencyServiceLocation/hospitals
		[HttpGet("hospitals")]
		public async Task<ActionResult<IEnumerable<EmergencyServiceLocation>>> GetHospitalEmergencyServiceLocations()
		{
			return await _context.EmergencyServiceLocations
				.Where(e => e.LocationType == "hospital")
				.ToListAsync();
		}

		// GET: api/EmergencyServiceLocation/police
		[HttpGet("police")]
		public async Task<ActionResult<IEnumerable<EmergencyServiceLocation>>> GetPoliceEmergencyServiceLocations()
		{
			return await _context.EmergencyServiceLocations
				.Where(e => e.LocationType == "police")
				.ToListAsync();
		}

		// GET: api/EmergencyServiceLocation/fire
		[HttpGet("fire")]
		public async Task<ActionResult<IEnumerable<EmergencyServiceLocation>>> GetFireEmergencyServiceLocations()
		{
			return await _context.EmergencyServiceLocations
				.Where(e => e.LocationType == "fire")
				.ToListAsync();
		}

		// GET: api/EmergencyServiceLocation/nearby?latitude=40.7128&longitude=-74.0060&locationtype=hospital
		[HttpGet("nearby")]
		public async Task<ActionResult<IEnumerable<EmergencyServiceLocation>>> GetNearbyEmergencyServiceLocations(double latitude, double longitude, string locationtype)
		{
			try 
			{ 
				var userLocation = new GeoCoordinate(latitude, longitude);

				var emergencyServiceLocations = await _context.EmergencyServiceLocations
					.Where(e => e.LocationType == locationtype)
					.ToListAsync();

				var nearbyLocations = emergencyServiceLocations
					.Where(e => userLocation.GetDistanceTo(new GeoCoordinate(e.Latitude, e.Longitude)) <= 40233.6) 
					.ToList();

				return nearbyLocations;
			}
			catch (Exception ex)
			{
				// Log the exception or send an error response to the client
				return StatusCode(500, "An error occurred while processing your request.");
			}
		}
	}
}
