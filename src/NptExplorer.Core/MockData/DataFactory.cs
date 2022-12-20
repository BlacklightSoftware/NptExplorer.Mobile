using System.Collections.Generic;
using MvvmHelpers;
using NptExplorer.Core.Enums;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Requests;
using BadgeTypes = NptExplorer.Core.Enums.BadgeTypes;
using Trail = NptExplorer.Core.Models.Trail;

namespace NptExplorer.Core.MockData
{
	public static class DataFactory
	{
		public static ObservableRangeCollection<Location> Places { get; private set; }
		public static ObservableRangeCollection<Trail> Trails { get; private set; }
		//public static List<TrailDto> TrailsList { get; set; }
		//public static List<ChallengeOverviewDto> ChallengesList { get; set; }
		public static List<Location> nearYouLocations { get; private set; }
		public static List<TestUser> users { get; private set; }
		public static List<TestUser> friends { get; private set; }
		public static Trail TrailWithRoute { get; private set; }

		static DataFactory()
		{
			//Places = new ObservableRangeCollection<Location>
			//{
			//	new Location
			//	{
			//		LocationName = "Argoed Walk",
			//		LocationImage = "https://dramaticheart.wales/wp-content/uploads/walks-images/18/.info.JPG",
			//		Facilities = new List<Facilities> { Facilities.Toilets, Facilities.Playground, Facilities.CafeRestaurants }
			//	},
			//	new Location
			//	{
			//		LocationName = "Banwen Meadows and Woods",
			//		LocationImage = "https://dramaticheart.wales/wp-content/uploads/walks-images/45/.info.JPG",
			//		Facilities = new List<Facilities> { Facilities.Parking, Facilities.CafeRestaurants }
			//	},
			//	new Location
			//	{
			//		LocationName = "Cilybebyll Walk",
			//		LocationImage = "https://dramaticheart.wales/wp-content/uploads/walks-images/26/.info.JPG",
			//		Facilities = new List<Facilities> { Facilities.WheelchairAccess, Facilities.DogFriendly }
			//	},
			//	new Location
			//	{
			//		LocationName = "Cwm Du Glen and Glanrhyd Plantation",
			//		LocationImage = "https://dramaticheart.wales/wp-content/uploads/walks-images/1/Clydach_River_Cwm_Du_Glen.jpg",
			//		Facilities = new List<Facilities> { Facilities.Toilets, Facilities.Playground, Facilities.CafeRestaurants }
			//	},
			//	new Location
			//	{
			//		LocationName = "Cwm Gwrelych Geo Trail",
			//		LocationImage = "https://dramaticheart.wales/wp-content/uploads/walks-images/31/Cwm_Gwrelych_Waterfall.jpg",
			//		Facilities = new List<Facilities> { Facilities.Toilets, Facilities.Playground, Facilities.CafeRestaurants }
			//	},
			//};

			//Trails = new ObservableRangeCollection<Trail>
			//{
			//	new Trail { TrailName = "Gnoll to Mosshouse Reservoir", Difficulty = "Easy", Length = "1.8 miles", Time = "55 mins"},
			//	new Trail { TrailName = "Gyfylchi Ridgetop Trail", Difficulty = "Easy", Length = "1.8 miles", Time = "55 mins"},
			//	new Trail { TrailName = "River & Railway Walk", Difficulty = "Moderate", Length = "2.7 miles", Time = "75 mins"},
			//	new Trail { TrailName = "Richard Burton Trail", Difficulty = "Easy", Length = "1.2 miles", Time = "45 mins"},
			//	new Trail { TrailName = "Pontardawe- Ystalyfera", Difficulty = "Difficult", Length = "7.9 miles", Time = "180 mins"},
			//	new Trail { TrailName = "Wales Coast Path – Mynydd Dinas", Difficulty = "Difficult", Length = "5.6 miles", Time = "120 mins"},
			//};

			//#region Trail with Route
			//TrailWithRoute = new Trail
			//{
			//	Id = 1,
			//	LocationName = "Mosshouse Reservoir",
			//	TrailName = "Gnoll to Mosshouse Reservoir",
			//	Difficulty = "Easy",
			//	Length = "0.6 miles",
			//	Time = "22 mins",
			//	Badge = new Badge { Id = 1, Collected = false, Type = Badges.Trail},
			//	StartPosition = new LatLong(51.6742177, -3.6544389),
			//	Route = new List<LatLong>
			//	{
			//		new LatLong(51.64050338,-3.71168677 ),
			//		new LatLong(51.64051388,-3.711820058),
			//		new LatLong(51.64051718,-3.711880045),
			//		new LatLong(51.64053763,-3.71188903 ),
			//		new LatLong(51.64054844,-3.711879311),
			//		new LatLong(51.64056661,-3.711862405),
			//		new LatLong(51.64059473,-3.711790353),
			//		new LatLong(51.64061747,-3.711690466),
			//		new LatLong(51.64064165,-3.711500618),
			//		new LatLong(51.64069258,-3.711310266),
			//		new LatLong(51.64072347,-3.711202556),
			//		new LatLong(51.64078131,-3.710875623),
			//		new LatLong(51.64080294,-3.710818806),
			//		new LatLong(51.64079364,-3.710741176),
			//		new LatLong(51.64079713,-3.710541853),
			//		new LatLong(51.64078798,-3.710457325),
			//		new LatLong(51.64079246,-3.710101431),
			//		new LatLong(51.64083001,-3.709992801),
			//		new LatLong(51.64086057,-3.7099224  ),
			//		new LatLong(51.64087784,-3.709751038),
			//		new LatLong(51.64090482,-3.709667803),
			//		new LatLong(51.64089841,-3.709605299),
			//		new LatLong(51.64094243,-3.709336867),
			//		new LatLong(51.6409851,-3.709078225 ),
			//		new LatLong(51.64099045,-3.708971845),
			//		new LatLong(51.64105413,-3.708747796),
			//		new LatLong(51.64112351,-3.708412517),
			//		new LatLong(51.64124111,-3.707990886),
			//		new LatLong(51.64121801,-3.707704402),
			//		new LatLong(51.64121221,-3.707643981),
			//		new LatLong(51.64110092,-3.707787667),
			//		new LatLong(51.64102658,-3.707883655),
			//		new LatLong(51.64092985,-3.707979643),
			//		new LatLong(51.64065145,-3.708255922),
			//		new LatLong(51.64059812,-3.708308843),
			//		new LatLong(51.64057426,-3.70833252 ),
			//		new LatLong(51.64025915,-3.708510145),
			//		new LatLong(51.64022502,-3.708529384),
			//		new LatLong(51.6400731,-3.708608478 ),
			//		new LatLong(51.64002792,-3.708631997),
			//		new LatLong(51.63999668,-3.708642973),
			//		new LatLong(51.63985448,-3.708692934),
			//		new LatLong(51.63966421,-3.70875978 ),
			//		new LatLong(51.63961304,-3.708764174),
			//		new LatLong(51.63953691,-3.708770711),
			//		new LatLong(51.63953597,-3.708770443),
			//		new LatLong(51.63940262,-3.708732321),
			//		new LatLong(51.6393035,-3.708703986 ),
			//		new LatLong(51.63928368,-3.708698322),
			//		new LatLong(51.63908002,-3.708580646),
			//		new LatLong(51.63902732,-3.70854091 ),
			//		new LatLong(51.63896173,-3.708507923),
			//		new LatLong(51.63892867,-3.708491088),
			//		new LatLong(51.63882135,-3.70844302 ),
			//		new LatLong(51.63872798,-3.708426976),
			//		new LatLong(51.63872042,-3.708425676),
			//		new LatLong(51.63867552,-3.708402449),
			//		new LatLong(51.63848693,-3.708304884),
			//		new LatLong(51.63846384,-3.708292939),
			//		new LatLong(51.63845833,-3.70829009 ),
			//		new LatLong(51.63845219,-3.708285087),
			//		new LatLong(51.6384279,-3.708265279 ),
			//		new LatLong(51.63823603,-3.70810885 ),
			//		new LatLong(51.63821247,-3.708087881),
			//		new LatLong(51.63810847,-3.707995292),
			//		new LatLong(51.63803327,-3.707928347),
			//		new LatLong(51.63802911,-3.707921332),
			//		new LatLong(51.63793207,-3.707757933),
			//		new LatLong(51.63791872,-3.707735442),
			//		new LatLong(51.63791594,-3.707728578),
			//		new LatLong(51.63787806,-3.707666574),
			//		new LatLong(51.63783657,-3.707571391),
			//		new LatLong(51.63779538,-3.707473935),
			//		new LatLong(51.63776825,-3.707385548),
			//		new LatLong(51.63769812,-3.707169938),
			//		new LatLong(51.63767242,-3.707089159),
			//		new LatLong(51.63760733,-3.706883523),
			//		new LatLong(51.63757823,-3.706772638),
			//		new LatLong(51.63754141,-3.706671777),
			//		new LatLong(51.63749445,-3.706507315),
			//		new LatLong(51.63747316,-3.706457725),
			//		new LatLong(51.63736175,-3.70612275 ),
			//		new LatLong(51.63733427,-3.706019687),
			//		new LatLong(51.63729105,-3.705889731),
			//		new LatLong(51.63727344,-3.705827137),
			//		new LatLong(51.63724157,-3.70575482 ),
			//		new LatLong(51.63707132,-3.705309562),
			//		new LatLong(51.63710956,-3.70532558 ),
			//		new LatLong(51.63708959,-3.705283051),
			//		new LatLong(51.63693231,-3.704948023),
			//		new LatLong(51.63681775,-3.704755132),
			//		new LatLong(51.63678815,-3.704721255),
			//		new LatLong(51.63671841,-3.704641463),
			//		new LatLong(51.63654494,-3.704442981),
			//		new LatLong(51.6365415,-3.704439047 ),
			//		new LatLong(51.63654084,-3.70443829 ),
			//		new LatLong(51.63653986,-3.704437173),
			//		new LatLong(51.63651953,-3.704413908),
			//		new LatLong(51.63636516,-3.704197188),
			//		new LatLong(51.63634667,-3.704171233),
			//		new LatLong(51.63632449,-3.704140087),
			//		new LatLong(51.6362895,-3.70409096  ),
			//		new LatLong(51.636189,-3.703942983  ),
			//		new LatLong(51.63615748,-3.703896561),
			//		new LatLong(51.63612142,-3.703843466),
			//		new LatLong(51.63605586,-3.703746926),
			//		new LatLong(51.63601301,-3.703677116),
			//		new LatLong(51.63597386,-3.703613327),
			//		new LatLong(51.63593341,-3.703547411),
			//		new LatLong(51.63583732,-3.703390848),
			//		new LatLong(51.63579529,-3.703322366),
			//		new LatLong(51.63575185,-3.703251601),
			//		new LatLong(51.63566162,-3.703104581),
			//		new LatLong(51.63566095,-3.703103489),
			//		new LatLong(51.6356148,-3.703034335 ),
			//		new LatLong(51.63556218,-3.702955489),
			//		new LatLong(51.63542421,-3.702748751),
			//		new LatLong(51.63538736,-3.702693524),
			//		new LatLong(51.63536437,-3.702659074),
			//		new LatLong(51.63534452,-3.702629329),
			//		new LatLong(51.63530994,-3.702551543),
			//		new LatLong(51.635227,-3.702364926  ),
			//		new LatLong(51.63513578,-3.702159683),
			//		new LatLong(51.63513142,-3.702149877),
			//		new LatLong(51.63511633,-3.70210432 ),
			//		new LatLong(51.63510541,-3.702071371),
			//		new LatLong(51.63506644,-3.701953695),
			//		new LatLong(51.63500849,-3.70177874 ),
			//		new LatLong(51.63500492,-3.701767965),
			//		new LatLong(51.63496271,-3.701691715),
			//		new LatLong(51.63490311,-3.701584066),
			//		new LatLong(51.63484756,-3.701483715),
			//		new LatLong(51.63481856,-3.70143133 ),
			//		new LatLong(51.63478745,-3.701375136),
			//		new LatLong(51.63468418,-3.7011886  ),
			//		new LatLong(51.63461218,-3.701058559),
			//		new LatLong(51.63461178,-3.701057835),
			//		new LatLong(51.63456939,-3.700966227),
			//		new LatLong(51.63453599,-3.700894056),
			//		new LatLong(51.63449294,-3.700801053),
			//		new LatLong(51.63443779,-3.700681892),
			//		new LatLong(51.63439987,-3.700599966),
			//		new LatLong(51.63438419,-3.700566069),
			//		new LatLong(51.63435344,-3.700457454),
			//		new LatLong(51.63426568,-3.700147506),
			//		new LatLong(51.63424668,-3.700080412),
			//		new LatLong(51.63422832,-3.700015555),
			//		new LatLong(51.63419337,-3.699892118),
			//		new LatLong(51.63418182,-3.699851309),
			//		new LatLong(51.63413165,-3.699660638),
			//		new LatLong(51.6340949,-3.699520942 ),
			//		new LatLong(51.63407142,-3.699431697),
			//		new LatLong(51.63405217,-3.699358546),
			//		new LatLong(51.63392443,-3.698873   ),
			//		new LatLong(51.63391784,-3.698847967),
			//		new LatLong(51.63391244,-3.698827448),
			//		new LatLong(51.63383419,-3.698530016),
			//		new LatLong(51.63381214,-3.698456148),
			//		new LatLong(51.63375476,-3.698263886),
			//		new LatLong(51.6337541,-3.698261676 ),
			//		new LatLong(51.63368494,-3.698029937),
			//		new LatLong(51.6336243,-3.697826778 ),
			//		new LatLong(51.63358964,-3.697765507),
			//		new LatLong(51.63356689,-3.697725286),
			//		new LatLong(51.63354007,-3.697677882),
			//		new LatLong(51.63348126,-3.697573923),
			//		new LatLong(51.63341566,-3.697399467),
			//		new LatLong(51.63340857,-3.697380597),
			//		new LatLong(51.63340654,-3.697375206),
			//		new LatLong(51.63333293,-3.697179436),
			//		new LatLong(51.63332092,-3.697091717),
			//		new LatLong(51.6333049,-3.696974741 ),
			//		new LatLong(51.63329415,-3.696896176),
			//		new LatLong(51.63325994,-3.69664633 ),
			//		new LatLong(51.63324105,-3.696380934),
			//		new LatLong(51.63321295,-3.695986049),
			//		new LatLong(51.63321116,-3.695960905),
			//		new LatLong(51.63321118,-3.695931661),
			//		new LatLong(51.63321122,-3.695871903),
			//		new LatLong(51.63321142,-3.695536609),
			//		new LatLong(51.63323384,-3.695354526),
			//		new LatLong(51.63324028,-3.695302241),
			//		new LatLong(51.63324465,-3.695266764),
			//		new LatLong(51.63320658,-3.695084191),
			//		new LatLong(51.63318977,-3.69500361 ),
			//		new LatLong(51.63315547,-3.694839134),
			//		new LatLong(51.63313106,-3.694772715),
			//		new LatLong(51.63311385,-3.694725876),
			//		new LatLong(51.63308618,-3.694650598),
			//		new LatLong(51.63295785,-3.694301388),
			//		new LatLong(51.63294156,-3.694283864),
			//		new LatLong(51.63288752,-3.69422574 ),
			//		new LatLong(51.6328784,-3.694215924 ),
			//		new LatLong(51.63282649,-3.694183386),
			//		new LatLong(51.63279844,-3.694165799),
			//		new LatLong(51.63275704,-3.694180798),
			//		new LatLong(51.63270995,-3.69419786 ),
			//		new LatLong(51.63256761,-3.694404704),
			//		new LatLong(51.63254931,-3.694465592),
			//		new LatLong(51.63252908,-3.694532916),
			//		new LatLong(51.63252265,-3.694674011),
			//		new LatLong(51.63251919,-3.694749905),
			//		new LatLong(51.63251835,-3.694768237),
			//		new LatLong(51.63252893,-3.695051498),
			//		new LatLong(51.63253919,-3.695166833),
			//		new LatLong(51.63255363,-3.69532916 ),
			//		new LatLong(51.63255418,-3.695335306),
			//		new LatLong(51.63255248,-3.695453103),
			//		new LatLong(51.63253426,-3.695508395),
			//		new LatLong(51.63252947,-3.695522961),
			//		new LatLong(51.63246264,-3.695579399),
			//		new LatLong(51.63239775,-3.695541932),
			//		new LatLong(51.63239717,-3.695541599),
			//		new LatLong(51.63234005,-3.695433395),
			//		new LatLong(51.63229315,-3.695125208),
			//		new LatLong(51.63224643,-3.694839704),
			//		new LatLong(51.63222998,-3.694739123),
			//		new LatLong(51.6322151,-3.694648214 ),
			//		new LatLong(51.63217019,-3.694373808),
			//		new LatLong(51.63216508,-3.694342558),
			//		new LatLong(51.63208136,-3.694044789),
			//		new LatLong(51.63199695,-3.693794139),
			//		new LatLong(51.63197162,-3.693713735),
			//		new LatLong(51.63194442,-3.693627401),
			//		new LatLong(51.63189887,-3.693482841),
			//		new LatLong(51.63189174,-3.693460213),
			//		new LatLong(51.63184828,-3.693192474),
			//		new LatLong(51.63183785,-3.693128201),
			//		new LatLong(51.63181748,-3.693042828),
			//		new LatLong(51.63180472,-3.692989386),
			//		new LatLong(51.63177423,-3.692861593),
			//		new LatLong(51.63172717,-3.69266443 ),
			//		new LatLong(51.63171289,-3.692638384),
			//		new LatLong(51.63167735,-3.692573575),
			//		new LatLong(51.63164141,-3.692508021),
			//		new LatLong(51.63156861,-3.692469953),
			//		new LatLong(51.63154728,-3.692479636),
			//		new LatLong(51.63149854,-3.692501759),
			//		new LatLong(51.63147263,-3.692513524),
			//		new LatLong(51.63138535,-3.692798777),
			//		new LatLong(51.63133242,-3.692971799),
			//		new LatLong(51.63118814,-3.693443367),
			//		new LatLong(51.63115759,-3.693543242),
			//		new LatLong(51.63112217,-3.693659006),
			//		new LatLong(51.63099845,-3.694063368),
			//		new LatLong(51.63090861,-3.694189663),
			//		new LatLong(51.6308445,-3.694255143 ),
			//		new LatLong(51.63043612,-3.694672258),
			//		new LatLong(51.63033899,-3.694771465),
			//		new LatLong(51.63029439,-3.695112491),
			//		new LatLong(51.63028952,-3.695149703),
			//		new LatLong(51.6302628,-3.695346923 ),
			//		new LatLong(51.63025258,-3.695422339),
			//		new LatLong(51.63028322,-3.695606947),
			//		new LatLong(51.63028801,-3.695635798),
			//		new LatLong(51.63036793,-3.695815563),
			//		new LatLong(51.63037545,-3.695825884),
			//		new LatLong(51.63051358,-3.696015457),
			//		new LatLong(51.63061076,-3.69614283 ),
			//		new LatLong(51.6306229,-3.696167317 ),
			//		new LatLong(51.63065989,-3.696241894),
			//		new LatLong(51.63070578,-3.69642039 ),
			//		new LatLong(51.63070717,-3.696440568),
			//		new LatLong(51.63071882,-3.69660887 ),
			//		new LatLong(51.63073519,-3.697029714),
			//		new LatLong(51.63082333,-3.697601431),
			//		new LatLong(51.63087918,-3.697831713),
			//		new LatLong(51.6308929,-3.6980139   ),
			//		new LatLong(51.63085786,-3.698347475),
			//		new LatLong(51.630845,-3.698725066  ),
			//		new LatLong(51.63088145,-3.698905176),
			//		new LatLong(51.63115473,-3.699605368),
			//		new LatLong(51.63120313,-3.699855036),
			//		new LatLong(51.63127043,-3.700107776),
			//		new LatLong(51.63133629,-3.700294666),
			//		new LatLong(51.63139521,-3.70047673 ),
			//		new LatLong(51.63141922,-3.700687362),
			//		new LatLong(51.63143794,-3.700988363),
			//		new LatLong(51.63180456,-3.701134743),
			//		new LatLong(51.63212242,-3.701412165),
			//		new LatLong(51.63310081,-3.702132958),
			//		new LatLong(51.63330468,-3.702273482),
			//		new LatLong(51.63352103,-3.702388222),
			//		new LatLong(51.63371078,-3.702544425),
			//		new LatLong(51.63376032,-3.702611616),
			//		new LatLong(51.63392804,-3.702818971),
			//		new LatLong(51.63401141,-3.702938035),
			//		new LatLong(51.63413324,-3.703115462),
			//		new LatLong(51.63421628,-3.703247132),
			//		new LatLong(51.6343428,-3.703406188 ),
			//		new LatLong(51.63462947,-3.703810426),
			//		new LatLong(51.63471773,-3.703969146),
			//		new LatLong(51.63483279,-3.704205758),
			//		new LatLong(51.63491981,-3.70443993 ),
			//		new LatLong(51.6349414,-3.704516493 ),
			//		new LatLong(51.63500292,-3.704638362),
			//		new LatLong(51.63504232,-3.704804138),
			//		new LatLong(51.63509988,-3.705004811),
			//		new LatLong(51.63513809,-3.705131244),
			//		new LatLong(51.63516774,-3.705240003),
			//		new LatLong(51.63522474,-3.705320136),
			//		new LatLong(51.63525759,-3.705361594),
			//		new LatLong(51.63527516,-3.705396749),
			//		new LatLong(51.63529366,-3.705423998),
			//		new LatLong(51.63540322,-3.705561542),
			//		new LatLong(51.63547187,-3.7057586  ),
			//		new LatLong(51.63548786,-3.705842751),
			//		new LatLong(51.63554797,-3.706159138),
			//		new LatLong(51.63555012,-3.706266786),
			//		new LatLong(51.63555325,-3.706423216),
			//		new LatLong(51.63555788,-3.706654685),
			//		new LatLong(51.63555699,-3.706660587),
			//		new LatLong(51.63552744,-3.706856866),
			//		new LatLong(51.63552464,-3.706867814),
			//		new LatLong(51.63542243,-3.707268411),
			//		new LatLong(51.63539582,-3.707324018),
			//		new LatLong(51.63523693,-3.707575704),
			//		new LatLong(51.63491404,-3.707899667),
			//		new LatLong(51.63479141,-3.708064161),
			//		new LatLong(51.6346571,-3.708169447 ),
			//		new LatLong(51.63451514,-3.708258784),
			//		new LatLong(51.63439712,-3.708340178),
			//		new LatLong(51.63433348,-3.70845462 ),
			//		new LatLong(51.63436151,-3.708618637),
			//		new LatLong(51.6345384,-3.708806071 ),
			//		new LatLong(51.63471035,-3.708965825),
			//		new LatLong(51.63494562,-3.709133228),
			//		new LatLong(51.63517972,-3.709340817),
			//		new LatLong(51.6355038,-3.70950995  ),
			//		new LatLong(51.63627629,-3.709759081),
			//		new LatLong(51.63639916,-3.709706788),
			//		new LatLong(51.63658233,-3.70974761 ),
			//		new LatLong(51.63673922,-3.709798417),
			//		new LatLong(51.63684177,-3.709812438),
			//		new LatLong(51.63698441,-3.709883899),
			//		new LatLong(51.63707214,-3.709958415),
			//		new LatLong(51.63720346,-3.709992299),
			//		new LatLong(51.63726143,-3.71005905 ),
			//		new LatLong(51.63738988,-3.710160686),
			//		new LatLong(51.63745511,-3.710198946),
			//		new LatLong(51.63751726,-3.710232709),
			//		new LatLong(51.63755894,-3.710281519),
			//		new LatLong(51.63763077,-3.71033609 ),
			//		new LatLong(51.63774972,-3.710373978),
			//		new LatLong(51.6378088,-3.710418317 ),
			//		new LatLong(51.63786386,-3.710465439),
			//		new LatLong(51.6379408,-3.710531739 ),
			//		new LatLong(51.63820361,-3.710804722),
			//		new LatLong(51.63830869,-3.710975712),
			//		new LatLong(51.63853893,-3.711129359),
			//		new LatLong(51.63856963,-3.71118916 ),
			//		new LatLong(51.63862137,-3.711224575),
			//		new LatLong(51.63867066,-3.711269012),
			//		new LatLong(51.63871452,-3.711320799),
			//		new LatLong(51.63882831,-3.711442542),
			//		new LatLong(51.6389245,-3.711558986 ),
			//		new LatLong(51.63901897,-3.711647845),
			//		new LatLong(51.63910272,-3.711710207),
			//		new LatLong(51.63920922,-3.711789685),
			//		new LatLong(51.63934738,-3.711856631),
			//		new LatLong(51.63946145,-3.711918954),
			//		new LatLong(51.63953896,-3.711943769),
			//		new LatLong(51.63985143,-3.712051719),
			//		new LatLong(51.63996932,-3.711372174),
			//		new LatLong(51.63999785,-3.711194038),
			//		new LatLong(51.6400455,-3.710968249 ),
			//		new LatLong(51.64005387,-3.710895501),
			//		new LatLong(51.6400522,-3.710847277 ),
			//		new LatLong(51.64012933,-3.710963865),
			//		new LatLong(51.64023259,-3.71100396 ),
			//		new LatLong(51.64030893,-3.711124743),
			//		new LatLong(51.64037384,-3.711258561),
			//		new LatLong(51.64044027,-3.711417993),
			//		new LatLong(51.64047361,-3.71155102),
			//		new LatLong(51.64050353,-3.711687298),
			//		new LatLong(51.64215427,-3.705376913),
			//		new LatLong(51.64211902,-3.70551581),
			//		new LatLong(51.6421392,-3.70555663),
			//		new LatLong(51.64215069,-3.705623833),
			//		new LatLong(51.64213639,-3.705750163),
			//		new LatLong(51.6421176,-3.705819326),
			//		new LatLong(51.64209689,-3.705895577),
			//		new LatLong(51.64201244,-3.706073469),
			//		new LatLong(51.64200679,-3.706087433),
			//		new LatLong(51.64192498,-3.706289751),
			//		new LatLong(51.64184743,-3.706480475),
			//		new LatLong(51.64182147,-3.706581807),
			//		new LatLong(51.64180338,-3.706652425),
			//		new LatLong(51.64180119,-3.706657804),
			//		new LatLong(51.64178267,-3.706703316),
			//		new LatLong(51.64176466,-3.706744449),
			//		new LatLong(51.64173879,-3.706781949),
			//		new LatLong(51.64168268,-3.706863277),
			//		new LatLong(51.64165896,-3.70689765),
			//		new LatLong(51.64162086,-3.70691933),
			//		new LatLong(51.6415369,-3.706886677),
			//		new LatLong(51.64152584,-3.706882376),
			//		new LatLong(51.64139074,-3.707117937),
			//		new LatLong(51.64138933,-3.707125306),
			//		new LatLong(51.64135441,-3.707307722),
			//		new LatLong(51.64134891,-3.707336447),
			//		new LatLong(51.64124972,-3.707586446),
			//		new LatLong(51.6412497,-3.707587632),
			//		new LatLong(51.64123627,-3.707607812),
			//		new LatLong(51.641212212381454,-3.707643981)
			//	}
			//};
			//#endregion

			//ChallengesList = new List<ChallengeOverviewDto>
			//{
			//	new ChallengeOverviewDto { LocationNameEnglish = "Gnoll to Mosshouse Reservoir"},
			//	new ChallengeOverviewDto { LocationNameEnglish = "Gyfylchi Ridgetop Trail"},
			//	new ChallengeOverviewDto { LocationNameEnglish = "River & Railway Walk"},
			//	new ChallengeOverviewDto { LocationNameEnglish = "Richard Burton Trail"},
			//	new ChallengeOverviewDto { LocationNameEnglish = "Pontardawe- Ystalyfera"},
			//	new ChallengeOverviewDto { LocationNameEnglish = "Wales Coast Path – Mynydd Dinas"},
			//};

			friends = new List<TestUser>()
			{
				new TestUser
				{
					Username = "WalkingGuy12",
					Badge = Badges.Hero,
					Points = 2000
				},
				new TestUser
				{
					Username = "JoeTrail54",
					Badge = Badges.Hero,
					Points = 1995
				},
				new TestUser
				{
					Username = "BobSmith24",
					Badge = Badges.Hero,
					Points = 1870
				},
				new TestUser
				{
					Username = "RoseJm",
					Badge = Badges.Hero,
					Points = 1500
				},
				new TestUser
				{
					Username = "MarkT",
					Badge = Badges.Champion,
					Points = 1430
				},
				new TestUser
				{
					Username = "SusanMoore",
					Badge = Badges.Champion,
					Points = 1225
				},
				new TestUser
				{
					Username = "KeanGriffth",
					Badge = Badges.Champion,
					Points = 1020
				},
				new TestUser
				{
					Username = "CocanT45",
					Badge = Badges.Champion,
					Points = 1000
				},
				new TestUser
				{
					Username = "TrailWizard43",
					Badge = Badges.Adventurer,
					Points = 995
				},
				new TestUser
				{
					Username = "MeghanRose18",
					Badge = Badges.Adventurer,
					Points = 800
				}
			};

			users = new List<TestUser>()
			{
				new TestUser
				{
					Username = "marc978"
				},
				new TestUser
				{
					Username = "woodkid01"
				},
				new TestUser
				{
					Username = "bob978"
				},
				new TestUser
				{
					Username = "james978"
				},
				new TestUser
				{
					Username = "ethan978"
				},
			};

			nearYouLocations = new List<Location>()
			{
				new Location{
					Address = "M134 Fairyland, Neath SA11 3EFeta",
					What3Words = "recall.common.idea",
					NearestBusStop = "153, X4",
					Website = "http://www.gnollestatecountrypark.co.uk/"
				},
				new Location{
					Address = "M134 Fairyland, Neath SA11 3EFeta",
					What3Words = "recall.common.idea",
					NearestBusStop = "153, X4",
					Website = "http://www.gnollestatecountrypark.co.uk/"
				},
				new Location{
					Address = "M134 Fairyland, Neath SA11 3EFeta",
					What3Words = "recall.common.idea",
					NearestBusStop = "153, X4",
					Website = "http://www.gnollestatecountrypark.co.uk/"
				},
				new Location{
					Address = "M134 Fairyland, Neath SA11 3EFeta",
					What3Words = "recall.common.idea",
					NearestBusStop = "153, X4",
					Website = "http://www.gnollestatecountrypark.co.uk/"
				},
				new Location{
					Address = "M134 Fairyland, Neath SA11 3EFeta",
					What3Words = "recall.common.idea",
					NearestBusStop = "153, X4",
					Website = "http://www.gnollestatecountrypark.co.uk/"
				},
				new Location{
					Address = "M134 Fairyland, Neath SA11 3EFeta",
					What3Words = "recall.common.idea",
					NearestBusStop = "153, X4",
					Website = "http://www.gnollestatecountrypark.co.uk/"
				}
			};
		}
	}
}