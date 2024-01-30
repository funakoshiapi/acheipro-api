using System;
namespace Shared.RequestFeatures
{
	public class CompanyParameters : RequestParameters
    {
        
        private string? _Industry;
        private string? _Province;

        public string? SearchTerm { get; set; }

        public CompanyParameters() => OrderBy = "name";

        public string Industry
        {
            get { return String.IsNullOrEmpty(_Industry) ? String.Empty : _Industry.ToUpper(); }

            set { _Industry = String.IsNullOrEmpty(value) ? String.Empty : value.ToUpper(); }
        }

       
        public string Province 
        {
            get {

                return String.IsNullOrEmpty(_Province) ? String.Empty : _Province.ToUpper();
                    
                }

            set {
                    _Province = String.IsNullOrEmpty(value) ? String.Empty : value.ToUpper();
                }
        }
    }
}

