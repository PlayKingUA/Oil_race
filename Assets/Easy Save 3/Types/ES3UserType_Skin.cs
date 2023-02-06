using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("ammountFramgents", "isOpenSkin")]
	public class ES3UserType_Skin : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Skin() : base(typeof(BlueStellar.Cor.Skin)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (BlueStellar.Cor.Skin)obj;
			
			writer.WritePrivateField("ammountFramgents", instance);
			writer.WritePrivateField("isOpenSkin", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (BlueStellar.Cor.Skin)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "ammountFramgents":
					instance = (BlueStellar.Cor.Skin)reader.SetPrivateField("ammountFramgents", reader.Read<System.Int32>(), instance);
					break;
					case "isOpenSkin":
					instance = (BlueStellar.Cor.Skin)reader.SetPrivateField("isOpenSkin", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_SkinArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_SkinArray() : base(typeof(BlueStellar.Cor.Skin[]), ES3UserType_Skin.Instance)
		{
			Instance = this;
		}
	}
}