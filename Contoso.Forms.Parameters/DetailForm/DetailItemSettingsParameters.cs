﻿namespace Contoso.Forms.Parameters.DetailForm
{
    abstract public class DetailItemSettingsParameters
    {
		public DetailItemSettingsParameters
		(
			string field
		)
		{
			Field = field;
		}

		public string Field { get; set; }
    }
}