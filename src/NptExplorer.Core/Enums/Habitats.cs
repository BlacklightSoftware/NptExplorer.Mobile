using System.ComponentModel;
using NptExplorer.Core.Attributes;

namespace NptExplorer.Core.Enums
{
	public enum Habitats
	{
		[Language("Woodland/Plantation", "Coetir/Planhigfa")]
		WoodlandPlantation = 1,
		[Language("Grassland/Meadow", "Glaswelltir/Dôl")]
		GrasslandMeadow = 2,
		[Language("Pasture/Rural Road", "Porfa/Ffordd Wledig")]
		PastureRuralRoad = 3,
		[Language("Moorland", "Rhostir")]
		Moorland = 4,
		[Language("Wetland", "Gwlypdir")]
		Wetland = 5,
		[Language("Fen", "Cors")]
		Fen = 6,
		[Language("Rockface", "Clogwyn")]
		Rockface = 7,
		[Language("Riverside", "Glan Afon")]
		Riverside = 8,
		[Language("Pond", "Pwll Dŵr")]
		Pond = 9,
		[Language("Canalside", "Ar Hyd Camlas")]
		Canalside = 10,
		[Language("Waterfall", "Rhaeadr")]
		Waterfall = 11,
		[Language("Coastal/Sand Dunes/Beach", "Arfordirol/Twyni Tywod/Traeth")]
		CoastalSandDunesBeach = 12,
		[Language("Urban", "Trefol")]
		Urban = 13,
		[Language("Brownfield", "Tir Llwyd")]
		Brownfield = 14,

	}
}