using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("indexOpenSkin", "indexProgressSkin")]
	public class ES3UserType_SkinsController : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_SkinsController() : base(typeof(BlueStellar.Cor.SkinsController)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (BlueStellar.Cor.SkinsController)obj;
			
			writer.WritePrivateField("indexOpenSkin", instance);
			writer.WritePrivateField("indexProgressSkin", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (BlueStellar.Cor.SkinsController)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "indexOpenSkin":
					instance = (BlueStellar.Cor.SkinsController)reader.SetPrivateField("indexOpenSkin", reader.Read<System.Int32>(), instance);
					break;
					case "indexProgressSkin":
					instance = (BlueStellar.Cor.SkinsController)reader.SetPrivateField("indexProgressSkin", reader.Read<System.Int32>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_SkinsControllerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_SkinsControllerArray() : base(typeof(BlueStellar.Cor.SkinsController[]), ES3UserType_SkinsController.Instance)
		{
			Instance = this;
		}
	}
}