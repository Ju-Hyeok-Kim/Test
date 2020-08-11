# Test
테스트용



public static string ComputeLicenceKey(LicenceModel licenceModel)
        {
            string licenceKey = string.Empty;
            using (HMACSHA256 hash = new HMACSHA256(CommonMethod.Serialize(licenceModel.SerialKey)))
            {
                byte[] hashCode = hash.ComputeHash(CommonMethod.Serialize(licenceModel));
                licenceKey = Convert.ToBase64String(hashCode);
            }
            return licenceKey;
        }
